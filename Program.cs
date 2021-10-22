namespace SendEmailMailKit
{
    class Program
    {
        static void Main(string[] args)
        {
            //Enviando Como texto
            var email = new Email("noreply", "noreply@localhost.com", "Sr Willian", "will.marciano@gmail.com", "Teste", "Teste Envio Email");
            var send = new Send();
            send.ToText(email);

            //Envio do Corpo com html
            send.ToHtml(email);
        }
    }
}
