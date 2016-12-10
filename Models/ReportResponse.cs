using Newtonsoft.Json;
using System.Collections.Generic;

namespace HubstaffReportGenerator.Models
{
    public class Task
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("summary")]
        public string Summary { get; set; }
        [JsonProperty("duration")]
        public int Duration { get; set; }
    }

    public class Note
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("duration")]
        public int Duration { get; set; }
        [JsonProperty("activity_percent")]

        public int ActivityPercent { get; set; }
        [JsonProperty("tasks_duration")]
        public int TasksDuration { get; set; }
        [JsonProperty("tasks")]
        public List<Task> Tasks { get; set; }
        [JsonProperty("notes")]
        public List<Note> Notes { get; set; }
    }

    public class Date
    {

        [JsonProperty("date")]
        public string date { get; set; }
        [JsonProperty("duration")]
        public int Duration { get; set; }
        [JsonProperty("activity_percent")]

        public int ActivityPercent { get; set; }
        [JsonProperty("tasks_duration")]
        public int TasksDuration { get; set; }
        [JsonProperty("users")]
        public List<User> Users { get; set; }
    }

    public class Project
    {

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("duration")]
        public int Duration { get; set; }
        [JsonProperty("activity_percent")]

        public int ActivityPercent { get; set; }
        [JsonProperty("tasks_duration")]
        public int TasksDuration { get; set; }
        [JsonProperty("dates")]
        public List<Date> Dates { get; set; }
    }

    public class Organization
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("duration")]
        public int Duration { get; set; }
        [JsonProperty("activity_percent")]

        public int ActivityPercent { get; set; }
        [JsonProperty("tasks_duration")]
        public int TasksDuration { get; set; }
        [JsonProperty("projects")]
        public List<Project> Projects { get; set; }
    }

    public class ReportResponse
    {
        [JsonProperty("organizations")]
        public List<Organization> Organizations { get; set; }
    }
}
