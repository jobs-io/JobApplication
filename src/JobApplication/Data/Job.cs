using System;
using Newtonsoft.Json.Linq;

namespace JobApplication.Data
{
    public class Job
    {
        public readonly string Title;
        public readonly string Description;
        public readonly string Company;
        public readonly DateTime DatePosted;

        public Job(JToken json)
        {
            this.Title = json["title"].ToString();
            this.Description = json["description"].ToString();
            this.Company = json["company"].ToString();
            this.DatePosted = DateTime.Parse(json["date-posted"].ToString());
        }
    }
}