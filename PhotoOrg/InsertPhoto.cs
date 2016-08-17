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
using Android.Util;

namespace PhotoOrg
{
    [Activity(Label = "Insert A Photo")]
    public class InsertPhoto : Activity
    {
        public static readonly int PickImageId = 1000;
        private ImageView _imageView;
        private Button btnAdd, btnGetImage;
        private TextView textView2, textView3, textView4;
        private EditText txtDate, txtLocation, txtPeople;
        private string _imageURI;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.InsertPhoto);

            // Set up selecting an image and adding it
            _imageView = FindViewById<ImageView>(Resource.Id.imageSelection);
            btnGetImage = FindViewById<Button>(Resource.Id.btnPickImage);
            btnGetImage.Click += btnGetImage_Click;

            btnAdd = FindViewById<Button>(Resource.Id.btnAddPhoto);
            btnAdd.Click += btnAdd_Click;

            // Instantiate the rest of the layout fields
            textView2 = FindViewById<TextView>(Resource.Id.textView2);
            txtDate = FindViewById<EditText>(Resource.Id.txtDate);
            txtDate.AfterTextChanged += enableSaveButton;

            textView3 = FindViewById<TextView>(Resource.Id.textView3);
            txtLocation = FindViewById<EditText>(Resource.Id.txtLocation);
            txtLocation.AfterTextChanged += enableSaveButton;

            textView4 = FindViewById<TextView>(Resource.Id.textView4);
            txtPeople = FindViewById<EditText>(Resource.Id.txtPeople);
            txtPeople.AfterTextChanged += enableSaveButton;

            // Display image and add if image is selected, hide getImage button
            if (string.IsNullOrWhiteSpace(_imageURI))
            {
                _imageView.Visibility = ViewStates.Invisible;
                btnGetImage.Visibility = ViewStates.Visible;
                btnAdd.Visibility = ViewStates.Invisible;
            }
            else
            {
                textView2.Visibility = ViewStates.Visible;
                txtDate.Visibility = ViewStates.Visible;
                textView3.Visibility = ViewStates.Visible;
                txtLocation.Visibility = ViewStates.Visible;
                textView4.Visibility = ViewStates.Visible;
                txtPeople.Visibility = ViewStates.Visible;

                _imageView.Visibility = ViewStates.Visible;
                btnGetImage.Visibility = ViewStates.Invisible;
                btnAdd.Visibility = ViewStates.Visible;
                btnAdd.Enabled = false;

            }
        }

        private void enableSaveButton(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtDate.Text) &&
                !string.IsNullOrWhiteSpace(txtLocation.Text) &&
                !string.IsNullOrWhiteSpace(txtPeople.Text))
            {
                btnAdd.Enabled = true;
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
            {
                Android.Net.Uri uri = data.Data;
                _imageURI = "" + uri;
                _imageView.SetImageURI(uri);

                // Since we have a picture, show it, hide the add button
                // and show the input titles and fields.
                _imageView.Visibility = ViewStates.Visible;
                btnGetImage.Visibility = ViewStates.Invisible;

                textView2.Visibility = ViewStates.Visible;
                txtDate.Visibility = ViewStates.Visible;
                textView3.Visibility = ViewStates.Visible;
                txtLocation.Visibility = ViewStates.Visible;
                textView4.Visibility = ViewStates.Visible;
                txtPeople.Visibility = ViewStates.Visible;
                btnAdd.Visibility = ViewStates.Visible;
            }
        }

        private void btnGetImage_Click(object sender, EventArgs e)
        {
            Intent = new Intent();
            Intent.SetType("image/*");
            Intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EditText txtDate = FindViewById<EditText>(Resource.Id.txtDate);
            EditText txtLocation = FindViewById<EditText>(Resource.Id.txtLocation);
            EditText txtPeople = FindViewById<EditText>(Resource.Id.txtPeople);
            _imageView = FindViewById<ImageView>(Resource.Id.imageSelection);

            DBRepo db = new DBRepo();
            Photos thisPhoto = new ORM.Photos();
            // TODO: implement getting latitude / longitude method
            thisPhoto.Latitude = null;
            thisPhoto.Longitude = null;
            thisPhoto.PicturePath = _imageURI;
            thisPhoto.Date = txtDate.Text;
            thisPhoto.Location = txtLocation.Text;
            thisPhoto.People = txtPeople.Text;
            string result = db.addRecord(thisPhoto);
            Toast.MakeText(this, result, ToastLength.Short).Show();
        }
    }
}