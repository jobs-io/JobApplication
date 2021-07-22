using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JobApplication.Data
{
    public class Note
    {
        public readonly string Title;
        public readonly string Description;
        public readonly DateTime DatePosted;

        public Note(string title, string description, DateTime datePosted)
        {
            this.Description = description;
            this.DatePosted = datePosted;
            this.Title = title;
        }
    }

    public class Notes : List<Note> {
        public Notes(JToken json)
        {
            foreach(var note in json.AsJEnumerable())
                this.Add(new Note(note["title"].ToString(), note["description"].ToString(), DateTime.Parse(note["date-posted"].ToString())));
        }
    }
}