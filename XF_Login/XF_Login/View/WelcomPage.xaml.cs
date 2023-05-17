using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF_Login.ViewModel;
using XF_Login.Models;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Diagnostics;
using Firebase.Database.Query;

namespace XF_Login.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WelcomPage : INotifyPropertyChanged
    {
        WelcomePageVM welcomePageVM;


        public WelcomPage(string email)
        {
            InitializeComponent();
            welcomePageVM = new WelcomePageVM(email);
            BindingContext = welcomePageVM;
        }

        private async void BtnClienteP_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new ClientesPage());
        }

        private async void BtnAjustesU_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AjustesUsuario(welcomePageVM.Email));
        }


    }
} 