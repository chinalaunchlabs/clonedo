using System;
using Xamarin.Forms;

namespace CloneDo
{
	/*
	 * Task detail page
	 */
	public class TaskDetails: ContentPage
	{
		public TaskDetails ()
		{
			Label taskNameLabel, taskDescLabel, taskDoneLabel, taskDateLabel;
			Entry taskNameEntry;
			Editor taskDescEntry;
			Switch taskDoneSwitch;
			DatePicker taskDatePicker;
			Button saveBtn, deleteBtn;
			StackLayout stackLayout;

			// Views
			taskNameLabel = new Label {
				Text = "Task",
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				FontAttributes = FontAttributes.Bold,
			};
			taskDescLabel = new Label {
				Text = "Description",
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				FontAttributes = FontAttributes.Bold,
			};
			taskDoneLabel = new Label {
				Text = "Done",
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				FontAttributes = FontAttributes.Bold,
			};
			taskDateLabel = new Label {
				Text = "Due Date",
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				FontAttributes = FontAttributes.Bold,
			};

			taskNameEntry = new Entry {
				Placeholder = "Be awesome",
				VerticalOptions = LayoutOptions.Start,
				Keyboard = Keyboard.Text
			};
			taskDescEntry = new Editor {
				
			};
			taskDoneSwitch = new Switch ();
			taskDatePicker = new DatePicker {
				Format = "D",
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Date = DateTime.Today
			};

			saveBtn = new Button {
				Text = "Save"
			};
			deleteBtn = new Button {
				Text = "Delete"
			};

			// Binding
			this.SetBinding(ContentPage.TitleProperty, "Task");

			taskNameEntry.SetBinding(Entry.TextProperty, "Task");
			taskDescEntry.SetBinding (Editor.TextProperty, "Description");
			taskDoneSwitch.SetBinding (Xamarin.Forms.Switch.IsToggledProperty, "Done");
			taskDatePicker.SetBinding (DatePicker.DateProperty, "Date");

			// Events
			saveBtn.Clicked += (sender, e) => {
				var task = (TaskItem)BindingContext;
				App.Database.SaveTask(task);

				// go back to previous page
				Navigation.PopAsync();
			};

			deleteBtn.Clicked += (sender, e) => {
				var task = (TaskItem)BindingContext;
				App.Database.DeleteTask(task.ID);

				// go back to previous page
				Navigation.PopAsync();
			};
				
			// Layout
			stackLayout = new StackLayout {
				Children = {
					taskNameLabel,
					taskNameEntry,
					taskDescLabel,
					taskDescEntry,
					taskDoneLabel,
					taskDoneSwitch,
					taskDateLabel,
					taskDatePicker,
					saveBtn,
					deleteBtn
				},
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(5),
			};
			Content = stackLayout;
			Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
		}
	}
}

