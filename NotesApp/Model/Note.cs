﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Model
{
    class Note : INotifyPropertyChanged
    {
		private int id;

		public int Id
		{
			get { return id; }
			set { 
				id = value;
				OnPropertyChanged("Id");
			}
		}


		private int notebookId;

		public int NotebookId
		{
			get { return notebookId; }
			set { 
				notebookId = value;
				OnPropertyChanged("NotebookId");
			}
		}

		private string title;

		public string Title
		{
			get { return title; }
			set { 
				title = value;
				OnPropertyChanged("Title");
			}
		}

		private DateTime createdTime;

		public DateTime CreatedTime
		{
			get { return createdTime; }
			set { 
				createdTime = value;
				OnPropertyChanged("CreatedTime");
			}
		}

		private string fileLocation;


		public string FileLocation
		{
			get { return fileLocation; }
			set { 
				fileLocation = value;
				OnPropertyChanged("FileLocation");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
