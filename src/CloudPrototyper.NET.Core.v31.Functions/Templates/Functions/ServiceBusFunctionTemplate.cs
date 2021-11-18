//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:6.0.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CloudPrototyper.NET.Core.v31.Functions.Templates.Functions {
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    
    public partial class ServiceBusFunctionTemplate : ServiceBusFunctionTemplateBase {
        
        
        private CloudPrototyper.NET.Core.v31.Functions.Generators.Functions.ServiceBusFunctionGenerator _ModelField;
        
        public CloudPrototyper.NET.Core.v31.Functions.Generators.Functions.ServiceBusFunctionGenerator Model {
            get {
                return this._ModelField;
            }
        }

        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 7 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write("using System;\r\nusing Microsoft.Azure.ServiceBus;\r\nusing Microsoft.Azure.WebJobs;\r" +
                    "\nusing System.Collections.Generic;\r\n\r\nnamespace ");
            
            #line default
            #line hidden
            
            #line 12 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.Namespace ));
            
            #line default
            #line hidden
            
            #line 12 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write("\r\n{\r\n    public class ");
            
            #line default
            #line hidden
            
            #line 14 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.Name ));
            
            #line default
            #line hidden
            
            #line 14 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write("\r\n    {\r\n");
            
            #line default
            #line hidden
            
            #line 16 "Templates\Functions\ServiceBusFunctionTemplate.tt"
 foreach(var action in Model.Actions) { 
            
            #line default
            #line hidden
            
            #line 17 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write("\t\tpublic ");
            
            #line default
            #line hidden
            
            #line 17 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( action.Namespace ));
            
            #line default
            #line hidden
            
            #line 17 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(".");
            
            #line default
            #line hidden
            
            #line 17 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( action.Name ));
            
            #line default
            #line hidden
            
            #line 17 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 17 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( action.Name ));
            
            #line default
            #line hidden
            
            #line 17 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(" { get; set; }\r\n");
            
            #line default
            #line hidden
            
            #line 18 "Templates\Functions\ServiceBusFunctionTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 19 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write("\r\n        public ");
            
            #line default
            #line hidden
            
            #line 20 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.Name ));
            
            #line default
            #line hidden
            
            #line 20 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write("(\r\n");
            
            #line default
            #line hidden
            
            #line 21 "Templates\Functions\ServiceBusFunctionTemplate.tt"
 foreach(var action in Model.Actions) {  if(Model.Actions.Last().Equals(action)) {
            
            #line default
            #line hidden
            
            #line 22 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write("\t\t\t");
            
            #line default
            #line hidden
            
            #line 22 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( action.Namespace ));
            
            #line default
            #line hidden
            
            #line 22 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(".");
            
            #line default
            #line hidden
            
            #line 22 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( action.Name ));
            
            #line default
            #line hidden
            
            #line 22 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 22 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( action.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 22 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write("\r\n");
            
            #line default
            #line hidden
            
            #line 23 "Templates\Functions\ServiceBusFunctionTemplate.tt"
 } else { 
            
            #line default
            #line hidden
            
            #line 24 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write("\t\t\t");
            
            #line default
            #line hidden
            
            #line 24 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( action.Namespace ));
            
            #line default
            #line hidden
            
            #line 24 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(".");
            
            #line default
            #line hidden
            
            #line 24 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( action.Name ));
            
            #line default
            #line hidden
            
            #line 24 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 24 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( action.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 24 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(",\r\n");
            
            #line default
            #line hidden
            
            #line 25 "Templates\Functions\ServiceBusFunctionTemplate.tt"
 }} 
            
            #line default
            #line hidden
            
            #line 26 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write("\t\t) \r\n\t\t{\r\n");
            
            #line default
            #line hidden
            
            #line 28 "Templates\Functions\ServiceBusFunctionTemplate.tt"
 foreach(var action in Model.Actions) { 
            
            #line default
            #line hidden
            
            #line 29 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write("\t\t\tthis.");
            
            #line default
            #line hidden
            
            #line 29 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( action.Name ));
            
            #line default
            #line hidden
            
            #line 29 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(" = ");
            
            #line default
            #line hidden
            
            #line 29 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( action.Name.ToLower() ));
            
            #line default
            #line hidden
            
            #line 29 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(";\r\n");
            
            #line default
            #line hidden
            
            #line 30 "Templates\Functions\ServiceBusFunctionTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 31 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write("        }\r\n\r\n        [FunctionName(\"");
            
            #line default
            #line hidden
            
            #line 33 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.Name ));
            
            #line default
            #line hidden
            
            #line 33 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write("\")]\r\n\t\tpublic void Run([ServiceBusTrigger(\"");
            
            #line default
            #line hidden
            
            #line 34 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.AzureServiceBusQueue.Name ));
            
            #line default
            #line hidden
            
            #line 34 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write("\", Connection = \"");
            
            #line default
            #line hidden
            
            #line 34 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.AzureServiceBusQueue.Name ));
            
            #line default
            #line hidden
            
            #line 34 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write("Connection\")]Message message)\r\n        {\t\t\r\n            var output = new List<str" +
                    "ing>();\r\n            \r\n");
            
            #line default
            #line hidden
            
            #line 38 "Templates\Functions\ServiceBusFunctionTemplate.tt"
 foreach(var action in Model.ModelParameters) { 
            
            #line default
            #line hidden
            
            #line 39 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write("\t\t\tif(message.Label == \"");
            
            #line default
            #line hidden
            
            #line 39 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( (action.Trigger as CloudPrototyper.Model.Applications.MessageReceivedTrigger).MessageType ));
            
            #line default
            #line hidden
            
            #line 39 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write("\")\r\n\t\t\t{\r\n\t\t\t\t ");
            
            #line default
            #line hidden
            
            #line 41 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.Actions.FirstOrDefault(x=>x.Key.Equals(action.Name)).Name ));
            
            #line default
            #line hidden
            
            #line 41 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write(".Execute(output);\r\n\t\t\t} \r\n\r\n");
            
            #line default
            #line hidden
            
            #line 44 "Templates\Functions\ServiceBusFunctionTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 45 "Templates\Functions\ServiceBusFunctionTemplate.tt"
            this.Write("            \r\n        }\r\n    }\r\n}\r\n");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
            if ((this.Errors.HasErrors == false)) {
                if (((this.Session != null) 
                            && this.Session.ContainsKey("Model"))) {
                    object data = this.Session["Model"];
                    if (typeof(CloudPrototyper.NET.Core.v31.Functions.Generators.Functions.ServiceBusFunctionGenerator).IsAssignableFrom(data.GetType())) {
                        this._ModelField = ((CloudPrototyper.NET.Core.v31.Functions.Generators.Functions.ServiceBusFunctionGenerator)(data));
                    }
                    else {
                        this.Error("The type \'CloudPrototyper.NET.Core.v31.Functions.Generators.Functions.ServiceBusF" +
                                "unctionGenerator\' of the parameter \'Model\' did not match the type passed to the " +
                                "template");
                    }
                }
            }

        }
    }
    
    public class ServiceBusFunctionTemplateBase {
        
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
