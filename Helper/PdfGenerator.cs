using System.Diagnostics;
using System.IO;

namespace HubstaffReportGenerator.Helper
{
    public class PdfGenerator
    {
        public static string Generate(string htmlFilePath, string outputPath)
        {
            if (!File.Exists(htmlFilePath))
            {
                throw new FileNotFoundException("File not found", htmlFilePath);
            }
            string outputFilePath = Path.Combine(outputPath, $"{Path.GetFileNameWithoutExtension(htmlFilePath)}.pdf");
            Process.Start(Constants.PdfConverterApp, $"\"{htmlFilePath}\" \"{outputFilePath}\"");
            return outputFilePath;
        }
    }
}
