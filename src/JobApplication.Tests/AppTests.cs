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
            var jobTitle = "job title";
            var jobDetail = $"\"job-detail\": {{\"title\": \"{jobTitle}\"}}";
            var value = $"{{\"cover-letter\": \"{coverLetter}\", \"cv\": \"{cv}\", {jobDetail}}}";
            System.Console.WriteLine(value);
            var jobApplication = new Dictionary<string, string>() { { key, value }};
            
            dataStoreMock.Setup(x => x.GetJobApplication()).Returns(jobApplication);
            
            var app = new App(key, dataStoreMock.Object);

            var actualJobApplication = app.GetJobApplication();
            Assert.AreEqual(coverLetter, actualJobApplication.CoverLetter);
            Assert.AreEqual(cv, actualJobApplication.Cv);
            Assert.AreEqual(jobTitle, actualJobApplication.JobDetail.Title);
            dataStoreMock.Verify(x => x.GetJobApplication());
        }
    }
}