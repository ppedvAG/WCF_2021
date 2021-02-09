using System.Collections.Generic;
using System.ServiceModel;

namespace WcfChat.Contracts
{
    [ServiceContract]
    public interface IClient
    {
        [OperationContract(IsOneWay = true)]
        void ShowText(string text);

        [OperationContract(IsOneWay = true)]
        void ShowUsers(IEnumerable<string> users);

        [OperationContract(IsOneWay = true)]
        void ShowInfo(string info);

        [OperationContract(IsOneWay = true)]
        void LoginResult(string msg);

        [OperationContract(IsOneWay = true)]
        void LogoutResult(string msg);
    }
}
