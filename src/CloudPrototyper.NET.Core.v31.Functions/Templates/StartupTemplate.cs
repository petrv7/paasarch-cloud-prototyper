//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:5.0.10
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CloudPrototyper.NET.Core.v31.Functions.Templates {
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    
    public partial class StartupTemplate : StartupTemplateBase {
        
        
        private CloudPrototyper.NET.Core.v31.Functions.Generators.StartupGenerator _ModelField;
        
        public CloudPrototyper.NET.Core.v31.Functions.Generators.StartupGenerator Model {
            get {
                return this._ModelField;
            }
        }

        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 7 "Templates\StartupTemplate.tt"
            this.Write("using Microsoft.Azure.Functions.Extensions.DependencyInjection;\r\nusing Microsoft." +
                    "Extensions.DependencyInjection;\r\nusing System.Linq;\r\nusing System.Reflection;\r\n\r" +
                    "\n[assembly: FunctionsStartup(typeof(");
            
            #line default
            #line hidden
            
            #line 12 "Templates\StartupTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.Namespace ));
            
            #line default
            #line hidden
            
            #line 12 "Templates\StartupTemplate.tt"
            this.Write(" .Startup))]\r\n\r\n\r\nnamespace ");
            
            #line default
            #line hidden
            
            #line 15 "Templates\StartupTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.Namespace ));
            
            #line default
            #line hidden
            
            #line 15 "Templates\StartupTemplate.tt"
            this.Write(" \r\n{\r\n    public class Startup : FunctionsStartup\r\n    {\r\n        public override" +
                    " void Configure(IFunctionsHostBuilder builder)\r\n        {\r\n            var opAss" +
                    "embly = typeof(");
            
            #line default
            #line hidden
            
            #line 21 "Templates\StartupTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.OperationInterface.Namespace ));
            
            #line default
            #line hidden
            
            #line 21 "Templates\StartupTemplate.tt"
            this.Write(".");
            
            #line default
            #line hidden
            
            #line 21 "Templates\StartupTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.OperationInterface.Name ));
            
            #line default
            #line hidden
            
            #line 21 "Templates\StartupTemplate.tt"
            this.Write(").Assembly;\r\n            var actionAssembly = typeof(");
            
            #line default
            #line hidden
            
            #line 22 "Templates\StartupTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.ActionBase.Namespace ));
            
            #line default
            #line hidden
            
            #line 22 "Templates\StartupTemplate.tt"
            this.Write(".");
            
            #line default
            #line hidden
            
            #line 22 "Templates\StartupTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.ActionBase.Name ));
            
            #line default
            #line hidden
            
            #line 22 "Templates\StartupTemplate.tt"
            this.Write(").Assembly;\r\n            var busAssembly = typeof(");
            
            #line default
            #line hidden
            
            #line 23 "Templates\StartupTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.MessageBusInterface.Namespace ));
            
            #line default
            #line hidden
            
            #line 23 "Templates\StartupTemplate.tt"
            this.Write(".");
            
            #line default
            #line hidden
            
            #line 23 "Templates\StartupTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.MessageBusInterface.Name ));
            
            #line default
            #line hidden
            
            #line 23 "Templates\StartupTemplate.tt"
            this.Write(").Assembly;\r\n            var storageAssembly = typeof(");
            
            #line default
            #line hidden
            
            #line 24 "Templates\StartupTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.StorageInterface.Namespace ));
            
            #line default
            #line hidden
            
            #line 24 "Templates\StartupTemplate.tt"
            this.Write(".");
            
            #line default
            #line hidden
            
            #line 24 "Templates\StartupTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.StorageInterface.Name ));
            
            #line default
            #line hidden
            
            #line 24 "Templates\StartupTemplate.tt"
            this.Write(@").Assembly;
            var assemblies = new Assembly[] {opAssembly, actionAssembly, busAssembly, storageAssembly};
            var types = assemblies.SelectMany(a => a.GetTypes()).Where(t => !t.IsAbstract && !t.IsInterface).ToList();
            
            foreach (var type in types.Where(t => typeof(");
            
            #line default
            #line hidden
            
            #line 28 "Templates\StartupTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.OperationInterface.Namespace ));
            
            #line default
            #line hidden
            
            #line 28 "Templates\StartupTemplate.tt"
            this.Write(".");
            
            #line default
            #line hidden
            
            #line 28 "Templates\StartupTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.OperationInterface.Name ));
            
            #line default
            #line hidden
            
            #line 28 "Templates\StartupTemplate.tt"
            this.Write(").IsAssignableFrom(t)))\r\n            {\r\n                builder.Services.AddTrans" +
                    "ient(type, type);\r\n            }\r\n            foreach (var type in types.Where(t" +
                    " => typeof(");
            
            #line default
            #line hidden
            
            #line 32 "Templates\StartupTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.ActionBase.Namespace ));
            
            #line default
            #line hidden
            
            #line 32 "Templates\StartupTemplate.tt"
            this.Write(".");
            
            #line default
            #line hidden
            
            #line 32 "Templates\StartupTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.ActionBase.Name ));
            
            #line default
            #line hidden
            
            #line 32 "Templates\StartupTemplate.tt"
            this.Write(").IsAssignableFrom(t)))\r\n            {\r\n                builder.Services.AddTrans" +
                    "ient(type, type);\r\n            }\r\n            foreach (var type in types.Where(t" +
                    " => typeof(");
            
            #line default
            #line hidden
            
            #line 36 "Templates\StartupTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.MessageBusInterface.Namespace ));
            
            #line default
            #line hidden
            
            #line 36 "Templates\StartupTemplate.tt"
            this.Write(".");
            
            #line default
            #line hidden
            
            #line 36 "Templates\StartupTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.MessageBusInterface.Name ));
            
            #line default
            #line hidden
            
            #line 36 "Templates\StartupTemplate.tt"
            this.Write(").IsAssignableFrom(t)))\r\n            {\r\n                builder.Services.AddTrans" +
                    "ient(type, type);\r\n            }\r\n            foreach (var type in types.Where(t" +
                    " => typeof(");
            
            #line default
            #line hidden
            
            #line 40 "Templates\StartupTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.StorageInterface.Namespace ));
            
            #line default
            #line hidden
            
            #line 40 "Templates\StartupTemplate.tt"
            this.Write(".");
            
            #line default
            #line hidden
            
            #line 40 "Templates\StartupTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.StorageInterface.Name ));
            
            #line default
            #line hidden
            
            #line 40 "Templates\StartupTemplate.tt"
            this.Write(").IsAssignableFrom(t)))\r\n            {\r\n                builder.Services.AddTrans" +
                    "ient(type, type);\r\n            }           \r\n        }\r\n    }\r\n}\r\n");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
            if ((this.Errors.HasErrors == false)) {
                if (((this.Session != null) 
                            && this.Session.ContainsKey("Model"))) {
                    object data = this.Session["Model"];
                    if (typeof(CloudPrototyper.NET.Core.v31.Functions.Generators.StartupGenerator).IsAssignableFrom(data.GetType())) {
                        this._ModelField = ((CloudPrototyper.NET.Core.v31.Functions.Generators.StartupGenerator)(data));
                    }
                    else {
                        this.Error("The type \'CloudPrototyper.NET.Core.v31.Functions.Generators.StartupGenerator\' of " +
                                "the parameter \'Model\' did not match the type passed to the template");
                    }
                }
            }

        }
    }
    
    public class StartupTemplateBase {
        
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
