using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubstaffReportGenerator
{
    public class Constants
    {
        public static string HubstaffApiRoot => ConfigurationManager.AppSettings["HubstaffApiRoot"];
        public static string HtmlTemplate => Resource.HtmlTemplate;
        public static string PdfConverterApp => ConfigurationManager.AppSettings["PdfConverterApp"];
        public static string SevenZipApp => ConfigurationManager.AppSettings["SevenZipApp"];
        public static string ArchivePassword => ConfigurationManager.AppSettings["ArchivePassword"];
        public static string HeadHtmlTemplate => Resource.THeadTemplate;
        public static string BodyHtmlTemplate => Resource.TBodyTemplate;
        private static string RootFolderName => ConfigurationManager.AppSettings["RootFolderName"];
        public static string OutputRoot => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), RootFolderName);
        public static string RateDataFilePath => ConfigurationManager.AppSettings["DataLocation"];
        public static string DecryptionKeyLocation => ConfigurationManager.AppSettings["DecryptionKeyLocation"];
        public static string HubStaffCredentialsFile => ConfigurationManager.AppSettings["HubstaffCredentialsFilePath"];
        public static string HubStaffAuthTokensFilePath => ConfigurationManager.AppSettings["HubStaffAuthTokensFilePath"];
        public static string SmtpHost => ConfigurationManager.AppSettings["SmtpHost"];
        public static string SmtpUser => ConfigurationManager.AppSettings["SmtpUser"];
        public static string SmtpSender => ConfigurationManager.AppSettings["SmtpSender"];
        public static string SmtpPassword => ConfigurationManager.AppSettings["SmtpPassword"];
        public static int SmtpPort => int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
        public static string[] EmailReceipients => ConfigurationManager.AppSettings["EmailRecipients"].Split(',');

    }
}
