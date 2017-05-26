using Android.App;
using Android.Content;
using Android.OS;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Droid.Platform;
using MvvmCross.Droid.Views;
using MvvmCross.Forms.Droid.Presenters;
using MvvmCross.Platform;
using Xamarin.Forms.Platform.Android;

namespace MvvmCross.Forms.Droid
{
    [Activity(Label = "Mvx Forms Hybrid Host Activity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, NoHistory = false)]
    public class MvxFormsHybridApplicationActivity : MvxFormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Xamarin.Forms.Forms.Init(this, bundle);
                        
            base.OnCreate(bundle);

            var presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsHybridDroidPresenter;
            LoadApplication(presenter.MvxFormsApp);
        }
    }
}
