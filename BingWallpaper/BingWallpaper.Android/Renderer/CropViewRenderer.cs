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
                Bitmap bitmp = BitmapFactory.DecodeByteArray(page.Image, 0, page.Image.Length);
                cropImageView.SetImageBitmap(bitmp);

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