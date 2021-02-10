using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WCF_REST_Flugzeuge
{
    [ServiceContract]
    public interface IFlugService
    {
        [OperationContract]
        [WebGet(UriTemplate = "Flugzeuge",ResponseFormat =WebMessageFormat.Xml)]
        IEnumerable<Flugzeug> GetAll();

        [OperationContract]
        [WebGet(UriTemplate = "Flugzeuge?id={id}")]
        Flugzeug GetById(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Flugzeuge")]
        void AddFlugzeug(Flugzeug fz);

        [OperationContract]
        [WebInvoke(Method = "UPDATE", UriTemplate = "Flugzeuge")]
        void Update(Flugzeug fz);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "Flugzeuge?id={id}")]
        void Delete(int id);
    }
}
