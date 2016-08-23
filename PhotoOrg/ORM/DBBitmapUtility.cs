using Android.Graphics;
using static Android.Graphics.Bitmap;
using System.IO;
using Java.IO;
using Java.Nio;

namespace PhotoOrg.ORM
{
    public class DbBitmapUtility
    {

        // convert from bitmap to byte array
        public byte[] getBytes(Bitmap bitmap)
        {
            byte[] bitmapData;
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                bitmapData = stream.ToArray();
            }

            return bitmapData;
        }

        // convert from byte array to bitmap
        public Bitmap getImage(byte[] image)
        {
            return BitmapFactory.DecodeByteArray(image, 0, image.Length);
        }

        // Create bitmap from file path
        public Bitmap getBitmap(string path)
        {
            Java.IO.File image = new Java.IO.File(path);
            Bitmap bitmap = BitmapFactory.DecodeFile(image.AbsolutePath);
            return bitmap;
        }
    }
}