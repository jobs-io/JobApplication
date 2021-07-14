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
            var jobApplication = new Data.JobApplication();
            dataStoreMock.Setup(x => x.GetJobApplication()).Returns(jobApplication);
            
            var app = new App(dataStoreMock.Object);

            Assert.AreEqual(jobApplication, app.GetJobApplication());
            dataStoreMock.Verify(x => x.GetJobApplication());
        }
    }
}