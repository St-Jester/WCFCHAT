using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ChatClient.ServiceReference1;
using System.IO;
namespace ChatClient
{
    class NotifyHandler : IChatServiceCallback
    {
        public static bool isDone = false;

        public void ReceiveClientsMessages(string str)
        {
            isDone = false;
            FileStream fs1 = new FileStream(@"..\..\DirectMessages.txt", FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs1);
            sw.WriteLine(str);
            sw.Close();
            isDone = true;
        }


        public void ReceiveNotify(string str)
        {
            isDone = false;
            FileStream fs1 = new FileStream(@"..\..\buffer.txt", FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs1);
            sw.WriteLine(str);
            sw.Close();
            isDone = true;
        }
    }
}
