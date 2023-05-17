using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Firebase.Database;
using Xamarin.Forms;
using XF_Login.Models;
using XF_Login.View;


namespace XF_Login.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public static FirebaseClient firebase = new FirebaseClient("https://piaxm-a11c4-default-rtdb.firebaseio.com/");
        public event PropertyChangedEventHandler PropertyChanged;
        public LoginViewModel()
        {

        }
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public Command LoginCommand
        {
            get
            {
                return new Command(Login);
            }
        }
        public Command SignUp
        {
            get
            {
                return new Command(() => { App.Current.MainPage.Navigation.PushAsync(new XF_SignUpPage()); });
            }
        }

        private async void Login()
        {
            
            //null or empty field validation, check weather email and password is null or empty
            
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                await App.Current.MainPage.DisplayAlert("Error", "Porfavor ingrese un correo y una contraseña", "OK");

            else
            {

                //call GetUser function which we define in Firebase helper class
                var user = await FirebaseHelper.GetUser(Email);
                //firebase return null valuse if user data not found in database
                if (user != null)
                    if (Email == user.Email && Password == user.Password)
                    {
                        await App.Current.MainPage.DisplayAlert("Login Exitoso", "Bienvenido!", "Ok");
                        //Navigate to Wellcom page after successfuly login
                        //pass user email to welcom page
                        await App.Current.MainPage.Navigation.PushAsync(new WelcomPage(Email));
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("Error", "Por favor ingrese un Correo y una Contraseña", "OK");
                else
                    await App.Current.MainPage.DisplayAlert("Error", "El usuario no existe", "OK");
            }


        }

    }
}
