

# Bing-Daily-Wallpaper

</br>
<a href="https://sourceforge.net/projects/bing-daily-wallpaper/files/latest/download"><img alt="Download Bing Daily Wallpaper" src="https://a.fsdn.com/con/app/sf-download-button" width=276 height=48 srcset="https://a.fsdn.com/con/app/sf-download-button?button_size=2x 2x"></a> &nbsp;&nbsp;&nbsp; <a href="https://sourceforge.net/projects/bing-daily-wallpaper/files/latest/download"><img alt="Download Bing Daily Wallpaper" src="https://img.shields.io/sourceforge/dt/bing-daily-wallpaper.svg" ></a> &nbsp;&nbsp;&nbsp;   <a href="https://sourceforge.net/p/bing-daily-wallpaper/"><img alt="Download Bing Daily Wallpaper" src="https://sourceforge.net/sflogo.php?type=13&group_id=3227304" ></a> 
</br></br>
<a href="https://play.google.com/store/apps/details?id=lk.stechbuzz.bingwallpaper">
<img border="0" alt="On Google Play" src="https://play.google.com/intl/en_us/badges/static/images/badges/en_badge_web_generic.png" height="65" width="200"> </a>
</br></br>
 


Android mobile app Seamlessly update your device wallpaper with Bing photos every day.
Wallpaper source is bing daily images. 
User also can set on device wallpapers.

## Built With
mobile technology - xamarin forms </br>
image archive - bing wallpaper

## code sample

#### wallpaper changer
```
WallpaperManager wallpaperManager = WallpaperManager.GetInstance(Application.Context);
wallpaperManager.SetBitmap(BitmapFactory.DecodeResource(Application.Context.Resources, id), null, true, WallpaperManagerFlags.System);
```

#### schedule workmanager
workmanger is checking whether new image available in archive.
```
String TAG = "WallpaperWork";

Constraints constraints = new Constraints();
constraints.SetRequiresStorageNotLow(true);
constraints.SetRequiresBatteryNotLow(true);
constraints.RequiredNetworkType = NetworkType.Connected;

PeriodicWorkRequest taxWorkRequest = PeriodicWorkRequest.Builder.From<DownloadBrain>(TimeSpan.FromHours(1))
.SetConstraints(constraints).Build();
            
WorkManager.GetInstance(Application.Context)
.EnqueueUniquePeriodicWork(TAG, ExistingPeriodicWorkPolicy.Keep ,taxWorkRequest);
```

#### Image compression

images compress to png format before pass wallpaperManager.

```
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
```

## GNU Lesser General Public License v3.0
Permissions of this copyleft license are conditioned on making available complete source code of licensed works and modifications under the same license or the GNU GPLv3. Copyright and license notices must be preserved. Contributors provide an express grant of patent rights. However, a larger work using the licensed work through interfaces provided by the licensed work may be distributed under different terms and without source code for the larger work.
