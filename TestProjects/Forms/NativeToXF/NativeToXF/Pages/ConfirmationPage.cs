using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace NativeToXF.Pages
{
    public class ConfirmationPage : ContentPage
    {
        public ConfirmationPage()
        {
            Padding = new Thickness(10);

            // see https://forums.xamarin.com/discussion/45111/has-anybody-managed-to-get-a-toolbar-working-on-winrt-windows-using-xf
            if (Device.RuntimePlatform == Device.Windows || Device.RuntimePlatform == Device.WinPhone)
            {
                Padding = new Xamarin.Forms.Thickness(Padding.Left, this.Padding.Top, this.Padding.Right, 95);
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                Padding = new Thickness(Padding.Left, 80, Padding.Right, Padding.Bottom);
            }

            ForceLayout();

            Title = " Confirmation Page";

            var helloResponse = new Label
            {
                Text = string.Empty,
                FontSize = 24
            };

            Content = new StackLayout
            {
                Spacing = 10,
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    helloResponse
                }
            };

            helloResponse.SetBinding(Label.TextProperty, new Binding("ConfirmationMessage"));
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            // Fixed in next version of Xamarin.Forms. BindingContext is not properly set on ToolbarItem.
            var aboutItem = new ToolbarItem { Text = "About", ClassId = "About", Order = ToolbarItemOrder.Primary, BindingContext = BindingContext };
            aboutItem.SetBinding(MenuItem.CommandProperty, new Binding("ShowAboutPageCommand"));


            ToolbarItems.Add(aboutItem);
        }
    }
}
