﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Model
{
    class Notebook : INotifyPropertyChanged
    {
		private int userId;

		public int UserId
		{
			get { return userId; }
			set { 
				userId = value;
				OnPropertyChanged("UserId");
			}
		}

		private string name;


		public string Name
		{
			get { return name; }
			set { 
				name = value;
				OnPropertyChanged("Name");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
