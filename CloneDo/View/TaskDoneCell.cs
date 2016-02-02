using System;
using Xamarin.Forms;

namespace CloneDo
{
	// How task is rendered in a ListView
	public class TaskDoneCell: ViewCell
	{
		public TaskDoneCell ()
		{
			Label 		taskDescription, taskName, taskDate;
			Image 		taskDone;
			StackLayout nameAndDescWrapper, wholeLayout;

			taskName = new Label {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				FontAttributes = FontAttributes.Bold,
				TextColor = Color.Gray
			};
			taskDescription = new Label {
				VerticalOptions = LayoutOptions.Fill,
				HorizontalOptions = LayoutOptions.Fill,
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
			};
			taskDone = new Image {
				Source = "http://yworld.co.za/yack/images/tick.png",
				Aspect = Aspect.AspectFit,
				HorizontalOptions = LayoutOptions.End
			};
			taskDate = new Label {
				Text = "Date here",
				HorizontalOptions = LayoutOptions.Fill,
				FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label))
			};

			// Binding
			taskName.SetBinding (Label.TextProperty, "Task");
			taskDescription.SetBinding (Label.TextProperty, "Description");
			taskDone.SetBinding (Image.IsVisibleProperty, "Done");
			taskDate.SetBinding (Label.TextProperty, new Binding ("Date", converter: new DateConverter()));

			// MenuItems
			var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true };
			deleteAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
			deleteAction.Clicked += (sender, e) => {
				var mi = ((MenuItem)sender);
				TaskItem t = (TaskItem) mi.CommandParameter;
				App.Database.DeleteTask(t.ID);

				// this > listview > stacklayout > scrollview > contentpage
				// wow this is unacceptable
				// TODO: Turn into delegate
				((TodoList)((ScrollView)((StackLayout)((ListView)this.Parent).Parent).Parent).Parent).Refresh();
			};

			ContextActions.Add (deleteAction);

			// Layout
			nameAndDescWrapper = new StackLayout {
				Children = {
					taskName,
					taskDate,
					//					taskDescription,
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
				Padding = new Thickness(10, 5),
			};

			View = wholeLayout;

		}

		class DateConverter : IValueConverter {
			public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
				DateTime date = (DateTime) value;
				return date.ToString ("MMMM dd yyyy").ToLower();
			}

			public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
				throw new NotImplementedException ();
			}
		}
	}

}

