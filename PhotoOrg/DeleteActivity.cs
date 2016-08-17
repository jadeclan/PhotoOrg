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
    [Activity(Label = "DeleteActivity")]
    public class DeleteActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DeletionLayout);
            // Create your application here
            Button btnDelete = FindViewById<Button>(Resource.Id.btnDelete);
            btnDelete.Click += btnDelete_Click;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DBRepo dbr = new ORM.DBRepo();
            EditText txtPhotoId = FindViewById<EditText>(Resource.Id.txtDeleteID);

            string result = dbr.deleteRecord(int.Parse(txtPhotoId.Text));
            Toast.MakeText(this, result, ToastLength.Long).Show();
        }
    }
}