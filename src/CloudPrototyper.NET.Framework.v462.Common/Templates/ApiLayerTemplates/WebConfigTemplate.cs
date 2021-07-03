//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:5.0.7
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace CloudPrototyper.NET.Framework.v462.Common.Templates.ApiLayerTemplates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;



    public partial class WebConfigTemplate : WebConfigTemplateBase
    {

        public virtual string TransformText()
        {
            this.GenerationEnvironment = null;

#line 6 "C:\Users\PV\source\repos\paasarch-cloud-prototyper\src\CloudPrototyper.NET.Framework.v462.Common\Templates\ApiLayerTemplates\WebConfigTemplate.tt"
            this.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<!--\r\n  For more information on how to co" +
                    "nfigure your ASP.NET application, please visit\r\n  http://go.microsoft.com/fwlink" +
                    "/?LinkId=301879\r\n  -->\r\n<configuration>\r\n  <configSections>\r\n    <!-- For more i" +
                    "nformation on Entity Framework configuration, visit http://go.microsoft.com/fwli" +
                    "nk/?LinkID=237468 -->\r\n    <section name=\"entityFramework\" type=\"System.Data.Ent" +
                    "ity.Internal.ConfigFile.EntityFrameworkSection, EntityFramework, Version=6.0.0.0" +
                    ", Culture=neutral, PublicKeyToken=b77a5c561934e089\" requirePermission=\"false\"/>\r" +
                    "\n  </configSections>\r\n  <appSettings></appSettings>\r\n  <system.web>\r\n    <compil" +
                    "ation debug=\"true\" targetFramework=\"4.6.2\"/>\r\n    <httpRuntime targetFramework=\"" +
                    "4.6.2\"/>\r\n  </system.web>\r\n  <system.webServer>\r\n    <handlers>\r\n      <remove n" +
                    "ame=\"ExtensionlessUrlHandler-Integrated-4.0\"/>\r\n      <remove name=\"OPTIONSVerbH" +
                    "andler\"/>\r\n      <remove name=\"TRACEVerbHandler\"/>\r\n      <add name=\"Extensionle" +
                    "ssUrlHandler-Integrated-4.0\" path=\"*.\" verb=\"*\" type=\"System.Web.Handlers.Transf" +
                    "erRequestHandler\"\r\n        preCondition=\"integratedMode,runtimeVersionv4.0\"/>\r\n " +
                    "   </handlers>\r\n  </system.webServer>\r\n  <runtime>\r\n    <assemblyBinding xmlns=\"" +
                    "urn:schemas-microsoft-com:asm.v1\">\r\n      <dependentAssembly>\r\n        <assembly" +
                    "Identity name=\"System.Web.Helpers\" publicKeyToken=\"31bf3856ad364e35\"/>\r\n        " +
                    "<bindingRedirect oldVersion=\"1.0.0.0-3.0.0.0\" newVersion=\"3.0.0.0\"/>\r\n      </de" +
                    "pendentAssembly>\r\n      <dependentAssembly>\r\n        <assemblyIdentity name=\"Sys" +
                    "tem.Web.Mvc\" publicKeyToken=\"31bf3856ad364e35\"/>\r\n        <bindingRedirect oldVe" +
                    "rsion=\"1.0.0.0-5.2.3.0\" newVersion=\"5.2.3.0\"/>\r\n      </dependentAssembly>\r\n    " +
                    "  <dependentAssembly>\r\n        <assemblyIdentity name=\"System.Web.WebPages\" publ" +
                    "icKeyToken=\"31bf3856ad364e35\"/>\r\n        <bindingRedirect oldVersion=\"1.0.0.0-3." +
                    "0.0.0\" newVersion=\"3.0.0.0\"/>\r\n      </dependentAssembly>\r\n\t        <assemblyBin" +
                    "ding xmlns=\"urn:schemas-microsoft-com:asm.v1\">\r\n        <dependentAssembly>\r\n   " +
                    "       <assemblyIdentity name=\"Newtonsoft.Json\" publicKeyToken=\"30AD4FE6B2A6AEED" +
                    "\" culture=\"neutral\"/>\r\n          <bindingRedirect oldVersion=\"0.0.0.0-6.0.0.0\" n" +
                    "ewVersion=\"6.0.0.0\"/>\r\n        </dependentAssembly>\r\n      </assemblyBinding>\r\n " +
                    "   </assemblyBinding>\r\n  </runtime>\r\n  <system.codedom>\r\n    <compilers>\r\n      " +
                    "<compiler language=\"c#;cs;csharp\" extension=\".cs\"\r\n        type=\"Microsoft.CodeD" +
                    "om.Providers.DotNetCompilerPlatform.CSharpCodeProvider, Microsoft.CodeDom.Provid" +
                    "ers.DotNetCompilerPlatform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31b" +
                    "f3856ad364e35\"\r\n        warningLevel=\"4\" compilerOptions=\"/langversion:6 /nowarn" +
                    ":1659;1699;1701\"/>\r\n      <compiler language=\"vb;vbs;visualbasic;vbscript\" exten" +
                    "sion=\".vb\"\r\n        type=\"Microsoft.CodeDom.Providers.DotNetCompilerPlatform.VBC" +
                    "odeProvider, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=1.0.0.0" +
                    ", Culture=neutral, PublicKeyToken=31bf3856ad364e35\"\r\n        warningLevel=\"4\" co" +
                    "mpilerOptions=\"/langversion:14 /nowarn:41008 /define:_MYTYPE=\\&quot;Web\\&quot; /" +
                    "optionInfer+\"/>\r\n    </compilers>\r\n  </system.codedom>\r\n   <entityFramework>\r\n  " +
                    "  <defaultConnectionFactory type=\"System.Data.Entity.Infrastructure.LocalDbConne" +
                    "ctionFactory, EntityFramework\">\r\n      <parameters>\r\n        <parameter value=\"m" +
                    "ssqllocaldb\"/>\r\n      </parameters>\r\n    </defaultConnectionFactory>\r\n    <provi" +
                    "ders>\r\n      <provider invariantName=\"System.Data.SqlClient\" type=\"System.Data.E" +
                    "ntity.SqlServer.SqlProviderServices, EntityFramework.SqlServer\"/>\r\n    </provide" +
                    "rs>\r\n  </entityFramework>\r\n</configuration>\r\n");

#line default
#line hidden
            return this.GenerationEnvironment.ToString();
        }

        public virtual void Initialize()
        {
        }
    }

    public class WebConfigTemplateBase
    {

        private global::System.Text.StringBuilder builder;

        private global::System.Collections.Generic.IDictionary<string, object> session;

        private global::System.CodeDom.Compiler.CompilerErrorCollection errors;

        private string currentIndent = string.Empty;

        private global::System.Collections.Generic.Stack<int> indents;

        private ToStringInstanceHelper _toStringHelper = new ToStringInstanceHelper();

        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.session;
            }
            set
            {
                this.session = value;
            }
        }

        public global::System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.builder == null))
                {
                    this.builder = new global::System.Text.StringBuilder();
                }
                return this.builder;
            }
            set
            {
                this.builder = value;
            }
        }

        protected global::System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errors == null))
                {
                    this.errors = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errors;
            }
        }

        public string CurrentIndent
        {
            get
            {
                return this.currentIndent;
            }
        }

        private global::System.Collections.Generic.Stack<int> Indents
        {
            get
            {
                if ((this.indents == null))
                {
                    this.indents = new global::System.Collections.Generic.Stack<int>();
                }
                return this.indents;
            }
        }

        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this._toStringHelper;
            }
        }

        public void Error(string message)
        {
            this.Errors.Add(new global::System.CodeDom.Compiler.CompilerError(null, -1, -1, null, message));
        }

        public void Warning(string message)
        {
            global::System.CodeDom.Compiler.CompilerError val = new global::System.CodeDom.Compiler.CompilerError(null, -1, -1, null, message);
            val.IsWarning = true;
            this.Errors.Add(val);
        }

        public string PopIndent()
        {
            if ((this.Indents.Count == 0))
            {
                return string.Empty;
            }
            int lastPos = (this.currentIndent.Length - this.Indents.Pop());
            string last = this.currentIndent.Substring(lastPos);
            this.currentIndent = this.currentIndent.Substring(0, lastPos);
            return last;
        }

        public void PushIndent(string indent)
        {
            this.Indents.Push(indent.Length);
            this.currentIndent = (this.currentIndent + indent);
        }

        public void ClearIndent()
        {
            this.currentIndent = string.Empty;
            this.Indents.Clear();
        }

        public void Write(string textToAppend)
        {
            this.GenerationEnvironment.Append(textToAppend);
        }

        public void Write(string format, params object[] args)
        {
            this.GenerationEnvironment.AppendFormat(format, args);
        }

        public void WriteLine(string textToAppend)
        {
            this.GenerationEnvironment.Append(this.currentIndent);
            this.GenerationEnvironment.AppendLine(textToAppend);
        }

        public void WriteLine(string format, params object[] args)
        {
            this.GenerationEnvironment.Append(this.currentIndent);
            this.GenerationEnvironment.AppendFormat(format, args);
            this.GenerationEnvironment.AppendLine();
        }

        public class ToStringInstanceHelper
        {

            private global::System.IFormatProvider formatProvider = global::System.Globalization.CultureInfo.InvariantCulture;

            public global::System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProvider;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProvider = value;
                    }
                }
            }

            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                global::System.Type type = objectToConvert.GetType();
                global::System.Type iConvertibleType = typeof(global::System.IConvertible);
                if (iConvertibleType.IsAssignableFrom(type))
                {
                    return ((global::System.IConvertible)(objectToConvert)).ToString(this.formatProvider);
                }
                global::System.Reflection.MethodInfo methInfo = type.GetMethod("ToString", new global::System.Type[] {
                        iConvertibleType});
                if ((methInfo != null))
                {
                    return ((string)(methInfo.Invoke(objectToConvert, new object[] {
                            this.formatProvider})));
                }
                return objectToConvert.ToString();
            }
        }
    }
}