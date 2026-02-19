# Emissor NFSe Nacional

Sistema de emissão de Nota Fiscal de Serviço Eletrônica (NFSe) compatível com o padrão nacional do SPED (Sistema Público de Escrituração Digital).

## Descrição

Este projeto implementa um emissor de NFSe completo que permite a geração, assinatura digital, validação e transmissão de documentos fiscais de serviço conforme as especificações do governo brasileiro. O sistema é compatível com os padrões EFD-Reinf e utiliza os esquemas XML oficiais para garantir a conformidade com as exigências legais.

## Características

- Geração de documentos NFSe/DPS conforme especificações do SPED
- Assinatura digital de documentos XML com certificados ICP-Brasil (A1/A3)
- Validação de documentos com esquemas XSD (versões 1.00 e 1.01)
- Compressão GZip e codificação Base64 para transmissão
- Comunicação segura via HTTPS com autenticação mútua TLS
- Suporte a diferentes ambientes (Produção e Homologação)
- Interface gráfica em Windows Forms para fácil operação

## Tecnologias Utilizadas

- .NET Framework
- C# Windows Forms
- XML Digital Signature (XMLDSIG)
- REST API para comunicação com serviços governamentais
- JSON para serialização de dados
- NuGet packages:
  - Newtonsoft.Json
  - ClosedXML (para manipulação de arquivos Excel)
  - AngleSharp (para processamento HTML)

## Estrutura do Projeto

```
efd-reinf/
├── efd-reinf/              # Projeto principal (UI)
│   ├── frmHome.cs          # Interface principal
│   └── Program.cs          # Ponto de entrada da aplicação
├── efdreinf.core/          # Camada de lógica de negócios
│   ├── models/             # Modelos de dados
│   ├── services/           # Serviços principais
│   ├── helpers/            # Classes auxiliares
│   └── nfse.core.csproj    # Arquivo de projeto
└── packages/               # Dependências NuGet
```

## Funcionalidades Principais

### Geração de Documentos
- Criação de documentos DPS (Documento de Prestação de Serviço)
- Preenchimento automático de dados cadastrais
- Cálculo de tributos e retenções
- Validação estrutural dos dados

### Assinatura Digital
- Suporte a certificados A1 (arquivo) e A3 (hardware)
- Implementação do padrão XMLDSIG
- Algoritmos SHA-1 e SHA-256
- Validação de integridade pós-assinatura

### Transmissão
- Compressão GZip de documentos
- Codificação Base64 para transporte
- Comunicação HTTPS com servidores da SEFIN
- Tratamento de respostas e erros

### Validação
- Validação XSD em tempo real
- Verificação de conformidade com schemas oficiais
- Detecção de erros comuns antes da transmissão

## Instalação

1. Clone o repositório:
```bash
git clone https://github.com/seu-usuario/emissor_nfse_nacional.git
```

2. Abra a solução no Visual Studio

3. Restaure os pacotes NuGet:
```bash
nuget restore
```

4. Compile a solução

5. Configure os certificados digitais e dados da empresa

## Configuração

O sistema requer as seguintes configurações:

- Certificado digital ICP-Brasil (A1 ou A3)
- Dados cadastrais da empresa emissora
- Configurações de ambiente (Produção/Homologação)
- Credenciais de acesso (quando necessário)

## Uso

1. Inicie a aplicação
2. Configure os dados da empresa
3. Carregue ou crie um novo documento NFSe
4. Preencha os dados do serviço prestado
5. Assine digitalmente o documento
6. Valide o documento
7. Transmita para o ambiente governamental

## Contribuição

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## Contato

DLX - devdlxtecnologia@gmail.com

Julia - jcristinny@protonmail.com



Link do Projeto: https://github.com/DLXdev/emissor_nfse_nacional
```
