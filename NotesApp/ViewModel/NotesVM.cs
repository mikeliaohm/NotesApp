using NotesApp.Model;
using NotesApp.ViewModel.Commands;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.ViewModel
{

	public class NotesVM
	{
		public bool IsEditing { get; set; }

		private Notebook selectedNotebook;

		public Notebook SelectedNotebook
		{
			get { return selectedNotebook; }
			set {
				selectedNotebook = value;
				ReadNotes();
			}
		}

		public ObservableCollection<Notebook> Notebooks { get; set; }

		public ObservableCollection<Note> Notes { get; set; }

		public NewNotebookCommand NewNotebookCommand { get; set; }

		public NewNoteCommand NewNoteCommand { get; set; }

		public NotesVM()
		{
			IsEditing = false;

			NewNotebookCommand = new NewNotebookCommand(this);
			NewNoteCommand = new NewNoteCommand(this);

			Notebooks = new ObservableCollection<Notebook>();
			Notes = new ObservableCollection<Note>();
			
			ReadNotebooks();
			ReadNotes();
		}

		public void CreateNote(int notebookId)
		{
			Note newNote = new Note()
			{
				NotebookId = notebookId,
				CreatedTime = DateTime.Now,
				UpdatedTime = DateTime.Now,
				Title = "New note"
			};

			DatabaseHelper.Insert(newNote);

			ReadNotes();
		}

		public void CreateNotebook()
		{
			Notebook newNotebook = new Notebook()
			{
				Name = "New notebook",
				UserId = int.Parse(App.UserId)
			};

			DatabaseHelper.Insert(newNotebook);

			ReadNotebooks();
		}

		public void ReadNotebooks()
		{
			using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.DbFile))
			{
				var notebooks = conn.Table<Notebook>().ToList();

				Notebooks.Clear();

				foreach (var notebook in notebooks)
				{
					Notebooks.Add(notebook);
				}
			}
		}

		public void ReadNotes()
		{
			using(SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.DbFile))
			{
				conn.CreateTable<Note>(); // create Note table, not covered in tutorial

				if (SelectedNotebook != null)
				{
					var notes = conn.Table<Note>().Where(n => n.NotebookId == SelectedNotebook.Id).ToList();

					Notes.Clear();
					foreach(var note in notes)
					{
						Notes.Add(note);
					}
				}
			}
		}
	}
}
