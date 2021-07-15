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
            var value = $"{{\"cover-letter\": \"{coverLetter}\"}}";
            var jobApplication = new Dictionary<string, string>() { { key, value }};
            
            dataStoreMock.Setup(x => x.GetJobApplication()).Returns(jobApplication);
            
            var app = new App(key, dataStoreMock.Object);

            Assert.AreEqual(coverLetter, app.GetJobApplication().CoverLetter);
            dataStoreMock.Verify(x => x.GetJobApplication());
        }
    }
}