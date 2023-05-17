using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Firebase.Database;
using Xamarin.Forms;
using XF_Login.Models;
using XF_Login.View;
using System.Text.RegularExpressions;
namespace XF_Login.ViewModel
{
    public class SignUpVM : INotifyPropertyChanged
    {
        public static FirebaseClient firebase = new FirebaseClient("https://piaxm-a11c4-default-rtdb.firebaseio.com/");
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

        private string confirmpassword;
        public string ConfirmPassword
        {
            get { return confirmpassword; }
            set
            {
                confirmpassword = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ConfirmPassword"));
            }
        }
        public Command SignUpCommand
        {
            get
            {
                return new Command(() => 
                {
                    if (Password == ConfirmPassword)
                        SignUp();
                    else
                         App.Current.MainPage.DisplayAlert("", "La Contraseña debe ser la misma!", "OK");
                } );
            }
        }
        private async void SignUp()
        {
            
            
            var user2 = await firebase.Child("Users").OnceAsync<Users>();
            foreach (var users in user2)
            {
                if (users.Object.Email == email)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "La dirección de correo electronico ya esta registrada", "OK");
                    return;
                }
            }

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Ingresa un correo y una contraseña", "OK");
            }
            else if (!Regex.IsMatch(Email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"))
            {
                await App.Current.MainPage.DisplayAlert("Error", "El correo electrónico no es válido", "OK");
            }
            else if (Password.Length < 6)
            {
                await App.Current.MainPage.DisplayAlert("Error", "La contraseña debe tener al menos 6 caracteres", "OK");
            }

            else
                    {
                        
                        var user = await FirebaseHelper.AddUser(Email, Password);
                        
                        if (user)
                        {
                            await App.Current.MainPage.DisplayAlert("Registro Exitoso", "Bienvenido!", "Ok");
                            
                            await App.Current.MainPage.Navigation.PushAsync(new WelcomPage(Email));
                        }
                        else
                            await App.Current.MainPage.DisplayAlert("Error", "Registro Fallido", "OK");

                    }
                
            }
        
    }
}

