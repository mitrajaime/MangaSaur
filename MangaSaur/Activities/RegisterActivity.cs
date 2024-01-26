using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.Firestore;
using Firebase;
using Java.Util;

namespace MangaSaur.Activities
{
    [Activity(Label = "RegisterActivity", Theme = "@style/AppTheme")]
   
    public class RegisterActivity : Activity
    {
        TextView btnRedirect;
        Button btnRegister;
        EditText editTextUsername, editTextPhone, editTextPassword, editTextEmail, editTextConfirmPassword;
        FirebaseFirestore db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.register_layout);
            db = getFireStore();

            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
            btnRedirect = FindViewById<TextView>(Resource.Id.btnRedirect);
            editTextConfirmPassword = FindViewById<EditText>(Resource.Id.editTextConfirmPassword);
            editTextUsername = FindViewById<EditText>(Resource.Id.editTextUsername);
            editTextPhone = FindViewById<EditText>(Resource.Id.editTextPhone);
            editTextEmail = FindViewById<EditText>(Resource.Id.editTextEmail);
            editTextPassword = FindViewById<EditText>(Resource.Id.editTextPassword);

            btnRedirect.Click += BtnRedirect_Click;
            btnRegister.Click += BtnRegister_Click;

        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            //restrictions
            if (string.IsNullOrEmpty(editTextPhone.Text) || string.IsNullOrEmpty(editTextUsername.Text) || string.IsNullOrEmpty(editTextEmail.Text) ||
            string.IsNullOrEmpty(editTextPassword.Text) || string.IsNullOrEmpty(editTextConfirmPassword.Text))
            {
                Toast.MakeText(this, "Please complete the form.", ToastLength.Short).Show();
                return;
            }
            if(editTextPassword.Text.Length < 8 || editTextPassword.Text.Length > 16)
            {
                Toast.MakeText(this, "Password should have 8 characters but not more than 16. Please try again", ToastLength.Short).Show();
                return;
            }
            if(editTextPassword. Text != editTextConfirmPassword. Text)
            {
                Toast.MakeText(this, "Password does not match. Please try again.", ToastLength.Short).Show();
                return;
            }


            HashMap userMap = new HashMap();
            userMap.Put("Username", editTextUsername.Text);
            userMap.Put("Phone", editTextPhone.Text);
            userMap.Put("Email", editTextEmail.Text);

            DocumentReference userRef = db.Collection("users").Document();
            userRef.Set(userMap);

            Toast.MakeText(this, "Registration successful.", ToastLength.Short).Show();
            StartActivity(typeof(LoginActivity));
        }

        private void BtnRedirect_Click(object sender, EventArgs e)
        {
            Intent lg = new Intent(this, typeof(LoginActivity));
            StartActivity(lg);
        }

        public FirebaseFirestore getFireStore()
        {
            var app = FirebaseApp.InitializeApp(this);

            FirebaseFirestore database;

            if(app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetProjectId("mangasaur-ee166")
                    .SetApplicationId("mangasaur-ee166")
                    .SetApiKey("AIzaSyCeENKvRWLecjg9z-xaypzVR2JBchQiRoE")
                    .SetStorageBucket("mangasaur-ee166.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(this, options);
                database = FirebaseFirestore.GetInstance(app);

            }
            else
            {
                database = FirebaseFirestore.GetInstance(app);
            }

            return database;
        }

    }
}