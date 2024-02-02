using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Android.Gms;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Java.Util;
using MangaSaur.EventListeners;
using MangaSaur.Helpers;
using System;

namespace MangaSaur.Activities
{
    [Activity(Label = "RegisterActivity", Theme = "@style/AppTheme")]

    public class RegisterActivity : Activity
    {
        TextView btnRedirect, textViewRegistration;
        Button btnRegister;
        EditText editTextUsername, editTextPhone, editTextPassword, editTextEmail, editTextConfirmPassword;
        FirebaseFirestore db;
        FirebaseAuth auth;
        AppDataHelper dataHelper = new AppDataHelper();
        TaskCompletionListeners taskCompletionListeners = new TaskCompletionListeners();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.register_layout);
            

            textViewRegistration = FindViewById<TextView>(Resource.Id.textViewRegistration);
            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
            btnRedirect = FindViewById<TextView>(Resource.Id.btnRedirect);
            editTextConfirmPassword = FindViewById<EditText>(Resource.Id.editTextConfirmPassword);
            editTextUsername = FindViewById<EditText>(Resource.Id.editTextUsername);
            editTextPhone = FindViewById<EditText>(Resource.Id.editTextPhone);
            editTextEmail = FindViewById<EditText>(Resource.Id.editTextEmail);
            editTextPassword = FindViewById<EditText>(Resource.Id.editTextPassword);

            btnRedirect.Click += BtnRedirect_Click;
            btnRegister.Click += BtnRegister_Click;

            Typeface tf = Typeface.CreateFromAsset(Assets, "BADABOOM.TTF");
            textViewRegistration.SetTypeface(tf, TypefaceStyle.Normal);
            db = dataHelper.getFirestore();
            auth = dataHelper.getFireBaseAuth();
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            //restrictions
            if (string.IsNullOrEmpty(editTextPhone.Text) || string.IsNullOrEmpty(editTextUsername.Text) || string.IsNullOrEmpty(editTextEmail.Text) ||
            string.IsNullOrEmpty(editTextPassword.Text) || string.IsNullOrEmpty(editTextConfirmPassword.Text))
            {
                Toast.MakeText(ApplicationContext, "Please complete the form.", ToastLength.Short).Show();
                return;
            }
            if (editTextPassword.Text.Length < 8 || editTextPassword.Text.Length > 16)
            {
                Toast.MakeText(ApplicationContext, "Password should have 8 characters but not more than 16. Please try again", ToastLength.Short).Show();
                return;
            }
            if (editTextPassword.Text != editTextConfirmPassword.Text)
            {
                Toast.MakeText(ApplicationContext, "Password does not match. Please try again.", ToastLength.Short).Show();
                return;
            }
            if (!editTextEmail.Text.Contains("@"))
            {
                Toast.MakeText(ApplicationContext, "Please enter a valid email address.", ToastLength.Short).Show();
                return;
            }


            auth.CreateUserWithEmailAndPassword(editTextEmail.Text, editTextPassword.Text)
                .AddOnSuccessListener(this, taskCompletionListeners)
                .AddOnFailureListener(this, taskCompletionListeners);

            taskCompletionListeners.Success += (success, args) =>
            {
                HashMap userMap = new HashMap();
                userMap.Put("Username", editTextUsername.Text);
                userMap.Put("Phone", editTextPhone.Text);
                userMap.Put("Email", editTextEmail.Text);

                DocumentReference userRef = db.Collection("users").Document(auth.CurrentUser.Uid);
                userRef.Set(userMap);

                Toast.MakeText(this, "Registration successful.", ToastLength.Short).Show();
                StartActivity(typeof(LoginActivity));
            };

            taskCompletionListeners.Failure += (failure, args) =>
            {
                Toast.MakeText(this, "Registration failed:" + args.Cause, ToastLength.Short).Show();
            };
            
        }

        private void BtnRedirect_Click(object sender, EventArgs e)
        {
            Intent lg = new Intent(this, typeof(LoginActivity));
            StartActivity(lg);
        }


    }
}