using Windows.UI.Xaml.Controls;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Uwp.Platform;
using Windows.ApplicationModel.Activation;
using MvvmCross.Uwp.Views;
using Xamarin.Forms;
using MvvmCross.Forms.Core;
using MvvmCross.Forms.Uwp.Presenters;
using MvvmCross.Core.Views;
using MvvmCross.Platform;
using System.Reflection;
using NativeToXF.UWP.Views;

namespace NativeToXF.UWP
{
    public class Setup : MvxWindowsSetup
    {
        private readonly LaunchActivatedEventArgs _launchActivatedEventArgs;

        private readonly Windows.UI.Xaml.Controls.Frame rootFrame;

        public Setup(Windows.UI.Xaml.Controls.Frame rootFrame, LaunchActivatedEventArgs e) : base(rootFrame)
        {
            _launchActivatedEventArgs = e;
            this.rootFrame = rootFrame as Windows.UI.Xaml.Controls.Frame;
        }


        protected override IMvxApplication CreateApp()
        {
            return new NativeToXF.App();
        }

        protected override IMvxWindowsViewPresenter CreateViewPresenter(IMvxWindowsFrame rootFrame)
        {
            Xamarin.Forms.Forms.Init(_launchActivatedEventArgs);

            var xamarinFormsApp = new MvxFormsApplication();
            var presenter = new MvxFormsHybridUwpPagePresenter(rootFrame, xamarinFormsApp);
            Mvx.RegisterSingleton<IMvxViewPresenter>(presenter);
            presenter.OnInitialise += Presenter_OnInitialise;

            return presenter;
        }

        private void Presenter_OnInitialise(object sender, MvxFormsHybridUwpPagePresenter.CustomEventArgs e)
        {
            this.rootFrame.Navigate(e.Page.GetType(), e.Message);
        }
    }
}
