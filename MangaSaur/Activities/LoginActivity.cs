using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.login_layout);

            btnRedirect = FindViewById<TextView>(Resource.Id.btnRedirect);
            btnRedirect.Click += BtnRedirect_Click;

            textViewMangaSaur = FindViewById<TextView>(Resource.Id.textViewMangaSaur);

            Typeface tf = Typeface.CreateFromAsset(Assets,"BADABOOM.TTF");
            textViewMangaSaur.SetTypeface(tf, TypefaceStyle.Normal);

        }

        private void BtnRedirect_Click(object sender, EventArgs e)
        {
            Intent rs = new Intent(this, typeof(RegisterActivity));
            StartActivity(rs);

        }
    }
}