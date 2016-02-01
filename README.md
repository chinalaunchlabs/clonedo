# Clone-Do
Yet another todo list manager app.

## What this is
* Shameless clone of the Xamarin Todo project ([link](https://github.com/xamarin/xamarin-forms-samples/tree/master/Todo)).
* Uses shared project to implement SQLite access (see [this](http://github.com/chinalaunchlabs/reference-sqlite) for more information).

## To Do
* Re-implement using PCL?
* ~~Add due date (DatePicker).~~
* Group tasks (Done, Not done - sorted in descending order)
* UI issues
	* Try to do the floating action bar for Android?
* Remove the damn title icon.

## Notes
* Binding non-string object to a label:
```
dateLabel.SetBinding(Label.TextProperty, new Binding(path: "Date", stringFormat: "{0:dd/MM/yyyy}"));
```
([source](https://forums.xamarin.com/discussion/comment/57802/#Comment_57802))
* When creating a task item (with a member of type DateTime), the default value of this member in the database is DatePicker.MinimumDate, which is why `datePicker.Date = DateTime.Today` and other variations thereof seemingly do not work.
	* Solution: Constructor should initialize the date member.
* For ListView to dynamically resize based on content, set `HasUnevenRows`.
* `ActionBar.SetIcon(null)` (Android) to hide the icon on the actionbar but still show title and menu.