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
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }


                WallpaperManager wallpaperManager = WallpaperManager.GetInstance(Application.Context);
                
                wallpaperManager.SetBitmap(BitmapFactory.DecodeFile(curImagePath));

                

            }
            catch (Exception)
            {

                return;
            }
           


        }

        

        

        
    }
}