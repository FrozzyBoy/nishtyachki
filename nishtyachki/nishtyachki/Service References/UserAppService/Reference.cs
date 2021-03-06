﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace nishtyachki.UserAppService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UserAppService.IUserAppService", CallbackContract=typeof(nishtyachki.UserAppService.IUserAppServiceCallback))]
    public interface IUserAppService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAppService/InitUser", ReplyAction="http://tempuri.org/IUserAppService/InitUserResponse")]
        void InitUser();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAppService/InitUser", ReplyAction="http://tempuri.org/IUserAppService/InitUserResponse")]
        System.Threading.Tasks.Task InitUserAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAppService/TryStandInQueue", ReplyAction="http://tempuri.org/IUserAppService/TryStandInQueueResponse")]
        bool TryStandInQueue();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAppService/TryStandInQueue", ReplyAction="http://tempuri.org/IUserAppService/TryStandInQueueResponse")]
        System.Threading.Tasks.Task<bool> TryStandInQueueAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAppService/LeaveQueue", ReplyAction="http://tempuri.org/IUserAppService/LeaveQueueResponse")]
        void LeaveQueue();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAppService/LeaveQueue", ReplyAction="http://tempuri.org/IUserAppService/LeaveQueueResponse")]
        System.Threading.Tasks.Task LeaveQueueAsync();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IUserAppService/AnswerForOfferToUse")]
        void AnswerForOfferToUse(bool willUse);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IUserAppService/AnswerForOfferToUse")]
        System.Threading.Tasks.Task AnswerForOfferToUseAsync(bool willUse);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAppService/StopUseObj", ReplyAction="http://tempuri.org/IUserAppService/StopUseObjResponse")]
        void StopUseObj();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAppService/StopUseObj", ReplyAction="http://tempuri.org/IUserAppService/StopUseObjResponse")]
        System.Threading.Tasks.Task StopUseObjAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAppService/Disconnect", ReplyAction="http://tempuri.org/IUserAppService/DisconnectResponse")]
        void Disconnect();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAppService/Disconnect", ReplyAction="http://tempuri.org/IUserAppService/DisconnectResponse")]
        System.Threading.Tasks.Task DisconnectAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserAppServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IUserAppService/NotifyServerReady")]
        void NotifyServerReady();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IUserAppService/ShowMessage")]
        void ShowMessage(string text);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IUserAppService/StandInQueue")]
        void StandInQueue();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IUserAppService/ShowPosition")]
        void ShowPosition(int position);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAppService/OfferToUseObj", ReplyAction="http://tempuri.org/IUserAppService/OfferToUseObjResponse")]
        void OfferToUseObj();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAppService/NotifyToUseObj", ReplyAction="http://tempuri.org/IUserAppService/NotifyToUseObjResponse")]
        void NotifyToUseObj();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAppService/DroppedByServer", ReplyAction="http://tempuri.org/IUserAppService/DroppedByServerResponse")]
        void DroppedByServer(string text);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserAppServiceChannel : nishtyachki.UserAppService.IUserAppService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserAppServiceClient : System.ServiceModel.DuplexClientBase<nishtyachki.UserAppService.IUserAppService>, nishtyachki.UserAppService.IUserAppService {
        
        public UserAppServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public UserAppServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public UserAppServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public UserAppServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public UserAppServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void InitUser() {
            base.Channel.InitUser();
        }
        
        public System.Threading.Tasks.Task InitUserAsync() {
            return base.Channel.InitUserAsync();
        }
        
        public bool TryStandInQueue() {
            return base.Channel.TryStandInQueue();
        }
        
        public System.Threading.Tasks.Task<bool> TryStandInQueueAsync() {
            return base.Channel.TryStandInQueueAsync();
        }
        
        public void LeaveQueue() {
            base.Channel.LeaveQueue();
        }
        
        public System.Threading.Tasks.Task LeaveQueueAsync() {
            return base.Channel.LeaveQueueAsync();
        }
        
        public void AnswerForOfferToUse(bool willUse) {
            base.Channel.AnswerForOfferToUse(willUse);
        }
        
        public System.Threading.Tasks.Task AnswerForOfferToUseAsync(bool willUse) {
            return base.Channel.AnswerForOfferToUseAsync(willUse);
        }
        
        public void StopUseObj() {
            base.Channel.StopUseObj();
        }
        
        public System.Threading.Tasks.Task StopUseObjAsync() {
            return base.Channel.StopUseObjAsync();
        }
        
        public void Disconnect() {
            base.Channel.Disconnect();
        }
        
        public System.Threading.Tasks.Task DisconnectAsync() {
            return base.Channel.DisconnectAsync();
        }
    }
}
