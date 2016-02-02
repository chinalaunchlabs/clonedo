using System;
using System.Diagnostics;
using System.Threading.Tasks;
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

			// Views
			taskName = new Label {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				FontAttributes = FontAttributes.Bold
			};
			taskDescription = new Label {
				VerticalOptions = LayoutOptions.Fill,
				HorizontalOptions = LayoutOptions.Fill,
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
				FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label))
			};

			// Data Binding
			taskName.SetBinding (Label.TextProperty, "Task");
			taskDescription.SetBinding (Label.TextProperty, "Description");
			taskDone.SetBinding (Image.IsVisibleProperty, "Done");
			taskDate.SetBinding (Label.TextProperty, new Binding ("Date", converter: new DateConverter ()));
			taskName.SetBinding (Label.TextColorProperty, new Binding ("Date", converter: new OverdueConverter ()));

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
//					taskDescription,
					taskDate,
				},
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(0)
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
				int overdue = date.CompareTo (DateTime.Today);
				if (overdue < 0)
					return "overdue";
				else if (overdue == 0)
					return "due today";
				else
					return "due " + date.ToString ("MMMM dd yyyy").ToLower();
			}

			public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
				throw new NotImplementedException ();
			}
		}
	}


	class OverdueConverter : IValueConverter {
		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			DateTime date = (DateTime) value;

			bool overDue = date.CompareTo (DateTime.Today) < 0;
			return overDue ? Color.Red : Color.Black;

		}

		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException ();
		}
	}
}

