using System.Collections.Generic;
using Newtonsoft.Json;

namespace JobApplication.Data
{
    public class JobApplication
    {
        public readonly string CoverLetter;
        public readonly string Cv;

        public JobApplication(string data)
        {
            this.CoverLetter = JsonConvert.DeserializeObject<IDictionary<string, string>>(data)["cover-letter"];
            this.Cv = JsonConvert.DeserializeObject<IDictionary<string, string>>(data)["cv"];
        }
    }
}