using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF_Login.ViewModel;

namespace XF_Login.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class XF_SignUpPage : ContentPage
    {
        SignUpVM signUpVM;
        public XF_SignUpPage()
        {
            InitializeComponent();
            signUpVM = new SignUpVM();
            //set binding
            BindingContext = signUpVM;
        }
    }
}