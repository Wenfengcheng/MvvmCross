using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Views;
using MvvmCross.Forms.Core;
using Xamarin.Forms;
using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Presenters;
using MvvmCross.Platform;
using MvvmCross.Forms.Attributes;

namespace MvvmCross.Forms.Droid.Presenters
{
    public class MvxFormsHybridDroidPresenter : MvxAndroidViewPresenter
    {
        private MvxFormsApplication mvxFormsApp;

        public MvxFormsApplication MvxFormsApp
        {
            get
            {
                return this.mvxFormsApp;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException("MvxFormsApp cannot be null");
                }

                this.mvxFormsApp = value;
            }
        }

        public override void Show(MvxViewModelRequest request)
        {
            var viewFromXF = request.ViewModelType.GetCustomAttributes(typeof(MvxViewWithXamarinFormsAttribute), false);

            if (viewFromXF.Any())
            {
                // get the Forms page from the request 
                var contentPage = MvxPresenterHelpers.CreatePage(request);

                //set DataContext of page to LoadViewModel
                var viewModel = MvxPresenterHelpers.LoadViewModel(request);

                //set the binding context of the content page
                contentPage.BindingContext = viewModel;

                var mainPage = mvxFormsApp.MainPage as NavigationPage;

                if (mainPage == null)
                {
                    // XF application exists but no Root.
                    this.InitialiseXfActivityStack(contentPage);
                }
                else
                {
                    try
                    {
                        if (this.Activity is MvxFormsApplicationActivity == false)
                        {
                            // XF application exists and there is a Root however the TopMostActivity
                            // (this.Activity) is not MvxFormsApplicationActivity. We must be back in Native world
                            // Restart XF stack
                            this.InitialiseXfActivityStack(contentPage);
                        }
                        else
                        {
                            mainPage.PushAsync(contentPage);
                        }
                    }
                    catch (Exception e)
                    {
                        Mvx.Error("Exception pushing {0}: {1}\n{2}", contentPage.GetType(), e.Message, e.StackTrace);
                    }
                }
            }
            else
            {
                base.Show(request);
            }
        }

        private void InitialiseXfActivityStack(Page contentPage)
        {
            this.MvxFormsApp = new MvxFormsApplication();

            // Start the Xamarin.Forms Activity
            this.Activity.StartActivity(typeof(MvxFormsHybridApplicationActivity));

            NavigationPage navigationPage = new NavigationPage(contentPage);
            mvxFormsApp.MainPage = navigationPage;
        }
    }
}