using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeToXF.ViewModels
{
    using MvvmCross.Core.ViewModels;

    public class FirstViewModel : MvxViewModel
    {
        public MvxCommand GoToHelloCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    this.InvokeOnMainThread(() =>
                    {
                        this.ShowViewModel(typeof(HelloViewModel));
                    });
                    
                });
            }
        }

        public MvxCommand GoToSecondCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    this.ShowViewModel(typeof(SecondViewModel));
                });
            }
        }

        public override void Start()
        {
            base.Start();
        }
    }
}
