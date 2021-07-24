using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using CloudPrototyper.Interface.Build;
using CloudPrototyper.Interface.Constants;

namespace CloudPrototyper.Build.NET
{
    /// <summary>
    /// Implementation of IBuilder for DotNet46 platform.
    /// </summary>
    public class DotNetBuilder : BuilderBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public DotNetBuilder() : base(new List<string> { "DotNet46"})
        {
        }

        /// <summary>
        /// DotNetBuilder builds .NET solution using nuget.exe to restore NuGets and
        /// Microsoft.Build library to build.
        /// </summary>
        /// <param name="configProvider">Provides configuration</param>
        /// <param name="buildable">Represents buildable .NET solution</param>
        public override void Build(IConfigProvider configProvider, IBuildable buildable)
        {
            RestoreNugets(configProvider,buildable);
            MakeBuild(configProvider, buildable);
        }

        /// <summary>
        /// Set up parametrs of build and make build, NuGets must be RESTORED!
        /// </summary>
        /// <param name="configProvider">Provides configuration</param>
        /// <param name="buildable">Represents buildable .NET solution</param>
        private void MakeBuild(IConfigProvider configProvider, IBuildable buildable)
        {
            try
            {
                var isDone = false;
                var solutionFile = Directory.GetFiles(buildable.SolutionRootPath, "*.sln", SearchOption.AllDirectories).First();
                Process process = new();

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "dotnet.exe",
                    Arguments = "build " + solutionFile + " /p:OutputPath=" + buildable.OutputBuildPath + "\\build /flp:v=d;logfile=" + buildable.OutputBuildPath + "\\build.log",
                    UseShellExecute = false,
                };

                process.EnableRaisingEvents = true;
                process.StartInfo = startInfo;

                process.OutputDataReceived += (sender, args) =>
                {
                    Console.WriteLine(args.ToString());
                };
                process.Exited += (sender, args) =>
                {
                    isDone = true;

                    if (process.ExitCode != 0)
                    {
                        process.Dispose();
                        throw new ArgumentException("dotnet build process failed");
                    }

                    process.Dispose();

                };
                process.Start();

                while (!isDone)
                {
                    Thread.Sleep(100);
                }              
            }
            catch (Exception e)
            {
                throw new ArgumentException("Provided buildable is not valid.", e);
            }
        }

        /// <summary>
        /// Opens nuget.exe with solution file as a parameter and
        /// </summary>
        /// <param name="configProvider">Provides configuration</param>
        /// <param name="buildable">Represents buildable .NET solution</param>
        private void RestoreNugets(IConfigProvider configProvider, IBuildable buildable)
        {

            try
            {
                var isDone = false;
                var solutionFile = Directory.GetFiles(buildable.SolutionRootPath, "*.sln", SearchOption.AllDirectories).First();
                Process process = new();

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "dotnet.exe",
                    Arguments = "restore " + solutionFile,
                    UseShellExecute = false,
                };

                process.EnableRaisingEvents = true;
                process.StartInfo = startInfo;

                process.OutputDataReceived += (sender, args) =>
                {
                    Console.WriteLine(args.ToString());
                };
                process.Exited += (sender, args) =>
                {
                    isDone = true;

                    if (process.ExitCode != 0)
                    {
                        process.Dispose();
                        throw new ArgumentException("dotnet restore process failed");
                    }

                    process.Dispose();

                };
                process.Start();

                while (!isDone)
                {
                    Thread.Sleep(100);
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Provided buildable is not valid.", e);
            }
        }
    }
}
