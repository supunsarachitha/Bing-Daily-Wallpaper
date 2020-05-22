using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Plugin.Media;
using Plugin.Media.Abstractions;
using Android.App;
using Android.Graphics;
using Android;
using Android.Content;
using Android.Util;

namespace BingWallpaper
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        ImageSource _imageSource;
        private IMedia _mediaPicker;
        Image image;

        public MainPage()
        {
            InitializeComponent();


            var list = DependencyService.Get<imagelist>().getListOfImages();

            List<ImageItems> imagelist = new List<ImageItems>();
            foreach (var item in list)
            {
                ImageItems itm = new ImageItems();

                itm.imgSrc = ImageSource.FromFile(item);
                itm.fileLocation = item;
                imagelist.Add(itm);

            }

            List<string> reImage = new List<string>();
            reImage.Add("LavenderBee");
            reImage.Add("ArmedForces");
            reImage.Add("CastleDay");
            reImage.Add("FalklandRockhoppers");
            reImage.Add("KubotaGarden");
            reImage.Add("LofotenIslands");
            reImage.Add("MegellanicCloud");
            reImage.Add("NorthRimOpens");
            reImage.Add("OldPatriarchTree");
            reImage.Add("QatarMuseum");
            reImage.Add("RoaringFork");
            reImage.Add("WildflowerWeek");

            foreach (var item in reImage)
            {
                ImageItems itm2 = new ImageItems();
                itm2.imgSrc = item;
                imagelist.Add(itm2);

            }


            lst.ItemsSource = imagelist;

        }



        private async void lst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ImageItems item = (ImageItems)e.CurrentSelection.FirstOrDefault();

            //await SelectPicture(item.fileLocation);

            bool res = await DisplayAlert("", "Set as wallpaper?", "Yes", "Cancel");

            if (res)
            {
                busyIndi.IsVisible = true;
                await Task.Delay(10);
                ImageItems item = (ImageItems)e.CurrentSelection.FirstOrDefault();

                

                Dispatcher.BeginInvokeOnMainThread(async () =>
                {

                    try
                    {
                        if (string.IsNullOrEmpty(item.fileLocation))
                        {
                            bool rest = DependencyService.Get<imagelist>().ChangeWallPaperRes(item.imgSrc.ToString());
                            await Task.Delay(3000);
                            if (rest)
                            {
                                busyIndi.IsVisible = false;
                                await DisplayAlert("", "Wallpaper changed", "Ok");
                            }
                        }
                        else
                        {
                            bool result = DependencyService.Get<imagelist>().ChangeWallPaper(item.fileLocation);
                            await Task.Delay(3000);
                            if (result)
                            {
                                busyIndi.IsVisible = false;
                                await DisplayAlert("", "Wallpaper changed", "Ok");
                            }
                        }

                        

                    }
                    catch (Exception ex)
                    {
                        busyIndi.IsVisible = false;
                        return;
                    }
                });
            }



        }




        private async void Setup()
        {
            if (_mediaPicker != null)
            {
                return;
            }

            ////RM: hack for working on windows phone? 
            await CrossMedia.Current.Initialize();
            _mediaPicker = CrossMedia.Current;
        }

        private async Task SelectPicture( string location)
        {
            Setup();

            _imageSource = null;

            try
            {
                byte[] imageAsByte = DependencyService.Get<imagelist>().GetFileBytes(location);


                //========================================pick image from library 
                //var mediaFile = await this._mediaPicker.PickPhotoAsync();

                //_imageSource = ImageSource.FromStream(mediaFile.GetStream);

                //var memoryStream = new MemoryStream();
                //await mediaFile.GetStream().CopyToAsync(memoryStream);
                //byte[] imageAsByte = memoryStream.ToArray();
                //================================================================

                await Navigation.PushModalAsync(new CropView(imageAsByte, Refresh));

            }
            catch (System.Exception ex)
            {
                
            }
        }

        private void Refresh()
        {
            try
            {
                if (App.CroppedImage != null)
                {
                    Stream stream = new MemoryStream(App.CroppedImage);

                    image.Source = ImageSource.FromStream(() => stream);

                    Content = image;
                }
            }
            catch (Exception ex)
            {
               
            }
        }

    }
}
