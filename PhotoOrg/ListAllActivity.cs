using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;
using PhotoOrg.ORM;
using Android.Database;

namespace PhotoOrg
{
    [Activity(Label = "ListAllActivity")]
    public class ListAllActivity : Activity
    {
        private List<Photos> allPhotos = new List<Photos>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PhotoList);
            populatePhotosList();  
        }
        private void populatePhotosList()
        {
            // Construct the data source
            getThePhotos();

            // Create an adapter to convert the array to views
            PhotosAdapter adapter = new PhotosAdapter(this, allPhotos);

            ListView lv = FindViewById<ListView>(Resource.Id.photoListView);

            // Only use the apadter if there are items to show
            if (allPhotos.Count > 0)
            {
                lv.Adapter=adapter;
            }

            // Set the listeners on each list view row
            lv.ItemClick += Lv_ItemClick;
        }
        private void getThePhotos()
        {
            allPhotos.Clear();
            DBAdapter db = new DBAdapter(this);
            db.OpenDB();
            ICursor cursor = db.GetPhotos();
            while (cursor.MoveToNext())
            {
                Photos thisphoto = new Photos();
                thisphoto.Id = int.Parse(cursor.GetString(0));
                thisphoto.Latitude = cursor.GetString(4);
                thisphoto.Longitude = cursor.GetString(5);
                thisphoto.PicturePath = cursor.GetString(7);
                thisphoto.Name = cursor.GetString(1);
                thisphoto.Date = cursor.GetString(2);
                thisphoto.Location = cursor.GetString(3);
                thisphoto.People = cursor.GetString(4);
                Toast.MakeText(this, "ID = " + thisphoto.Id, ToastLength.Long).Show();
                allPhotos.Add(thisphoto);
            }
            db.CloseDB();
            return;
        }
        private void Lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, "You clicked " + allPhotos[e.Position].Name, ToastLength.Short).Show();
        }
    }
}