using System;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using BingWallpaper;
using BingWallpaper.Droid.Renderer;
using Com.Theartofdev.Edmodo.Cropper;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CropView), typeof(CropViewRenderer))]
namespace BingWallpaper.Droid.Renderer
{

    class CropViewRenderer: PageRenderer
    {

        public CropViewRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            var page = Element as CropView;
            if (page != null)
            {
                var cropImageView = new CropImageView(Context);
                cropImageView.LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);

                BitmapFactory.Options options = new BitmapFactory.Options();
                options.InSampleSize = 2;


                Bitmap bitmp= BitmapFactory.DecodeByteArray(page.Image, 0, page.Image.Length, options);


                cropImageView.SetImageBitmap(bitmp);
                cropImageView.SetMaxCropResultSize(Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Width), Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Height));
                cropImageView.SetMinCropResultSize(Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Width) / 4,Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Height) / 4);
                cropImageView.SetMinimumHeight(Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Height) / 2);
                cropImageView.SetMinimumWidth(Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Width)/2);

                var scrollView = new ScrollView { Content = cropImageView.ToView() };
                var stackLayout = new StackLayout { Children = { scrollView } };

                var rotateButton = new Button { Text = "Rotate" };
                rotateButton.CornerRadius = 50;

                rotateButton.Clicked += (sender, ex) =>
                {
                    cropImageView.RotateImage(90);
                };
                stackLayout.Children.Add(rotateButton);

                var finishButton = new Button { Text = "Finished" };
                finishButton.CornerRadius = 50;

                Bitmap decoded = null;
                finishButton.Clicked += (sender, ex) =>
                {
                    finishButton.IsEnabled = false;
                    rotateButton.IsVisible = false;
                    finishButton.Text = "Please Wait...";
                    Bitmap cropped = cropImageView.CroppedImage;

                    using (MemoryStream memory = new MemoryStream())
                    {

                        cropped.Compress(Bitmap.CompressFormat.Png, 100, memory);
                        memory.Position = 0;
                        decoded = BitmapFactory.DecodeStream(memory);
                        memory.Flush();
                    }

                    


                    WallpaperSet wallpaperSet = new WallpaperSet();
                    bool val = wallpaperSet.GetWallpaperBysystem(decoded);



                    page.DidCrop = true;
                    page.Navigation.PopModalAsync();



                };

                stackLayout.Children.Add(finishButton);
                page.Content = stackLayout;
            }
        }
    }
}