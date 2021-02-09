using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
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
            var netAdr = "net.tcp://localhost:1";

            var dcf = new DuplexChannelFactory<IServer>(this, netBind, netAdr);
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


    }
}
