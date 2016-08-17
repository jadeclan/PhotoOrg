using System;
using System.Data;
using System.IO;
using SQLite;

namespace PhotoOrg.ORM
{
    [Table("Photos")]
    public class Photos
    {
        // TODO: implement getting lat/long based on location
        // TODO: implement setting picture path
        [PrimaryKey, AutoIncrement]
        public int Id{get;set;}
        public string Date { get; set; }
        [MaxLength(50)]
        public string Location { get; set; }
        public string People { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public string PicturePath { get; set; }
    }
}