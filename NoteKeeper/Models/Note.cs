using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteKeeper.Models
{
    public class Note
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public Note()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public override string ToString()
        {
            string modified = ModifiedAt != default(DateTime) ? ModifiedAt.ToString() : "Never";
            return $"ID: {Id}\nTitle: {Title}\nContent: {Content}\nCreated At: {CreatedAt}\nModified At: {modified}\n";

        }
    }
}
