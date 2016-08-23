using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using PhotoOrg.ORM;
using Android.Provider;
using Android.Database;

namespace PhotoOrg
{
    [Activity(Label = "Insert A Photo")]
    public class InsertPhoto : Activity
    {
        public static readonly int PickImageId = 1000;
        private ImageView _imageView;
        private Button btnAdd, btnGetImage;
        private TextView textView2, textView3, textView4, textView5;
        private EditText txtDate, txtLocation, txtPeople, txtName;
        private string _imagePath = null;

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
            textView5 = FindViewById<TextView>(Resource.Id.textView5);
            txtName = FindViewById<EditText>(Resource.Id.txtName);
            txtName.AfterTextChanged += enableSaveButton;

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
            if (string.IsNullOrWhiteSpace(_imagePath))
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
                textView5.Visibility = ViewStates.Visible;
                txtName.Visibility = ViewStates.Visible;

                _imageView.Visibility = ViewStates.Visible;
                btnGetImage.Visibility = ViewStates.Invisible;
                btnAdd.Visibility = ViewStates.Visible;
                btnAdd.Enabled = false;
            }
        }

        private void enableSaveButton(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) &&
                !string.IsNullOrWhiteSpace(txtDate.Text) &&
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
                _imageView.SetImageURI(data.Data);

                // Get the picture path
                _imagePath = getPicturePath(data.Data);

                // Since we have a picture, show it, hide the add button
                // and show the input titles and fields.
                _imageView.Visibility = ViewStates.Visible;
                btnGetImage.Visibility = ViewStates.Invisible;

                textView5.Visibility = ViewStates.Visible;
                txtName.Visibility = ViewStates.Visible;
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
            Intent.SetType("image/jpg");
            Intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EditText txtName = FindViewById<EditText>(Resource.Id.txtName);
            EditText txtDate = FindViewById<EditText>(Resource.Id.txtDate);
            EditText txtLocation = FindViewById<EditText>(Resource.Id.txtLocation);
            EditText txtPeople = FindViewById<EditText>(Resource.Id.txtPeople);
            _imageView = FindViewById<ImageView>(Resource.Id.imageSelection);

            Photos thisPhoto = new ORM.Photos();

            // TODO: implement getting latitude / longitude method
            thisPhoto.Latitude = "n/a";
            thisPhoto.Longitude = "n/a";
            thisPhoto.Name = txtName.Text;
            thisPhoto.Date = txtDate.Text;
            thisPhoto.Location = txtLocation.Text;
            thisPhoto.People = txtPeople.Text;
            thisPhoto.PicturePath = _imagePath;

            Save(thisPhoto);
        }
        public void Save(Photos photo)
        {
            DBAdapter db = new DBAdapter(this);
            db.OpenDB();
            if (db.insertRecord(photo))
            {
                db.CloseDB();
                StartActivity(typeof(MainActivity));
            }
            Toast.MakeText(this, "Oooops Insertion Failed", ToastLength.Long).Show();
            db.CloseDB();
        }
        public string getPicturePath(Android.Net.Uri uri)
        {
        // Thanks to Benoit Jadinon via StackOverFlow
        // http://stackoverflow.com/questions/23309080/android-file-path-xamarin
            string path = null;
            String[] projection = new[] { MediaStore.Images.Media.InterfaceConsts.Data };
            using (ICursor cursor = ContentResolver.Query(uri, projection, null, null, null))
            {
                if (cursor != null)
                {
                    int columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Audio.Media.InterfaceConsts.Data);
                    cursor.MoveToFirst();
                    path = cursor.GetString(columnIndex);
                }
            }
            return path;
        }
    }
}