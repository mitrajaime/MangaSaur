using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Firebase.Auth;
using MangaSaur.EventListeners;
using MangaSaur.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using RecyclerView = AndroidX.RecyclerView.Widget.RecyclerView;
using MangaSaur.Adapters;
using MangaSaur.DataModels;
using AndroidX.RecyclerView.Widget;
using Android;
using Android.Content.PM;
using Plugin.Media;
using System.Runtime.CompilerServices;

namespace MangaSaur.Activities
{
    [Activity(Theme = "@style/AppTheme")]
    public class CreatePostActivity : AppCompatActivity
    {
        ImageView imageViewPost;
        Button btnCancel, btnCreatePost;

        readonly string[] permissionsGroup =
        {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.Camera
        };
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.create_post);
            
            imageViewPost = FindViewById<ImageView>(Resource.Id.imageViewPost);
            btnCancel = FindViewById<Button>(Resource.Id.btnCancel);
            btnCreatePost = FindViewById<Button>(Resource.Id.btnCreatePost);

            imageViewPost.Click += ImageViewPost_Click;
            btnCancel.Click += BtnCancel_Click;
            btnCreatePost.Click += BtnCreatePost_Click;
        }

        private void ImageViewPost_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetMessage("Post Image");

            builder.SetPositiveButton("Camera", (sender, e) =>
            {
                takePhoto();
            });

            builder.SetNegativeButton("Upload Photo", (sender, e) =>
            {
                selectPhoto();
            });

            Dialog dialog = builder.Create();
            dialog.Show();
        }

        private void BtnCreatePost_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(HomeActivity));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        string GenerateRandomString(int length)
        {
            Random random = new Random();
            char[] allowChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            string sResult = "";
            for(int x = 0; x < length; x++)
            {
                sResult += allowChars[random.Next(0, allowChars.Length)];
            }
            return sResult;
        }

        async void selectPhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                builder.SetMessage("File is not supported.");

                builder.SetPositiveButton("OK", (sender, e) =>
                {
                    return;
                });
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CompressionQuality = 30
            });

            if (file == null) { return; }

            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            imageViewPost.SetImageBitmap(bitmap);
        }
        async void takePhoto()
        {
            await CrossMedia.Current.Initialize();
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CompressionQuality = 20,
                Directory = "Sample",
                Name = GenerateRandomString(6) + "facepost.jpg"
            });

            if (file == null) { return; }

            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            imageViewPost.SetImageBitmap(bitmap);
        }
    }
}