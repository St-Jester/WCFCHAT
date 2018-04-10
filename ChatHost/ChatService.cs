using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.Threading;
namespace ChatHost
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ChatService" in both code and config file together.
    public class ChatService : IChatService
    {
        public void SendMessage(string message)
        {
            FileStream fs1 = new FileStream(@"..\..\MesageLog.txt", FileMode.Append, FileAccess.Write);
            StreamWriter em = new StreamWriter(fs1);
            em.WriteLine(message);
            em.Close();

            FileStream fs2 = new FileStream(@"..\..\MesageLog.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs2);
            string notify = sr.ReadToEnd();
            sr.Close();
            Callback.ReceiveNotify(notify);

        }

        public void SendMessageDirectly(string message, string address, string sender)
        {
            //get all clients = made by bruteforce write in .txt!!!
            FileStream fs2 = new FileStream(@"..\..\Clients.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs2);
            string directname = "";
            while (sr.Peek() >= 0)
            {
            // read the line and do whatever with it
            directname = sr.ReadLine();
                //find appropriate address and client
                if (directname == address)
                {
                    message = "to" + directname + "from: " + message;
                    break;
                }
            }

            //check if connection is still awaliable
           
            //yes - send()
            //no - create another .xml with key-value pair Person, messages{date;string}
            //detect what client are we using

            Callback.ReceiveClientsMessages(message);
        }

        public void Connect(string nickname)
        {
            FileStream fs2 = new FileStream(@"..\..\Clients.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs2);
            string connectedclients = sr.ReadToEnd();
            sr.Close();

            //checks for identical users
            if (connectedclients.IndexOf(nickname) == -1)
            {
                FileStream fs1 = new FileStream(@"..\..\Clients.txt", FileMode.Append, FileAccess.Write);
                StreamWriter em = new StreamWriter(fs1);
                em.WriteLine(nickname);
                em.Close();
            }
        }

        public void Disconnect(string name)
        {
            FileStream fs2 = new FileStream(@"..\..\Clients.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs2);
            string connectedclients = sr.ReadToEnd();
            sr.Close();

            if (connectedclients.IndexOf(name) != -1)
            {
                connectedclients.Replace(name, "");

                FileStream fs1 = new FileStream(@"..\..\Clients.txt", FileMode.Append, FileAccess.Write);
                StreamWriter em = new StreamWriter(fs1);
                em.Write(connectedclients);
            }
        }

        

        private IChatServiceCallback Callback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IChatServiceCallback>();
            }
        }


    }   
}