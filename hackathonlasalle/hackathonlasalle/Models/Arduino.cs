using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
namespace hackathonlasalle.Models
{
    public class Arduino
    {
        public string clientId = "1";
        public MqttClient c = new MqttClient("3.83.148.83");  
        public Arduino()
        {
            c.Connect(clientId);
            c.MqttMsgPublishReceived += Client_recievedMessage;
            c.Subscribe(new String[] { "mandarEmail/" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }
        //public void MandarMsg()
        //{
        //    c.MqttMsgPublished += client_MqttMsgPublished;

        //    ushort msgId = c.Publish("/addAlarme", // topic
        //                   Encoding.UTF8.GetBytes("true"), // message body
        //                   MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, // QoS level
        //                   false); // retained
       
        //}
   
        static void Client_recievedMessage(object sender, MqttMsgPublishEventArgs e)
        {
            //Handle message received
            var message = System.Text.Encoding.Default.GetString(e.Message);
            System.Console.WriteLine("Message received: " + message);

        }
    }
}