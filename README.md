# Gerador de DANFSE

Aplicação desktop desenvolvida em C# que converte arquivos XML de NFS-e (Nota Fiscal de Serviços Eletrônica) em arquivos PDF de DANFSE (Documento Auxiliar da Nota Fiscal de Serviços Eletrônica), facilitando a geração e armazenamento desses documentos fiscais.

## Tecnologias Utilizadas

- **C#**: Linguagem de programação principal
- **.NET Framework 4.8**: Plataforma de desenvolvimento
- **Windows Forms**: Framework para criação da interface gráfica
- **XML Processing**: Bibliotecas nativas do .NET para processamento de arquivos XML
- **WebClient**: Para download dos arquivos PDF
- **X.509 Certificates**: Suporte a certificados digitais para autenticação

## Estrutura do Projeto

```
Danfse/
├── Form1.cs              # Classe principal da interface gráfica
├── Form1.Designer.cs     # Código gerado automaticamente para elementos visuais
├── Form1.resx           # Recursos de interface localizados
├── CustomWebClient.cs   # Implementação personalizada de WebClient com suporte a certificados
├── PasswordForm.cs      # Formulário para entrada de senha de certificado digital
├── PasswordForm.resx    # Recursos do formulário de senha
├── Program.cs           # Ponto de entrada da aplicação
├── Danfse.csproj        # Arquivo de projeto
└── README.md            # Documentação do projeto
```

## Funcionalidades Principais

- **Processamento em lote**: Converte múltiplos arquivos XML para PDF em uma única operação
- **Extração automática de chave de acesso**: Identifica e extrai a chave de acesso dos arquivos XML
- **Download de DANFSE**: Acessa o serviço oficial para gerar os documentos PDF
- **Suporte a certificados digitais**: Permite autenticação com certificados .pfx/.p12
- **Interface intuitiva**: Design limpo com controles para seleção de pastas e monitoramento de progresso
- **Log de processamento**: Registra detalhes do processamento em arquivos de log
- **Tratamento de erros robusto**: Implementa estratégias de retry para lidar com problemas de rede
- **Validação de certificados**: Verifica a validade e integridade dos certificados digitais

## Instalação

### Pré-requisitos

- Microsoft .NET Framework 4.8
- Sistema operacional Windows 7 ou superior

### Passos para instalação

1. Clone ou baixe este repositório
2. Abra a solução no Visual Studio
3. Compile o projeto
4. Execute o executável gerado

Alternativamente, você pode executar diretamente o executável compilado se disponível.

## Configuração

Não é necessária configuração adicional além da instalação do .NET Framework. O aplicativo utiliza as seguintes configurações padrão:

- Timeout de requisição: 60 segundos
- Timeout de leitura/gravação: 120 segundos
- Limite de conexões: 10
- Protocolos de segurança: TLS 1.0, TLS 1.1, TLS 1.2

Para utilizar certificados digitais, prepare seus arquivos .pfx/.p12 com as respectivas senhas.

## Uso

1. **Selecionar pasta de origem**: Clique em "Selecionar Origem" para escolher a pasta contendo os arquivos XML de NFS-e
2. **Selecionar pasta de destino**: Clique em "Selecionar Destino" para escolher onde salvar os arquivos PDF gerados
3. **Carregar certificado digital (opcional)**: Clique em "Selecionar Certificado" para carregar um certificado digital (.pfx/.p12) necessário para autenticação
4. **Gerar DANFSE**: Clique no botão "GERAR DANFSE" para iniciar o processo de conversão
5. **Monitorar progresso**: Observe a barra de progresso e as mensagens de status durante o processamento

O aplicativo também permite testar chaves de acesso individuais através da funcionalidade "Testar Chave".

## Contribuição

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/NovaFeature`)
3. Commit suas mudanças (`git commit -m 'Adiciona NovaFeature'`)
4. Push para a branch (`git push origin feature/NovaFeature`)
5. Abra um Pull Request

Sugestões de melhorias bem-vindas, especialmente nas áreas de:
- Melhoria na extração de chaves de acesso de diferentes formatos XML
- Adição de opções de configuração avançadas
- Melhoria na interface do usuário
- Implementação de testes automatizados

## Contato
Para dúvidas, sugestões ou suporte técnico, entre em contato:

DLX - devdlxtecnologia@gmail.com

Julia - jcristinny@protonmail.com

Link do Projeto: https://github.com/DLXdev/Danfse

- Reporte problemas no GitHub Issues
- Envie sugestões de melhorias através de Pull Requests

Projeto criado para facilitar a geração de DANFSE a partir de arquivos XML de NFS-e.
