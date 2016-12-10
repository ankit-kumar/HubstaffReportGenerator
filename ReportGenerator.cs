using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using HubstaffReportGenerator.Helper;
using HubstaffReportGenerator.Models;
using Newtonsoft.Json;
using RestSharp;
using ClosedXML.Excel;

namespace HubstaffReportGenerator
{
    public static class ReportGenerator
    {
        public static List<string> GeneratePdfs(string outputFolder, List<string> htmlFiles)
        {            
            List<string> pdfFiles = new List<string>();
            foreach (var htmlFile in htmlFiles)
            {
                string pdfFile = PdfGenerator.Generate(htmlFile, outputFolder);
                pdfFiles.Add(pdfFile);
            }            
            return pdfFiles;
        }
        public static List<string> PrepareReports(DateTime startDate, DateTime endDate, string outputFolder, List<string> jsonFiles)
        {
            
            List<string> htmlFiles = new List<string>();

            foreach (var jsonFile in jsonFiles)
            {
                string htmlFilePath = JsonToHtml.GenerateHtmlReport(jsonFile, startDate, endDate, outputFolder);
                htmlFiles.Add(htmlFilePath);
            }
            if (!Directory.Exists(PathHelper.HtmlDirectoryInfo.FullName))
            {
                throw new DirectoryNotFoundException("Html directory not found.");
            }
            return htmlFiles;
        }
        public static List<string> GetJson(DateTime startDate, DateTime endDate, string outputFolder, string separateReportFor = null)
        {
            List<string> jsonFiles = new List<string>();
            try
            {
                List<HubStaffCredential> hubStaffCredentials;
                using (TextReader textReader = File.OpenText(Constants.HubStaffCredentialsFile))
                {
                    CsvReader csv = new CsvReader(textReader);
                    csv.Configuration.Delimiter = ",";
                    csv.Configuration.HasHeaderRecord = false;
                    csv.Configuration.WillThrowOnMissingField = false;
                    hubStaffCredentials = csv.GetRecords<HubStaffCredential>().ToList();
                }

                if (!File.Exists(Constants.HubStaffAuthTokensFilePath))
                {
                    Console.WriteLine($"Getting authentication token for:");
                    List<string> authStrings = new List<string>();
                    foreach (var c in hubStaffCredentials)
                    {
                        Console.WriteLine($"Getting authentication token for:");
                        Console.WriteLine($"Name: {c.Name}");
                        Console.WriteLine($"Email: {c.Email}");
                        string authToken = JsonDownloader.GetAuthToken(c);
                        authStrings.Add($"{c.Name},{c.Email},{c.Password},{c.AppToken},{authToken}");
                        
                    }
                    //Create HubStaffAuthTokensFilePath.csv
                    File.WriteAllLines(Constants.HubStaffAuthTokensFilePath, authStrings);
                }
                List<HubStaffCredential> hubStaffAuthKeys;
                using (TextReader textReader = File.OpenText(Constants.HubStaffAuthTokensFilePath))
                {
                    CsvReader csv = new CsvReader(textReader);
                    csv.Configuration.Delimiter = ",";
                    csv.Configuration.HasHeaderRecord = false;
                    hubStaffAuthKeys = csv.GetRecords<HubStaffCredential>().ToList();
                }

                DataTable table1 = new DataTable();
                table1.Columns.Add(new DataColumn("From", typeof(DateTime)) { AllowDBNull = true });
                table1.Columns.Add(new DataColumn("To", typeof(DateTime)) { AllowDBNull = true });
                table1.Columns.Add(new DataColumn("Name", typeof(string)) { AllowDBNull = true });
                table1.Columns.Add(new DataColumn("Rate", typeof(decimal)) { AllowDBNull = true });
                table1.Columns.Add(new DataColumn("Hours", typeof(decimal)) { AllowDBNull = true });
                table1.Columns.Add(new DataColumn("DueAmount", typeof(decimal)) { AllowDBNull = false });
                DataTable table2 = null;
                if (!string.IsNullOrEmpty(separateReportFor))
                {
                    table2 = new DataTable();
                    table2.Columns.Add(new DataColumn("From", typeof(DateTime)) { AllowDBNull = true });
                    table2.Columns.Add(new DataColumn("To", typeof(DateTime)) { AllowDBNull = true });
                    table2.Columns.Add(new DataColumn("Name", typeof(string)) { AllowDBNull = true });
                    table2.Columns.Add(new DataColumn("Rate", typeof(decimal)) { AllowDBNull = true });
                    table2.Columns.Add(new DataColumn("Hours", typeof(decimal)) { AllowDBNull = true });
                    table2.Columns.Add(new DataColumn("DueAmount", typeof(decimal)) { AllowDBNull = false });
                }

                //Get Rates
                var rates = EncryptionHelper.GetRates();

                Console.WriteLine($"#####################################");
                Console.WriteLine($"Generated on: {DateTime.Now}");
                Console.WriteLine($" Hubstaff hourly  report from {startDate} to {endDate}");
                Console.WriteLine($"#####################################");
                foreach (var c in hubStaffAuthKeys)
                {

                    Console.WriteLine($"Name: {c.Name}");
                    Console.WriteLine($"Email: {c.Email}");
                    string jsonOutPath = Path.Combine(outputFolder, $"{c.Name}.json");
                    ReportResponse reportResponse;
                    JsonDownloader.GetReportJson(c.AuthToken, c.AppToken, startDate.ToString("dd_MMM_yyyy"), endDate.ToString("dd_MMM_yyyy"), jsonOutPath, out reportResponse);
                    jsonFiles.Add(jsonOutPath);
                    int seconds = 0;
                    Organization org = reportResponse.Organizations.First();
                    int projectCount = org.Projects.Count;
                    for (int i = 0; i < projectCount; i++)
                    {
                        Project proj = org.Projects[i];
                        Console.WriteLine($"Organization: {org.Name}");
                        Console.WriteLine($"Projects:");
                        Console.WriteLine($"Project: {proj.Name}, Duration: {proj.Duration}seconds or {Math.Round(proj.Duration / 3600.0M, 2)} hours.");

                        if (!string.IsNullOrEmpty(separateReportFor) && proj.Name.Equals(separateReportFor, StringComparison.OrdinalIgnoreCase))
                        {
                            Project project = proj;

                            //Create organization object
                            Organization orgnization = new Organization();
                            orgnization.Id = org.Id;
                            orgnization.Name = org.Name;
                            orgnization.Projects = new List<Project> { project };
                            orgnization.Duration = org.Duration;

                            //remove separately requested project
                            org.Projects.Remove(project);
                            //
                            ReportResponse singleProjectReport = new ReportResponse
                            {
                                Organizations = new List<Organization> { orgnization }
                            };
                            string jsonStringForSingleProject = JsonConvert.SerializeObject(singleProjectReport);

                            var fileName = Path.Combine(PathHelper.JsonDirectoryInfo.FullName, $"{c.Name}-{project.Name}.json");
                            File.WriteAllText(fileName, jsonStringForSingleProject);
                            jsonFiles.Add(fileName);

                            var durationForSeparateProject = Math.Round((proj.Duration / 3600.0M), 2);
                            table2?.Rows.Add(startDate, endDate, c.Name, rates[c.Name], durationForSeparateProject, rates[c.Name] * durationForSeparateProject);
                            Console.WriteLine($"{c.Name} {separateReportFor} Total Seconds: {seconds} Total Hours: {durationForSeparateProject}");
                        }
                        else
                        {
                            seconds += proj.Duration;
                            Console.WriteLine($"{c.Name}-{proj.Name} Total Seconds: {seconds} Total Hours: {proj.Duration}");
                        }                       
                    }
                    var duration = Math.Round((seconds / 3600.0M), 2);
                    table1.Rows.Add(startDate, endDate, c.Name, rates[c.Name], duration, rates[c.Name] * duration);
                    string jsonString = JsonConvert.SerializeObject(reportResponse);
                    File.WriteAllText(Path.Combine(PathHelper.JsonDirectoryInfo.FullName, $"{c.Name}.json"), jsonString);

                }
                if (!string.IsNullOrEmpty(separateReportFor))
                {
                    if (table2 != null)
                    {
                        decimal totalHours2 = decimal.Parse(table2.Compute("Sum(Hours)", "").ToString());
                        decimal totalDue2 = decimal.Parse(table2.Compute("Sum(DueAmount)", "").ToString());
                        table2.Rows.Add(DBNull.Value, DBNull.Value, "Total", DBNull.Value, Math.Round(totalHours2, 2), Math.Round(totalDue2, 2));
                    }
                    XLWorkbook workbook2 = new XLWorkbook();
                    workbook2.Worksheets.Add(table2, "Sheet1");
                    workbook2.SaveAs(Path.Combine(PathHelper.FinalDirectoryInfo.FullName, $"Invoice-{separateReportFor}.xlsx"));
                }

                decimal totalHours1 = decimal.Parse(table1.Compute("Sum(Hours)", "").ToString());
                decimal totalDue1 = decimal.Parse(table1.Compute("Sum(DueAmount)", "").ToString());
                table1.Rows.Add(DBNull.Value, DBNull.Value, "Total", DBNull.Value, Math.Round(totalHours1, 2), Math.Round(totalDue1, 2));
                XLWorkbook workbook1 = new XLWorkbook();
                workbook1.Worksheets.Add(table1, "Sheet1");
                workbook1.SaveAs(Path.Combine(PathHelper.FinalDirectoryInfo.FullName, "Cascade Invoice.xlsx"));

                Console.WriteLine("Json files downloaded.");
            }
            catch (Exception ex)
            {
                //Directory.Delete(PathHelper.WorkingDirectoryInfo.FullName, true);
                Console.WriteLine(ex.Message);
                throw;
            }
            return jsonFiles;
        }
    }
}
