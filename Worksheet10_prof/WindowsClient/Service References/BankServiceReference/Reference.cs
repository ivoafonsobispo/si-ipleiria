﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsClient.BankServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BankServiceResponse", Namespace="http://schemas.datacontract.org/2004/07/AuthService")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(WindowsClient.BankServiceReference.BalanceResponse))]
    public partial class BankServiceResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WindowsClient.BankServiceReference.StatusCode StatusCodeField;
        
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
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WindowsClient.BankServiceReference.StatusCode StatusCode {
            get {
                return this.StatusCodeField;
            }
            set {
                if ((this.StatusCodeField.Equals(value) != true)) {
                    this.StatusCodeField = value;
                    this.RaisePropertyChanged("StatusCode");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="BalanceResponse", Namespace="http://schemas.datacontract.org/2004/07/AuthService")]
    [System.SerializableAttribute()]
    public partial class BalanceResponse : WindowsClient.BankServiceReference.BankServiceResponse {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PKCS7Base64BalanceField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PKCS7Base64Balance {
            get {
                return this.PKCS7Base64BalanceField;
            }
            set {
                if ((object.ReferenceEquals(this.PKCS7Base64BalanceField, value) != true)) {
                    this.PKCS7Base64BalanceField = value;
                    this.RaisePropertyChanged("PKCS7Base64Balance");
                }
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="StatusCode", Namespace="http://schemas.datacontract.org/2004/07/AuthService")]
    public enum StatusCode : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        OK = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        AuthenticationError = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        AuthorizationError = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CryptograficError = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DatabaseError = 4,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BankServiceReference.IBankService")]
    public interface IBankService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBankService/GetBalance", ReplyAction="http://tempuri.org/IBankService/GetBalanceResponse")]
        WindowsClient.BankServiceReference.BalanceResponse GetBalance(string pkcs7Base64);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBankService/GetBalance", ReplyAction="http://tempuri.org/IBankService/GetBalanceResponse")]
        System.Threading.Tasks.Task<WindowsClient.BankServiceReference.BalanceResponse> GetBalanceAsync(string pkcs7Base64);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBankServiceChannel : WindowsClient.BankServiceReference.IBankService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BankServiceClient : System.ServiceModel.ClientBase<WindowsClient.BankServiceReference.IBankService>, WindowsClient.BankServiceReference.IBankService {
        
        public BankServiceClient() {
        }
        
        public BankServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BankServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BankServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BankServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public WindowsClient.BankServiceReference.BalanceResponse GetBalance(string pkcs7Base64) {
            return base.Channel.GetBalance(pkcs7Base64);
        }
        
        public System.Threading.Tasks.Task<WindowsClient.BankServiceReference.BalanceResponse> GetBalanceAsync(string pkcs7Base64) {
            return base.Channel.GetBalanceAsync(pkcs7Base64);
        }
    }
}