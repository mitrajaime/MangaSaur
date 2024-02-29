using Android.App;
using Android.Content;
using Android.Gms.Common.Data;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using MangaSaur.EventListeners;
using MangaSaur.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MangaSaur.Activities
{
    [Activity(Theme = "@style/AppTheme", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        TextView btnRedirect, textViewMangaSaur;
        Button btnLogin;
        EditText editTextEmail, editTextPassword;
        FirebaseAuth auth;
        AppDataHelper dataHelper = new AppDataHelper();
        TaskCompletionListeners taskCompletionListeners = new TaskCompletionListeners();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.login_layout);

            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnRedirect = FindViewById<TextView>(Resource.Id.btnRedirect);
            editTextEmail = FindViewById<EditText>(Resource.Id.editTextEmail);
            editTextPassword = FindViewById<EditText>(Resource.Id.editTextPassword);

            btnRedirect.Click += BtnRedirect_Click;
            btnLogin.Click += BtnLogin_Click;


            textViewMangaSaur = FindViewById<TextView>(Resource.Id.textViewMangaSaur);

            Typeface tf = Typeface.CreateFromAsset(Assets,"BADABOOM.TTF");
            textViewMangaSaur.SetTypeface(tf, TypefaceStyle.Normal);
            auth = dataHelper.getFireBaseAuth();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(editTextEmail.Text) || string.IsNullOrEmpty(editTextPassword.Text))
            {
                Toast.MakeText(this, "Email or password is invalid. Please try again", ToastLength.Short).Show();
                return;
            }
            auth.SignInWithEmailAndPassword(editTextEmail.Text, editTextPassword.Text)
                .AddOnSuccessListener(taskCompletionListeners)
                .AddOnFailureListener(taskCompletionListeners);
            taskCompletionListeners.Success += (success, args) =>
            {
                Toast.MakeText(this, "Login successful.", ToastLength.Short).Show();
                StartActivity(typeof(HomeActivity));
            };
            taskCompletionListeners.Failure += (failure, args) =>
            {
                Toast.MakeText(this, "Login failed: " + args.Cause, ToastLength.Short).Show();
                return;
            };
        }

        private void BtnRedirect_Click(object sender, EventArgs e)
        {
            Intent rs = new Intent(this, typeof(RegisterActivity));
            StartActivity(rs);

        }
    }
}