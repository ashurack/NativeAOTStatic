using Microsoft.Build.Logging.StructuredLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MSBuildExtractLinkNative
{
    internal class LinkNativeCompileCommand
    {
        [JsonPropertyName("project_name")]
        public string? ProjectName { private set; get; }
        [JsonPropertyName("project_path")]
        public string? ProjectPath { private set; get; }
        [JsonPropertyName("original_build_command")]
        public string? OriginalBuildCommand { private set; get; }
        [JsonPropertyName("static_build_command")]
        public string? StaticBuildCommand { get { return GetStaticBuildCommand(); } }
        [JsonPropertyName("output_file")]
        public string? OutputFile { private set; get; }

        internal LinkNativeCompileCommand() { }

        internal LinkNativeCompileCommand(Target target) 
        {
            this.ProjectName = target.Project.Name;
            this.ProjectPath = new FileInfo(target.Project.SourceFilePath)?.DirectoryName;

            target.VisitAllChildren<Microsoft.Build.Logging.StructuredLogger.Task>(task =>
            {
                if (task.Name != "Exec" || String.IsNullOrEmpty(task.CommandLineArguments) || !task.CommandLineArguments.Contains("clang"))
                    return;

                this.OriginalBuildCommand = task.CommandLineArguments;
                this.OutputFile = Regex.Match(this.OriginalBuildCommand, "-o \"(?<output>[^\"]+)").Groups["output"].Value;
            });
        }

        private string GetStaticBuildCommand()
        {
            if (String.IsNullOrWhiteSpace(this.OriginalBuildCommand))
                throw new Exception("OriginalBuildCommand IsNullOrWhiteSpace");

            // THIS IS A HACK JOB
            // TODO: Parse the command and do this cleanly
            return this.OriginalBuildCommand
                .Replace("\"clang\" ", "\"gcc\" ")
                .Replace(" -static-pie ", " -static ")
                .Replace("-fuse-ld=bfd", " ")
                .Replace("-Wl,-pie", "")
                .Replace("-pie", "") + " -static";
        }
    }
}
