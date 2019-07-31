using Android.App;
using Android.OS;
using Android.Widget;
using EmployeeManager;

namespace AdmissionDB
{
	[Activity(Label = "Read")]
	public class Read : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_read);

			TextView textView = (TextView)FindViewById(Resource.Id.maintext);
			textView.Text = MainActivity.message;
			MainActivity.message = "";
		}
	}
}
