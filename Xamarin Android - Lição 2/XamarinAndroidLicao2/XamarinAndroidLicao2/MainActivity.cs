﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using System.Collections.Generic;
using Android.Content;

namespace XamarinAndroidLicao2
{
    [Activity(Label = "Calcular Capital", MainLauncher = true, Icon = "@drawable/icon", WindowSoftInputMode = SoftInput.AdjustPan)]
    public class MainActivity : Activity
    {
        double capitalBrasil, capitalMexico;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Mobile Center Call
            MobileCenter.Start("ec51b039-2f80-4948-b573-c5831148e177",
                    typeof(Analytics), typeof(Crashes)); MobileCenter.Start("ec51b039-2f80-4948-b573-c5831148e177",
                     typeof(Analytics), typeof(Crashes));

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button btnCalcular = FindViewById<Button>(Resource.Id.btnCalcular);
            EditText txtRBrasil = FindViewById<EditText>(Resource.Id.txtrbrasil);
            EditText txtRMexico = FindViewById<EditText>(Resource.Id.txtrmexico);
            EditText txtDBrasil = FindViewById<EditText>(Resource.Id.txtdbrasil);
            EditText txtDMexico = FindViewById<EditText>(Resource.Id.txtdmexico);

            double rBrasil, rMexico, dBrasil, dMexico;

            btnCalcular.Click += delegate
            {
                try
                {
                    rBrasil = double.Parse(txtRBrasil.Text);
                    rMexico = double.Parse(txtRMexico.Text);
                    dBrasil = double.Parse(txtDBrasil.Text);
                    dMexico = double.Parse(txtDMexico.Text);
                    capitalBrasil = rBrasil - dBrasil;
                    capitalMexico = rMexico - dMexico;

                    Analytics.TrackEvent("Button Clicked", new Dictionary<string, string> { { "Category", "Button Clicked" }, { "Button Name", "Calcular" } });
                    Carregar();
                }
                catch (System.Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                }
            };
        }

        public void Carregar()
        {
            Intent objIntent = new Intent(this, typeof(Capital));
            objIntent.PutExtra("capitalBrasil", capitalBrasil);
            objIntent.PutExtra("capitalMexico", capitalMexico);
            StartActivity(objIntent);
        }
    }
}

