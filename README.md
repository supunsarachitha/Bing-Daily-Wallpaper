

# Bing-Daily-Wallpaper

This mobile app use bing wallpaper and set as mobile wallpaper automatically.

[![Download Bing Daily Wallpaper](https://a.fsdn.com/con/app/sf-download-button)](https://sourceforge.net/projects/bing-daily-wallpaper/files/latest/download)
</br>

<a href="https://play.google.com/store/apps/details?id=lk.stechbuzz.bingwallpaper">
<img border="0" alt="On Google Play" src="http://www.gstatic.com/android/market_images/web/play_prism_hlock_2x.png" height="45" width="200"> </a>  


## Built With
mobile technology - xamarin forms

image archive - bing wallpaper

## code sample

- wallpaper changer
```
WallpaperManager wallpaperManager = WallpaperManager.GetInstance(Application.Context);
wallpaperManager.SetBitmap(BitmapFactory.DecodeResource(Application.Context.Resources, id), null, true, WallpaperManagerFlags.System);
```

- schedule workmanager

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


