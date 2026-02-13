using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace GeradorDanfse
{
    public class CustomWebClient : WebClient
    {
        public X509Certificate2 Certificado { get; set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);

            // Adicionar certificado digital à requisição, se disponível
            if (Certificado != null)
            {
                request.ClientCertificates.Add(Certificado);
            }

            // Configurar opções de segurança adicionais
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            
            return request;
        }
    }
}