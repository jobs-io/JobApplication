using System;
using System.Collections.Generic;
using JobApplication.Data;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace JobApplication.Tests
{
    public class AppTests
    {
        private Mock<IDataStore> dataStoreMock;
        private const string source = "source";

        [SetUp]
        public void Setup()
        {
            dataStoreMock = new Mock<IDataStore>();
        }

        [Test]
        public void ShouldGetJobApplication()
        {
            var coverLetter = "Sample cover letter...";
            var cv = "these are my details...";
            var jobDescription = "some details describing the job";
            var jobTitle = "job title";
            var company = "advertiser";
            var datePosted = DateTime.Parse("2019-07-26T00:00:00");
            var jobDetail = $"\"job-detail\": {{\"title\": \"{jobTitle}\", \"description\": \"{jobDescription}\", \"company\": \"{company}\", \"date-posted\": \"{datePosted}\" }}";
            var value = $"{{\"cover-letter\": \"{coverLetter}\", \"cv\": \"{cv}\", {jobDetail}}}";
            var jobApplication = new Dictionary<string, string>() { { source, value }};            
            dataStoreMock.Setup(x => x.GetJobApplication()).Returns(jobApplication);
            var app = new App(source, dataStoreMock.Object);

            var actualJobApplication = app.GetJobApplication();

            Assert.AreEqual(coverLetter, actualJobApplication.CoverLetter);
            Assert.AreEqual(cv, actualJobApplication.Cv);
            Assert.AreEqual(jobTitle, actualJobApplication.JobDetail.Title);
            Assert.AreEqual(jobDescription, actualJobApplication.JobDetail.Description);
            Assert.AreEqual(company, actualJobApplication.JobDetail.Company);
            Assert.AreEqual(datePosted, actualJobApplication.JobDetail.DatePosted);
            dataStoreMock.Verify(x => x.GetJobApplication());
        }

        [Test]
        public void ShouldCreateJobApplication() {
            var data = new {
                jobTitle = "job title", 
                jobDescription = "job description", 
                company = "company",
                datePosted = "2019-07-26T00:00:00"
            };
            var jobDetail = $"{{\"title\": \"{data.jobTitle}\", \"description\": \"{data.jobDescription}\", \"company\": \"{data.company}\", \"date-posted\": \"{data.datePosted}\" }}";
            var job = JsonConvert.DeserializeObject<IDictionary<string, string>>(jobDetail);
            
            dataStoreMock.Setup(x => x.CreateJobApplication(source, job));
            var app = new App(source, dataStoreMock.Object);

            
            app.CreateJobApplication(jobDetail);

            dataStoreMock.Verify(x => x.CreateJobApplication(source, job));
        }
    }
}