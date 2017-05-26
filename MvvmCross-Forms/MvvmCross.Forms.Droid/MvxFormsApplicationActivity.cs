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
    [Activity(Label = "Mvx Forms Host Activity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, NoHistory = false)]
    public class MvxFormsApplicationActivity : FormsApplicationActivity, IMvxAndroidView
    {
        public IMvxBindingContext BindingContext { get; set; }

        private IMvxAndroidActivityLifetimeListener _lifetimeListener;
        private IMvxAndroidActivityLifetimeListener LifetimeListener
        {
            get
            {
                if (_lifetimeListener == null)
                {
                    _lifetimeListener = Mvx.Resolve<IMvxAndroidActivityLifetimeListener>();
                }

                return _lifetimeListener;
            }
        }

        public object DataContext
        {
            get { return BindingContext.DataContext; }
            set { BindingContext.DataContext = value; }
        }

        public IMvxViewModel ViewModel
        {
            get { return DataContext as IMvxViewModel; }
            set
            {
                DataContext = value;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // TODO PL This needs to go into a new class MvxFormsHybridApplicationActivity

            // Start PL

            Xamarin.Forms.Forms.Init(this, bundle);

            var presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsHybridDroidPresenter;
            LoadApplication(presenter.MvxFormsApp);

            // End PL

            // Required for proper Push notifications handling
            var setupSingleton = MvxAndroidSetupSingleton.EnsureSingletonAvailable(ApplicationContext);
            setupSingleton.EnsureInitialized();

            LifetimeListener.OnCreate(this);
        }

        public void MvxInternalStartActivityForResult(Intent intent, int requestCode)
        {
            StartActivityForResult(intent, requestCode);
        }

        protected override void OnStart()
        {
            base.OnStart();

            LifetimeListener.OnStart(this);
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            LifetimeListener.OnStop(this);
        }

        protected override void OnResume()
        {
            base.OnResume();

            LifetimeListener.OnResume(this);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            LifetimeListener.OnDestroy(this);
        }

        protected override void OnPause()
        {
            base.OnPause();

            LifetimeListener.OnPause(this);
        }
        
        protected override void OnRestart()
        {
            base.OnRestart();

            LifetimeListener.OnRestart(this);
        }
    }
}
