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
            var expected = new {
                coverLetter = "Sample cover letter...",
                cv = "these are my details...",
                jobDetail = new {
                    title = "job title",
                    description = "some details describing the job",
                    company = "advertiser",
                    datePosted = DateTime.Parse("2019-07-26T00:00:00")
                }
            };
            var jobDetail = $"\"job-detail\": {{\"title\": \"{expected.jobDetail.title}\", \"description\": \"{expected.jobDetail.description}\", \"company\": \"{expected.jobDetail.company}\", \"date-posted\": \"{expected.jobDetail.datePosted}\" }}";
            var value = $"{{\"cover-letter\": \"{expected.coverLetter}\", \"cv\": \"{expected.cv}\", {jobDetail}}}";
            dataStoreMock.Setup(x => x.GetJobApplication()).Returns(new Dictionary<string, string>() { { source, value }});
            var app = new App(source, dataStoreMock.Object);

            var result = app.GetJobApplication();

            Assert.AreEqual(expected.coverLetter, result.CoverLetter);
            Assert.AreEqual(expected.cv, result.Cv);
            Assert.AreEqual(expected.jobDetail.title, result.JobDetail.Title);
            Assert.AreEqual(expected.jobDetail.description, result.JobDetail.Description);
            Assert.AreEqual(expected.jobDetail.company, result.JobDetail.Company);
            Assert.AreEqual(expected.jobDetail.datePosted, result.JobDetail.DatePosted);
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

        [Test]
        public void ShouldCheckIfJobApplicationExists() {
            dataStoreMock.Setup(x => x.Exists(source)).Returns(true);
            var app = new App(source, dataStoreMock.Object);

            var exists = app.JobApplicationExists();

            dataStoreMock.Verify(x => x.Exists(source));
            Assert.IsTrue(exists);
        }
    }
}