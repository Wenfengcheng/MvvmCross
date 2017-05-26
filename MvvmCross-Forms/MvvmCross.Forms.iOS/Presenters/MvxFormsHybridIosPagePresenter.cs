using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Forms.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Attributes;
using MvvmCross.Forms.Presenters;
using Xamarin.Forms;

namespace MvvmCross.Forms.iOS.Presenters
{
    public class MvxFormsHybridIosPagePresenter : MvxIosViewPresenter
    {
        public MvxFormsHybridIosPagePresenter(IUIApplicationDelegate applicationDelegate, UIWindow window, MvxFormsApplication mvxFormsApp) : base (applicationDelegate, window)
        {
            this.MvxFormsApp = mvxFormsApp;
        }

        private MvxFormsApplication mvxFormsApp;

        public MvxFormsApplication MvxFormsApp
        {
            get { return this.mvxFormsApp; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("MvxFormsApp cannot be null");
                }

                this.mvxFormsApp = value;
            }
        }

        public override void ChangePresentation(MvxPresentationHint hint)
        {
            this.MasterNavigationController.PopViewController(true);
        }

        public override void Show(MvxViewModelRequest request)
        {
            var viewFromXf = request.ViewModelType.GetCustomAttributes(typeof(MvxViewWithXamarinFormsAttribute), false);

            if (viewFromXf.Any())
            {
                var contentPage = MvxPresenterHelpers.CreatePage(request);
                //set DataContext of page to LoadViewModel
                var viewModel = MvxPresenterHelpers.LoadViewModel(request);

                contentPage.BindingContext = viewModel;

                var mainPage = this.MvxFormsApp.MainPage as NavigationPage;

                if (mainPage == null)
                {
                    this.MvxFormsApp.MainPage = new NavigationPage(contentPage);
                    mainPage = MvxFormsApp.MainPage as NavigationPage;
                }

                UIViewController vc = contentPage.CreateViewController();

                vc.NavigationItem.Title = contentPage.Title;

                if (this.MasterNavigationController == null)
                {
                    _window.RootViewController = vc;
                }
                else
                {

                    this.MasterNavigationController.PushViewController(vc, true);
                }
            }
            else
            {
                base.Show(request);
            }
        }
    }
}