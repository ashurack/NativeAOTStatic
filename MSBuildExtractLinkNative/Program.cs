using Microsoft.Build.Logging.StructuredLogger;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MSBuildExtractLinkNative
{
    internal class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            var msbuild_log_path = @"C:\Users\ashurack\source\repos\btoolex\publish.binlog";
#else
            var msbuild_log_path = String.Join(' ', args);
#endif
            if (!File.Exists(msbuild_log_path))
            {
                Console.WriteLine($"Unable to find file {msbuild_log_path}");
                Environment.Exit(1);
            }

            var linkNativeCompileCommands = new List<LinkNativeCompileCommand>();
            var buildRoot = BinaryLog.ReadBuild(msbuild_log_path);
            buildRoot.VisitAllChildren<Target>(target =>
            {
                if (target.Name != "LinkNative" && target.Project.Name != "MSBuildExtractLinkNative")
                    return;

                var linkNativeCompileCommand = new LinkNativeCompileCommand(target);
                linkNativeCompileCommands.Add(linkNativeCompileCommand);
            });

            Console.WriteLine(JsonSerializer.Serialize(linkNativeCompileCommands));
        }
    }
}