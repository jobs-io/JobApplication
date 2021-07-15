using System.Collections.Generic;

namespace JobApplication.Data
{
    public interface IDataStore
    {
         IDictionary<string, string> GetJobApplication();
    }
}