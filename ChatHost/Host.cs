using System;
using System.Windows.Forms;
using System.ServiceModel;

namespace ChatHost
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public partial class Host : Form
    {
        ServiceHost host = new ServiceHost(typeof(ChatService));
        public Host()
        {
            InitializeComponent();
        }

        private void Host_Load(object sender, EventArgs e)
        {
            host.Open();
            label1.Text = "ONLINE ";
            
        }

        private void Host_FormClosed(object sender, FormClosedEventArgs e)
        {
            host.Close();
        }
      
    }
}
