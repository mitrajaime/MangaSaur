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

namespace MangaSaur.Activities
{
        [Activity(Theme = "@style/AppTheme")]
        public class HomeActivity : AppCompatActivity //AppCompatActivity?
        {
            RelativeLayout btnCreatePost;
            Toolbar toolbar;
            FirebaseAuth auth;
            AppDataHelper aph = new AppDataHelper();
            RecyclerView postRecyclerView;
            PostAdapter postAdapter;
            List<Post> postList = new List<Post>();
            protected override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);
                Xamarin.Essentials.Platform.Init(this, savedInstanceState);
                // Set our view from the "main" layout resource
                SetContentView(Resource.Layout.home_layout);
                toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
                btnCreatePost = FindViewById<RelativeLayout>(Resource.Id.btnCreatePost);

                SetSupportActionBar(toolbar);
                auth = aph.getFireBaseAuth();

                postRecyclerView = FindViewById<RecyclerView>(Resource.Id.postRecyclerView);

                createData();
                SetUpRecyclerView();

            btnCreatePost.Click += BtnCreatePost_Click;
            }

        private void BtnCreatePost_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(CreatePostActivity));
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

                else if (id == Resource.Id.action_refresh)
                {
                    Toast.MakeText(this, "Refresh", ToastLength.Short).Show();
                }

                else if (id == Resource.Id.action_settings) 
                {
                    Toast.MakeText(this, "Settings", ToastLength.Short).Show();
                }

                else if (id == Resource.Id.action_account)
                {
                    Toast.MakeText(this, "Acccount", ToastLength.Short).Show();
                }


            return base.OnOptionsItemSelected(item);
            }

            public override bool OnCreateOptionsMenu(IMenu menu)
            {
                MenuInflater.Inflate(Resource.Menu.feed_menu, menu);
                return true;
            }

        // Adding Static Data to Post
            void createData()
            {
                postList.Add(new Post
                {
                    Description = "Anita Max Wynn",
                    Username = "Drake",
                    LikeCount = 12130
                });
                postList.Add(new Post
                {
                    Description = "FLY HGIH ACT WOOHOO YEAH",
                    Username = "Fitzgerald Varga",
                    LikeCount = 120
                });
                postList.Add(new Post
                {
                    Description = "Kaila ka Shimo te? Kanang kontra ni Godzilla-",
                    Username = "Seth Zafra",
                    LikeCount = 0
                });
            }
            
            void SetUpRecyclerView()
            {
                postRecyclerView.SetLayoutManager(new AndroidX.RecyclerView.Widget.LinearLayoutManager(postRecyclerView.Context));
                postAdapter = new PostAdapter(postList);
                postRecyclerView.SetAdapter(postAdapter);
            }
        }
        
}