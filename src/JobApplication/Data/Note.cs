using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JobApplication.Data
{
    public class Note
    {
        public readonly string Description;
        public readonly DateTime DatePosted;

        public Note(string description, DateTime datePosted)
        {
            this.Description = description;
            this.DatePosted = datePosted;
        }
    }

    public class Notes : List<Note> {
        public Notes(JToken json)
        {
            foreach(var note in json.AsJEnumerable())
                this.Add(new Note(note["description"].ToString(), DateTime.Parse(note["date-posted"].ToString())));
        }
    }
}