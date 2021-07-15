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
            var jobApplication = new Dictionary<string, string>() { { "source", "{\"cover-letter\": \"Sample cover letter...\"}" }};
            
            dataStoreMock.Setup(x => x.GetJobApplication()).Returns(jobApplication);
            
            var app = new App(dataStoreMock.Object);

            Assert.AreEqual(new Data.JobApplication().CoverLetter, app.GetJobApplication().CoverLetter);
            dataStoreMock.Verify(x => x.GetJobApplication());
        }
    }
}