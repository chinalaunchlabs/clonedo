using System;
using SQLite;

namespace CloneDo
{
	public class TaskItem
	{
		public TaskItem ()
		{
			Date = DateTime.Today;
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public string Task { get; set; }

		public string Description { get; set; }

		public bool Done { get; set; }

		public DateTime Date { get; set; }
	}
}

