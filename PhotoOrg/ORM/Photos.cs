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
        public int Id{get;set;}
        public string Name { get; set; }
        public string Date { get; set; }
        public string Location { get; set; }
        public string People { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PicturePath { get; set; }
    }
}