using System;
using Evernote.EDAM.Type;

namespace ENSDK
{
	[Serializable]
	public class ENLinkedNotebookRef
	{

		// This object contains the minimum information required to authenticate to a shared notebook.
		// It is intentionally narrower than a full EDAMLinkedNotebook, allowing these general bits of information
		// to be persisted independently of a full EDAMLinkedNotebook.

		public string Guid {get; set;}
		public string NoteStoreUrl {get; set;}
		public string ShardId {get; set;}
		public string SharedNotebookGlobalId {get; set;}

		internal static ENLinkedNotebookRef LinkedNotebookRefFromLinkedNotebook(LinkedNotebook linkedNotebook)
		{
			ENLinkedNotebookRef linkedNotebookRef = new ENLinkedNotebookRef();
			linkedNotebookRef.Guid = linkedNotebook.Guid;
			linkedNotebookRef.NoteStoreUrl = linkedNotebook.NoteStoreUrl;
			linkedNotebookRef.ShardId = linkedNotebook.ShardId;
			linkedNotebookRef.SharedNotebookGlobalId = linkedNotebook.ShareKey;
			return linkedNotebookRef;
		}

		internal bool IsEqual(object obj)
		{
			if (this == (ENSDK.ENLinkedNotebookRef)obj)
			{
				return true;
			}
			if (obj != null && obj.GetType() != this.GetType())
			{
				return false;
			}

			ENLinkedNotebookRef other = (ENSDK.ENLinkedNotebookRef)obj;
			if (other.Guid == Guid && other.NoteStoreUrl == NoteStoreUrl && other.ShardId == ShardId && other.SharedNotebookGlobalId == SharedNotebookGlobalId)
			{
				return true;
			}

			return false;
		}

		internal int Hash()
		{
			int prime = 31;
			int result = 1;
			result = prime * result + Guid.GetHashCode();
			result = prime * result + NoteStoreUrl.GetHashCode();
			result = prime * result + ShardId.GetHashCode();
			result = prime * result + SharedNotebookGlobalId.GetHashCode();
			return result;
		}

	}

}