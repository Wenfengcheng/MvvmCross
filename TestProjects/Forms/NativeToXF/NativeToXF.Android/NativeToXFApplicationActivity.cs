
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Content.PM;
using Xamarin.Forms;
using MvvmCross.Platform;
using MvvmCross.Core.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Core;
using MvvmCross.Forms.Droid;
using MvvmCross.Forms.Droid.Presenters;
using Android.Views;

namespace NativeToXF.Droid
{
    [Activity(Label = "NativeToXFApplicationActivity", ScreenOrientation=ScreenOrientation.Portrait, NoHistory = false)]
    public class NativeToXFApplicationActivity_Obsolete : MvxFormsApplicationActivity
    {
        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            return base.OnPrepareOptionsMenu(menu);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Forms.Init(this, bundle);

            var presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsHybridDroidPresenter;
            LoadApplication(presenter.MvxFormsApp);
        }
    }
}

