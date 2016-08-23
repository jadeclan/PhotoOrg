using System;

using Android.App;
using Android.OS;
using Android.Widget;
using PhotoOrg.ORM;

namespace PhotoOrg
{
    [Activity(Label = "SearchActivity")]
    public class SearchActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Search);

            Button btnSearch = FindViewById<Button>(Resource.Id.btnSearch);
            btnSearch.Click += btnSearch_Click;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            EditText txtId = FindViewById<EditText>(Resource.Id.txtPhotoID);

            // Get the photo from the id selected
            DBAdapter db = new DBAdapter(this);
            db.OpenDB();
            Photos thisPhoto = db.getById(int.Parse(txtId.Text));
            db.CloseDB();

            if (thisPhoto == null)
            {
                Toast.MakeText(this, "Invalid ID selected", ToastLength.Long).Show();
                txtId.Text = "";
            }

            SetContentView(Resource.Layout.SinglePhotoLayout);

            // Get the views
            ImageView image = FindViewById<ImageView>(Resource.Id.singleImageView);
            TextView name = FindViewById<TextView>(Resource.Id.singleImageNameTextView);
            TextView date = FindViewById<TextView>(Resource.Id.singleImageDateTextView);
            TextView location = FindViewById<TextView>(Resource.Id.singleImageLocationTextView);
            TextView people = FindViewById<TextView>(Resource.Id.singleImagePeopleTextView);
            TextView latitude = FindViewById<TextView>(Resource.Id.singleImageLatitudeTextView);
            TextView longitude = FindViewById<TextView>(Resource.Id.singleImageLongitudeTextView);

            // Set the views
            var uri = Android.Net.Uri.Parse(thisPhoto.PicturePath);
            image.SetImageURI(uri);

            name.Text = thisPhoto.Name;
            date.Text = thisPhoto.Date;
            location.Text = thisPhoto.Location;
            latitude.Text = thisPhoto.Latitude;
            longitude.Text = thisPhoto.Longitude;
            people.Text = thisPhoto.People;
        }
    }
}