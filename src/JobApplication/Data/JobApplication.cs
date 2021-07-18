using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JobApplication.Data
{
    public class JobApplication
    {
        public readonly string CoverLetter;
        public readonly string Cv;
        public readonly Job JobDetail;
        public readonly Notes Notes;

        public JobApplication(string data)
        {
            var json = JObject.Parse(data);
            if(json["cover-letter"] != null) this.CoverLetter = json["cover-letter"].ToString();
            if(json["cv"] != null) this.Cv = json["cv"].ToString();
            this.JobDetail = new Job(json["job-detail"]);
            if (json["notes"] != null) this.Notes = new Notes(json["notes"]);
        }
    }
}