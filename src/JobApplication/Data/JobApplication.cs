using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JobApplication.Data
{
    public class JobApplication
    {
        public readonly string CoverLetter;
        public readonly string Cv;
        public readonly Job JobDetail;

        public JobApplication(string data)
        {
            var json = JObject.Parse(data);
            this.CoverLetter = json["cover-letter"].ToString();
            this.Cv = json["cv"].ToString();
            this.JobDetail = new Job(json["job-detail"]);
        }
    }
}