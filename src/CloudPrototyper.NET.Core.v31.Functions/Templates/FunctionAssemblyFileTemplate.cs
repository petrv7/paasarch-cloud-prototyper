//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:6.0.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CloudPrototyper.NET.Core.v31.Functions.Templates {
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System.IO;
    using System;
    
    
    public partial class FunctionAssemblyFileTemplate : FunctionAssemblyFileTemplateBase {
        
        
        private CloudPrototyper.NET.v6.Functions.Generators.FunctionAssemblyFileGenerator _ModelField;
        
        public CloudPrototyper.NET.v6.Functions.Generators.FunctionAssemblyFileGenerator Model {
            get {
                return this._ModelField;
            }
        }

        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 8 "Templates\FunctionAssemblyFileTemplate.tt"
            this.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Project Sdk=\"Microsoft.NET.Sdk\">  \r\n  <P" +
                    "ropertyGroup>\r\n    <TargetFramework>net6.0</TargetFramework>\r\n    <AzureFunction" +
                    "sVersion>v4</AzureFunctionsVersion>\r\n  </PropertyGroup>\r\n  <PropertyGroup Condit" +
                    "ion=\" \'$(Configuration)|$(Platform)\' == \'Debug|AnyCPU\' \">\r\n    <DebugSymbols>tru" +
                    "e</DebugSymbols>\r\n    <DebugType>full</DebugType>\r\n    <Optimize>false</Optimize" +
                    ">\r\n    <OutputPath>bin\\Debug\\</OutputPath>\r\n    <DefineConstants>DEBUG;TRACE</De" +
                    "fineConstants>\r\n    <ErrorReport>prompt</ErrorReport>\r\n    <WarningLevel>4</Warn" +
                    "ingLevel>\r\n    <AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkTo" +
                    "OutputPath>\r\n    <AppendRuntimeIdentifierToOutputPath>false</AppendRuntimeIdenti" +
                    "fierToOutputPath>\r\n  </PropertyGroup>\r\n  <PropertyGroup Condition=\" \'$(Configura" +
                    "tion)|$(Platform)\' == \'Release|AnyCPU\' \">\r\n    <DebugType>pdbonly</DebugType>\r\n " +
                    "   <Optimize>true</Optimize>\r\n    <OutputPath>bin\\Release\\</OutputPath>\r\n    <De" +
                    "fineConstants>TRACE</DefineConstants>\r\n    <ErrorReport>prompt</ErrorReport>\r\n  " +
                    "  <WarningLevel>4</WarningLevel>\r\n    <AppendTargetFrameworkToOutputPath>false</" +
                    "AppendTargetFrameworkToOutputPath>\r\n    <AppendRuntimeIdentifierToOutputPath>fal" +
                    "se</AppendRuntimeIdentifierToOutputPath>\r\n  </PropertyGroup>\r\n<ItemGroup>\r\n    <" +
                    "PackageReference Include=\"Microsoft.Azure.Functions.Extensions\" Version=\"1.1.0\" " +
                    "/>\r\n    <PackageReference Include=\"Microsoft.NET.Sdk.Functions\" Version=\"3.0.13\"" +
                    " />\r\n    <PackageReference Include=\"Microsoft.Azure.WebJobs.Script.ExtensionsMet" +
                    "adataGenerator\" Version=\"1.2.3\" />\r\n");
            
            #line default
            #line hidden
            
            #line 39 "Templates\FunctionAssemblyFileTemplate.tt"
 foreach(var reference in Model.AssemblyInfo.Packages) {
            
            #line default
            #line hidden
            
            #line 40 "Templates\FunctionAssemblyFileTemplate.tt"
            this.Write("    <PackageReference Include=\"");
            
            #line default
            #line hidden
            
            #line 40 "Templates\FunctionAssemblyFileTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( reference.Id ));
            
            #line default
            #line hidden
            
            #line 40 "Templates\FunctionAssemblyFileTemplate.tt"
            this.Write("\">\r\n      <Version>");
            
            #line default
            #line hidden
            
            #line 41 "Templates\FunctionAssemblyFileTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( reference.Version ));
            
            #line default
            #line hidden
            
            #line 41 "Templates\FunctionAssemblyFileTemplate.tt"
            this.Write("</Version>\r\n    </PackageReference>\t\r\n");
            
            #line default
            #line hidden
            
            #line 43 "Templates\FunctionAssemblyFileTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 44 "Templates\FunctionAssemblyFileTemplate.tt"
            this.Write("  </ItemGroup>\r\n   \r\n <ItemGroup>\r\n");
            
            #line default
            #line hidden
            
            #line 47 "Templates\FunctionAssemblyFileTemplate.tt"
 foreach(var import in Model.AssemblyInfo.AssemblyImports) {
            
            #line default
            #line hidden
            
            #line 48 "Templates\FunctionAssemblyFileTemplate.tt"
            this.Write("\t<ProjectReference Include=\"..\\\\");
            
            #line default
            #line hidden
            
            #line 48 "Templates\FunctionAssemblyFileTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Path.Combine(import.AssemblyInfo.ProjectFileRelativePath, import.AssemblyInfo.Name) ));
            
            #line default
            #line hidden
            
            #line 48 "Templates\FunctionAssemblyFileTemplate.tt"
            this.Write(".csproj\">\r\n     \r\n    </ProjectReference>\r\n");
            
            #line default
            #line hidden
            
            #line 51 "Templates\FunctionAssemblyFileTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 52 "Templates\FunctionAssemblyFileTemplate.tt"
            this.Write("  </ItemGroup>\r\n<ItemGroup>\r\n");
            
            #line default
            #line hidden
            
            #line 54 "Templates\FunctionAssemblyFileTemplate.tt"
 foreach(var include in Model.AssemblyInfo.IncludeOnlys) { 
            
            #line default
            #line hidden
            
            #line 55 "Templates\FunctionAssemblyFileTemplate.tt"
            this.Write("    <Content Include=\"");
            
            #line default
            #line hidden
            
            #line 55 "Templates\FunctionAssemblyFileTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( include ));
            
            #line default
            #line hidden
            
            #line 55 "Templates\FunctionAssemblyFileTemplate.tt"
            this.Write("\" />\r\n");
            
            #line default
            #line hidden
            
            #line 56 "Templates\FunctionAssemblyFileTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 57 "Templates\FunctionAssemblyFileTemplate.tt"
 foreach(var content in Model.AssemblyInfo.Contents) { 
            
            #line default
            #line hidden
            
            #line 58 "Templates\FunctionAssemblyFileTemplate.tt"
            this.Write("    <Content Include=\"..\\\\");
            
            #line default
            #line hidden
            
            #line 58 "Templates\FunctionAssemblyFileTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( content.OutputPath ));
            
            #line default
            #line hidden
            
            #line 58 "Templates\FunctionAssemblyFileTemplate.tt"
            this.Write("\">\r\n      <CopyToOutputDirectory>Always</CopyToOutputDirectory>\r\n    </Content>\r\n" +
                    "");
            
            #line default
            #line hidden
            
            #line 61 "Templates\FunctionAssemblyFileTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 62 "Templates\FunctionAssemblyFileTemplate.tt"
            this.Write(@"</ItemGroup>
<ItemGroup>
    <None Update=""host.json"">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
    <None Update=""local.settings.json"">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
      <CopyToPublishDirectory>Never</CopyToPublishDirectory>
    </None>
</ItemGroup>
</Project>
");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
            if ((this.Errors.HasErrors == false)) {
                if (((this.Session != null) 
                            && this.Session.ContainsKey("Model"))) {
                    object data = this.Session["Model"];
                    if (typeof(CloudPrototyper.NET.v6.Functions.Generators.FunctionAssemblyFileGenerator).IsAssignableFrom(data.GetType())) {
                        this._ModelField = ((CloudPrototyper.NET.v6.Functions.Generators.FunctionAssemblyFileGenerator)(data));
                    }
                    else {
                        this.Error("The type \'CloudPrototyper.NET.v6.Functions.Generators.FunctionAssemblyFileGenerat" +
                                "or\' of the parameter \'Model\' did not match the type passed to the template");
                    }
                }
            }

        }
    }
    
    public class FunctionAssemblyFileTemplateBase {
        
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
