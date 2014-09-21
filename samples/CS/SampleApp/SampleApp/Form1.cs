using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ENSDK;

namespace SampleApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ENSession.SetSharedSessionConsumerKey("your key", "your secret");

            if (ENSession.SharedSession.IsAuthenticated == false)
            {
                ENSession.SharedSession.AuthenticateToEvernote();
            }

            List<ENNotebook> myNotebookList = ENSession.SharedSession.ListNotebooks();

            ENNote myNote = new ENNote();
            myNote.Title = "My first note";
            myNote.Content = ENNoteContent.NoteContentWithString("Hello, world!");
            ENNoteRef myNoteRef = ENSession.SharedSession.UploadNote(myNote, null);

            string shareUrl = ENSession.SharedSession.ShareNote(myNoteRef);

            ENSession.SharedSession.DeleteNote(myNoteRef);

            ENNote myNote2 = new ENNote();
            myNote2.Title = "My first note with resource";
            myNote2.Content = ENNoteContent.NoteContentWithString("Hello, world - this is a resource note");
            byte[] myFile = StreamFile("<complete path and filename of a JPG file on your computer>");
            ENResource myResource = new ENResource(myFile, "image/jpg", "My First Picture.jpg");
            myNote2.Resources.Add(myResource);
            ENNoteRef myRef2 = ENSession.SharedSession.UploadNote(myNote2, null);

            List<ENSessionFindNotesResult> myNotesList = ENSession.SharedSession.FindNotes(ENNoteSearch.NoteSearch("some text to find"), null, ENSession.SearchScope.All, ENSession.SortOrder.RecentlyUpdated, 500);
            int personalCount = 0;
            int sharedCount = 0;
            int businessCount = 0;
            foreach (ENSessionFindNotesResult nte in myNotesList)
            {
                if (nte.NoteRef.Type == ENNoteRef.ENNoteRefType.TypePersonal)
                {
                    personalCount += 1;
                }
                else if (nte.NoteRef.Type == ENNoteRef.ENNoteRefType.TypeShared)
                {
                    sharedCount += 1;
                }
                else if (nte.NoteRef.Type == ENNoteRef.ENNoteRefType.TypeBusiness)
                {
                    businessCount += 1;
                }
            }

            byte[] mySavedRef = myNotesList[0].NoteRef.AsData();
            ENNoteRef newRef = ENNoteRef.NoteRefFromData(mySavedRef);

            ENNote myDownloadedNote = ENSession.SharedSession.DownloadNote(myNotesList[0].NoteRef);

            byte[] thumbnail = ENSession.SharedSession.DownloadThumbnailForNote(myNotesList[0].NoteRef, 120);
            try
            {
                MemoryStream ms = new MemoryStream(thumbnail, 0, thumbnail.Length);
                ms.Position = 0;
                System.Drawing.Image image1 = System.Drawing.Image.FromStream(ms, false, false);
                pictureBoxThumbnail.Image = image1;
            }
            catch (Exception)
            {
            }

        }


         static byte[] StreamFile(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);

            // Create a byte array of file stream length
            byte[] ImageData = new byte[Convert.ToInt32(fs.Length - 1) + 1];

            //Read block of bytes from stream into the byte array
            fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length));

            //Close the File Stream
            fs.Close();
            //return the byte data
            return ImageData;
        }
    }
}
