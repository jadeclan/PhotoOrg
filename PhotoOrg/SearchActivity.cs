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
            DBRepo dbr = new DBRepo();
            EditText txtId = FindViewById<EditText>(Resource.Id.txtPhotoID);
            Photos thisPhoto = dbr.getById(int.Parse(txtId.Text));
            Toast.MakeText(this, thisPhoto.Location, ToastLength.Short).Show();
        }
    }
}