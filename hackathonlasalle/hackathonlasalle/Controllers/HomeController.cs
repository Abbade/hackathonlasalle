using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace hackathonlasalle.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {

                MqttClient c = new MqttClient("3.83.148.83");

                string clientId = "";

                c.Connect(clientId);

                c.MqttMsgPublishReceived += Client_recievedMessage;


                c.Subscribe(new String[] { "hackaton/" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

            }
            catch (Exception)
            {

                throw;
            }
      
            return View();
        }

        static void Client_recievedMessage(object sender, MqttMsgPublishEventArgs e)
        {
            //Handle message received
            var message = System.Text.Encoding.Default.GetString(e.Message);
            System.Console.WriteLine("Message received: " + message);
            enviarEmail(new string[] { "gabbade@gmail.com", "fabio.barreto@gmail.com", "mario.joao@lasalle.org.br", "marcia.sadok@lasalle.org.br" });
        }
        static void enviarEmail(string[] emailTo)
        {
            string userName = "automatico@mobvendas.com.br";
            string senha = "mobvendas305503";
            string host = "email-ssl.com.br";
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(host);
            mail.IsBodyHtml = true;
            mail.From = new MailAddress("automatico@mobvendas.com.br");
            foreach(var emailusuario in emailTo)
            {
                mail.To.Add(emailusuario);
            }

            mail.Subject = "60+CUIDADOS ALERTA";
            mail.Body = "ALERTA!<br> O senhor Dã Lael saiu do perímetro seguro, verifique sua localização!";
            SmtpServer.Port = 587;

            SmtpServer.Credentials = new System.Net.NetworkCredential(userName, senha);
            SmtpServer.EnableSsl = false;
            SmtpServer.Send(mail);
        }

    }
}
