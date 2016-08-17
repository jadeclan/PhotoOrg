using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PhotoOrg.ORM;

namespace PhotoOrg
{
    [Activity(Label = "UpdatePhotoActivity")]
    public class UpdatePhotoActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.updateLayout);
            // Search Button Click
            Button btnIdSearch = FindViewById<Button>(Resource.Id.btnIdSearch);
            btnIdSearch.Click += btnIdSearch_Click;
            // Update Button Click
            Button btnUpdate = FindViewById<Button>(Resource.Id.btnUpdate);
            btnUpdate.Click += btnUpdate_Click;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            EditText txtId = FindViewById<EditText>(Resource.Id.txtPhotoId);
            EditText txtDate = FindViewById<EditText>(Resource.Id.txtDate);
            EditText txtLocation = FindViewById<EditText>(Resource.Id.txtLocation);
            EditText txtPeople = FindViewById<EditText>(Resource.Id.txtPeople);

            DBRepo dbr = new DBRepo();
            Photos thisPhoto = new Photos();
            thisPhoto.Id = int.Parse(txtId.Text);
            thisPhoto.Latitude = null;
            thisPhoto.Longitude = null;
            thisPhoto.PicturePath = null;
            thisPhoto.Date = txtDate.Text;
            thisPhoto.Location = txtLocation.Text;
            thisPhoto.People = txtPeople.Text;

            string result = dbr.updateRecord(int.Parse(txtId.Text), thisPhoto);
            Toast.MakeText(this, result, ToastLength.Long).Show();
        }

        private void btnIdSearch_Click(object sender, EventArgs e)
        {
            DBRepo dbr = new DBRepo();
            EditText txtId = FindViewById<EditText>(Resource.Id.txtPhotoId);
            EditText txtDate = FindViewById<EditText>(Resource.Id.txtDate);
            EditText txtLocation = FindViewById<EditText>(Resource.Id.txtLocation);
            EditText txtPeople = FindViewById<EditText>(Resource.Id.txtPeople);

            Photos thisPhoto = dbr.getById(int.Parse(txtId.Text));

            txtId.Text = "" + thisPhoto.Id;
            txtDate.Text = thisPhoto.Date;
            txtLocation.Text = thisPhoto.Location;
            txtPeople.Text = thisPhoto.People;
        }
    }
}