using System;
using System.IO;
using AdmissionDB;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using ApplicationDB;
using SQLite;

namespace EmployeeManager
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        //All varibales
        public static string dbPath;
        public static string message;
        public static SQLiteConnection db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);


            //Assigning Views with their layout ID
            EditText IDEmp = (EditText)FindViewById(Resource.Id.id_emp);
            EditText NameEmp = (EditText)FindViewById(Resource.Id.name_emp);
            EditText PhoneEmp = (EditText)FindViewById(Resource.Id.phone_emp);
            EditText RemoveID = (EditText)FindViewById(Resource.Id.removeid_emp);
            Button CreateEntry = (Button)FindViewById(Resource.Id.Btn_Create);
            Button UpdateEntry = (Button)FindViewById(Resource.Id.Btn_Update);
            Button DeleteEntry = (Button)FindViewById(Resource.Id.Btn_Delete);
            Button ReadEntry = (Button)FindViewById(Resource.Id.Btn_Read);

            dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "data.db3");
            db = new SQLiteConnection(dbPath);
            db.CreateTable<Employee>();


            //Create Function
            CreateEntry.Click += delegate {
                Employee employee = new Employee(Int32.Parse(IDEmp.Text), NameEmp.Text, PhoneEmp.Text);
                db.Insert(employee);
                Toast.MakeText(Application.Context, "Created a new employee profile", ToastLength.Long).Show();
            };

            //Update Function
            UpdateEntry.Click += delegate {
                var table = db.Table<Employee>();

                int i = 0;
                foreach (var item in table)
                {
                    if (item.ID.ToString().Equals(IDEmp.Text))
                    {
                        db.Execute("UPDATE Employee SET name = ?, phoneNumber = ? Where id = ?", NameEmp.Text, PhoneEmp.Text, IDEmp.Text);
                        Toast.MakeText(Application.Context, "Updated employee profile associated with Employee No.: " + IDEmp.Text, ToastLength.Long).Show();
                        i = 1;
                        break;
                    }
                }

                //If no record was found
                if (i == 0)
                {
                    Toast.MakeText(Application.Context, "Profile with this Employee No. hasn't been created yet", ToastLength.Long).Show();
                }
            };

            //Delete Function
            DeleteEntry.Click += delegate {

                var table = db.Table<Employee>();

                int i = 0;
                foreach (var item in table)
                {
                    if (item.ID.ToString().Equals(RemoveID.Text))
                    {

                        //Update Query
                        string query = "delete from Employee where id=" + RemoveID.Text;
                        db.Execute(query);
                        Toast.MakeText(Application.Context, "Deleted employee profile associated with Employee No.: " + RemoveID.Text, ToastLength.Long).Show();
                        i = 1;
                        break;
                    }
                }

                //If no record was found
                if (i == 0)
                {
                    Toast.MakeText(Application.Context, "Profile with this Employee No. hasn't been created yet", ToastLength.Long).Show();
                }

            };

            message = "";

            //Read Function
            ReadEntry.Click += delegate {
                var table = db.Table<Employee>();


                foreach (var item in table)
                {
                    message = message + item.ID + "," + item.Name + "," + item.PhoneNumber + "\n";
                }
                StartActivity(new Android.Content.Intent(this, typeof(Read)));
            };


        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

