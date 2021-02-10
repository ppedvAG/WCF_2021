using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WcfChat.Contracts;


namespace WcfChat.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IClient
    {
        IServer server = null;

        public MainWindow()
        {
            InitializeComponent();
            SetUi(false);
        }



        private void LoginClick(object sender, RoutedEventArgs e)
        {
            var netBind = new NetTcpBinding();
            netBind.Security.Mode = SecurityMode.None;
            netBind.MaxReceivedMessageSize = int.MaxValue;
            var netAdr = "net.tcp://localhost:1";

            var wsBind = new WSDualHttpBinding();
            wsBind.Security.Mode = WSDualHttpSecurityMode.Message;
            wsBind.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
            


            wsBind.MaxReceivedMessageSize = int.MaxValue;
            var wsAdr = "http://localhost:2";



            //var dcf = new DuplexChannelFactory<IServer>(this, netBind, netAdr);
            var dcf = new DuplexChannelFactory<IServer>(this, wsBind, new EndpointAddress(new Uri(wsAdr), UpnEndpointIdentity.CreateDnsIdentity("RootCA")));

            dcf.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
            dcf.Credentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByThumbprint, "607c288bc692426c900dafa7a77f29e0226f2ef1");
            
            dcf.Credentials.Windows.ClientCredential.UserName = "Fred";
            dcf.Credentials.Windows.ClientCredential.Password = "123456";

            server = dcf.CreateChannel();

            server.Login(userNameTb.Text);
        }

        public void LoginResult(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg) || msg == "ok")
                SetUi(true);
            else
                MessageBox.Show(msg);
        }

        private void SetUi(bool isLoggedIn)
        {
            loginBtn.IsEnabled = !isLoggedIn;
            userNameTb.IsEnabled = !isLoggedIn;


            textTb.IsEnabled = isLoggedIn;
            logoutBtn.IsEnabled = isLoggedIn;
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            server?.Logout();
        }

        public void LogoutResult(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg) || msg == "ok")
            {
                server = null;
                SetUi(false);
            }
            else
                MessageBox.Show(msg);
        }

        public void ShowInfo(string info)
        {
            MessageBox.Show(info);
        }

        public void ShowText(string text)
        {
            chatLb.Items.Add($"[{DateTime.Now:T}] {text}");
        }

        public void ShowUsers(IEnumerable<string> users)
        {
            usersLb.ItemsSource = users;
        }

        private void SendTextClick(object sender, RoutedEventArgs e)
        {
            if (server != null && !string.IsNullOrWhiteSpace(textTb.Text))
            {
                server.SendText(textTb.Text);
                textTb.Clear();
                textTb.Focus();
            }
        }

        public void ShowImage(Stream image)
        {
            
            var ms = new MemoryStream();
            image.CopyTo(ms);
            ms.Position = 0;
            var img = new Image();
            img.BeginInit();
            img.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            img.Stretch = Stretch.None;
            img.EndInit();
            chatLb.Items.Add(img);
        }

        private void SendImageClick(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog() { Title = "Bild wählen", Filter = "Bild|*.png;*.jpg|Alle Dateien|*.*" };
            if (server != null && dlg.ShowDialog() == true)
            {
                using (var stream = File.OpenRead(dlg.FileName))
                {
                    server.SendImage(stream);
                }
            }
        }
    }
}
