using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
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
                callback.ShowUsers(users.Select(x => x.Value));
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
            }
        }

        public void SendImage(Stream image)
        {
            Console.WriteLine($"SendImage");
        }

        public void SendText(string text)
        {
            Console.WriteLine($"SendText {text}");

            var callback = OperationContext.Current.GetCallbackChannel<IClient>();
            if (users.TryGetValue(callback, out string senderName))
            {
                var msg = $"({senderName}) {text}";

                foreach (var usr in users.ToList())
                {
                    try
                    {
                        usr.Key.ShowText(msg);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\tError:{ex.Message}");
                        users.Remove(callback);
                    }
                }

            }
        }
    }
}
