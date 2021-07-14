using JobApplication.Data;

namespace JobApplication {
    public class App {
        private readonly IDataStore dataStore;
        public App (IDataStore dataStore) {
            this.dataStore = dataStore;

        }

        public Data.JobApplication GetJobApplication() {
            return this.dataStore.GetJobApplication();
        }
    }
}