using System;
using Android.Content;
using Android.Database.Sqlite;
using Android.Database;
using Android.Widget;

namespace PhotoOrg.ORM
{
    class DBAdapter:IDisposable
    {
        private Context c;
        private DBHelper helper;
        private SQLiteDatabase db;

        public DBAdapter(Context c)
        {
            this.c = c;
            helper = new DBHelper(c);
        }
        // Open DB Connection
        public DBAdapter OpenDB()
        {
            try
            {
                db = helper.WritableDatabase;
            }
            catch (Exception ex)
            {
                Console.WriteLine("** Database Creation Error ** " + ex.Message);
            }
            return this;
        }
        // CLose DB Connection
        public void CloseDB()
        {
            try
            {
                helper.Close();
                helper.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("** Database Close Error ** " + ex.Message);
            }
        }

        // Insert Data
        public bool insertRecord(Photos photo)
        {
            try
            {
                 db.ExecSQL("INSERT INTO " + Constants.TB_NAME +
                    " (" + Constants.DATE + ", " + Constants.NAME + 
                    ", " + Constants.LOCATION + ", " + Constants.LONGITUDE +
                    ", " + Constants.LATITUDE + ", " + Constants.PEOPLE + 
                    ", " + Constants.PICTUREPATH + ") " +
                    "VALUES ('" + photo.Date + "', '" + photo.Name + 
                    "', '" + photo.Location + "', '" + photo.Longitude +
                    "', '"+ photo.Latitude +"', '" +
                    photo.People + "', '" + photo.PicturePath +"');");
                Console.WriteLine("***** Apparent Insertion Success *****");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("** Database Insertion Error ** " + ex.Message);
            }
            return false;
        }

        // Retrieve Data
        public ICursor GetPhotos()
        {
            String[] columns= { Constants.ROW_ID, Constants.NAME,
                                Constants.DATE, Constants.LOCATION,
                                Constants.LATITUDE, Constants.LONGITUDE,
                                Constants.PEOPLE, Constants.PICTUREPATH};

            // Parameters are table name, column names,selection, selection args,
            // groupby, having, and finally orderby
            return db.Query(Constants.TB_NAME,columns,null,null,null,null,null);
        }
        public Photos getById(int id)
        {
            Photos thisPhoto = null;
            string sql = "Select * From "+Constants.TB_NAME+ " where id=?";
            string[] arguments = { "" + id };
            var cursor = db.RawQuery(sql,arguments);
            if (cursor.Count > 0)
            {
                thisPhoto = cursorToPhoto(cursor, 0);
            }
            return thisPhoto;
        }
        // Code to update a record using ORM
        public Boolean updateRecord(Photos photo)
        {
            try
            {
                ContentValues cv = new ContentValues();
                cv.Put(Constants.NAME, photo.Name);
                cv.Put(Constants.DATE, photo.Date);
                cv.Put(Constants.LOCATION, photo.Location);
                cv.Put(Constants.PEOPLE, photo.People);
                db.Update(Constants.TB_NAME, cv, "id=" + photo.Id, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine("******  Update Error ******" + ex.Message);
                return false;
            }
            return true;
        }
        public Boolean deleteRecord(int id)
        {
            string[] whereArgs = { "" + id };
            if (getById(id) != null)
            {
                db.Delete(Constants.TB_NAME, "id=?", whereArgs);
                return true;
            }
            return false;
        }
        private Photos cursorToPhoto(ICursor cursor, int position)
        {
            cursor.MoveToPosition(position);
            Photos thisPhoto = new Photos();

            thisPhoto.Id = int.Parse(cursor.GetString(cursor.GetColumnIndex("id")));
            thisPhoto.Latitude = cursor.GetString(cursor.GetColumnIndex("latitude"));
            thisPhoto.Longitude = cursor.GetString(cursor.GetColumnIndex("longitude"));
            thisPhoto.PicturePath = cursor.GetString(cursor.GetColumnIndex("picturePath"));
            thisPhoto.Name = cursor.GetString(cursor.GetColumnIndex("name"));
            thisPhoto.Date = cursor.GetString(cursor.GetColumnIndex("date"));
            thisPhoto.Location = cursor.GetString(cursor.GetColumnIndex("location"));
            thisPhoto.People = cursor.GetString(cursor.GetColumnIndex("people"));

            return thisPhoto;
        }

        public void Dispose()
        {
            if (helper != null) {helper.Dispose(); helper = null; }
            db.Close();
        }
    }
}