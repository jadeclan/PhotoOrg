using System;

using Android.App;
using Android.OS;
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
            EditText txtPhotoId = FindViewById<EditText>(Resource.Id.txtDeleteID);
            // TODO: Input validation
            DBAdapter db = new DBAdapter(this);
            db.OpenDB();
            if (!db.deleteRecord(int.Parse(txtPhotoId.Text)))
            {
                Toast.MakeText(this, "Deletion Failed", ToastLength.Long).Show();
            }
            db.CloseDB();

            StartActivity(typeof(MainActivity));
        }
    }
}