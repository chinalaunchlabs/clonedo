using System;
using Xamarin.Forms;

namespace CloneDo
{
	// How task is rendered in a ListView
	public class TaskDoneCell: TaskItemCell
	{
		public TaskDoneCell ()
		{
			taskDone.Source = "http://yworld.co.za/yack/images/tick.png";
			taskName.TextColor = Color.Gray;
			taskDate.SetBinding (Label.TextProperty, new Binding ("Date", converter: new DateConverter ()));
		}

		private class DateConverter : IValueConverter {
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

