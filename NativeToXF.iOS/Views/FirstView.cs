using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using NativeToXF.ViewModels;
using System.Drawing;

namespace NativeToXF.iOS.Views
{
    public class FirstView : MvxViewController<FirstViewModel>
    {
        public FirstView(IntPtr handle) : base(handle)
        {
        }

        public FirstView() : base()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.Title = "First Page (Native)";
            this.View.BackgroundColor = UIColor.White;

            var button = new UIButton(UIButtonType.System);
            button.Font = UIFont.BoldSystemFontOfSize(28.0f);
            button.Frame = new RectangleF(10, 190, 300, 40);
            button.SetTitle("Go To Hello", UIControlState.Normal);

            this.Add(button);
            
            var set = this.CreateBindingSet<FirstView, FirstViewModel>();

            set.Bind(button).To(vm => vm.GoToHelloCommand);

            set.Apply();
        }
    }
}