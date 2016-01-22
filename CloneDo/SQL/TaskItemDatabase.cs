using System;
using SQLite;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace CloneDo
{
	public class TaskItemDatabase
	{
		protected static object locker = new object();
		protected SQLiteConnection database;

		string DatabasePath {
			get {
				var dbName = "TaskDatabase.db3";
				#if __IOS__
					string folder = Environment.GetFolderPath (Environment.SpecialFolder.Personal); folder = Path.Combine (folder, "..", "Library");
					var databasePath = Path.Combine(folder, dbName);
				#else
				#if __ANDROID__
					string folder = Environment.GetFolderPath (Environment.SpecialFolder.Personal); 
					var databasePath = Path.Combine(folder, dbName);
				#else
				// WinPhone
					var databasePath = Path.Combine(Windows.Storage.ApplicationData.Current.
					LocalFolder.Path, dbName);;
				#endif
				#endif
				return databasePath;
			}
		}

		public TaskItemDatabase ()
		{
			database = new SQLiteConnection (DatabasePath);
			database.CreateTable<TaskItem> ();
		}

		public IEnumerable<TaskItem> GetTasks() {
			lock (locker) {
				return (from i in database.Table<TaskItem> ()
				        select i).ToList ();
			}
		}

		public TaskItem GetTask(int id) {
			lock (locker) {
				return database.Table<TaskItem> ().FirstOrDefault (x => x.ID == id);
			}
		}

		public int SaveTask (TaskItem task) {
			lock (locker) {
				if (task.ID != 0) {
					database.Update (task);
					return task.ID;
				} else {
					return database.Insert (task);
				}
			}
		}

		public int DeleteTask (int id) {
			lock (locker) {
				return database.Delete<TaskItem> (id);
			}
		}

	}
}

