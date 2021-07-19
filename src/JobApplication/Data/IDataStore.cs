using System.Collections.Generic;

namespace JobApplication.Data
{
    public interface IDataStore
    {
         IDictionary<string, string> GetJobApplication();
         void CreateJobApplication(string source, IDictionary<string, string> job);
         bool Exists(string source);

         void UpdateCv(string source, string cv);
    }
}