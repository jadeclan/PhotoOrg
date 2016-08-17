
using System;
using System.Data;
using System.IO;
using SQLite;


namespace PhotoOrg.ORM
{
    public class DBRepo
        {
        private string path;
        public DBRepo()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            path = Path.Combine(folder, "photos.db3);");
        }
        
        /// <summary>
        /// Method to create the database
        /// </summary>
        /// <returns>String indicating success</returns>
        public string createDB()
        {
            string output = "Creating DB...";
            var db = new SQLiteConnection(path);
            output += "\nDB Created successully...";
            return output;
        }
        /// <summary>
        /// Public method to create the table required
        /// </summary>
        /// <returns></returns>
        public string createTable()
        {
            try
            {
                var db = new SQLiteConnection(path);
                db.CreateTable<Photos>();
                return "Photos table created successfully...";
            }
            catch (Exception ex)
            {
                return "ERROR - Failed to create table: " + ex.Message;
            }
        }

        public string addRecord(Photos photo)
        {
            try
            {
                var db = new SQLiteConnection(path);
                db.Insert(photo);
                return "Photo added...";
            }
            catch (Exception ex)
            {
                return "ERROR - Insert failed: " + ex.Message;
            }
        }

        public string getAllRecords()
        {
            try
            {
                var db = new SQLiteConnection(path);
                string output = "Retrieving the data using ORM...";
                var table = db.Table<Photos>();
                foreach (var item in table)
                {
                    output += "\n" + item.Id +
                              " - " + item.Location + 
                              " - " + item.Date + 
                              " - " + item.People;
                    if(!string.IsNullOrWhiteSpace(item.PicturePath))
                    {
                        output += " - " + item.PicturePath;
                    }
                }
                return output;
            }
            catch (Exception ex)
            {
                return "ERROR - Insert failed: " + ex.Message;
            }
        }
        public Photos getById(int id)
        {
            var db = new SQLiteConnection(path);
            var item = db.Get<Photos>(id);
            return item;
        }
        // Code to update a record using ORM
        public string updateRecord(int id, Photos photo)
        {
            var db = new SQLiteConnection(path);
            Photos item = db.Get<Photos>(id);
            item.Date = photo.Date;
            item.Location = photo.Location;
            item.People = photo.People;
            SQLiteCommand cmd = new SQLiteCommand(db);
            cmd.CommandText = "Update Photos set People='" + photo.People + "' where Id='" + photo.Id + "'";
            var result = cmd.ExecuteNonQuery();
            //db.Update(item);
            return "Record updated...";
        }
        public string deleteRecord(int id)
        {
            var db = new SQLiteConnection(path);
            Photos item = db.Get<Photos>(id);
            db.Delete(item);
            return "Record deleted...";
        }
    }
}