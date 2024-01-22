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

namespace MangaSaur.Activities
{
    [Activity(Label = "RegisterActivity", Theme = "@style/AppTheme")]
   
    public class RegisterActivity : Activity
    {
        TextView btnRedirect;
        Button btnRegister;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.register_layout);

            btnRedirect = FindViewById<TextView>(Resource.Id.btnRedirect);

            btnRedirect.Click += BtnRedirect_Click;
            
        }

        private void BtnRedirect_Click(object sender, EventArgs e)
        {
            Intent lg = new Intent(this, typeof(LoginActivity));
            StartActivity(lg);
        }
    }
}