using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubstaffReportGenerator.Helper
{
    public static class CompressionHelper
    {
        public static string Compress(string directoryName)
        {
            //7za a -p<Password> -tzip -mx9 invoice.zip D:\hubstaff\16_Oct_2016_to_31_Oct_2016_ptK9Di\16_Oct_2016_to_31_Oct_2016/
            string format = "zip";
            string outputFile = $"{PathHelper.FinalDirectoryInfo.FullName}_Invoice.{format}";
            string args = $"a -t{format} -p{Constants.ArchivePassword} -mx9 {outputFile} {directoryName}/";
            ProcessStartInfo p = new ProcessStartInfo
            {
                CreateNoWindow  = false,
                FileName = Constants.SevenZipApp,
                Arguments = args
            };            
            Process pc = Process.Start(p);
            pc.WaitForExit();
            if (pc.ExitCode == 0)            
                return outputFile;
            else
                return null;                      
        }
    }
}
