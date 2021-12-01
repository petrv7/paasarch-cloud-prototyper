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
    
    
    public partial class CosmosDbFunctionTemplate : CosmosDbFunctionTemplateBase {
        
        
        private CloudPrototyper.NET.v6.Functions.Generators.Functions.CosmosDbFunctionGenerator _ModelField;
        
        public CloudPrototyper.NET.v6.Functions.Generators.Functions.CosmosDbFunctionGenerator Model {
            get {
                return this._ModelField;
            }
        }

        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 7 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write("using System.Collections.Generic;\r\nusing Microsoft.Azure.Documents;\r\nusing Micros" +
                    "oft.Azure.WebJobs;\r\n\r\nnamespace ");
            
            #line default
            #line hidden
            
            #line 11 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.Namespace ));
            
            #line default
            #line hidden
            
            #line 11 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write("\r\n{\r\n    public class ");
            
            #line default
            #line hidden
            
            #line 13 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.Name ));
            
            #line default
            #line hidden
            
            #line 13 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write("\r\n    {\r\n        private readonly ");
            
            #line default
            #line hidden
            
            #line 15 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.Action.Namespace ));
            
            #line default
            #line hidden
            
            #line 15 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write(".");
            
            #line default
            #line hidden
            
            #line 15 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.Action.Name ));
            
            #line default
            #line hidden
            
            #line 15 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write(" _action;\r\n\r\n        public ");
            
            #line default
            #line hidden
            
            #line 17 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.Name ));
            
            #line default
            #line hidden
            
            #line 17 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write("(");
            
            #line default
            #line hidden
            
            #line 17 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.Action.Namespace ));
            
            #line default
            #line hidden
            
            #line 17 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write(".");
            
            #line default
            #line hidden
            
            #line 17 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.Action.Name ));
            
            #line default
            #line hidden
            
            #line 17 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write(" action)\r\n        {\r\n            _action = action;\r\n        }\r\n\r\n        [Functio" +
                    "nName(\"");
            
            #line default
            #line hidden
            
            #line 22 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.Action.Key ));
            
            #line default
            #line hidden
            
            #line 22 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write("\")]\r\n\t\tpublic void Run([CosmosDBTrigger(\r\n            databaseName: \"");
            
            #line default
            #line hidden
            
            #line 24 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.Container.DatabaseName ));
            
            #line default
            #line hidden
            
            #line 24 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write("\",\r\n            collectionName: \"");
            
            #line default
            #line hidden
            
            #line 25 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.Container.Name ));
            
            #line default
            #line hidden
            
            #line 25 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write("\",\r\n            ConnectionStringSetting = \"");
            
            #line default
            #line hidden
            
            #line 26 "Templates\Functions\CosmosDbFunctionTemplate.tt"
 if(Model.Container.IsServerless) {
            
            #line default
            #line hidden
            
            #line 27 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write("serverlessConnStr");
            
            #line default
            #line hidden
            
            #line 27 "Templates\Functions\CosmosDbFunctionTemplate.tt"
 } else { 
            
            #line default
            #line hidden
            
            #line 28 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write("ConnStr");
            
            #line default
            #line hidden
            
            #line 28 "Templates\Functions\CosmosDbFunctionTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 29 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write("\",\r\n            CreateLeaseCollectionIfNotExists = true,\r\n            LeaseCollec" +
                    "tionName = \"leases\",\r\n            LeaseCollectionPrefix = \"");
            
            #line default
            #line hidden
            
            #line 32 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.Action.Name ));
            
            #line default
            #line hidden
            
            #line 32 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write("\")]IReadOnlyList<Document> input)\r\n        {\r\n");
            
            #line default
            #line hidden
            
            #line 34 "Templates\Functions\CosmosDbFunctionTemplate.tt"
 if(!Model.Trigger.ProcessOncePerTrigger) {
            
            #line default
            #line hidden
            
            #line 35 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write("            foreach(var doc in input)\r\n            {\r\n                var output " +
                    "= new List<string>();\r\n                _action.Execute(output);\r\n            }\r\n" +
                    "");
            
            #line default
            #line hidden
            
            #line 40 "Templates\Functions\CosmosDbFunctionTemplate.tt"
 } else { 
            
            #line default
            #line hidden
            
            #line 41 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write("            var output = new List<string>();\r\n            _action.Execute(output)" +
                    ";\r\n");
            
            #line default
            #line hidden
            
            #line 43 "Templates\Functions\CosmosDbFunctionTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 44 "Templates\Functions\CosmosDbFunctionTemplate.tt"
            this.Write("        }\r\n    }\r\n}\r\n");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
            if ((this.Errors.HasErrors == false)) {
                if (((this.Session != null) 
                            && this.Session.ContainsKey("Model"))) {
                    object data = this.Session["Model"];
                    if (typeof(CloudPrototyper.NET.v6.Functions.Generators.Functions.CosmosDbFunctionGenerator).IsAssignableFrom(data.GetType())) {
                        this._ModelField = ((CloudPrototyper.NET.v6.Functions.Generators.Functions.CosmosDbFunctionGenerator)(data));
                    }
                    else {
                        this.Error("The type \'CloudPrototyper.NET.v6.Functions.Generators.Functions.CosmosDbFunctionG" +
                                "enerator\' of the parameter \'Model\' did not match the type passed to the template" +
                                "");
                    }
                }
            }

        }
    }
    
    public class CosmosDbFunctionTemplateBase {
        
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