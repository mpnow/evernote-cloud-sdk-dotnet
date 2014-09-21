using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENSDK;
using ENSDK.Advanced;
using Evernote.EDAM.Type;

namespace SampleAppAdvanced
{
    class Program
    {
        static void Main(string[] args)
        {
            ENSessionAdvanced.SetSharedSessionConsumerKey("your key", "your secret");

            if (ENSession.SharedSession.IsAuthenticated == false)
            {
                ENSession.SharedSession.AuthenticateToEvernote();
            }

            ENNoteAdvanced myNoteAdv = new ENNoteAdvanced();
            myNoteAdv.Title = "Sample note with Reminder set";
            myNoteAdv.Content = ENNoteContent.NoteContentWithString("Hello, world - this note has a Reminder on it");
            myNoteAdv.EdamAttributes["ReminderOrder"] = DateTime.Now.ToEdamTimestamp();
            ENNoteRef myRef = ENSession.SharedSession.UploadNote(myNoteAdv, null);

            // Create the ENML content for the note.
            ENMLWriter writer = new ENMLWriter();
            writer.WriteStartDocument();
            writer.WriteString("Hello again, world.");
            writer.WriteEndDocument();
            writer.Flush();

            // Create a note locally.
            Note myNote = new Note();
            myNote.Title = "Sample note from the Advanced world";
            myNote.Content = writer.Contents.ToString();

            // Create the note in the service, in the user's personal, default notebook.
            ENNoteStoreClient store = ENSessionAdvanced.SharedSession.PrimaryNoteStore;
            Note resultNote = store.CreateNote(myNote);
        }
    }
}
