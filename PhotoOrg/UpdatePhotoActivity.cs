using System;

using Android.App;
using Android.OS;
using Android.Widget;
using Android.Views;
using PhotoOrg.ORM;

namespace PhotoOrg
{
    [Activity(Label = "UpdatePhotoActivity")]
    public class UpdatePhotoActivity : Activity
    {
        EditText txtId, txtName, txtDate, txtLocation, txtPeople;
        TextView textView2, textView3, textView4, textView5, textView6;
        Button btnIdSearch, btnUpdate;
        Photos thisPhoto;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.updateLayout);
            // Search Button Click
            btnIdSearch = FindViewById<Button>(Resource.Id.btnIdSearch);
            btnIdSearch.Click += btnIdSearch_Click;
            // Update Button Click
            btnUpdate = FindViewById<Button>(Resource.Id.btnUpdate);
            btnUpdate.Click += btnUpdate_Click;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            thisPhoto.Name = txtName.Text;
            thisPhoto.Date = txtDate.Text;
            thisPhoto.Location = txtLocation.Text;
            thisPhoto.People = txtPeople.Text;
            DBAdapter db = new DBAdapter(this);
            db.OpenDB();
            if (db.updateRecord(thisPhoto))
            {
                Toast.MakeText(this, "Photo Info Updated Successfully", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this, "Ooops Info Not Updated", ToastLength.Long).Show();
            }
            db.CloseDB();
            StartActivity(typeof(MainActivity));
        }

        private void btnIdSearch_Click(object sender, EventArgs e)
        {
            txtId = FindViewById<EditText>(Resource.Id.txtPhotoId);
            // TODO: Check for valid ID
            DBAdapter db = new DBAdapter(this);
            db.OpenDB();
            thisPhoto = db.getById(int.Parse(txtId.Text));
            db.CloseDB();
            if (thisPhoto == null)
            {
                Toast.MakeText(this, "Invalid ID selected", ToastLength.Long).Show();
                txtId.Text = "";
                StartActivity(typeof(UpdatePhotoActivity));
            }
            // Now we have valid photo so hide the search fields and show found fields
            btnIdSearch.Visibility = ViewStates.Gone;
            txtId.Visibility = ViewStates.Gone;
            textView2 = FindViewById<TextView>(Resource.Id.textView2);
            textView2.Visibility = ViewStates.Gone;

            ImageView imageSelected = FindViewById<ImageView> (Resource.Id.imageSelected);
            var uri = Android.Net.Uri.Parse(thisPhoto.PicturePath);
            imageSelected.SetImageURI(uri);
            imageSelected.Visibility = ViewStates.Visible;

            textView6 = FindViewById<TextView>(Resource.Id.textView6);
            textView6.Visibility = ViewStates.Visible;
            txtName = FindViewById<EditText>(Resource.Id.txtName);
            txtName.Text = thisPhoto.Name;
            txtName.Visibility = ViewStates.Visible;
            txtName.AfterTextChanged += enableUpdateButton;

            textView3 = FindViewById<TextView>(Resource.Id.textView3);
            textView3.Visibility = ViewStates.Visible;
            txtDate = FindViewById<EditText>(Resource.Id.txtDate);
            txtDate.Text = thisPhoto.Date;
            txtDate.Visibility = ViewStates.Visible;
            txtDate.AfterTextChanged += enableUpdateButton;

            textView4 = FindViewById<TextView>(Resource.Id.textView4);
            textView4.Visibility = ViewStates.Visible;
            txtLocation = FindViewById<EditText>(Resource.Id.txtLocation);
            txtLocation.Text = thisPhoto.Location;
            txtLocation.Visibility = ViewStates.Visible;
            txtLocation.AfterTextChanged += enableUpdateButton;

            textView5 = FindViewById<TextView>(Resource.Id.textView5);
            textView5.Visibility = ViewStates.Visible;
            txtPeople = FindViewById<EditText>(Resource.Id.txtPeople);
            txtPeople.Text = thisPhoto.People;
            txtPeople.Visibility = ViewStates.Visible;
            txtPeople.AfterTextChanged += enableUpdateButton;

            btnUpdate.Visibility = ViewStates.Visible;
        }
        private void enableUpdateButton(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) &&
                !string.IsNullOrWhiteSpace(txtDate.Text) &&
                !string.IsNullOrWhiteSpace(txtLocation.Text) &&
                !string.IsNullOrWhiteSpace(txtPeople.Text))
            {
                btnUpdate.Enabled = true;
            }
        }
    }
}