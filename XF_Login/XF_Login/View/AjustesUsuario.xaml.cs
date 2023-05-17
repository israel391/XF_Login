using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XF_Login.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF_Login.Models;
using System.ComponentModel;

namespace XF_Login.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AjustesUsuario : INotifyPropertyChanged
    {
        public AjustesUsuario(string email)
        {
            InitializeComponent();
            BindingContext = new AjustesUsuarioVM(email);
        }
    }
}