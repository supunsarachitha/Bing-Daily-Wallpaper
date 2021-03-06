﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BingWallpaper
{
    public class CropView : ContentPage
    {

        public byte[] Image;
        public Action RefreshAction;
        public bool DidCrop = false;

        public CropView(byte[] imageAsByte, Action refreshAction)
        {

            NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Color.Black;
            Image = imageAsByte;

            RefreshAction = refreshAction;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (DidCrop)
            {
                Preferences.Set("EnableAutoWallpaper", false);
                
            }
            RefreshAction.Invoke();



        }
    }
}
