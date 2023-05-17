using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using XF_Login.ViewModel;


namespace XF_Login.ViewModel
{
   public class WelcomePageVM: INotifyPropertyChanged
    {
        
        public WelcomePageVM(string email2)
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
            set { password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        
        
    }

} 
