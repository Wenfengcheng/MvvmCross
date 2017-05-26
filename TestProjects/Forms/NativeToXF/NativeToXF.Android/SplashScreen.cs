using Android.App;
using Android.Content.PM;
using MvvmCross.Core.Views;
using MvvmCross.Droid.Views;
using MvvmCross.Forms.Core;
using MvvmCross.Forms.Droid.Presenters;
using MvvmCross.Platform;
using Xamarin.Forms;

namespace NativeToXF.Droid
{
    [Activity(
        Label = "NativeToXF.Droid"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen
        : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }

        ////private bool isInitializationComplete = false;
        ////public override void InitializationComplete()
        ////{
        ////    if (!isInitializationComplete)
        ////    {
        ////        isInitializationComplete = true;
        ////        ////StartActivity(typeof(NativeToXFApplicationActivity));
        ////    }
        ////}

        ////protected override void OnCreate(Android.OS.Bundle bundle)
        ////{
        ////    Forms.Init(this, bundle);
        ////    // Leverage controls' StyleId attrib. to Xamarin.UITest
        ////    Forms.ViewInitialized += (object sender, ViewInitializedEventArgs e) => {
        ////        if (!string.IsNullOrWhiteSpace(e.View.StyleId))
        ////        {
        ////            e.NativeView.ContentDescription = e.View.StyleId;
        ////        }
        ////    };

        ////    base.OnCreate(bundle);
        ////}

        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);
            Forms.Init(this, bundle);

            var mvxFormsApp = new MvxFormsApplication();

            MvxFormsHybridDroidPresenter presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsHybridDroidPresenter;
            presenter.MvxFormsApp = mvxFormsApp;
        }
    }
}