using MvvmCross.Core.ViewModels;
using System.Windows.Input;

namespace NativeToXF.ViewModels
{
    using MvvmCross.Forms.Attributes;
    using System.Collections.Generic;

    [MvxViewWithXamarinForms]
    public class HelloViewModel : MvxViewModel
    {
		private string _yourNickname = "???";
        public string YourNickname
		{ 
			get { return _yourNickname; }
			set
            {
                _yourNickname = value;
                RaisePropertyChanged(() => YourNickname);
                RaisePropertyChanged(() => Hello);
            }
		}

        private double _donationAmount = 0;
        public double Donation
        {
            get { return _donationAmount; }
            set
            {
                SetProperty(ref this._donationAmount, value);
            }
        }

        public string Hello
        {
            get { return "Hello " + YourNickname; }
        }


        public ICommand ShowAboutPageCommand
        {
            get
            {
                return new MvxCommand(() => ShowViewModel<AboutViewModel>());
            }
        }

        public ICommand SaveNickNameCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    this.ShowViewModel<ConfirmationViewModel>(new Dictionary<string, string>(){ { "nickName", this.YourNickname} }, null);
                });
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    this.Close(this);
                });
            }
        }
    }
}
