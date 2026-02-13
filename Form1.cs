using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;

namespace GeradorDanfse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string pastaOrigem = "";
        private string pastaDestino = "";
        private string caminhoCertificado = "";
        private string senhaCertificado = "";
        private System.Security.Cryptography.X509Certificates.X509Certificate2 certificadoSelecionado = null;
        
        #region ORIGEM
        private void btnSelecionarOrigem_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Selecione a pasta com os arquivos XML";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pastaOrigem = dialog.SelectedPath;
                    txtPastaOrigem.Text = pastaOrigem;
                    lblStatus.Text = $"Pasta origem selecionada: {pastaOrigem}";
                }
            }
        }
        #endregion

        #region DESTINO
        private void btnSelecionarDestino_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Selecione a pasta de destino para os PDFs";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pastaDestino = dialog.SelectedPath;
                    txtPastaDestino.Text = pastaDestino;
                    lblStatus.Text = $"Pasta destino selecionada: {pastaDestino}";
                }
            }
        }
        #endregion

        #region Gerar Danfse
        private async void btn05GerarDanfse_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar pastas
                if (string.IsNullOrEmpty(pastaOrigem) || !Directory.Exists(pastaOrigem))
                {
                    MessageBox.Show("Selecione uma pasta origem válida!", "Atenção",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(pastaDestino))
                {
                    pastaDestino = pastaOrigem; // Salvar diretamente na pasta de origem
                    txtPastaDestino.Text = pastaDestino;
                }

                // Não criar subpasta se for a mesma pasta de origem
                if (pastaDestino != pastaOrigem && !Directory.Exists(pastaDestino))
                {
                    Directory.CreateDirectory(pastaDestino);
                }

                // Desabilitar botões durante o processamento
                btn05GerarDanfse.Enabled = false;
                btnSelecionarOrigem.Enabled = false;
                btnSelecionarDestino.Enabled = false;

                lblStatus.Text = "Processando arquivos...";
                progressBar.Value = 0;

                // Buscar arquivos XML
                string[] arquivosXml = Directory.GetFiles(pastaOrigem, "*.xml", SearchOption.TopDirectoryOnly);

                if (arquivosXml.Length == 0)
                {
                    MessageBox.Show("Nenhum arquivo XML encontrado na pasta origem!", "Atenção",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                progressBar.Maximum = arquivosXml.Length;
                int sucessos = 0;
                int falhas = 0;

                // Lista para armazenar resultados
                List<string> resultados = new List<string>();

                // Processar cada arquivo XML
                for (int i = 0; i < arquivosXml.Length; i++)
                {
                    string arquivoXml = arquivosXml[i];
                    try
                    {
                        lblStatus.Text = $"Processando: {Path.GetFileName(arquivoXml)} ({i + 1}/{arquivosXml.Length})";

                        // Extrair chave de acesso do XML
                        string chaveAcesso = ExtrairChaveAcesso(arquivoXml);

                        if (!string.IsNullOrEmpty(chaveAcesso))
                        {
                            // Gerar PDF
                            bool resultado = await GerarDanfsePdf(chaveAcesso, arquivoXml, pastaDestino);

                            if (resultado)
                            {
                                sucessos++;
                                resultados.Add($"✓ {Path.GetFileName(arquivoXml)} - SUCESSO");
                            }
                            else
                            {
                                falhas++;
                                resultados.Add($"✗ {Path.GetFileName(arquivoXml)} - FALHA (Erro no download)");
                            }
                        }
                        else
                        {
                            falhas++;
                            resultados.Add($"✗ {Path.GetFileName(arquivoXml)} - FALHA (Chave não encontrada)");
                        }
                    }
                    catch (ArgumentException argEx)
                    {
                        falhas++;
                        resultados.Add($"✗ {Path.GetFileName(arquivoXml)} - ERRO DE ARGUMENTO: {argEx.Message}");
                    }
                    catch (WebException webEx)
                    {
                        falhas++;
                        if (webEx.Response is HttpWebResponse response)
                        {
                            resultados.Add($"✗ {Path.GetFileName(arquivoXml)} - ERRO WEB: HTTP {(int)response.StatusCode} {response.StatusDescription} - {webEx.Message}");
                        }
                        else
                        {
                            resultados.Add($"✗ {Path.GetFileName(arquivoXml)} - ERRO WEB: {webEx.Message}");
                        }
                    }
                    catch (Exception ex)
                    {
                        falhas++;
                        resultados.Add($"✗ {Path.GetFileName(arquivoXml)} - ERRO: {ex.Message} (Tipo: {ex.GetType().Name})");
                    }

                    progressBar.Value++;
                    Application.DoEvents(); // Permitir atualização da interface

                    // Adicionar delay entre requisições para evitar limitação de taxa
                    if (i < arquivosXml.Length - 1) // Não adicionar delay após o último item
                    {
                        await Task.Delay(2000); // 2000ms de delay entre requisições para reduzir carga no servidor
                    }
                }

                // Mostrar relatório
                string mensagem = $"Processamento concluído!\n\n" +
                                $"Total de arquivos: {arquivosXml.Length}\n" +
                                $"Sucessos: {sucessos}\n" +
                                $"Falhas: {falhas}\n\n" +
                                $"Arquivos PDF salvos em:\n{pastaDestino}";

                MessageBox.Show(mensagem, "Processamento Concluído",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Salvar log de processamento
                SalvarLog(resultados, pastaDestino);

                lblStatus.Text = $"Concluído: {sucessos} sucesso(s), {falhas} falha(s)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro durante o processamento:\n{ex.Message}", "Erro",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Erro no processamento";
            }
            finally
            {
                // Reabilitar botões
                btn05GerarDanfse.Enabled = true;
                btnSelecionarOrigem.Enabled = true;
                btnSelecionarDestino.Enabled = true;
                progressBar.Value = 0;
            }
        }
        #endregion

        #region Extrair chave de acesso
        private string ExtrairChaveAcesso(string caminhoXml)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(caminhoXml);

                // Namespaces para NFS-e
                XmlNamespaceManager nsManager = new XmlNamespaceManager(xmlDoc.NameTable);
                nsManager.AddNamespace("ns", "http://www.abrasf.org.br/nfse.xsd");
                nsManager.AddNamespace("nfse", "http://www.sped.fazenda.gov.br/nfse");

                // PRIORITÁRIO: Procurar pelo atributo Id do elemento infNFSe (como sugerido)
                XmlNode infNFSeNode = xmlDoc.SelectSingleNode("//nfse:infNFSe[@Id]", nsManager) ??
                                    xmlDoc.SelectSingleNode("//infNFSe[@Id]");
                if (infNFSeNode != null && infNFSeNode.Attributes["Id"] != null)
                {
                    string idValue = infNFSeNode.Attributes["Id"].Value;
                    // Extrair apenas os dígitos do valor do ID
                    string chaveAcesso = System.Text.RegularExpressions.Regex.Replace(idValue, @"[^0-9]", "");
                    if (chaveAcesso.Length == 44 || chaveAcesso.Length == 50)
                    {
                        // Retornar a chave completa (44 ou 50 dígitos)
                        return chaveAcesso;
                    }
                    else if (chaveAcesso.Length > 44)
                    {
                        // Se for maior que 44 mas não 50, tentar pegar os primeiros 44 ou 50 dígitos
                        if (chaveAcesso.Length >= 50)
                        {
                            return chaveAcesso.Substring(0, 50);
                        }
                        else
                        {
                            return chaveAcesso.Substring(0, 44);
                        }
                    }
                    return chaveAcesso;
                }

                // Tentar diferentes padrões de chave de acesso
                string[] xpaths = new string[]
                {
                    "//ChaveAcesso",
                    "//chaveAcesso",
                    "//InfNfse/ChaveAcesso",
                    "//InfNfse/chaveAcesso",
                    "//ns:ChaveAcesso",
                    "//ns:chaveAcesso",
                    "//nfse:ChaveAcesso",
                    "//nfse:chaveAcesso"
                };

                foreach (string xpath in xpaths)
                {
                    XmlNode node = xmlDoc.SelectSingleNode(xpath, nsManager);
                    if (node != null && !string.IsNullOrEmpty(node.InnerText))
                    {
                        return node.InnerText.Trim();
                    }
                }

                // Se não encontrar nos padrões acima, tentar extrair de outros campos
                XmlNode chaveNode = xmlDoc.SelectSingleNode("//CodigoVerificacao", nsManager) ??
                                    xmlDoc.SelectSingleNode("//codigoVerificacao", nsManager) ??
                                    xmlDoc.SelectSingleNode("//nfse:CodigoVerificacao", nsManager) ??
                                    xmlDoc.SelectSingleNode("//nfse:codigoVerificacao", nsManager);

                if (chaveNode != null)
                {
                    return chaveNode.InnerText.Trim();
                }

                // Se não encontrar no conteúdo XML, tentar extrair do nome do arquivo
                string fileName = Path.GetFileNameWithoutExtension(caminhoXml);
                // A chave de acesso geralmente tem 44 dígitos
                // Procurar por sequência de 44 dígitos no nome do arquivo
                var digitGroups = System.Text.RegularExpressions.Regex.Matches(fileName, @"\d{44}");
                if (digitGroups.Count > 0)
                {
                    return digitGroups[0].Value;
                }

                // Se ainda não encontrar, procurar por outras possíveis chaves no nome do arquivo
                // Pode ter padrões como "[chave_acesso].xml" ou "algumacoisa[chave_acesso].xml"
                var possibleKeys = System.Text.RegularExpressions.Regex.Matches(fileName, @"\d{32,44}");
                if (possibleKeys.Count > 0)
                {
                    // Retornar a maior sequência numérica encontrada (possivelmente a chave)
                    string longest = "";
                    foreach (System.Text.RegularExpressions.Match match in possibleKeys)
                    {
                        if (match.Value.Length > longest.Length)
                        {
                            longest = match.Value;
                        }
                    }
                    return longest;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao extrair chave de acesso: {ex.Message}");
            }
        }
        #endregion

        #region Gerar PDF
        private async Task<bool> GerarDanfsePdf(string chaveAcesso, string arquivoXml, string pastaDestino)
        {
            int maxTentativas = 5;
            int tentativaAtual = 0;

            while (tentativaAtual < maxTentativas)
            {
                try
                {
                    // Validar chave de acesso
                    if (string.IsNullOrWhiteSpace(chaveAcesso))
                    {
                        throw new ArgumentException("Chave de acesso não pode ser vazia ou nula.");
                    }

                    // Remover caracteres inválidos da chave de acesso
                    string chaveLimpa = Regex.Replace(chaveAcesso, @"[^0-9]", "");

                    // Validar formato da chave (deve ter 44 ou 50 dígitos)
                    if (chaveLimpa.Length != 44 && chaveLimpa.Length != 50)
                    {
                        throw new ArgumentException($"Formato inválido da chave de acesso. Deve conter 44 ou 50 dígitos, mas contém {chaveLimpa.Length} dígitos.");
                    }

                    string url = $"https://adn.nfse.gov.br/danfse/{chaveLimpa}";

                    // Validar URL antes de criar o objeto Uri
                    if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    {
                        throw new ArgumentException($"URL malformada: {url}");
                    }

                    using (CustomWebClient client = new CustomWebClient())
                    {
                        // Adicionar certificado digital se estiver disponível
                        if (certificadoSelecionado != null)
                        {
                            client.Certificado = certificadoSelecionado;
                        }

                        client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
                        client.Headers.Add("Accept", "application/pdf, text/html, */*");
                        client.Headers.Add("Accept-Language", "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7");
                        client.Headers.Add("Cache-Control", "no-cache");
                        client.Headers.Add("Referer", "https://adn.nfse.gov.br/");

                        // Pequeno delay para evitar sobrecarga no servidor
                        await Task.Delay(1000);

                        // Nome do arquivo PDF (usando somente a chave de acesso)
                        string nomeArquivo = $"{chaveLimpa}.pdf";
                        string caminhoCompleto = Path.Combine(pastaDestino, nomeArquivo);

                        // Baixar o PDF
                        await client.DownloadFileTaskAsync(new Uri(url), caminhoCompleto);

                        // Verificar se o arquivo foi baixado com sucesso
                        if (File.Exists(caminhoCompleto) && new FileInfo(caminhoCompleto).Length > 0)
                        {
                            return true;
                        }

                        return false;
                    }
                }
                catch (WebException webEx)
                {
                    // Tratar erros específicos de HTTP
                    if (webEx.Response is HttpWebResponse httpResponse)
                    {
                        if (httpResponse.StatusCode == HttpStatusCode.NotFound)
                        {
                            // Não tentar novamente se o recurso não for encontrado
                            throw new Exception($"DANFSE não encontrada para a chave: {chaveAcesso}");
                        }
                        else if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                        {
                            // Tentar continuar mesmo com bad request, pois às vezes o servidor responde assim mas ainda retorna o PDF
                            tentativaAtual++;
                            if (tentativaAtual >= maxTentativas)
                            {
                                // Se todas as tentativas falharem, verificar se o arquivo foi baixado mesmo com o erro
                                string chaveLimpaLocal = Regex.Replace(chaveAcesso, @"[^0-9]", "");
                                string nomeArquivo = $"{chaveLimpaLocal}.pdf";
                                string caminhoCompleto = Path.Combine(pastaDestino, nomeArquivo);
                                if (File.Exists(caminhoCompleto) && new FileInfo(caminhoCompleto).Length > 0)
                                {
                                    return true; // Arquivo foi baixado com sucesso apesar do erro
                                }
                                throw new Exception($"Requisição inválida para a chave: {chaveAcesso} - {httpResponse.StatusDescription}");
                            }

                            // Esperar antes de tentar novamente
                            await Task.Delay(2000);
                            continue; // Tenta novamente
                        }
                        else if ((int)httpResponse.StatusCode == 502)  // Bad Gateway
                        {
                            // Tratar Bad Gateway como erro temporário do servidor
                            tentativaAtual++;
                            if (tentativaAtual >= maxTentativas)
                            {
                                throw new Exception($"Erro de gateway para a chave: {chaveAcesso} - {httpResponse.StatusDescription}");
                            }

                            // Esperar tempo crescente antes da próxima tentativa
                            int delay = (int)Math.Pow(2, tentativaAtual) * 1000; // 2 segundos, depois 4, depois 8...
                            await Task.Delay(delay);
                            continue; // Tenta novamente
                        }
                        else if ((int)httpResponse.StatusCode == 429)
                        {
                            tentativaAtual++;
                            if (tentativaAtual >= maxTentativas)
                            {
                                throw new Exception($"Muitas requisições para a chave: {chaveAcesso} - {httpResponse.StatusDescription}");
                            }

                            // Esperar tempo crescente antes da próxima tentativa (exponential backoff)
                            int delay = (int)Math.Pow(2, tentativaAtual) * 1000; // 2 segundos, depois 4, depois 8
                            await Task.Delay(delay);
                            continue; // Tenta novamente
                        }
                        else if ((int)httpResponse.StatusCode >= 500)
                        {
                            // Para erros do servidor, tentar novamente
                            tentativaAtual++;
                            if (tentativaAtual >= maxTentativas)
                            {
                                throw new Exception($"Erro HTTP {httpResponse.StatusCode} para a chave {chaveAcesso}: {httpResponse.StatusDescription}");
                            }

                            // Esperar tempo crescente antes da próxima tentativa
                            int delay = (int)Math.Pow(2, tentativaAtual) * 1000; // 2 segundos, depois 4, depois 8...
                            await Task.Delay(delay);
                            continue; // Tenta novamente
                        }
                        else
                        {
                            throw new Exception($"Erro HTTP {httpResponse.StatusCode} para a chave {chaveAcesso}: {httpResponse.StatusDescription}");
                        }
                    }

                    // Se não for uma WebException específica, tentar novamente se for dentro do limite de tentativas
                    tentativaAtual++;
                    if (tentativaAtual >= maxTentativas)
                    {
                        throw;
                    }

                    // Esperar antes de tentar novamente
                    await Task.Delay(2000);
                    continue;
                }
                catch (Exception)
                {
                    tentativaAtual++;
                    if (tentativaAtual >= maxTentativas)
                    {
                        throw;
                    }

                    // Esperar antes de tentar novamente
                    await Task.Delay(2000);
                    continue;
                }
            }

            return false; // Retorna falso se todas as tentativas falharem
        }
        #endregion

        #region Certificado
        private void btnSelecionarCertificado_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Certificados digitais (*.pfx, *.p12)|*.pfx;*.p12|Todos os arquivos (*.*)|*.*";
                dialog.Title = "Selecione o certificado digital";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    caminhoCertificado = dialog.FileName;

                    // Solicitar senha do certificado
                    using (var passwordForm = new PasswordForm())
                    {
                        if (passwordForm.ShowDialog() == DialogResult.OK)
                        {
                            senhaCertificado = passwordForm.Password;

                            try
                            {
                                // Carregar o certificado
                                certificadoSelecionado = new System.Security.Cryptography.X509Certificates.X509Certificate2(
                                    caminhoCertificado, senhaCertificado);

                                // Verificar se o certificado é válido
                                if (ValidarCertificado(certificadoSelecionado))
                                {
                                    txtCaminhoCertificado.Text = caminhoCertificado;
                                    lblStatus.Text = $"Certificado carregado: {Path.GetFileName(caminhoCertificado)}";
                                    MessageBox.Show("Certificado digital carregado com sucesso!", "Sucesso",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Certificado digital inválido ou expirado!", "Erro",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    certificadoSelecionado = null;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Erro ao carregar o certificado: {ex.Message}", "Erro",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                certificadoSelecionado = null;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Validar certificado
        private bool ValidarCertificado(System.Security.Cryptography.X509Certificates.X509Certificate2 certificado)
        {
            try
            {
                // Verificar se o certificado está dentro do período de validade
                DateTime inicioValidade = certificado.NotBefore;
                DateTime fimValidade = certificado.NotAfter;
                DateTime agora = DateTime.Now;

                if (agora < inicioValidade || agora > fimValidade)
                {
                    return false; // Certificado fora do período de validade
                }

                // Adicionalmente, pode-se verificar se o certificado é de um tipo apropriado para autenticação
                // Por exemplo, verificar se é um certificado de chave privada disponível
                return certificado.HasPrivateKey;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region LOG
        private void SalvarLog(List<string> resultados, string pastaDestino)
        {
            try
            {
                string caminhoLog = Path.Combine(pastaDestino, $"log_processamento_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
                string conteudo = $"LOG DE PROCESSAMENTO DANFSE\n" +
                                 $"Data: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n" +
                                 $"========================================\n\n" +
                                 string.Join(Environment.NewLine, resultados);

                File.WriteAllText(caminhoLog, conteudo, Encoding.UTF8);
            }
            catch
            {
                // Ignorar erro no log
            }
        }
        #endregion

       private void Form1_Load(object sender, EventArgs e)
        {
            lblStatus.Text = "Pronto para processar";
        }

        // Método para testar download direto de uma chave específica
        private async Task<bool> BaixarDanfseUnica(string chaveAcesso, string pastaDestino)
        {
            try
            {
                // Validar chave de acesso
                if (string.IsNullOrWhiteSpace(chaveAcesso))
                {
                    MessageBox.Show("Chave de acesso não pode ser vazia ou nula.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Remover caracteres inválidos da chave de acesso
                string chaveLimpa = Regex.Replace(chaveAcesso, @"[^0-9]", "");

                // Validar formato da chave (deve ter 44 ou 50 dígitos)
                if (chaveLimpa.Length != 44 && chaveLimpa.Length != 50)
                {
                    MessageBox.Show($"Formato inválido da chave de acesso. Deve conter 44 ou 50 dígitos, mas contém {chaveLimpa.Length} dígitos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                string url = $"https://adn.nfse.gov.br/danfse/{chaveLimpa}";

                // Validar URL antes de criar o objeto Uri
                if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    MessageBox.Show($"URL malformada: {url}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                using (CustomWebClient client = new CustomWebClient())
                {
                    // Adicionar certificado digital se estiver disponível
                    if (certificadoSelecionado != null)
                    {
                        client.Certificado = certificadoSelecionado;
                    }

                    client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
                    client.Headers.Add("Accept", "application/pdf, text/html, */*");
                    client.Headers.Add("Accept-Language", "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7");
                    client.Headers.Add("Cache-Control", "no-cache");
                    client.Headers.Add("Referer", "https://adn.nfse.gov.br/");

                    // Nome do arquivo PDF
                    string nomeArquivo = $"{chaveLimpa}.pdf";
                    string caminhoCompleto = Path.Combine(pastaDestino, nomeArquivo);

                    // Baixar o PDF
                    await client.DownloadFileTaskAsync(new Uri(url), caminhoCompleto);

                    // Verificar se o arquivo foi baixado com sucesso
                    if (File.Exists(caminhoCompleto) && new FileInfo(caminhoCompleto).Length > 0)
                    {
                        MessageBox.Show($"PDF baixado com sucesso para:\n{caminhoCompleto}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("O arquivo não foi baixado corretamente ou está vazio.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch (WebException webEx)
            {
                if (webEx.Response is HttpWebResponse httpResponse)
                {
                    MessageBox.Show($"Erro de rede ao baixar DANFSE:\nHTTP {(int)httpResponse.StatusCode} {httpResponse.StatusDescription}\n{webEx.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Erro de rede: {webEx.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao baixar DANFSE: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Método público para chamar o download direto de uma chave
        public async Task<bool> DownloadDanfsePorChave(string chaveAcesso, string pastaDestino = null)
        {
            string destino = string.IsNullOrEmpty(pastaDestino) ? this.pastaOrigem : pastaDestino;
            if (string.IsNullOrEmpty(destino))
            {
                MessageBox.Show("Selecione uma pasta de destino primeiro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return await BaixarDanfseUnica(chaveAcesso, destino);
        }
    }
}