//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:5.0.7
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CloudPrototyper.NET.Framework.v462.Common.Templates.DataLayerTemplates.Entities {
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    
    public partial class EntityTemplate : EntityTemplateBase {
        
        
        private CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities.EntityGenerator _ModelField;
        
        public CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities.EntityGenerator Model {
            get {
                return this._ModelField;
            }
        }

        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 7 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write("using System;\r\nusing System.Collections.Generic;\r\nusing System.ComponentModel.Dat" +
                    "aAnnotations;\r\nusing System.ComponentModel.DataAnnotations.Schema;\r\nusing Micros" +
                    "oft.WindowsAzure.Storage.Table;\r\nusing Newtonsoft.Json;\r\n\r\n// Entity\r\nnamespace " +
                    "");
            
            #line default
            #line hidden
            
            #line 15 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.Namespace));
            
            #line default
            #line hidden
            
            #line 15 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write("\r\n{\r\n    public class ");
            
            #line default
            #line hidden
            
            #line 17 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.Name));
            
            #line default
            #line hidden
            
            #line 17 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write(" : TableEntity,");
            
            #line default
            #line hidden
            
            #line 17 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.EntityInterface.Namespace ));
            
            #line default
            #line hidden
            
            #line 17 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write(".");
            
            #line default
            #line hidden
            
            #line 17 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.EntityInterface.Name ));
            
            #line default
            #line hidden
            
            #line 17 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write(@"
    {
		//cosmos db id
		[JsonProperty(PropertyName = ""id"")]
		public string CosmosId { get; set; }

		private int _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get { return _id; } set { RowKey = value.ToString().PadLeft(9, '0'); PartitionKey = ""p""; _id = value; CosmosId = value.ToString().PadLeft(9, '0'); } }
");
            
            #line default
            #line hidden
            
            #line 27 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
foreach(var prop in Model.ModelParameters.Properties.Where(x=>x.Name != "Id")) {
            
            #line default
            #line hidden
            
            #line 28 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write("\t\tpublic ");
            
            #line default
            #line hidden
            
            #line 28 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
 if(prop.IsMany) { 
            
            #line default
            #line hidden
            
            #line 29 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write(" \r\n\t\tList<");
            
            #line default
            #line hidden
            
            #line 30 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( prop.Type ));
            
            #line default
            #line hidden
            
            #line 30 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write("> ");
            
            #line default
            #line hidden
            
            #line 30 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
 } else { 
            
            #line default
            #line hidden
            
            #line 31 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 31 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( prop.Type ));
            
            #line default
            #line hidden
            
            #line 31 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 31 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
 }
            
            #line default
            #line hidden
            
            #line 32 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 32 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( prop.Name ));
            
            #line default
            #line hidden
            
            #line 32 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write(" { get; set; }\r\n");
            
            #line default
            #line hidden
            
            #line 33 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
}
            
            #line default
            #line hidden
            
            #line 34 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write("\r\n\t\tpublic static int GetContentSize(string propertyName)\r\n\t\t{\r\n\t\t\ttry {\r\n\t\t\tDict" +
                    "ionary<string, int> sizeDict = new Dictionary<string, int>();\r\n");
            
            #line default
            #line hidden
            
            #line 39 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
foreach(var prop in Model.ModelParameters.Properties) {
            
            #line default
            #line hidden
            
            #line 40 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write("\t\t\tsizeDict.Add(\"");
            
            #line default
            #line hidden
            
            #line 40 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(prop.Name));
            
            #line default
            #line hidden
            
            #line 40 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write("\",");
            
            #line default
            #line hidden
            
            #line 40 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( prop.ContentSize ));
            
            #line default
            #line hidden
            
            #line 40 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write(");\r\n");
            
            #line default
            #line hidden
            
            #line 41 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
}
            
            #line default
            #line hidden
            
            #line 42 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write("\t\t\treturn sizeDict[propertyName];\r\n\t\t\t} \r\n\t\t\tcatch(Exception)\r\n\t\t\t{\r\n\t\t\t\treturn -" +
                    "1;\r\n\t\t\t}\r\n\t\t}\r\n\r\n\t\tpublic static int GetTotalContentSize()\r\n\t\t{\r\n\t\t\ttry \r\n\t\t\t{\r\n" +
                    "\t\t\t\tint size = 0;\r\n\r\n");
            
            #line default
            #line hidden
            
            #line 56 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
foreach(var prop in Model.ModelParameters.Properties) {
            
            #line default
            #line hidden
            
            #line 57 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write("\t\t\t\t//");
            
            #line default
            #line hidden
            
            #line 57 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(prop.Name));
            
            #line default
            #line hidden
            
            #line 57 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write("\r\n\t\t\t\tsize += ");
            
            #line default
            #line hidden
            
            #line 58 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( prop.ContentSize ));
            
            #line default
            #line hidden
            
            #line 58 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write(";\r\n\r\n");
            
            #line default
            #line hidden
            
            #line 60 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
}
            
            #line default
            #line hidden
            
            #line 61 "Templates\DataLayerTemplates\Entities\EntityTemplate.tt"
            this.Write("\t\t\t\treturn size;\r\n\t\t\t} \r\n\t\t\tcatch(Exception)\r\n\t\t\t{\r\n\t\t\t\treturn -1;\r\n\t\t\t}\r\n\t\t}\r\n\t}" +
                    "\r\n}");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
            if ((this.Errors.HasErrors == false)) {
                if (((this.Session != null) 
                            && this.Session.ContainsKey("Model"))) {
                    object data = this.Session["Model"];
                    if (typeof(CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities.EntityGenerator).IsAssignableFrom(data.GetType())) {
                        this._ModelField = ((CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities.EntityGenerator)(data));
                    }
                    else {
                        this.Error("The type \'CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerator" +
                                "s.Entities.EntityGenerator\' of the parameter \'Model\' did not match the type pass" +
                                "ed to the template");
                    }
                }
            }

        }
    }
    
    public class EntityTemplateBase {
        
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
