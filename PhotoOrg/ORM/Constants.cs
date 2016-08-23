using System;

namespace PhotoOrg.ORM
{
    class Constants
    {
        // Table Columns
        public static string ROW_ID = "id";
        public static string NAME = "name";
        public static string DATE = "date";
        public static string LOCATION = "location";
        public static string PEOPLE = "people";
        public static string LATITUDE = "latitude";
        public static string LONGITUDE = "longitude";
        public static string PICTUREPATH = "picturePath";

        // DB Properties
        public static string DB_NAME = "photos_DB";
        public static int DB_VERSION = 1;
        public static string TB_NAME = "photos_TB";

        // Create Table
        public static String CREATE_TB = "CREATE TABLE photos_TB (" +
                    "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "name VARCHAR(50), " +
                    "date VARCHAR(30), " +
                    "location VARCHAR(50), " +
                    "people VARCHAR(50), " +
                    "latitude VARCHAR(20), longitude VARCHAR(20), " +
                    "picturePath VARCHAR(255))";

        // Drop Table
        public static String DROP_TB = "DROP TABLE IF EXISTS" + TB_NAME;
    }
}