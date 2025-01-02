using NoteKeeper.Models;
using NoteKeeper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NoteKeeper.Services
{
    public class NoteManager
    {
        private List<Note> Notes; // List of notes.

        public NoteManager() // Constructor that initializes the Notes list by loading the notes from the file.
        {
            Notes = FileHelper.LoadNotes();
        }

        public void AddNote(Note note)
        {
            Notes.Add(note);
            FileHelper.SaveNotes(Notes);
        }

        public List<Note> GetAllNotes()
        {
            return Notes;
        }

        public Note GetNoteById(Guid id)
        {
            return Notes.FirstOrDefault(n => n.Id == id) ?? throw new ArgumentException("Note not found");
        }

        public void updateNote(Note updateNote)
        {
            var index = Notes.FindIndex(n => n.Id == updateNote.Id);
            if (index != -1)
            {
                updateNote.ModifiedAt = DateTime.Now;
                Notes[index] = updateNote;
                FileHelper.SaveNotes(Notes);
            }
            else
            {
                throw new ArgumentException("Note not found");
            }
        }

        public void DeleteNote(Guid id)
        {
            var note = GetNoteById(id);
            if (note != null)
            {
                Notes.Remove(note);
                FileHelper.SaveNotes(Notes);
            }
            else
            {
                throw new ArgumentException("Note not found");
            }
        }
        public List<Note> SearchNotes(string keyword)
        {
            return Notes.Where(n => n.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) || n.Content.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
