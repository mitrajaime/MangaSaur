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

namespace MangaSaur.Activities
{
        [Activity(Theme = "@style/AppTheme")]
        public class HomeActivity : AppCompatActivity //AppCompatActivity?
        {
            Toolbar toolbar;
            FirebaseAuth auth;
            AppDataHelper aph = new AppDataHelper();
            protected override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);
                Xamarin.Essentials.Platform.Init(this, savedInstanceState);
                // Set our view from the "main" layout resource
                SetContentView(Resource.Layout.home_layout);
                toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

                SetSupportActionBar(toolbar);
                auth = aph.getFireBaseAuth();
            }

            public override bool OnOptionsItemSelected(IMenuItem item)
            {
                int id = item.ItemId;

                if (id == Resource.Id.action_logout)
                {
                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    builder.SetTitle("Confirm");
                    builder.SetMessage("Are you sure you want to log out?");

                    builder.SetPositiveButton("Confirm", (sender, e) =>
                    {
                        auth.SignOut();
                        Toast.MakeText(this, "Logout successful", ToastLength.Short).Show();
                        StartActivity(typeof(LoginActivity));
                    });

                    builder.SetNegativeButton("Cancel", (sender, e) =>
                    {
                        return;
                    });

                    Dialog dialog = builder.Create();
                    dialog.Show();
                }

                if (id == Resource.Id.action_refresh)
                {
                    Toast.MakeText(this, "Refresh", ToastLength.Short).Show();
                }

                return base.OnOptionsItemSelected(item);
            }
            public override bool OnCreateOptionsMenu(IMenu menu)
            {
                MenuInflater.Inflate(Resource.Menu.feed_menu, menu);
                return true;
            }
        }
        
}