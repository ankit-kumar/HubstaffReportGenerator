using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using HtmlAgilityPack;
using HubstaffReportGenerator.Models;
using Newtonsoft.Json;

namespace HubstaffReportGenerator.Helper
{
    public class JsonToHtml
    {
        public static string GenerateHtmlReport(string jsonFilePath, DateTime fromDate, DateTime toDate, string outputFolder)
        {
            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException("File not found.", jsonFilePath);
            }
            string jsonString = File.ReadAllText(jsonFilePath);
            ReportResponse reportResponse = JsonConvert.DeserializeObject<ReportResponse>(jsonString);

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(Constants.HtmlTemplate);
            HtmlNode rootNode = document.DocumentNode;
            HtmlNode bodyNode = document.DocumentNode.SelectSingleNode("//body/div");

            HtmlNode dateRangeNode = HtmlNode.CreateNode($"<p class=\"text-left\">{fromDate.ToString("ddd, MMM dd yyyy")} - {toDate.ToString("ddd, MMM dd yyyy")}</p>");
            HtmlNode documentTitleNode = HtmlNode.CreateNode($"<h3 class=\"text-center\">Work Report</h3>");
            bodyNode.AppendChild(dateRangeNode);
            bodyNode.AppendChild(documentTitleNode);

            HtmlNode theadNode = HtmlNode.CreateNode(Constants.HeadHtmlTemplate);
            foreach (var org in reportResponse.Organizations)
            {
                HtmlNode orgNode = HtmlNode.CreateNode($"<h2>{org.Name}</h2>");
                bodyNode.AppendChild(orgNode);
                foreach (var project in org.Projects)
                {
                    HtmlNode tableNode = HtmlNode.CreateNode($"<table class=\"table table-striped\"></table>");
                    tableNode.AppendChild(theadNode);
                    HtmlNode projectNode = HtmlNode.CreateNode($"<h4>{project.Name}</h4>");
                    bodyNode.AppendChild(projectNode);

                    HtmlNode tbodyNode = HtmlNode.CreateNode("<tbody></tbody>");
                    foreach (Date date in project.Dates)
                    {
                        StringBuilder tRow = new StringBuilder(Constants.BodyHtmlTemplate);
                        //DateTime d = DateTime.Fro;
                        tRow.Replace("{{Date}}", date.date); //TODO
                        tRow.Replace("{{Member}}", date.Users[0].Name);
                        tRow.Replace("{{Project Hours}}", GetTimeString(date.Duration));
                        string tsks = null;
                        if (date.Users[0].Tasks != null)
                        {
                            var tasksArray = date.Users[0].Tasks.Select(t => t.Summary).ToArray();
                            tsks = string.Join("\r\n", tasksArray);
                        }
                        tRow.Replace("{{Tasks}}", tsks);
                        tRow.Replace("{{Task Hours}}", GetTimeString(date.TasksDuration));
                        tRow.Replace("{{Activity}}", $"{date.ActivityPercent}%");
                        string notes = null;
                        if (date.Users[0].Notes != null)
                        {
                            var notesArray = date.Users[0].Notes.Select(t => t.Description).ToArray();
                            notes = string.Join("\r\n", notesArray);
                        }
                        tRow.Replace("{{Notes}}", notes); //TODO
                        tbodyNode.AppendChild(HtmlNode.CreateNode(tRow.ToString()));
                    }
                    StringBuilder totalRow = new StringBuilder(Constants.BodyHtmlTemplate);
                    totalRow.Replace("{{Date}}", ""); //TODO
                    totalRow.Replace("{{Member}}", "Total");
                    totalRow.Replace("{{Project Hours}}", GetTimeString(project.Duration));
                    totalRow.Replace("{{Tasks}}", "");
                    totalRow.Replace("{{Task Hours}}", GetTimeString(project.TasksDuration));
                    totalRow.Replace("{{Activity}}", $"{project.ActivityPercent}%");
                    totalRow.Replace("{{Notes}}", "");
                    tbodyNode.AppendChild(HtmlNode.CreateNode(totalRow.ToString()));
                    tableNode.AppendChild(tbodyNode);
                    bodyNode.AppendChild(tableNode);

                }
            }//foreach org end

            try
            {
                XElement xElement = HtmlToXElement(document.DocumentNode.InnerHtml);
                var formattedOutput = xElement.ToString();
                document.DocumentNode.InnerHtml = formattedOutput;
            }
            catch (Exception ex)
            {
                // isn't well-formed xml
            }
            string outputFilePath = Path.Combine(outputFolder, $"{Path.GetFileNameWithoutExtension(jsonFilePath)}.html");
            document.Save(outputFilePath);
            return outputFilePath;
            //Process.Start(outputFilePath);

        }
        
        private static string GetTimeString(long seconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            return String.Format($"{(int)t.TotalHours:D2}:{t.Minutes:D2}:{t.Seconds:D2}");
        }

        private static XElement HtmlToXElement(string html)
        {
            if (html == null)
                throw new ArgumentNullException("html");

            HtmlDocument doc = new HtmlDocument();
            doc.OptionOutputAsXml = true;
            doc.LoadHtml(html);
            using (StringWriter writer = new StringWriter())
            {
                doc.Save(writer);
                using (StringReader reader = new StringReader(writer.ToString()))
                {
                    return XElement.Load(reader);
                }
            }
        }
    }
}
