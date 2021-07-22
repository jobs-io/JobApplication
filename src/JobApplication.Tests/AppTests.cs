using System;
using System.Collections.Generic;
using JobApplication.Data;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

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
                },
                notes = new [] {
                    new {title = "a note", description = "a note", datePosted = "2019-07-26T00:00:00"}
                }
            };

            var notesAsJson = "";
            foreach(var note in expected.notes) {
                if(notesAsJson == "") {
                    notesAsJson += $"{{\"title\": \"{note.title}\", \"description\": \"{note.description}\", \"date-posted\": \"{note.datePosted}\"}}";
                } else {
                    notesAsJson += $", {{\"title\": \"{note.title}\", \"description\": \"{note.description}\", \"date-posted\": \"{note.datePosted}\"}}";
                }
            }
            var notes = $"\"notes\": [{notesAsJson}]";
            var jobDetail = $"\"job-detail\": {{\"title\": \"{expected.jobDetail.title}\", \"description\": \"{expected.jobDetail.description}\", \"company\": \"{expected.jobDetail.company}\", \"date-posted\": \"{expected.jobDetail.datePosted}\" }}";
            var value = $"{{\"cover-letter\": \"{expected.coverLetter}\", \"cv\": \"{expected.cv}\", {jobDetail}, {notes}}}";
            dataStoreMock.Setup(x => x.GetJobApplication()).Returns(new Dictionary<string, string>() { { source, value }});
            var app = new App(source, dataStoreMock.Object);

            var result = app.GetJobApplication();

            Assert.AreEqual(expected.coverLetter, result.CoverLetter);
            Assert.AreEqual(expected.cv, result.Cv);
            Assert.AreEqual(expected.jobDetail.title, result.JobDetail.Title);
            Assert.AreEqual(expected.jobDetail.description, result.JobDetail.Description);
            Assert.AreEqual(expected.jobDetail.company, result.JobDetail.Company);
            Assert.AreEqual(expected.jobDetail.datePosted, result.JobDetail.DatePosted);
            Assert.AreEqual(expected.notes[0].description, result.Notes[0].Description);
            Assert.AreEqual(DateTime.Parse(expected.notes[0].datePosted), result.Notes[0].DatePosted);
            Assert.AreEqual(expected.notes[0].title, result.Notes[0].Title);
            dataStoreMock.Verify(x => x.GetJobApplication());
        }

        [Test]
        public void ShouldGetJobApplicationThatHasJustBeenCreated()
        {
            var expected = new {
                jobDetail = new {
                    title = "job title",
                    description = "some details describing the job",
                    company = "advertiser",
                    datePosted = DateTime.Parse("2019-07-26T00:00:00")
                }
            };
            var jobDetail = $"{{\"job-detail\": {{\"title\": \"{expected.jobDetail.title}\", \"description\": \"{expected.jobDetail.description}\", \"company\": \"{expected.jobDetail.company}\", \"date-posted\": \"{expected.jobDetail.datePosted}\" }}}}";
            dataStoreMock.Setup(x => x.GetJobApplication()).Returns(new Dictionary<string, string>() { { source, jobDetail }});
            var app = new App(source, dataStoreMock.Object);

            var result = app.GetJobApplication();

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

        [TestCase("cover-letter", "these are some details")]
        [TestCase("cv", "these are some details")]
        public void ShouldUpdateItem(string key, string data) {
            dataStoreMock.Setup(x => x.Update<string>(source, key, data));
            var app = new App(source, dataStoreMock.Object);

            app.UpdateItem<string>(key, data);

            dataStoreMock.Verify(x => x.Update<string>(source, key, data));
        }

        [Test]
        public void ShouldUpdateNotes() {
            var key = "notes";
            var expected = new {
                notes = new [] {
                    new {title = "a note", description = "a note", datePosted = "2019-07-26T00:00:00"}
                }
            };
            var notesAsJson = "";
            foreach(var note in expected.notes) {
                if(notesAsJson == "") {
                    notesAsJson += $"{{\"title\": \"{note.title}\", \"description\": \"{note.description}\", \"date-posted\": \"{note.datePosted}\"}}";
                } else {
                    notesAsJson += $", {{\"title\": \"{note.title}\", \"description\": \"{note.description}\", \"date-posted\": \"{note.datePosted}\"}}";
                }
            }
            var notes = $"{{\"{key}\": [{notesAsJson}]}}";
            var savedNotes = new Notes(JObject.Parse(notes)["notes"]);
            dataStoreMock.Setup(x => x.Update<Notes>(source, key, savedNotes));
            var app = new App(source, dataStoreMock.Object);

            app.UpdateItem<Notes>(key, savedNotes);

            dataStoreMock.Verify(x => x.Update<Notes>(source, key, savedNotes));            
        }
    }
}