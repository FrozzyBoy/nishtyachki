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
    [System.Runtime.Serialization.DataContractAttribute(Name="Nishtiachok", Namespace="http://schemas.datacontract.org/2004/07/UsersQueue.Services.TransferObjects")]
    [System.SerializableAttribute()]
    public partial class Nishtiachok : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int StateField;
        
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
        public int State {
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
    [System.Runtime.Serialization.DataContractAttribute(Name="QueueUser", Namespace="http://schemas.datacontract.org/2004/07/UsersQueue.Services.TransferObjects")]
    [System.SerializableAttribute()]
    public partial class QueueUser : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime PremiumEndDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RoleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int StateField;
        
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
        public System.DateTime PremiumEndDate {
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
        public int Role {
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
        public int State {
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
    [System.Runtime.Serialization.DataContractAttribute(Name="UserInfo", Namespace="http://schemas.datacontract.org/2004/07/UsersQueue.Queue.UserInformtion")]
    [System.SerializableAttribute()]
    public partial class UserInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
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
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
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
    [System.Runtime.Serialization.DataContractAttribute(Name="ChartValues", Namespace="http://schemas.datacontract.org/2004/07/UsersQueue.Services.TransferObjects")]
    [System.SerializableAttribute()]
    public partial struct ChartValues : System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] labelsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int[] numbersField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] labels {
            get {
                return this.labelsField;
            }
            set {
                if ((object.ReferenceEquals(this.labelsField, value) != true)) {
                    this.labelsField = value;
                    this.RaisePropertyChanged("labels");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int[] numbers {
            get {
                return this.numbersField;
            }
            set {
                if ((object.ReferenceEquals(this.numbersField, value) != true)) {
                    this.numbersField = value;
                    this.RaisePropertyChanged("numbers");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        void RaisePropertyChanged(string propertyName) {
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
        void AddNishtiak();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/AddNishtiak", ReplyAction="http://tempuri.org/IAdminAppService/AddNishtiakResponse")]
        System.Threading.Tasks.Task AddNishtiakAsync();
        
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
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/GetNishtiakById", ReplyAction="http://tempuri.org/IAdminAppService/GetNishtiakByIdResponse")]
        AdminApp.AdminAppService.Nishtiachok GetNishtiakById(string nishtiakId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/GetNishtiakById", ReplyAction="http://tempuri.org/IAdminAppService/GetNishtiakByIdResponse")]
        System.Threading.Tasks.Task<AdminApp.AdminAppService.Nishtiachok> GetNishtiakByIdAsync(string nishtiakId);
        
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
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/GetQueueState", ReplyAction="http://tempuri.org/IAdminAppService/GetQueueStateResponse")]
        int GetQueueState();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/GetQueueState", ReplyAction="http://tempuri.org/IAdminAppService/GetQueueStateResponse")]
        System.Threading.Tasks.Task<int> GetQueueStateAsync();
        
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
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/GetStatisticsPersonal", ReplyAction="http://tempuri.org/IAdminAppService/GetStatisticsPersonalResponse")]
        AdminApp.AdminAppService.ChartValues GetStatisticsPersonal(string userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/GetStatisticsPersonal", ReplyAction="http://tempuri.org/IAdminAppService/GetStatisticsPersonalResponse")]
        System.Threading.Tasks.Task<AdminApp.AdminAppService.ChartValues> GetStatisticsPersonalAsync(string userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/GetStatisticsGeneralWasMoreThenAthoresInState" +
            "", ReplyAction="http://tempuri.org/IAdminAppService/GetStatisticsGeneralWasMoreThenAthoresInState" +
            "Response")]
        AdminApp.AdminAppService.ChartValues GetStatisticsGeneralWasMoreThenAthoresInState(int stat);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/GetStatisticsGeneralWasMoreThenAthoresInState" +
            "", ReplyAction="http://tempuri.org/IAdminAppService/GetStatisticsGeneralWasMoreThenAthoresInState" +
            "Response")]
        System.Threading.Tasks.Task<AdminApp.AdminAppService.ChartValues> GetStatisticsGeneralWasMoreThenAthoresInStateAsync(int stat);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/GetStatisticsForNishtiak", ReplyAction="http://tempuri.org/IAdminAppService/GetStatisticsForNishtiakResponse")]
        AdminApp.AdminAppService.ChartValues GetStatisticsForNishtiak(string nishtiakID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/GetStatisticsForNishtiak", ReplyAction="http://tempuri.org/IAdminAppService/GetStatisticsForNishtiakResponse")]
        System.Threading.Tasks.Task<AdminApp.AdminAppService.ChartValues> GetStatisticsForNishtiakAsync(string nishtiakID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/Ping", ReplyAction="http://tempuri.org/IAdminAppService/PingResponse")]
        bool Ping();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/Ping", ReplyAction="http://tempuri.org/IAdminAppService/PingResponse")]
        System.Threading.Tasks.Task<bool> PingAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/Init", ReplyAction="http://tempuri.org/IAdminAppService/InitResponse")]
        void Init();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminAppService/Init", ReplyAction="http://tempuri.org/IAdminAppService/InitResponse")]
        System.Threading.Tasks.Task InitAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAdminAppServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminAppService/UpdateQueue")]
        void UpdateQueue();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminAppService/UpdateNishtiachok")]
        void UpdateNishtiachok();
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
        
        public void AddNishtiak() {
            base.Channel.AddNishtiak();
        }
        
        public System.Threading.Tasks.Task AddNishtiakAsync() {
            return base.Channel.AddNishtiakAsync();
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
        
        public AdminApp.AdminAppService.Nishtiachok GetNishtiakById(string nishtiakId) {
            return base.Channel.GetNishtiakById(nishtiakId);
        }
        
        public System.Threading.Tasks.Task<AdminApp.AdminAppService.Nishtiachok> GetNishtiakByIdAsync(string nishtiakId) {
            return base.Channel.GetNishtiakByIdAsync(nishtiakId);
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
        
        public int GetQueueState() {
            return base.Channel.GetQueueState();
        }
        
        public System.Threading.Tasks.Task<int> GetQueueStateAsync() {
            return base.Channel.GetQueueStateAsync();
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
        
        public AdminApp.AdminAppService.ChartValues GetStatisticsPersonal(string userId) {
            return base.Channel.GetStatisticsPersonal(userId);
        }
        
        public System.Threading.Tasks.Task<AdminApp.AdminAppService.ChartValues> GetStatisticsPersonalAsync(string userId) {
            return base.Channel.GetStatisticsPersonalAsync(userId);
        }
        
        public AdminApp.AdminAppService.ChartValues GetStatisticsGeneralWasMoreThenAthoresInState(int stat) {
            return base.Channel.GetStatisticsGeneralWasMoreThenAthoresInState(stat);
        }
        
        public System.Threading.Tasks.Task<AdminApp.AdminAppService.ChartValues> GetStatisticsGeneralWasMoreThenAthoresInStateAsync(int stat) {
            return base.Channel.GetStatisticsGeneralWasMoreThenAthoresInStateAsync(stat);
        }
        
        public AdminApp.AdminAppService.ChartValues GetStatisticsForNishtiak(string nishtiakID) {
            return base.Channel.GetStatisticsForNishtiak(nishtiakID);
        }
        
        public System.Threading.Tasks.Task<AdminApp.AdminAppService.ChartValues> GetStatisticsForNishtiakAsync(string nishtiakID) {
            return base.Channel.GetStatisticsForNishtiakAsync(nishtiakID);
        }
        
        public bool Ping() {
            return base.Channel.Ping();
        }
        
        public System.Threading.Tasks.Task<bool> PingAsync() {
            return base.Channel.PingAsync();
        }
        
        public void Init() {
            base.Channel.Init();
        }
        
        public System.Threading.Tasks.Task InitAsync() {
            return base.Channel.InitAsync();
        }
    }
}
