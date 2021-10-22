using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Utils;
using System;
using System.IO;
using System.Text;

namespace SendEmailMailKit
{
    public class Send
    {
        public void ToText(Email email)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Programa Iniciado");
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(email.FromDisplay, email.From));
                message.To.Add(new MailboxAddress(email.ToEmailDisplay, email.ToEmail));
                message.Subject = email.Subject;
                message.Body = new TextPart("plain") { Text = email.Body };
                SendSMTP(message);
                Console.WriteLine($"Email Enviado para {message.To} com sucesso");
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Ops, ocoreu um erro -{e.Message}");
            }
        }

        public void ToHtml(Email email)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Programa Iniciado");
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(email.FromDisplay, email.From));
                message.To.Add(new MailboxAddress(email.ToEmailDisplay, email.ToEmail));
                message.Subject = email.Subject;

                //Infomar o caminho dos Anexos
                string FullFormatPath = "C:\\Dev\\source\\SendEmailMailKit\\Anexo";
                string[] ImgPaths = Directory.GetFiles(FullFormatPath);
                string HtmlFormat = string.Empty;
                var builder = new BodyBuilder();

                using (FileStream fs = new FileStream(Path.Combine(FullFormatPath, "Template.html"), FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gbk")))
                    {
                        HtmlFormat = sr.ReadToEnd();
                    }
                }
                //Adiciona imagens a recursos incorporados e substitua links para imagens na mensagem
                foreach (string imgpath in ImgPaths)
                {
                    if (imgpath.EndsWith(".png"))
                    {
                        var image = builder.LinkedResources.Add(imgpath);
                        image.ContentId = MimeUtils.GenerateMessageId();
                        HtmlFormat = HtmlFormat.Replace(Path.GetFileName(imgpath), string.Format("cid:{0}", image.ContentId));
                    }
                }

                builder.HtmlBody = HtmlFormat;
                message.Body = builder.ToMessageBody();

                SendSMTP(message);
                Console.WriteLine($"Email Enviado para {message.To} com sucesso");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ops, ocoreu um erro -{e.Message}");
            }
        }

        void SendSMTP(MimeMessage message)
        {
            //Configurar Gmail para "Aplicativos menos seguros" e gerar senha para Aplicativo
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587);

                client.Authenticate("mymail@gmail.com", "mypassword");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
