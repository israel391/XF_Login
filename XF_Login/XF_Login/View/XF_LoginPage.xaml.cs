using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF_Login.ViewModel;

namespace XF_Login.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class XF_LoginPage : ContentPage
	{
        LoginViewModel loginViewModel;
        public XF_LoginPage ()
		{
            loginViewModel = new LoginViewModel(); 
			InitializeComponent ();
            BindingContext = loginViewModel;
            NavigationPage.SetHasBackButton(this, false);
           

        
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}