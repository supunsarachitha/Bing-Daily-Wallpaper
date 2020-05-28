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
        WallpaperSet wallpaperSet = new WallpaperSet();
        //select online images
        public bool ChangeWallPaperRes(string fileLocation, int screen)
        {
            int id = (int)typeof(Resource.Drawable).GetField(fileLocation).GetValue(null);

            Bitmap final = BitmapFactory.DecodeResource(Application.Context.Resources, id);

            
            bool val = wallpaperSet.GetWallpaperBysystem(final);

            

            return true;
        }


        //get saved location

        public List<string> getListOfImages()
        {
            List<string> imagesList = new List<string>();

                var files = Directory.EnumerateFiles("/storage/emulated/0/Android/data/lk.stechbuzz.bingwallpaper/files/");
                foreach (var item in files)
                {
                    imagesList.Add(item);
                }

            return imagesList;
        }



        //select resource images
        bool imagelist.ChangeWallPaper(string filelocation, int screen)
        {

            WallpaperManager wallpaperManager = WallpaperManager.GetInstance(Application.Context);
            Bitmap decoded = null;
            Bitmap rowImage = BitmapFactory.DecodeFile((filelocation));
            using (MemoryStream memory = new MemoryStream())
            {

                rowImage.Compress(Bitmap.CompressFormat.Png, 100, memory);
                memory.Position = 0;
                decoded = BitmapFactory.DecodeStream(memory);
                memory.Flush();
            }

            bool val = wallpaperSet.GetWallpaperBysystem(decoded);

            return true;
        }

        byte[] imagelist.GetFileBytes(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

    }
}