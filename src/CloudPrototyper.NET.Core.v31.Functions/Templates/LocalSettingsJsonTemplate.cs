//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:5.0.7
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
    
    
    public partial class LocalSettingsJsonTemplate : LocalSettingsJsonTemplateBase {
        
        
        private CloudPrototyper.NET.Core.v31.Functions.Generators.LocalSettingsJsonGenerator _ModelField;
        
        public CloudPrototyper.NET.Core.v31.Functions.Generators.LocalSettingsJsonGenerator Model {
            get {
                return this._ModelField;
            }
        }

        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 7 "Templates\LocalSettingsJsonTemplate.tt"
            this.Write("{\r\n    \"IsEncrypted\": false,\r\n    \"Values\": {\r\n        \"AzureWebJobsStorage\": \"Us" +
                    "eDevelopmentStorage=true\",\r\n        \"FUNCTIONS_WORKER_RUNTIME\": \"dotnet\"\r\n");
            
            #line default
            #line hidden
            
            #line 12 "Templates\LocalSettingsJsonTemplate.tt"
 foreach(var queue in Model.Queues) { 
            
            #line default
            #line hidden
            
            #line 13 "Templates\LocalSettingsJsonTemplate.tt"
            this.Write("\t\t,\"");
            
            #line default
            #line hidden
            
            #line 13 "Templates\LocalSettingsJsonTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( queue.Name ));
            
            #line default
            #line hidden
            
            #line 13 "Templates\LocalSettingsJsonTemplate.tt"
            this.Write("Connection\" : \"");
            
            #line default
            #line hidden
            
            #line 13 "Templates\LocalSettingsJsonTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( queue.ConnectionString ));
            
            #line default
            #line hidden
            
            #line 13 "Templates\LocalSettingsJsonTemplate.tt"
            this.Write("\"\r\n");
            
            #line default
            #line hidden
            
            #line 14 "Templates\LocalSettingsJsonTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 15 "Templates\LocalSettingsJsonTemplate.tt"
            this.Write("    }\r\n}");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
            if ((this.Errors.HasErrors == false)) {
                if (((this.Session != null) 
                            && this.Session.ContainsKey("Model"))) {
                    object data = this.Session["Model"];
                    if (typeof(CloudPrototyper.NET.Core.v31.Functions.Generators.LocalSettingsJsonGenerator).IsAssignableFrom(data.GetType())) {
                        this._ModelField = ((CloudPrototyper.NET.Core.v31.Functions.Generators.LocalSettingsJsonGenerator)(data));
                    }
                    else {
                        this.Error("The type \'CloudPrototyper.NET.Core.v31.Functions.Generators.LocalSettingsJsonGene" +
                                "rator\' of the parameter \'Model\' did not match the type passed to the template");
                    }
                }
            }

        }
    }
    
    public class LocalSettingsJsonTemplateBase {
        
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
