using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WCF_SelfHost.Host;

namespace WCF_SelfHost.Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //channelFactory = new ChannelFactory<IWetterService>(new NetTcpBinding(), "net.tcp://localhost:1");
            channelFactory = new ChannelFactory<IWetterService>(new BasicHttpBinding(), "http://localhost:2");
            //channelFactory = new ChannelFactory<IWetterService>(new WSHttpBinding(), "http://localhost:3");
            //channelFactory = new ChannelFactory<IWetterService>(new NetNamedPipeBinding(), "net.pipe://localhost/Wetter");
        }

        ChannelFactory<IWetterService> channelFactory;

        private void button1_Click(object sender, EventArgs e)
        {
            IWetterService client = channelFactory.CreateChannel();
            label1.Text = $"{client.GetTemperature("Heidelberg")}°C";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IWetterService client = channelFactory.CreateChannel();
            label2.Text = $"{client.GetWetter("Heidelberg")}";
        }
    }
}
