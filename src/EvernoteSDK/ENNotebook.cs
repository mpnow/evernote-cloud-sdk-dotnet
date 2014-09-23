
using Evernote.EDAM.Type;

namespace EvernoteSDK
{
	public class ENNotebook
	{

		private Notebook Notebook {get; set;}
		internal LinkedNotebook LinkedNotebook {get; set;}
		private SharedNotebook SharedNotebook {get; set;}
		internal bool IsDefaultNotebookOverride {get; set;}

		//Private _isShared As Boolean
		//Public Property IsShared As Boolean
		//    Get
		//        Return _isShared
		//    End Get
		//    Private Set(ByVal value As Boolean)
		//        _isShared = value
		//    End Set
		//End Property

		// Default constructor. Don't use this constructor; this is just here so ENNotebookAdvanced can inherit this class.
		internal ENNotebook()
		{
		}

		// Constructor for a regular notebook
		internal ENNotebook(Notebook notebook)
		{
			this.Notebook = notebook;
			this.SharedNotebook = null;
			this.LinkedNotebook = null;
		}

		// Constructor for a Linked notebook
		internal ENNotebook(SharedNotebook sharedNotebook, LinkedNotebook linkedNotebook)
		{
			this.Notebook = null;
			this.SharedNotebook = sharedNotebook;
			this.LinkedNotebook = linkedNotebook;
		}

		// Constructor for a Public notebook
		internal ENNotebook(Notebook publicNotebook, LinkedNotebook linkedNotebook)
		{
			this.Notebook = publicNotebook;
			this.SharedNotebook = null;
			this.LinkedNotebook = linkedNotebook;
		}

		// Constructor for a Business notebook
		internal ENNotebook(Notebook notebook, SharedNotebook sharedNotebook, LinkedNotebook linkedNotebook)
		{
			this.Notebook = notebook;
			this.SharedNotebook = sharedNotebook;
			this.LinkedNotebook = linkedNotebook;
		}

		public string Name
		{
			get
			{
				if (this.Notebook != null)
				{
					return this.Notebook.Name;
				}
				else if (this.LinkedNotebook != null)
				{
					return this.LinkedNotebook.ShareName;
				}
				else
				{
					return null;
				}
			}
		}

		public string OwnerDisplayName
		{
			get
			{
				string ownerName = null;
				if (this.IsBusinessNotebook)
				{
					ownerName = this.Notebook.Contact.Name;
					if (ownerName.Length == 0)
					{
						ownerName = ENSession.SharedSession.BusinessDisplayName;
					}
				}
				else if (this.LinkedNotebook != null)
				{
					ownerName = this.LinkedNotebook.Username;
				}
				else
				{
					ownerName = ENSession.SharedSession.UserDisplayName;
				}
				return ownerName;
			}
		}

		internal string Guid
		{
			get
			{
				// Personal notebooks have a native guid, and if we've stashed a public/business-native notebook here, then we can look at that
				// as well.
				if (this.Notebook != null)
				{
					return this.Notebook.Guid;
				}
				else if (this.SharedNotebook != null)
				{
					return this.SharedNotebook.NotebookGuid;
				}
				else
				{
					return null;
				}
			}
		}

		public bool IsShared
		{
			get
			{
				return IsOwnShared || IsJoinedShared;
			}
		}

		public bool IsOwnShared
		{
			get
			{
				return !IsLinked && Notebook.SharedNotebookIds.Count > 0;
			}
		}

		public bool IsJoinedShared
		{
			get
			{
				return IsLinked;
			}
		}

		public bool IsLinked
		{
			get
			{
				return this.LinkedNotebook != null;
			}
		}

		public bool IsPublic
		{
			get
			{
				return IsOwnPublic || IsJoinedPublic;
			}
		}

		public bool IsJoinedPublic
		{
			get
			{
				return IsLinked && string.IsNullOrEmpty(LinkedNotebook.ShareKey);
			}
		}

		public bool IsOwnPublic
		{
			get
			{
				return !IsLinked && Notebook.Publishing.Uri.Length > 0;
			}
		}

		public bool IsBusinessNotebook
		{
			get
			{
				// Business notebooks are the only ones that have a combination of a linked notebook and normal
				// notebook being set. In this case, the normal notebook represents the notebook inside the business.
				// Additionally, checking linked notebook record is actually pointing to a shared notebook record so it's not a public notebook.
				return Notebook != null && LinkedNotebook != null && LinkedNotebook.ShareKey != null;
			}
		}

		public bool IsOwnedByUser
		{
			get
			{
				if (this.LinkedNotebook == null)
				{
					// If there's no linked record, the notebook exists in the primary account, which means owned by user.
					return true;
				}
				else if (this.IsBusinessNotebook)
				{
					// If it's not a business notebook, but it is linked, then it's definitely NOT owned by the user.
					return false;
				}
				else
				{
					// Business notebooks are a little trickier. They are always linked, because technically the business owns
					// them. What we really want to know is whether the contact user is the same as the current user.
					return Notebook.Contact.Id == ENSession.SharedSession.UserID;
				}
			}
		}

		public bool IsDefaultNotebook
		{
			get
			{
				if (this.IsDefaultNotebookOverride)
				{
					return true;
				}
				else if (this.Notebook != null && !IsJoinedPublic)
				{
					return this.Notebook.DefaultNotebook;
				}
				else
				{
					return false;
				}
			}
		}

		public bool AllowsWriting
		{
			get
			{
				if (this.LinkedNotebook == null)
				{
					// All personal notebooks are readwrite.
					return true;
				}

				if (IsJoinedPublic)
				{
					// All public notebooks are readonly.
					return false;
				}

				int privilege = (int)this.SharedNotebook.Privilege;
				if (privilege == (int)SharedNotebookPrivilegeLevel.GROUP)
				{
					// Need to consult the business notebook object privilege.
					privilege = (int)this.Notebook.BusinessNotebook.Privilege;
				}

				if (privilege == (int)SharedNotebookPrivilegeLevel.MODIFY_NOTEBOOK_PLUS_ACTIVITY || privilege == (int)SharedNotebookPrivilegeLevel.FULL_ACCESS || privilege == (int)SharedNotebookPrivilegeLevel.BUSINESS_FULL_ACCESS)
				{
					return true;
				}

				return false;
			}
		}

		internal string Description
		{
			get
			{
				string owner = null;
				if (this.IsOwnedByUser)
				{
					owner = string.Format("\"{0}\"", this.OwnerDisplayName) + " (me)";
				}
				else
				{
					owner = string.Format("\"{0}\"", this.OwnerDisplayName);
				}
				return string.Format("<name = \"{0}\"; business = {1}; shared = {2}; owner = {3}; access = {4}>", this.Name, (this.IsBusinessNotebook ? "YES" : "NO"), (this.IsShared ? "YES" : "NO"), owner, (this.AllowsWriting ? "R/W" : "R/O"));
			}
		}

		public override bool Equals(object obj)
		{
			if (obj == null || !(this.GetType() == obj.GetType()))
			{
				return false;
			}

			return this.Guid == ((ENNotebook)obj).Guid;
		}

		public override int GetHashCode()
		{
			return this.Guid.GetHashCode();
		}

	}

}