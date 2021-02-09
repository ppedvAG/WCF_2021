using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using WcfChat.Contracts;

namespace WcfChat.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class WcfChatServer : IServer
    {
        static Dictionary<IClient, string> users = new Dictionary<IClient, string>();

        public void Login(string username)
        {
            Console.WriteLine($"Login {username}");

            var callback = OperationContext.Current.GetCallbackChannel<IClient>();
            callback.ShowText($"Hallo {username}");

            if (users.ContainsValue(username))
                callback.LoginResult("Username is already in use");
            else
            {
                callback.LoginResult("ok");
                users.Add(callback, username);
                SendUserListToAllUsers();
            }
        }

        private void SendUserListToAllUsers()
        {
            CallForAllUsers(x => x.ShowUsers(users.Select(u => u.Value)));
        }

        private void CallForAllUsers(Action<IClient> client)
        {
            //Parallel.ForEach(users.ToList(), usr =>
            //{
            //    try
            //    {
            //        client.Invoke(usr.Key);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"ERROR: {usr.Value}: {ex.Message}");
            //        users.Remove(usr.Key);
            //        SendUserListToAllUsers();
            //    }
            //});

            foreach (var usr in users.ToList().AsParallel())
            {
                try
                {
                    client.Invoke(usr.Key);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {usr.Value}: {ex.Message}");
                    users.Remove(usr.Key);
                    SendUserListToAllUsers();
                }
            }
        }


        public void Logout()
        {
            Console.WriteLine($"Logout");
            var callback = OperationContext.Current.GetCallbackChannel<IClient>();
            if (users.TryGetValue(callback, out string senderName))
            {
                users.Remove(callback);
                callback.LogoutResult("ok");
                SendUserListToAllUsers();

            }
        }

        public void SendImage(Stream image)
        {
            Console.WriteLine($"SendImage");


            var ms = new MemoryStream();
            image.CopyTo(ms);

            CallForAllUsers(x =>
            {
                ms.Position = 0;
                //Thread.Sleep(5000);
                x.ShowImage(ms);
            });
        }

        public void SendText(string text)
        {
            Console.WriteLine($"SendText {text}");

            var callback = OperationContext.Current.GetCallbackChannel<IClient>();
            if (users.TryGetValue(callback, out string senderName))
            {
                var msg = $"({senderName}) {text}";

                CallForAllUsers(x => x.ShowText(msg));

            }
        }
    }
}
