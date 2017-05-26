using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Attributes;
using System.Windows.Input;

namespace NativeToXF.ViewModels
{
    [MvxViewWithXamarinForms]
    public class ConfirmationViewModel : MvxViewModel
    {
        private string _yourNickname;
        public string YourNickname
		{ 
			get { return _yourNickname; }
            set
            {
                _yourNickname = value;
                RaisePropertyChanged(() => YourNickname);
                RaisePropertyChanged(() => ConfirmationMessage);
            }
		}

        public string ConfirmationMessage
        {
            get { return "Congratulations " + YourNickname + "!"; }
        }
        
        public ICommand ShowAboutPageCommand
        {
            get
            {
                return new MvxCommand(() => ShowViewModel<AboutViewModel>());
            }
        }

        public void Init(string nickName)
        {
            this.YourNickname = nickName;
        }
    }
}
