﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.3705.209
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 1.0.3705.209.
// 
namespace Anderson.Chris.BlogX.Services 
{
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Web.Services;
    using Anderson.Chris.BlogX.Runtime;
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="BlogXBrowsingSoap", Namespace="http://www.simplegeek.com")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(EntryBase))]
    public class BlogXBrowsing : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public BlogXBrowsing() {
            this.Url = "http://localhost/weblogx/blogxbrowsing.asmx";
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.simplegeek.com/GetRssUrl", RequestNamespace="http://www.simplegeek.com", ResponseNamespace="http://www.simplegeek.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetRssUrl() {
            object[] results = this.Invoke("GetRssUrl", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetRssUrl(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetRssUrl", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public string EndGetRssUrl(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.simplegeek.com/GetRss", RequestNamespace="http://www.simplegeek.com", ResponseNamespace="http://www.simplegeek.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetRss() {
            object[] results = this.Invoke("GetRss", new object[0]);
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetRss(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetRss", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public System.Xml.XmlNode EndGetRss(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.simplegeek.com/GetRssCategory", RequestNamespace="http://www.simplegeek.com", ResponseNamespace="http://www.simplegeek.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetRssCategory(string categoryName) {
            object[] results = this.Invoke("GetRssCategory", new object[] {
                        categoryName});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetRssCategory(string categoryName, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetRssCategory", new object[] {
                        categoryName}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Xml.XmlNode EndGetRssCategory(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.simplegeek.com/GetRssWithDayCount", RequestNamespace="http://www.simplegeek.com", ResponseNamespace="http://www.simplegeek.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetRssWithDayCount(int maxDayCount) {
            object[] results = this.Invoke("GetRssWithDayCount", new object[] {
                        maxDayCount});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetRssWithDayCount(int maxDayCount, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetRssWithDayCount", new object[] {
                        maxDayCount}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Xml.XmlNode EndGetRssWithDayCount(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.simplegeek.com/GetReferrerLog", RequestNamespace="http://www.simplegeek.com", ResponseNamespace="http://www.simplegeek.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public LogDataItem[] GetReferrerLog(System.DateTime date) {
            object[] results = this.Invoke("GetReferrerLog", new object[] {
                        date});
            return ((LogDataItem[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetReferrerLog(System.DateTime date, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetReferrerLog", new object[] {
                        date}, callback, asyncState);
        }
        
        /// <remarks/>
        public LogDataItem[] EndGetReferrerLog(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((LogDataItem[])(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.simplegeek.com/GetCategoryList", RequestNamespace="http://www.simplegeek.com", ResponseNamespace="http://www.simplegeek.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string[] GetCategoryList() {
            object[] results = this.Invoke("GetCategoryList", new object[0]);
            return ((string[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetCategoryList(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetCategoryList", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public string[] EndGetCategoryList(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.simplegeek.com/GetCategoryEntries", RequestNamespace="http://www.simplegeek.com", ResponseNamespace="http://www.simplegeek.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Entry[] GetCategoryEntries(string categoryName) {
            object[] results = this.Invoke("GetCategoryEntries", new object[] {
                        categoryName});
            return ((Entry[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetCategoryEntries(string categoryName, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetCategoryEntries", new object[] {
                        categoryName}, callback, asyncState);
        }
        
        /// <remarks/>
        public Entry[] EndGetCategoryEntries(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((Entry[])(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.simplegeek.com/GetDaysWithEntries", RequestNamespace="http://www.simplegeek.com", ResponseNamespace="http://www.simplegeek.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.DateTime[] GetDaysWithEntries() {
            object[] results = this.Invoke("GetDaysWithEntries", new object[0]);
            return ((System.DateTime[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetDaysWithEntries(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetDaysWithEntries", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public System.DateTime[] EndGetDaysWithEntries(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.DateTime[])(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.simplegeek.com/GetDayEntry", RequestNamespace="http://www.simplegeek.com", ResponseNamespace="http://www.simplegeek.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public DayEntry GetDayEntry(System.DateTime date) {
            object[] results = this.Invoke("GetDayEntry", new object[] {
                        date});
            return ((DayEntry)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetDayEntry(System.DateTime date, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetDayEntry", new object[] {
                        date}, callback, asyncState);
        }
        
        /// <remarks/>
        public DayEntry EndGetDayEntry(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((DayEntry)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.simplegeek.com/GetDayExtra", RequestNamespace="http://www.simplegeek.com", ResponseNamespace="http://www.simplegeek.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public DayExtra GetDayExtra(System.DateTime date) {
            object[] results = this.Invoke("GetDayExtra", new object[] {
                        date});
            return ((DayExtra)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetDayExtra(System.DateTime date, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetDayExtra", new object[] {
                        date}, callback, asyncState);
        }
        
        /// <remarks/>
        public DayExtra EndGetDayExtra(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((DayExtra)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.simplegeek.com/AddComment", RequestNamespace="http://www.simplegeek.com", ResponseNamespace="http://www.simplegeek.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void AddComment(Comment comment) {
            this.Invoke("AddComment", new object[] {
                        comment});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginAddComment(Comment comment, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("AddComment", new object[] {
                        comment}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndAddComment(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
    }
}
