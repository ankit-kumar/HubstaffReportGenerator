using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HubstaffReportGenerator.Helper
{
    public static class Resources
    {
        private static ResourceManager _resourceManager = _resourceManager = new ResourceManager("HubstaffReportGenerator.Resource", Assembly.GetExecutingAssembly());
        public static string HtmlTemplate => _resourceManager.GetString("HtmlTemplate");
        public static string TBodyTemplate => _resourceManager.GetString("TBodyTemplate");
        public static string THeadTemplate => _resourceManager.GetString("THeadTemplate");
        public static string Data 
        {
            get
            {
                using (Stream stream = _resourceManager.GetStream("filename.txt"))
                {
                    StreamReader reader = new StreamReader(stream);
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
