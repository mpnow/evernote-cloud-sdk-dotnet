
namespace EvernoteSDK
{
	public class ENNoteContentCOM
	{

		public ENNoteContent NoteContentWithString(string contentString)
		{
			return ENNoteContent.NoteContentWithString(contentString);
		}

		public ENNoteContent ComNoteContentWithSanitizedHTML(string html)
		{
			return ENNoteContent.NoteContentWithSanitizedHTML(html);
		}

	}

}