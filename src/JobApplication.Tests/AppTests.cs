using System.Collections.Generic;
using JobApplication.Data;
using Moq;
using NUnit.Framework;

namespace JobApplication.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetJobApplication()
        {
            var dataStoreMock = new Mock<IDataStore>();
            var key = "source";
            var coverLetter = "Sample cover letter...";
            var cv = "these are my details...";
            var jobDescription = "some details describing the job";
            var jobTitle = "job title";
            var company = "advertiser";
            var jobDetail = $"\"job-detail\": {{\"title\": \"{jobTitle}\", \"description\": \"{jobDescription}\", \"company\": \"{company}\"}}";
            var value = $"{{\"cover-letter\": \"{coverLetter}\", \"cv\": \"{cv}\", {jobDetail}}}";
            var jobApplication = new Dictionary<string, string>() { { key, value }};
            
            dataStoreMock.Setup(x => x.GetJobApplication()).Returns(jobApplication);
            
            var app = new App(key, dataStoreMock.Object);

            var actualJobApplication = app.GetJobApplication();
            Assert.AreEqual(coverLetter, actualJobApplication.CoverLetter);
            Assert.AreEqual(cv, actualJobApplication.Cv);
            Assert.AreEqual(jobTitle, actualJobApplication.JobDetail.Title);
            Assert.AreEqual(jobDescription, actualJobApplication.JobDetail.Description);
            Assert.AreEqual(company, actualJobApplication.JobDetail.Company);
            dataStoreMock.Verify(x => x.GetJobApplication());
        }
    }
}