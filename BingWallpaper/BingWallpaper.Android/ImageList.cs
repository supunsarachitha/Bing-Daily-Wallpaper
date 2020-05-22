using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BingWallpaper.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(ImageList))]

namespace BingWallpaper.Droid
{
    public class ImageList : imagelist
    {
        public bool ChangeWallPaperRes(string fileLocation)
        {
            int id = (int)typeof(Resource.Drawable).GetField(fileLocation).GetValue(null);

            WallpaperManager wallpaperManager = WallpaperManager.GetInstance(Application.Context);

            wallpaperManager.SetBitmap(BitmapFactory.DecodeResource(Application.Context.Resources, id));

            return true;
        }

        public List<string> getListOfImages()
        {
            List<string> imagesList = new List<string>();

                var files = Directory.EnumerateFiles("/storage/emulated/0/Android/data/com.companyname.bingwallpaper/files/");
                foreach (var item in files)
                {
                    imagesList.Add(item);
                }

            return imagesList;
        }




        bool imagelist.ChangeWallPaper(string filelocation)
        {

            //Thread thread = new Thread(async() =>
            //{
                WallpaperManager wallpaperManager = WallpaperManager.GetInstance(Application.Context);

                wallpaperManager.SetBitmap(BitmapFactory.DecodeFile((filelocation)));
            //});
            
           // thread.Start();


            
            return true;
        }

        byte[] imagelist.GetFileBytes(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

    }
}