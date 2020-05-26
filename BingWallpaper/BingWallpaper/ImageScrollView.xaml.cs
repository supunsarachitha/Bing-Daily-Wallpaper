using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BingWallpaper
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageScrollView : ContentPage
    {
        public ImageScrollView()
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
            
            busyIndi.IsVisible = true;
            

            string action = await DisplayActionSheet("Set as wallpaper?", "Cancel", null, "Home screen", "Lock screen", "Both");

            int location = 0;
            if (action == "Home screen")
            {
                Preferences.Set("Screen", 1);
            }
            if (action == "Lock screen")
            {
                Preferences.Set("Screen", 1);
            }
            else if (action == "Both")
            {
                Preferences.Set("Screen", 1);
            }
            else if (action == "Cancel")
            {
                return;
            }

            

            if (action != null)
            
            {
                

                ImageItems item = (ImageItems)e.CurrentSelection.FirstOrDefault();



                Dispatcher.BeginInvokeOnMainThread(async () =>
                {
                    progressname.Text = "Proceessing image..";
                    try
                    {

                        if (string.IsNullOrEmpty(item.fileLocation))
                        {
                            bool rest = DependencyService.Get<imagelist>().ChangeWallPaperRes(item.imgSrc.ToString(), location);
                            await Task.Delay(3000);
                            if (rest)
                            {
                                busyIndi.IsVisible = false;
                                await DisplayAlert("", "Wallpaper changed", "Ok");
                            }
                        }
                        else
                        {
                            bool result = DependencyService.Get<imagelist>().ChangeWallPaper(item.fileLocation, location);
                            await Task.Delay(3000);
                            if (result)
                            {
                                busyIndi.IsVisible = false;
                                await DisplayAlert("", "Wallpaper changed", "Ok");
                            }
                        }

                        progressname.Text = "Loading...";



                    }
                    catch (Exception ex)
                    {
                        busyIndi.IsVisible = false;
                        return;
                    }
                });
            }
            else
            {
                busyIndi.IsVisible = false;
            }



        }

    }



}