using JobApplication.Data;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JobApplication {
    public class App {
        private readonly IDataStore dataStore;
        private readonly string source;
        public App (string source, IDataStore dataStore) {
            this.source = source;
            this.dataStore = dataStore;
        }

        public Data.JobApplication GetJobApplication() {
            var data = this.dataStore.GetJobApplication ();
            return new Data.JobApplication (data[this.source]);
        }

        public void CreateJobApplication(string jobDetail) {
            this.dataStore.CreateJobApplication(this.source, JsonConvert.DeserializeObject<IDictionary<string, string>>(jobDetail));
        }

        public bool JobApplicationExists() {
            return this.dataStore.Exists(source);
        }

        public void UpdateCv(string cv) {
            this.dataStore.UpdateCv(source, cv);
        }
    }
}