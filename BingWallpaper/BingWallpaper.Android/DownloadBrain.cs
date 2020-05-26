using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Work;

namespace BingWallpaper.Droid
{
    public class DownloadBrain : Worker
    {

        public DownloadBrain(Context context, WorkerParameters workerParameters) : base(context, workerParameters)
        {

        }

        public override Result DoWork()
        {
            //Android.Util.Log.Debug("CalculatorWorker", $"Your Tax Return is: 1");
            downloadImage();
            return Result.InvokeSuccess();
        }

        public void downloadImage()
        {
            try
            {
                Uri uriBing = new Uri("http://cn.bing.com/HPImageArchive.aspx?idx=0&n=1");
                WebRequest webRequest = WebRequest.Create(uriBing);
                WebResponse webResponse = webRequest.GetResponse();
                Stream stream = webResponse.GetResponseStream();

                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(stream);

                string picturePath = Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
                string wallpaperSavePath = Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads).AbsolutePath;


                //check folder
                if (!Directory.Exists(wallpaperSavePath))
                {
                    Directory.CreateDirectory(wallpaperSavePath);
                }

                string fullStartDate = xmldoc["images"]["image"]["fullstartdate"].InnerText;
                string urlBase = xmldoc["images"]["image"]["urlBase"].InnerText;

                string curImagePath = wallpaperSavePath + fullStartDate + ".jpg";

                if (File.Exists(curImagePath))
                {

                }
                else
                {
                    string downloadUrl = "http://www.bing.com" + urlBase + "_1920x1080.jpg";
                    WebClient webClient = new WebClient();

                    try
                    {
                        webClient.DownloadFile(downloadUrl, @curImagePath);

                        WallpaperManager wallpaperManager = WallpaperManager.GetInstance(Application.Context);
                        if (Xamarin.Essentials.Preferences.Get("EnableAutoWallpaper", false))
                        {
                            

                            Android.Graphics.Bitmap rowBitmap = BitmapFactory.DecodeFile(curImagePath);

                            Android.Graphics.Bitmap CroppedBitmap = Android.Graphics.Bitmap.CreateScaledBitmap(rowBitmap, wallpaperManager.DesiredMinimumWidth, wallpaperManager.DesiredMinimumWidth, true);


                            Android.Graphics.Bitmap decoded = null;
                            using (MemoryStream memory = new MemoryStream())
                            {

                                CroppedBitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 100, memory);
                                memory.Position = 0;
                                decoded = Android.Graphics.BitmapFactory.DecodeStream(memory);
                                memory.Flush();
                            }



                            if (Xamarin.Essentials.Preferences.Get("Screen", 1) == 1)
                            {
                                wallpaperManager.SetBitmap(decoded, null, true, WallpaperManagerFlags.System);
                            }
                            else if (Xamarin.Essentials.Preferences.Get("Screen", 1) == 2)
                            {
                                wallpaperManager.SetBitmap(decoded, null, true, WallpaperManagerFlags.Lock);
                            }
                            else if (Xamarin.Essentials.Preferences.Get("Screen", 1) == 3)
                            {
                                wallpaperManager.SetBitmap(decoded, null, true, WallpaperManagerFlags.System);
                                wallpaperManager.SetBitmap(decoded, null, true, WallpaperManagerFlags.Lock);
                            }
                            else
                            {
                                wallpaperManager.SetBitmap(decoded, null, true, WallpaperManagerFlags.System);
                            }

                        }



                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

            }
            catch (Exception)
            {

                return;
            }



        }


    }
}