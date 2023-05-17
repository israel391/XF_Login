using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Firebase.Database;
using Xamarin.Forms;
using Firebase.Database.Query;
using XF_Login.View;
using XF_Login.ViewModel;
using XF_Login.Models;
namespace XF_Login.ViewModel
{
    
    public class AjustesUsuarioVM : INotifyPropertyChanged
    {
        public static FirebaseClient firebase = new FirebaseClient("https://piaxm-a11c4-default-rtdb.firebaseio.com/");
        public AjustesUsuarioVM(string email2)
        {
            Email = email2;
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private string password;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public Command UpdateCommand
        {
            get { return new Command(Update); }
        }

        public Command DeleteCommand
        {
            get { return new Command(Delete); }
        }

        public Command LogoutCommand
        {
            get
            {
                return new Command(() =>
                {
                    App.Current.MainPage.Navigation.PushAsync(new XF_LoginPage());
                });
            }
        }
        //Update user data
        private async void Update()
        {
            try
            {
                if (Password.Length < 6)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Su contraseña debe de ser de minimo 5 caracteres", "Ok");
                }
                else
                {
                    var isUpdated = await FirebaseHelper.UpdateUser(Email, Password);
                    if (isUpdated)
                        await App.Current.MainPage.DisplayAlert("Actualización", "La contraseña ha sido actualizada", "Ok");
                    else
                        await App.Current.MainPage.DisplayAlert("Error", "No se ha logrado actualizar la información del usuario", "Ok");
                }

            }
            catch (Exception e)
            {

                Debug.WriteLine($"Error:{e}");
            }
        }
        //Delete user data

        private async void Delete()
        {
            try
            {
                var isdelete = await FirebaseHelper.DeleteUser(Email);
                if (isdelete)
                    await App.Current.MainPage.Navigation.PushAsync(new XF_LoginPage());
                else
                    await App.Current.MainPage.DisplayAlert("Error", "No se ha logrado eliminar al Usuario", "Ok");
            }
            catch (Exception e)
            {

                Debug.WriteLine($"Error:{e}");
            }
        }
    }
}
