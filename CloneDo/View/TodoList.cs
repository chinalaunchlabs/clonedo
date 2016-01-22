using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CloneDo
{
	public class TodoList: ContentPage
	{
		ListView todoList; 

		public TodoList ()
		{
//			TaskItem[] tasks = new TaskItem[] {
//				new TaskItem { Task = "Finish this", Description = "Now na", Done = false },
//				new TaskItem { Task = "Take out trash", Description = "...", Done = false },
//				new TaskItem { Task = "Read book", Description = "Xamarin Cross-Platform", Done = true },
//			};
//
			// Views
			todoList = new ListView {
				RowHeight = 60,
			};
			todoList.ItemTemplate = new DataTemplate (typeof(TaskItemCell));

			Button newBtn = new Button {
				Text = "New Task",
			};
				
			// Events
			todoList.ItemSelected += (sender, e) => {
				var task = (TaskItem)e.SelectedItem;
				var taskDetails = new TaskDetails();
				taskDetails.BindingContext = task;
				Navigation.PushAsync(taskDetails);
			};
			newBtn.Clicked += (sender, e) => {
				var task = new TaskItem();
				var taskDetails = new TaskDetails();
				taskDetails.BindingContext = task;
				Navigation.PushAsync(taskDetails);
			};

			// Layout
			StackLayout stackLayout = new StackLayout {
				Children = {
					newBtn,
					todoList
				}
			};

			Title = "CloneDo";
			Content = stackLayout;
			Padding = new Thickness (0, Device.OnPlatform (20, 0, 0), 0, 0);
		}
	
		// Override to update the list
		protected override void OnAppearing() {
			base.OnAppearing ();
			todoList.ItemsSource = App.Database.GetTasks ();
		}
	
	}
}

