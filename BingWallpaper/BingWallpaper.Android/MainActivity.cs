using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AndroidX.Work;
using Android.Content;

namespace BingWallpaper.Droid
{
    [Activity(Label = "BingWallpaper",  Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            
            
            
            String TAG = "WallpaperWork";

            Constraints constraints = new Constraints();
            constraints.SetRequiresStorageNotLow(true);
            constraints.SetRequiresBatteryNotLow(true);
            constraints.RequiredNetworkType = NetworkType.Connected;

            PeriodicWorkRequest taxWorkRequest = PeriodicWorkRequest
                .Builder.From<DownloadBrain>(TimeSpan.FromHours(6))
                .SetConstraints(constraints).Build();
            
            WorkManager.GetInstance(Application.Context).EnqueueUniquePeriodicWork(TAG, ExistingPeriodicWorkPolicy.Keep ,taxWorkRequest);

            LoadApplication(new App());

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }



    }
}