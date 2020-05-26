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
using Xamarin.Essentials;
using Android.OS;
using Android.Graphics.Drawables;

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


            if (!Preferences.ContainsKey("EnableAutoWallpaper"))
            {
                Preferences.Set("EnableAutoWallpaper", true);
                wallpaperEnableswitch.IsToggled= Preferences.Get("EnableAutoWallpaper", false);
            }
            else
            {
                wallpaperEnableswitch.IsToggled = Preferences.Get("EnableAutoWallpaper", false);
            }

        }




        private void Refresh()
        {

            wallpaperEnableswitch.IsToggled = Preferences.Get("EnableAutoWallpaper", false);

            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(2000);
                busyIndi.IsVisible = false;
            });


            //try
            //{
            //    if (App.CroppedImage != null)
            //    {
            //        //Stream stream = new MemoryStream(App.CroppedImage);

            //        //image.Source = ImageSource.FromStream(() => stream);

            //        //Content = image;
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }

        private async void wallpaperEnableswitch_Toggled(object sender, ToggledEventArgs e)
        {
            Preferences.Set("EnableAutoWallpaper", e.Value);


            //string action = await DisplayActionSheet("Set as wallpaper?", "Cancel", null, "Home screen", "Lock screen", "Both");

            //if (action == "Home screen")
            //{
            //    Preferences.Set("Screen", 1);
            //}
            //if (action == "Lock screen")
            //{
            //    Preferences.Set("Screen", 1);
            //}
            //else if (action == "Both")
            //{
            //    Preferences.Set("Screen", 1);
            //}
            //else if (action == "Cancel")
            //{
            //    return;
            //}
        }


        private async void btnOndevice_Clicked(object sender, EventArgs e)
        {
            await SelectPicture();
        }

        private async Task SelectPicture()
        {
            Setup();

            _imageSource = null;

            try
            {

                //========================================pick image from library 
                var mediaFile = await this._mediaPicker.PickPhotoAsync();

                _imageSource = ImageSource.FromStream(mediaFile.GetStream);

                var memoryStream = new MemoryStream();
                await mediaFile.GetStream().CopyToAsync(memoryStream);
                byte[] imageAsByte = memoryStream.ToArray();
                //================================================================



                string action = await DisplayActionSheet("Set as wallpaper?", "Cancel", null, "Home screen", "Lock screen", "Both");

                int location = 0;
                if (action == "Home screen")
                {
                    Preferences.Set("Screen", 1);
                    busyIndi.IsVisible = true;
                    await Navigation.PushModalAsync(new CropView(imageAsByte, Refresh));
                }
                if (action == "Lock screen")
                {
                    Preferences.Set("Screen", 2);
                    busyIndi.IsVisible = true;
                    await Navigation.PushModalAsync(new CropView(imageAsByte, Refresh));
                }
                else if (action == "Both")
                {
                    Preferences.Set("Screen", 3);
                    busyIndi.IsVisible = true;
                    await Navigation.PushModalAsync(new CropView(imageAsByte, Refresh));
                }
                else if (action == "Cancel")
                {
                    return;
                }

            }
            catch (System.Exception ex)
            {

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

        private async void btnImgBingWallpaper_Tapped(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new ImageScrollView());

        }
    }
}
