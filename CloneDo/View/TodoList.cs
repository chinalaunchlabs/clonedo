using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace CloneDo
{
	public delegate void OnTaskItemDelete();

	public class TodoList: ContentPage
	{
		public ListView todoList, doneList;

		public TodoList ()
		{
			
			// Views
			todoList = new ListView {
				HasUnevenRows = true,
				RowHeight = 50,
				VerticalOptions = LayoutOptions.Fill,
			};
			todoList.ItemTemplate = new DataTemplate (typeof(TaskItemCell));

			doneList = new ListView {
				HasUnevenRows = true,
				RowHeight = 50,
				VerticalOptions = LayoutOptions.Fill
			};
			doneList.ItemTemplate = new DataTemplate (typeof(TaskDoneCell));

			Button newBtn = new Button {
				Text = "New",
			};
				
			// Events
			todoList.ItemSelected += (sender, e) => {
				var task = (TaskItem)e.SelectedItem;
				var taskDetails = new TaskDetails();
				taskDetails.BindingContext = task;
				Navigation.PushAsync(taskDetails);
			};
			doneList.ItemSelected += (sender, e) => {
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
					todoList,
					doneList
				},
				Padding = new Thickness(10, 0)
			};


			ScrollView sv = new ScrollView {
				Content = stackLayout
			};

			Title = "CloneDo";
			Content = sv;
			Padding = new Thickness (0, Device.OnPlatform (20, 0, 0), 0, 0);
		}
	
		// Override to update the list
		protected override void OnAppearing() {
			base.OnAppearing ();
			Refresh ();
		}

		public void Refresh() {
			List<TaskItem> tasks = (List<TaskItem>) App.Database.GetTasks ();
			List<TaskItem> done = (List<TaskItem>) App.Database.GetTasks (true);
			todoList.ItemsSource = tasks;
			todoList.HeightRequest = 1.1*(todoList.RowHeight * tasks.Count);
			doneList.ItemsSource = done;
			doneList.HeightRequest = 1.1*(doneList.RowHeight * done.Count);
		}
	
	}
}