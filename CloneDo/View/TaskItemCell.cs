using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CloneDo
{
	// How task is rendered in a ListView
	public class TaskItemCell: ViewCell
	{
		public delegate void TaskItemEvent (TaskItem t);
		public static event TaskItemEvent TaskDeleted;

		protected Label 		taskDescription, taskName, taskDate;
		protected Image 		taskDone;
		protected StackLayout	nameAndDescWrapper, wholeLayout;

		public TaskItemCell ()
		{
			Setup ();
		}

		protected void Setup() {
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
				Aspect = Aspect.AspectFit,
				HorizontalOptions = LayoutOptions.End
			};
			taskDate = new Label {
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
				TaskItem t = (TaskItem)((MenuItem)sender).CommandParameter;
				TaskItemEvent handler = TaskDeleted;
				if (handler != null)
					handler(t);
			};

			ContextActions.Remove (deleteAction);
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

		private class DateConverter : IValueConverter {
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


		private class OverdueConverter : IValueConverter {
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
}

