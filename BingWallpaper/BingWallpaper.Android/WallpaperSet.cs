using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BingWallpaper.Droid
{
    public class WallpaperSet
    {
        //sevice wallpapers
        public  bool GetWallpaperBysystem(Bitmap cropped)
        {
            


            WallpaperManager wallpaperManager = WallpaperManager.GetInstance(Android.App.Application.Context);


            Android.Graphics.Bitmap CroppedBitmap = Android.Graphics.Bitmap.CreateScaledBitmap(cropped, wallpaperManager.DesiredMinimumWidth, wallpaperManager.DesiredMinimumWidth, true);


            if (Xamarin.Essentials.Preferences.Get("Screen", 1) == 1)
            {

                wallpaperManager.SetBitmap(cropped, null, true, WallpaperManagerFlags.System);
            }
            else if (Xamarin.Essentials.Preferences.Get("Screen", 1) == 2)
            {

                wallpaperManager.SetBitmap(cropped, null, true, WallpaperManagerFlags.Lock);
            }
            else if (Xamarin.Essentials.Preferences.Get("Screen", 1) == 3)
            {

                wallpaperManager.SetBitmap(cropped, null, true, WallpaperManagerFlags.Lock);
                wallpaperManager.SetBitmap(cropped, null, true, WallpaperManagerFlags.System);
            }

           

            return true;



        }
    }
}