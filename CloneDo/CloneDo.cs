using System;

using Xamarin.Forms;

namespace CloneDo
{
	public class App : Application
	{
		static TaskItemDatabase database;
		public static TaskItemDatabase Database {
			get {
				database = database ?? new TaskItemDatabase ();
				return database;
			}
		}

		public App ()
		{
			MainPage = new NavigationPage (new TodoList());

		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

