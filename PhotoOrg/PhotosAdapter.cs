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
using Android.Graphics;
using Java.IO;

namespace PhotoOrg
{
    class PhotosAdapter : BaseAdapter<Photos>
    {
        private readonly Activity _context;
        private List<Photos> _allPhotos;
        public PhotosAdapter(Activity context, List<Photos> allPhotos)
        {
            _context = context;
            _allPhotos = allPhotos;
        }

        public override Photos this[int position]
        {
            get
            {
               return _allPhotos[position];
            }
        }

        public override int Count
        {
            get
            {
                return _allPhotos.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return _allPhotos[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // Get the data for this position
            Photos thisPhoto = _allPhotos[position];
            // Check if an existing view is being reused, otherwise inflate the view
            if (convertView == null)
            {
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.ItemLayout, null);
            }
            // TODO:  Need to standardize naming conventions for view items
            ImageView picture = (ImageView)convertView.FindViewById(Resource.Id.listImageView);
            TextView name = (TextView)convertView.FindViewById(Resource.Id.nameTextView);
            TextView location = (TextView)convertView.FindViewById(Resource.Id.locationTextView);
            TextView date = (TextView)convertView.FindViewById(Resource.Id.dateTextView);

            var uri = Android.Net.Uri.Parse(thisPhoto.PicturePath);
            picture.SetImageURI(uri);
 
            name.Text = thisPhoto.Name;
            date.Text = thisPhoto.Date;
            location.Text = thisPhoto.Location;

            // Return the completed view to render on screen
            return convertView;
        }
        public Android.Graphics.Bitmap GetImageBitMap(string picturePath)
        {
            Bitmap imageBitMap = null;

            File imageFile = new File(picturePath);
            try
            { 
                imageBitMap = BitmapFactory.DecodeFile(picturePath);
                Toast.MakeText(_context, "File Exists", ToastLength.Long).Show();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("****** \n****** "+ex.Message+"\n*****");
            }
            return imageBitMap;
        }
    }
}