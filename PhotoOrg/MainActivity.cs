using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PhotoOrg.ORM;

namespace PhotoOrg
{
    [Activity(Label = "Photo Organizer", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "home" layout resource
            SetContentView(Resource.Layout.Home);

            // Create the database (note the using PhotoOrg.ORM above)
            DBRepo dbr = new DBRepo();
            var result = dbr.createDB();
            // Popup message that disappears in a Short time.
            Toast.MakeText(this, result, ToastLength.Short).Show();

            // Create the Photos table
            result = dbr.createTable ();
            // Popup message that disappears in a Long time.
            Toast.MakeText(this, result, ToastLength.Long).Show();

            // To insert the record
            Button btnAddRecord = FindViewById<Button>(Resource.Id.btnAdd);
            btnAddRecord.Click += btnAddRecord_Click;

            // To retrieve all data
            Button btnGetAll = FindViewById<Button>(Resource.Id.btnGetAll);
            btnGetAll.Click += btnGetAll_Click;

            // To retrieve a specific record
            Button btnGetById = FindViewById<Button>(Resource.Id.btnGetId);
            btnGetById.Click += btnGetById_Click;

            // To update the record
            Button btnUpdate = FindViewById<Button>(Resource.Id.btnUpdate);
            btnUpdate.Click += btnUpdate_Click;

            // To delete a record
            Button btnDelete = FindViewById<Button>(Resource.Id.btnDelete);
            btnDelete.Click += btnDelete_Click;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(DeleteActivity));
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(UpdatePhotoActivity));
        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SearchActivity));
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            DBRepo dbr = new DBRepo();
            var result = dbr.getAllRecords();
            Toast.MakeText(this, result, ToastLength.Long).Show();
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(InsertPhoto));
        }
    }
}
