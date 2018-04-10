using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChatHost
{
    // 1. Оcновной контракт(used by a client)
    [ServiceContract(CallbackContract = typeof(IChatServiceCallback))]
    public interface IChatService
    {
        
        [OperationContract(IsOneWay =true)]
        void SendMessage(string message);

        [OperationContract(IsOneWay = true)]
        void SendMessageDirectly(string message, string address, string sender);

        
        [OperationContract(IsOneWay = true)]
        void Disconnect(string name);//disconnects/Closes conenction, clears name from active users log

        [OperationContract(IsOneWay = true)]
        void Connect(string name);//adds user to active users list

    }
}
