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
    [Activity( Label = "Photo Organizer", 
        MainLauncher = true, 
                Icon = "@drawable/photoOrganizer")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "home" layout resource
            SetContentView(Resource.Layout.Home);

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
            StartActivity(typeof(ListAllActivity));
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(InsertPhoto));
        }
    }
}
