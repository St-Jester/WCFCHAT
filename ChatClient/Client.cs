using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using ChatClient.ServiceReference1;
using System.IO;
using System.Threading;
namespace ChatClient
{
    
    public partial class Client : Form
    {
        public static InstanceContext context;
        public static ChatServiceClient proxy;
        string directname = "";
        private string nickname;

        
        public Client()
        {
            InitializeComponent();
            
        }

        private void LeaveButton_Click(object sender, EventArgs e)
        {

        }

        private void JoinButton_Click(object sender, EventArgs e)
        {
            nickname = Login.Text;
            context = new InstanceContext(new NotifyHandler());
            proxy = new ChatServiceClient(context);
            proxy.Connect(nickname);

            label1.Text = "Joined";
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")

            {
                proxy.SendMessage(Login.Text + ": " + SendingMessage.Text);
                Thread th = new Thread(new ThreadStart(ReceiveMessages))
                {
                    IsBackground = true
                };
                th.Start();
            }
            else
            {
                proxy.SendMessageDirectly(Login.Text + ": " + SendingMessage.Text, textBox1.Text, Login.Text);
                MessageBox.Show("Directly" + textBox1.Text);

                Thread th = new Thread(new ThreadStart(ReceiveMessagesFrom))
                {
                    IsBackground = true
                };
                th.Start();
            }
        }

        private async void ReceiveMessagesFrom()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    if (NotifyHandler.isDone)
                    {
                        FileStream fs2 = new FileStream(@"..\..\DirectMessages.txt", FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(fs2);
                        string notify = sr.ReadToEnd();
                        sr.Close();
                        ReceivedMsg.Text = notify;
                        NotifyHandler.isDone = false;
                    }
                }
            });
        }

        //way to make things work. Chat messages are loaded as soon as info is passed to client but not earlier
        private async void ReceiveMessages()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    if (NotifyHandler.isDone)
                    {
                        FileStream fs2 = new FileStream(@"..\..\buffer.txt", FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(fs2);
                        string notify = sr.ReadToEnd();
                        sr.Close();
                        ReceivedMsg.Text = notify;
                        NotifyHandler.isDone = false;
                    }
                }
            });
        }

        private void AddConnectedUsers()
        {
            //receive file contetnt from host via handler
            //clear combobox
            //populate again
          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            directname = textBox1.Text;
            label4.Text = directname;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            directname = textBox1.Text;
            label4.Text = directname;
        }

        private void Client_FormClosed(object sender, FormClosedEventArgs e)
        {
            proxy.Disconnect(nickname);

        }
    }
}
