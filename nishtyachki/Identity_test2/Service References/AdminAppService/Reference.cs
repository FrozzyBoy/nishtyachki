﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdminApp.AdminAppService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Nishtiachok", Namespace="http://schemas.datacontract.org/2004/07/UsersQueue.Queue.Nishtiachki")]
    [System.SerializableAttribute()]
    public partial class Nishtiachok : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private AdminApp.AdminAppService.Nishtiachok_State StateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private AdminApp.AdminAppService.QueueUser ownerField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ID {
            get {
                return this.IDField;
            }
            set {
                if ((object.ReferenceEquals(this.IDField, value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AdminApp.AdminAppService.Nishtiachok_State State {
            get {
                return this.StateField;
            }
            set {
                if ((this.StateField.Equals(value) != true)) {
                    this.StateField = value;
                    this.RaisePropertyChanged("State");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AdminApp.AdminAppService.QueueUser owner {
            get {
                return this.ownerField;
            }
            set {
                if ((object.ReferenceEquals(this.ownerField, value) != true)) {
                    this.ownerField = value;
                    this.RaisePropertyChanged("owner");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="QueueUser", Namespace="http://schemas.datacontract.org/2004/07/UsersQueue.Queue.UserInformtion")]
    [System.SerializableAttribute()]
    public partial class QueueUser : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private AdminApp.AdminAppService.Role RoleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> _premiumEndDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private AdminApp.AdminAppService.UserCurrentState _stateField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ID {
            get {
                return this.IDField;
            }
            set {
                if ((object.ReferenceEquals(this.IDField, value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AdminApp.AdminAppService.Role Role {
            get {
                return this.RoleField;
            }
            set {
                if ((this.RoleField.Equals(value) != true)) {
                    this.RoleField = value;
                    this.RaisePropertyChanged("Role");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> _premiumEndDate {
            get {
                return this._premiumEndDateField;
            }
            set {
                if ((this._premiumEndDateField.Equals(value) != true)) {
                    this._premiumEndDateField = value;
                    this.RaisePropertyChanged("_premiumEndDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AdminApp.AdminAppService.UserCurrentState _state {
            get {
                return this._stateField;
            }
            set {
                if ((this._stateField.Equals(value) != true)) {
                    this._stateField = value;
                    this.RaisePropertyChanged("_state");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Nishtiachok_State", Namespace="http://schemas.datacontract.org/2004/07/UsersQueue.Queue.Nishtiachki")]
    public enum Nishtiachok_State : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        free = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        locked = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        wait_for_user = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        in_using = 3,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Role", Namespace="http://schemas.datacontract.org/2004/07/UsersQueue.Queue.UserInformtion")]
    public enum Role : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        standart = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        premium = 1,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserCurrentState", Namespace="http://schemas.datacontract.org/2004/07/UsersQueue.Queue.UserInformtion")]
    public enum UserCurrentState : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Offline = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Online = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        InQueue = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        AcceptingOffer = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UsingNishtiak = 4,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserInfo", Namespace="http://schemas.datacontract.org/2004/07/UsersQueue.Queue.UserInformtion")]
    [System.SerializableAttribute()]
    public partial class UserInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> PremiumEndDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private AdminApp.AdminAppService.UserCurrentState StateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> PremiumEndDate {
            get {
                return this.PremiumEndDateField;
            }
            set {
                if ((this.PremiumEndDateField.Equals(value) != true)) {
                    this.PremiumEndDateField = value;
                    this.RaisePropertyChanged("PremiumEndDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AdminApp.AdminAppService.UserCurrentState State {
            get {
                return this.StateField;
            }
            set {
                if ((this.StateField.Equals(value) != true)) {
                    this.StateField = value;
                    this.RaisePropertyChanged("State");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EventArgs", Namespace="http://schemas.datacontract.org/2004/07/System")]
    [System.SerializableAttribute()]
    public partial class EventArgs : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AdminAppService.IAdminAppService", CallbackContract=typeof(AdminApp.AdminAppService.IAdminAppServiceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IAdminAppService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/AddNishtiak", ReplyAction="http://tempuri.org/IAdminAppService/AddNishtiakResponse")]
        void AddNishtiak(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/AddNishtiak", ReplyAction="http://tempuri.org/IAdminAppService/AddNishtiakResponse")]
        System.Threading.Tasks.Task AddNishtiakAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/DeleteNishtiak", ReplyAction="http://tempuri.org/IAdminAppService/DeleteNishtiakResponse")]
        void DeleteNishtiak(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/DeleteNishtiak", ReplyAction="http://tempuri.org/IAdminAppService/DeleteNishtiakResponse")]
        System.Threading.Tasks.Task DeleteNishtiakAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/ChangeNishtiakState", ReplyAction="http://tempuri.org/IAdminAppService/ChangeNishtiakStateResponse")]
        void ChangeNishtiakState(string id, int state);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/ChangeNishtiakState", ReplyAction="http://tempuri.org/IAdminAppService/ChangeNishtiakStateResponse")]
        System.Threading.Tasks.Task ChangeNishtiakStateAsync(string id, int state);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/GetAllNishtiachoks", ReplyAction="http://tempuri.org/IAdminAppService/GetAllNishtiachoksResponse")]
        AdminApp.AdminAppService.Nishtiachok[] GetAllNishtiachoks();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/GetAllNishtiachoks", ReplyAction="http://tempuri.org/IAdminAppService/GetAllNishtiachoksResponse")]
        System.Threading.Tasks.Task<AdminApp.AdminAppService.Nishtiachok[]> GetAllNishtiachoksAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/DeleteUserByAdmin", ReplyAction="http://tempuri.org/IAdminAppService/DeleteUserByAdminResponse")]
        void DeleteUserByAdmin(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/DeleteUserByAdmin", ReplyAction="http://tempuri.org/IAdminAppService/DeleteUserByAdminResponse")]
        System.Threading.Tasks.Task DeleteUserByAdminAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/SwitchQueueState", ReplyAction="http://tempuri.org/IAdminAppService/SwitchQueueStateResponse")]
        void SwitchQueueState();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/SwitchQueueState", ReplyAction="http://tempuri.org/IAdminAppService/SwitchQueueStateResponse")]
        System.Threading.Tasks.Task SwitchQueueStateAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/UpdateUsersInQueue", ReplyAction="http://tempuri.org/IAdminAppService/UpdateUsersInQueueResponse")]
        void UpdateUsersInQueue(string[] userNames);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/UpdateUsersInQueue", ReplyAction="http://tempuri.org/IAdminAppService/UpdateUsersInQueueResponse")]
        System.Threading.Tasks.Task UpdateUsersInQueueAsync(string[] userNames);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/GetUserInQueueByID", ReplyAction="http://tempuri.org/IAdminAppService/GetUserInQueueByIDResponse")]
        AdminApp.AdminAppService.QueueUser GetUserInQueueByID(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/GetUserInQueueByID", ReplyAction="http://tempuri.org/IAdminAppService/GetUserInQueueByIDResponse")]
        System.Threading.Tasks.Task<AdminApp.AdminAppService.QueueUser> GetUserInQueueByIDAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/GetAllUsersInQueue", ReplyAction="http://tempuri.org/IAdminAppService/GetAllUsersInQueueResponse")]
        AdminApp.AdminAppService.QueueUser[] GetAllUsersInQueue();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/GetAllUsersInQueue", ReplyAction="http://tempuri.org/IAdminAppService/GetAllUsersInQueueResponse")]
        System.Threading.Tasks.Task<AdminApp.AdminAppService.QueueUser[]> GetAllUsersInQueueAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/SendMsg", ReplyAction="http://tempuri.org/IAdminAppService/SendMsgResponse")]
        void SendMsg(string msg, string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/SendMsg", ReplyAction="http://tempuri.org/IAdminAppService/SendMsgResponse")]
        System.Threading.Tasks.Task SendMsgAsync(string msg, string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/ChangeUserRole", ReplyAction="http://tempuri.org/IAdminAppService/ChangeUserRoleResponse")]
        void ChangeUserRole(string id, int role);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/ChangeUserRole", ReplyAction="http://tempuri.org/IAdminAppService/ChangeUserRoleResponse")]
        System.Threading.Tasks.Task ChangeUserRoleAsync(string id, int role);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/GetUserInfoByID", ReplyAction="http://tempuri.org/IAdminAppService/GetUserInfoByIDResponse")]
        AdminApp.AdminAppService.UserInfo GetUserInfoByID(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/GetUserInfoByID", ReplyAction="http://tempuri.org/IAdminAppService/GetUserInfoByIDResponse")]
        System.Threading.Tasks.Task<AdminApp.AdminAppService.UserInfo> GetUserInfoByIDAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/GetInfoForAllUsers", ReplyAction="http://tempuri.org/IAdminAppService/GetInfoForAllUsersResponse")]
        AdminApp.AdminAppService.UserInfo[] GetInfoForAllUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/GetInfoForAllUsers", ReplyAction="http://tempuri.org/IAdminAppService/GetInfoForAllUsersResponse")]
        System.Threading.Tasks.Task<AdminApp.AdminAppService.UserInfo[]> GetInfoForAllUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/Ping", ReplyAction="http://tempuri.org/IAdminAppService/PingResponse")]
        bool Ping();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/Ping", ReplyAction="http://tempuri.org/IAdminAppService/PingResponse")]
        System.Threading.Tasks.Task<bool> PingAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAdminAppServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminAppService/UpdateQueue")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdminApp.AdminAppService.EventArgs))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdminApp.AdminAppService.Nishtiachok[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdminApp.AdminAppService.Nishtiachok))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdminApp.AdminAppService.Nishtiachok_State))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdminApp.AdminAppService.QueueUser))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdminApp.AdminAppService.Role))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdminApp.AdminAppService.UserCurrentState))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdminApp.AdminAppService.QueueUser[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdminApp.AdminAppService.UserInfo))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdminApp.AdminAppService.UserInfo[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(string[]))]
        void UpdateQueue(object sender, AdminApp.AdminAppService.EventArgs e);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminAppService/UpdateNishtiachok")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdminApp.AdminAppService.EventArgs))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdminApp.AdminAppService.Nishtiachok[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdminApp.AdminAppService.Nishtiachok))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdminApp.AdminAppService.Nishtiachok_State))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdminApp.AdminAppService.QueueUser))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdminApp.AdminAppService.Role))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdminApp.AdminAppService.UserCurrentState))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdminApp.AdminAppService.QueueUser[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdminApp.AdminAppService.UserInfo))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdminApp.AdminAppService.UserInfo[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(string[]))]
        void UpdateNishtiachok(object sender, AdminApp.AdminAppService.EventArgs e);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAdminAppServiceChannel : AdminApp.AdminAppService.IAdminAppService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AdminAppServiceClient : System.ServiceModel.DuplexClientBase<AdminApp.AdminAppService.IAdminAppService>, AdminApp.AdminAppService.IAdminAppService {
        
        public AdminAppServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public AdminAppServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public AdminAppServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public AdminAppServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public AdminAppServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void AddNishtiak(string id) {
            base.Channel.AddNishtiak(id);
        }
        
        public System.Threading.Tasks.Task AddNishtiakAsync(string id) {
            return base.Channel.AddNishtiakAsync(id);
        }
        
        public void DeleteNishtiak(string id) {
            base.Channel.DeleteNishtiak(id);
        }
        
        public System.Threading.Tasks.Task DeleteNishtiakAsync(string id) {
            return base.Channel.DeleteNishtiakAsync(id);
        }
        
        public void ChangeNishtiakState(string id, int state) {
            base.Channel.ChangeNishtiakState(id, state);
        }
        
        public System.Threading.Tasks.Task ChangeNishtiakStateAsync(string id, int state) {
            return base.Channel.ChangeNishtiakStateAsync(id, state);
        }
        
        public AdminApp.AdminAppService.Nishtiachok[] GetAllNishtiachoks() {
            return base.Channel.GetAllNishtiachoks();
        }
        
        public System.Threading.Tasks.Task<AdminApp.AdminAppService.Nishtiachok[]> GetAllNishtiachoksAsync() {
            return base.Channel.GetAllNishtiachoksAsync();
        }
        
        public void DeleteUserByAdmin(string id) {
            base.Channel.DeleteUserByAdmin(id);
        }
        
        public System.Threading.Tasks.Task DeleteUserByAdminAsync(string id) {
            return base.Channel.DeleteUserByAdminAsync(id);
        }
        
        public void SwitchQueueState() {
            base.Channel.SwitchQueueState();
        }
        
        public System.Threading.Tasks.Task SwitchQueueStateAsync() {
            return base.Channel.SwitchQueueStateAsync();
        }
        
        public void UpdateUsersInQueue(string[] userNames) {
            base.Channel.UpdateUsersInQueue(userNames);
        }
        
        public System.Threading.Tasks.Task UpdateUsersInQueueAsync(string[] userNames) {
            return base.Channel.UpdateUsersInQueueAsync(userNames);
        }
        
        public AdminApp.AdminAppService.QueueUser GetUserInQueueByID(string id) {
            return base.Channel.GetUserInQueueByID(id);
        }
        
        public System.Threading.Tasks.Task<AdminApp.AdminAppService.QueueUser> GetUserInQueueByIDAsync(string id) {
            return base.Channel.GetUserInQueueByIDAsync(id);
        }
        
        public AdminApp.AdminAppService.QueueUser[] GetAllUsersInQueue() {
            return base.Channel.GetAllUsersInQueue();
        }
        
        public System.Threading.Tasks.Task<AdminApp.AdminAppService.QueueUser[]> GetAllUsersInQueueAsync() {
            return base.Channel.GetAllUsersInQueueAsync();
        }
        
        public void SendMsg(string msg, string id) {
            base.Channel.SendMsg(msg, id);
        }
        
        public System.Threading.Tasks.Task SendMsgAsync(string msg, string id) {
            return base.Channel.SendMsgAsync(msg, id);
        }
        
        public void ChangeUserRole(string id, int role) {
            base.Channel.ChangeUserRole(id, role);
        }
        
        public System.Threading.Tasks.Task ChangeUserRoleAsync(string id, int role) {
            return base.Channel.ChangeUserRoleAsync(id, role);
        }
        
        public AdminApp.AdminAppService.UserInfo GetUserInfoByID(string id) {
            return base.Channel.GetUserInfoByID(id);
        }
        
        public System.Threading.Tasks.Task<AdminApp.AdminAppService.UserInfo> GetUserInfoByIDAsync(string id) {
            return base.Channel.GetUserInfoByIDAsync(id);
        }
        
        public AdminApp.AdminAppService.UserInfo[] GetInfoForAllUsers() {
            return base.Channel.GetInfoForAllUsers();
        }
        
        public System.Threading.Tasks.Task<AdminApp.AdminAppService.UserInfo[]> GetInfoForAllUsersAsync() {
            return base.Channel.GetInfoForAllUsersAsync();
        }
        
        public bool Ping() {
            return base.Channel.Ping();
        }
        
        public System.Threading.Tasks.Task<bool> PingAsync() {
            return base.Channel.PingAsync();
        }
    }
}
