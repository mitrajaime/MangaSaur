using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Graphics;
using Plugin.Media;


namespace MangaSaur.Helpers
{
    public class AppDataHelper
    {
        public FirebaseFirestore getFirestore()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseFirestore database;

            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                     .SetProjectId("mangasaur-ee166")
                     .SetApplicationId("mangasaur-ee166")
                     .SetApiKey("AIzaSyCeENKvRWLecjg9z-xaypzVR2JBchQiRoE")
                     .SetStorageBucket("mangasaur-ee166.appspot.com")
                     .Build();

                app = FirebaseApp.InitializeApp(Application.Context, options);
                database = FirebaseFirestore.GetInstance(app);
            }
            else
            {
                database = FirebaseFirestore.GetInstance(app);
            }
            return database;
        }

        public FirebaseAuth getFireBaseAuth()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseAuth mAuth;

            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetProjectId("mangasaur-ee166")
                    .SetApplicationId("mangasaur-ee166")
                    .SetApiKey("AIzaSyCeENKvRWLecjg9z-xaypzVR2JBchQiRoE")
                    .SetStorageBucket("mangasaur-ee166.appspot.com")
                    .Build();
                
                app = FirebaseApp.InitializeApp(Application.Context, options);
                mAuth = FirebaseAuth.GetInstance(app);
            }
            else
            {
                mAuth = FirebaseAuth.GetInstance(app);
            }
            return mAuth;
        }

        //public Bitmap selectPhoto()
        //{
        //    CrossMedia.Current.Initialize();

        //    if (!CrossMedia.Current.IsTakePhotoSupported)
        //    {
        //        //AlertDialog.Builder builder = new AlertDialog.Builder(this);
        //        //builder.SetMessage("File is not supported.");

        //        //builder.SetPositiveButton("OK", (sender, e) =>
        //        //{
        //        //    return;
        //        //});
         
        //    }

        //    var file = CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
        //    {
        //        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
        //        CompressionQuality = 30
        //    });

        //    if (file == null) { return null; }

        //    byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
        //    Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
        //    return bitmap;
        //}
    }
}
