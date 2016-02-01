using System;
using Xamarin.Forms;

namespace CloneDo
{
	// How task is rendered in a ListView
	public class TaskItemCell: ViewCell
	{
		public TaskItemCell ()
		{
			Label 		taskDescription, taskName, taskDate;
			Image 		taskDone;
			StackLayout nameAndDescWrapper, wholeLayout;

			taskName = new Label {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				FontAttributes = FontAttributes.Bold
			};
			taskDescription = new Label {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
			};
			taskDone = new Image {
				Source = "https://cdn4.iconfinder.com/data/icons/tupix-1/30/checkmark-128.png",
				Aspect = Aspect.AspectFit,
				HorizontalOptions = LayoutOptions.End
			};
			taskDate = new Label {
				Text = "Date here",
				HorizontalOptions = LayoutOptions.FillAndExpand,
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
			};

			taskName.SetBinding (Label.TextProperty, "Task");
			taskDescription.SetBinding (Label.TextProperty, "Description");
			taskDone.SetBinding (Image.IsVisibleProperty, "Done");
			taskDate.SetBinding (Label.TextProperty, new Binding("Date", stringFormat: "{0:dd/MM/yyyy}"));

			nameAndDescWrapper = new StackLayout {
				Children = {
					taskName,
					taskDate,
					taskDescription,
				},
				HorizontalOptions = LayoutOptions.StartAndExpand
			};
			wholeLayout = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				VerticalOptions = LayoutOptions.Fill,
				Children = {
					nameAndDescWrapper,
					taskDone
				},
				Padding = new Thickness(10),
			};

			View = wholeLayout;

		}
	}
}

