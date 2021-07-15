using JobApplication.Data;

namespace JobApplication {
    public class App {
        private readonly IDataStore dataStore;
        private readonly string source;
        public App (string source, IDataStore dataStore) {
            this.source = source;
            this.dataStore = dataStore;
        }

        public Data.JobApplication GetJobApplication () {
            var data = this.dataStore.GetJobApplication ();
            return new Data.JobApplication (data[this.source]);
        }
    }
}