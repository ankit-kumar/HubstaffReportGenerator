using System;
using System.Diagnostics;
using System.IO;

namespace HubstaffReportGenerator.Helper
{
    public class PathHelper
    {
        private static string RootPath => Constants.OutputRoot;

        public static DirectoryInfo WorkingDirectoryInfo { get; private set; }
        public static DirectoryInfo JsonDirectoryInfo { get; private set; }
        public static DirectoryInfo HtmlDirectoryInfo { get; private set; }
        public static DirectoryInfo PdfDirectoryInfo { get; private set; }
        public static DirectoryInfo FinalDirectoryInfo { get; private set; }
        public static DirectoryInfo FinalReportsFolder { get; private set; }

        public static void  CreateFolderStructure(string fileName, string folderSuffix = null)
        {
            string suffix = string.IsNullOrEmpty(folderSuffix) ? "" : $"_{folderSuffix}";
            string directoryName = Path.Combine(RootPath, $"{fileName}{suffix}");
            WorkingDirectoryInfo = Directory.CreateDirectory(directoryName);
            JsonDirectoryInfo = Directory.CreateDirectory(Path.Combine(directoryName, "json"));
            HtmlDirectoryInfo = Directory.CreateDirectory(Path.Combine(directoryName, "html"));
            PdfDirectoryInfo  = Directory.CreateDirectory(Path.Combine(directoryName, "pdf"));
            FinalDirectoryInfo = Directory.CreateDirectory(Path.Combine(directoryName, fileName));
            FinalReportsFolder = Directory.CreateDirectory(Path.Combine(FinalDirectoryInfo.FullName, "Reports"));
        }
        public static void Init(string workingDirectory)
        {
            if (!Directory.Exists(workingDirectory))
            {
                throw new DirectoryNotFoundException($"Directory: {workingDirectory} not found.");
            }
            var directories = Directory.GetDirectories(workingDirectory);
            if (directories.Length != 4)
            {
                throw new Exception("Incorrect folder structure in working directory provided.");
            }
            foreach (var item in directories)
            {
                if (item.EndsWith("json", StringComparison.OrdinalIgnoreCase))
                    JsonDirectoryInfo = new DirectoryInfo(item);
                else if (item.EndsWith("html", StringComparison.OrdinalIgnoreCase))
                    HtmlDirectoryInfo = new DirectoryInfo(item);
                else if (item.EndsWith("pdf", StringComparison.OrdinalIgnoreCase))
                    PdfDirectoryInfo = new DirectoryInfo(item);
                else
                {
                    FinalDirectoryInfo = new DirectoryInfo(item);
                    if (Directory.Exists(FinalDirectoryInfo.FullName))
                    {
                        FinalReportsFolder = new DirectoryInfo(Path.Combine(FinalDirectoryInfo.FullName, "Reports"));
                    }
                }
                
            }
        }

        public static void  CopyFolder(string sourceFolder, string destinationFolder)
        {
            if (!Directory.Exists(sourceFolder))
            {
                throw new DirectoryNotFoundException($"Directory: {sourceFolder} does not exist.");
            }
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = true;
            string xCopyPath = @"C:\WINDOWS\system32\xcopy.exe";
            if (!File.Exists(xCopyPath))
            {
                throw new FileNotFoundException("XCopy.exe not found.");
            }
            proc.StartInfo.FileName = xCopyPath;
            //E copy all sub directories (even empty ones). /I create if destination doesn't exist
            proc.StartInfo.Arguments = $"{sourceFolder} {destinationFolder} /E /I"; 
            proc.Start();
        }
    }
}
