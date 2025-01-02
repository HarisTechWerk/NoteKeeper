using System;
using System.IO.MemoryMappedFiles;
using Microsoft.VisualBasic.FileIO;
using NoteKeeper.Models;
using NoteKeeper.Services;

namespace NoteKeeper
{
    class Program
    {
        static void Main(string[] args)
        {
            NoteManager manager = new NoteManager();
            bool exit = false;

            Console.WriteLine("===File Based Note Keeping Application===");

            while (!exit)
            {
                DisplayMenu();
                Console.Write("Select an option: ");
                string choice = Console.ReadLine() ?? string.Empty;

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddNote(manager);
                            break;
                        case "2":
                            ViewAllNotes(manager);
                            break;
                        case "3":
                            ViewNoteDetails(manager);
                            break;
                        case "4":
                            UpdateNote(manager);
                            break;
                        case "5":
                            DeleteNote(manager);
                            break;
                        case "6":
                            SearchNotes(manager);
                            break;
                        case "0":
                            exit = true;
                            Console.WriteLine("Exiting the application. Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }

                }

                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            Console.WriteLine();
        }
    
    static void DisplayMenu()
        {
            Console.WriteLine("===============================");
            Console.WriteLine("1. Add a new note");
            Console.WriteLine("2. View all notes");
            Console.WriteLine("3. View note details");
            Console.WriteLine("4. Update a note");
            Console.WriteLine("5. Delete a note");
            Console.WriteLine("6. Search notes");
            Console.WriteLine("0. Exit");
            Console.WriteLine("===============================");
        }

        static void AddNote(NoteManager manager)
        {
            Console.WriteLine("=== Add New Note ===");

            Console.Write("Enter Note Title: ");
            string title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title cannot be empty.");
                return;
            }

            Console.Write("Enter Note Content: ");
            string content = Console.ReadLine();

            Note note = new Note
            {
                Title = title,
                Content = content
            };

            manager.AddNote(note);
            Console.WriteLine("Note added successfully.");
        }

        static void ViewAllNotes(NoteManager manager)
        {
            Console.WriteLine("=== All Notes ===");
            var notes = manager.GetAllNotes();
            if (notes.Count == 0)
            {
                Console.WriteLine("No notes found.");
            return;
            }

            foreach (var note in notes)
            {
                Console.WriteLine($"ID: {note.Id} | Title: {note.Title} | Created At: {note.CreatedAt}");
            }
        }

        static void ViewNoteDetails(NoteManager manager)
        {
            Console.WriteLine("Enter Note ID to view details: ");
            string idInput = Console.ReadLine();

            if (Guid.TryParse(idInput, out Guid id))
            {
                var note = manager.GetNoteById(id);

                if (note != null)
                {
                    Console.WriteLine("=== Note Details ===");
                    Console.WriteLine(note);
                }
                else
                {
                    Console.WriteLine("Note not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        static void UpdateNote(NoteManager manager)
        {
            Console.Write("Enter Note ID to update: ");
            string idInput = Console.ReadLine();

            if (Guid.TryParse(idInput, out Guid id))
            {
                var note = manager.GetNoteById(id);
                if (note != null)
                {
                    Console.WriteLine("Leave field empty to keep current value.");

                    Console.Write($"Enter New Title ({note.Title}): ");
                    string title = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(title))
                        note.Title = title;

                    Console.Write($"Enter New Content ({note.Content}): ");
                    string content = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(content))
                        note.Content = content;

                    manager.updateNote(note);
                    Console.WriteLine("Note updated successfully!");
                }
                else
                {
                    Console.WriteLine("Note not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        static void DeleteNote(NoteManager manager)
        {
            Console.Write("Enter Note ID to delete: ");
            string idInput = Console.ReadLine();

            if (Guid.TryParse(idInput, out Guid id))
            {
                try
                {
                    manager.DeleteNote(id);
                    Console.WriteLine("Note deleted successfully!");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        static void SearchNotes(NoteManager manager)
        {
            Console.Write("Enter keyword to search: ");
            string keyword = Console.ReadLine();

            var results = manager.SearchNotes(keyword);

            if (results.Count == 0)
            {
                Console.WriteLine("No matching notes found.");
                return;
            }

            Console.WriteLine("=== Search Results ===");
            foreach (var note in results)
            {
                Console.WriteLine($"ID: {note.Id} | Title: {note.Title} | Created At: {note.CreatedAt}");
            }
        }
    }
}