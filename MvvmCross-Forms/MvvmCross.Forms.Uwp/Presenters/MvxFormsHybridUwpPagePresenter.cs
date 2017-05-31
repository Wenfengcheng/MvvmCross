using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Attributes;
using MvvmCross.Forms.Core;
using MvvmCross.Forms.Presenters;
using MvvmCross.Platform;
using MvvmCross.Platform.Core;
using MvvmCross.Uwp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MvvmCross.Forms.Uwp.Presenters
{
    public class MvxFormsHybridUwpPagePresenter : MvxWindowsViewPresenter
    {
        private MvxFormsApplication mvxFormsApp;

        private readonly IMvxWindowsFrame _rootFrame;

        public class CustomEventArgs : EventArgs
        {
            public CustomEventArgs(Page page, string s)
            {
                msg = s;
                this.page = page;
            }
            private string msg;
            public string Message
            {
                get { return msg; }
            }

            private Page page;
            public Page Page
            {
                get { return this.page; }
            }
        }

        public delegate void CustomEventHandler(object sender, CustomEventArgs a);

        public event CustomEventHandler OnInitialise;

        public MvxFormsHybridUwpPagePresenter(IMvxWindowsFrame rootFrame, MvxFormsApplication mvxFormsApp) : base(rootFrame)
        {
            MvxFormsApp = mvxFormsApp;
            _rootFrame = rootFrame;
        }

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

                //////set the binding context of the content page
                               
                var mvxContentPage = contentPage as IMvxContentPage;
                if (mvxContentPage != null)
                {
                    mvxContentPage.Request = request;
                    mvxContentPage.ViewModel = viewModel;
                }
                else
                {
                    contentPage.BindingContext = viewModel;
                }

                var mainPage = mvxFormsApp.MainPage as NavigationPage;

                if (mainPage == null)
                {
                    this.MvxFormsApp.MainPage = new NavigationPage(contentPage);
                    mainPage = MvxFormsApp.MainPage as NavigationPage;
                    
                    var converter = Mvx.Resolve<IMvxNavigationSerializer>();
                    var requestText = converter.Serializer.SerializeObject(request);
                    
                    _rootFrame.Navigate(mainPage.GetType(), requestText); 
                }
                else
                {
                    try
                    {
                        ((NavigationPage)mainPage).PushAsync(contentPage);
                        
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
    }
}
