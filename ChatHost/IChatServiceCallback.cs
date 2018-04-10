using System.ServiceModel;

namespace ChatHost
{
    //2. callbackclient used by service
    public interface IChatServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveNotify(string str);

        [OperationContract(IsOneWay = true)]
        void ReceiveClientsMessages(string str);

        
    } 
}
