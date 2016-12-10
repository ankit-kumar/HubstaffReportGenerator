using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using HubstaffReportGenerator.Models;
using Newtonsoft.Json;
using RestSharp;

namespace HubstaffReportGenerator.Helper
{
    public static class JsonDownloader
    {
        
        public static void GetReportJson(string authToken, string appToken, string startDate, string enddate, string fileName, out ReportResponse reportResponse)
        {
            reportResponse = new ReportResponse();
            var client = new RestClient(Constants.HubstaffApiRoot);

            var request = new RestRequest("custom/by_project/my", Method.GET);
            request.AddParameter("start_date", startDate); // adds to POST or URL querystring based on Method
            request.AddParameter("end_date", enddate);
            request.AddParameter("show_tasks", "true");
            request.AddParameter("show_notes", "true");
            request.AddParameter("show_activity", "true");
            request.AddHeader("App-Token", appToken);
            request.AddHeader("Auth-Token", authToken);
            request.AddHeader("Content-Type", "application/xml");

            IRestResponse response = client.Execute(request);
            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                reportResponse = JsonConvert.DeserializeObject<ReportResponse>(response.Content);
            }
            else
            {
                throw new Exception($"Request failed. ResponseStatus:{response.ResponseStatus} StatusCode:{response.StatusCode} Message:{response.ErrorMessage}");
            }
        }

        public static string GetAuthToken(HubStaffCredential c)
        {
            var client = new RestClient(Constants.HubstaffApiRoot);
            var request = new RestRequest("auth", Method.POST);
            request.AddParameter("email", c.Email);
            request.AddParameter("password", c.Password);
            request.AddHeader("App-Token", c.AppToken);
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);
            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                HubstaffAuthResponse hubstaffAuthResponse = JsonConvert.DeserializeObject<HubstaffAuthResponse>(response.Content);
                return string.IsNullOrEmpty(hubstaffAuthResponse.User.AuthToken) ? "" : hubstaffAuthResponse.User.AuthToken;
            }
            throw new Exception($"Request failed. ResponseStatus:{response.ResponseStatus.ToString()} StatusCode:{response.StatusCode.ToString()} Message:{response.ErrorMessage}");
        }
    }
}
