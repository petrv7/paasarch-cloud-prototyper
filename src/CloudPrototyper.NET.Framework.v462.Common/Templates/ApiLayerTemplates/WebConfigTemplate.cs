//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:6.0.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CloudPrototyper.NET.Framework.v462.Common.Templates.ApiLayerTemplates {
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    
    public partial class WebConfigTemplate : WebConfigTemplateBase {
        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 6 "Templates\ApiLayerTemplates\WebConfigTemplate.tt"
            this.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<!--\r\n  For more information on how to co" +
                    "nfigure your ASP.NET application, please visit\r\n  http://go.microsoft.com/fwlink" +
                    "/?LinkId=301879\r\n  -->\r\n<configuration>\r\n  <configSections>\r\n    <!-- For more i" +
                    "nformation on Entity Framework configuration, visit http://go.microsoft.com/fwli" +
                    "nk/?LinkID=237468 -->\r\n    <section name=\"entityFramework\" type=\"System.Data.Ent" +
                    "ity.Internal.ConfigFile.EntityFrameworkSection, EntityFramework, Version=6.0.0.0" +
                    ", Culture=neutral, PublicKeyToken=b77a5c561934e089\" requirePermission=\"false\"/>\r" +
                    "\n  </configSections>\r\n  <appSettings></appSettings>\r\n  <system.web>\r\n    <compil" +
                    "ation debug=\"true\" targetFramework=\"4.6.2\"/>\r\n    <httpRuntime targetFramework=\"" +
                    "4.6.2\"/>\r\n  </system.web>\r\n  <assemblyBinding xmlns=\"urn:schemas-microsoft-com:a" +
                    "sm.v1\">\r\n    <!-- For local debbuging replace with local absolute path -->\r\n    " +
                    "<linkedConfiguration href=\"file:C:/home/site/wwwroot/bin/ApiLayer.dll.config\"/>\r" +
                    "\n  </assemblyBinding>  \r\n  <system.webServer>\r\n    <handlers>\r\n      <remove nam" +
                    "e=\"ExtensionlessUrlHandler-Integrated-4.0\"/>\r\n      <remove name=\"OPTIONSVerbHan" +
                    "dler\"/>\r\n      <remove name=\"TRACEVerbHandler\"/>\r\n      <add name=\"Extensionless" +
                    "UrlHandler-Integrated-4.0\" path=\"*.\" verb=\"*\" type=\"System.Web.Handlers.Transfer" +
                    "RequestHandler\"\r\n        preCondition=\"integratedMode,runtimeVersionv4.0\"/>\r\n   " +
                    " </handlers>\r\n  </system.webServer>\r\n   <entityFramework>\r\n    <defaultConnectio" +
                    "nFactory type=\"System.Data.Entity.Infrastructure.LocalDbConnectionFactory, Entit" +
                    "yFramework\">\r\n      <parameters>\r\n        <parameter value=\"mssqllocaldb\"/>\r\n   " +
                    "   </parameters>\r\n    </defaultConnectionFactory>\r\n    <providers>\r\n      <provi" +
                    "der invariantName=\"System.Data.SqlClient\" type=\"System.Data.Entity.SqlServer.Sql" +
                    "ProviderServices, EntityFramework.SqlServer\"/>\r\n    </providers>\r\n  </entityFram" +
                    "ework>\r\n</configuration>\r\n");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
        }
    }
    
    public class WebConfigTemplateBase {
        
        private global::System.Text.StringBuilder builder;
        
        private global::System.Collections.Generic.IDictionary<string, object> session;
        
        private global::System.CodeDom.Compiler.CompilerErrorCollection errors;
        
        private string currentIndent = string.Empty;
        
        private global::System.Collections.Generic.Stack<int> indents;
        
        private ToStringInstanceHelper _toStringHelper = new ToStringInstanceHelper();
        
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session {
            get {
                return this.session;
            }
            set {
                this.session = value;
            }
        }
        
        public global::System.Text.StringBuilder GenerationEnvironment {
            get {
                if ((this.builder == null)) {
                    this.builder = new global::System.Text.StringBuilder();
                }
                return this.builder;
            }
            set {
                this.builder = value;
            }
        }
        
        protected global::System.CodeDom.Compiler.CompilerErrorCollection Errors {
            get {
                if ((this.errors == null)) {
                    this.errors = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errors;
            }
        }
        
        public string CurrentIndent {
            get {
                return this.currentIndent;
            }
        }
        
        private global::System.Collections.Generic.Stack<int> Indents {
            get {
                if ((this.indents == null)) {
                    this.indents = new global::System.Collections.Generic.Stack<int>();
                }
                return this.indents;
            }
        }
        
        public ToStringInstanceHelper ToStringHelper {
            get {
                return this._toStringHelper;
            }
        }
        
        public void Error(string message) {
            this.Errors.Add(new global::System.CodeDom.Compiler.CompilerError(null, -1, -1, null, message));
        }
        
        public void Warning(string message) {
            global::System.CodeDom.Compiler.CompilerError val = new global::System.CodeDom.Compiler.CompilerError(null, -1, -1, null, message);
            val.IsWarning = true;
            this.Errors.Add(val);
        }
        
        public string PopIndent() {
            if ((this.Indents.Count == 0)) {
                return string.Empty;
            }
            int lastPos = (this.currentIndent.Length - this.Indents.Pop());
            string last = this.currentIndent.Substring(lastPos);
            this.currentIndent = this.currentIndent.Substring(0, lastPos);
            return last;
        }
        
        public void PushIndent(string indent) {
            this.Indents.Push(indent.Length);
            this.currentIndent = (this.currentIndent + indent);
        }
        
        public void ClearIndent() {
            this.currentIndent = string.Empty;
            this.Indents.Clear();
        }
        
        public void Write(string textToAppend) {
            this.GenerationEnvironment.Append(textToAppend);
        }
        
        public void Write(string format, params object[] args) {
            this.GenerationEnvironment.AppendFormat(format, args);
        }
        
        public void WriteLine(string textToAppend) {
            this.GenerationEnvironment.Append(this.currentIndent);
            this.GenerationEnvironment.AppendLine(textToAppend);
        }
        
        public void WriteLine(string format, params object[] args) {
            this.GenerationEnvironment.Append(this.currentIndent);
            this.GenerationEnvironment.AppendFormat(format, args);
            this.GenerationEnvironment.AppendLine();
        }
        
        public class ToStringInstanceHelper {
            
            private global::System.IFormatProvider formatProvider = global::System.Globalization.CultureInfo.InvariantCulture;
            
            public global::System.IFormatProvider FormatProvider {
                get {
                    return this.formatProvider;
                }
                set {
                    if ((value != null)) {
                        this.formatProvider = value;
                    }
                }
            }
            
            public string ToStringWithCulture(object objectToConvert) {
                if ((objectToConvert == null)) {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                global::System.Type type = objectToConvert.GetType();
                global::System.Type iConvertibleType = typeof(global::System.IConvertible);
                if (iConvertibleType.IsAssignableFrom(type)) {
                    return ((global::System.IConvertible)(objectToConvert)).ToString(this.formatProvider);
                }
                global::System.Reflection.MethodInfo methInfo = type.GetMethod("ToString", new global::System.Type[] {
                            iConvertibleType});
                if ((methInfo != null)) {
                    return ((string)(methInfo.Invoke(objectToConvert, new object[] {
                                this.formatProvider})));
                }
                return objectToConvert.ToString();
            }
        }
    }
}
