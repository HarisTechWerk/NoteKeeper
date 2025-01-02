using Newtonsoft.Json;
using NoteKeeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteKeeper.Utilities
{
    public static class FileHelper
    {
        private static readonly string FilePath = "notes.json";

        public static List<Note> LoadNotes() 
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    return new List<Note>();
                }

                string json = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<List<Note>>(json) ?? new List<Note>(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading notes: {ex.Message}");
                return new List<Note>();
            }
        }

        public static void SaveNotes(List<Note> notes) 
        {
            try
            {
                string json = JsonConvert.SerializeObject(notes, Formatting.Indented);
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving notes: {ex.Message}");
            }
        }
    }
}
