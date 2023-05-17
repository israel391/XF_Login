using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF_Login.Models;
using XF_Login.ViewModel;
using XF_Login.View;
using Firebase.Database.Query;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace XF_Login.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientesPage : ContentPage
    {
        public static FirebaseClient firebase = new FirebaseClient("https://piaxm-a11c4-default-rtdb.firebaseio.com/");
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ClientesPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            var allPersons = await FirebaseHelper.GetAllClientes();
            lstPersons.ItemsSource = allPersons;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        static async Task<bool> VerificarDuplicado(int ClienteId, FirebaseClient firebase)
        {
            var clientes = await firebase.Child("clientes")
                .OrderBy("ClienteId")
                .EqualTo(ClienteId)
                .OnceAsync<Clientes>();

            return clientes.Any();
        }
        Regex regex = new Regex(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$");
        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtNum.Text) || string.IsNullOrEmpty(txtDireccion.Text))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Por favor ingrese datos", "OK");
                }
                else if (!regex.IsMatch(txtName.Text))
                {
                    await DisplayAlert("Error", "El nombre solo puede contener letras", "OK");
                }
                else if (!Regex.IsMatch(txtNum.Text, @"^\d+$"))
                {
                    await DisplayAlert("Error", "Ingrese un número de compras válido", "OK");
                    return;

                }

                else
                {
                    int idCliente = Convert.ToInt32(txtId.Text);
                    int ClienteNum = Convert.ToInt32(txtNum.Text);

                    var clienteExistente = await FirebaseHelper.GetCliente(idCliente);
                    if (clienteExistente != null)
                    {
                        await DisplayAlert("Error", "Ya existe un cliente con esa ID", "Aceptar");
                        return;
                    }
                    


                    await FirebaseHelper.AddCliente(idCliente, txtName.Text, ClienteNum, txtDireccion.Text);
                    txtId.Text = string.Empty;
                    txtName.Text = string.Empty;
                    txtNum.Text = string.Empty;
                    txtDireccion.Text = string.Empty;
                    await DisplayAlert("FIN DEL PROCESO", "El cliente ha sido añadido con éxito", "OK");
                    var allPersons = await FirebaseHelper.GetAllClientes();
                    lstPersons.ItemsSource = allPersons;
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Verifique que el ID esté compuesto solo de caracteres numéricos", "Aceptar");
            }



        }

        private async void BtnRetrive_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtId.Text))
                    await App.Current.MainPage.DisplayAlert("Error", "Porfavor ingrese datos", "OK");


                else
                {
                    var person = await FirebaseHelper.GetCliente(Convert.ToInt32(txtId.Text));
                    if (person != null)
                    {
                        txtId.Text = person.ClienteId.ToString();
                        txtName.Text = person.Cliente_N;
                        txtNum.Text = person.Cliente_Num_Compras.ToString();
                        txtDireccion.Text = person.Cliente_Dir;
                        await DisplayAlert("FIN DEL PROCESO", "La consulta se ha realizado con exito", "OK");

                    }

                    else
                    {
                        await DisplayAlert("FIN DEL PROCESO", "El cliente no ha sido encontrado", "OK");

                    }

                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Verifique que el ID este compuesto solo de caracteres numericos", "Aceptar");
            }

        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Por favor ingrese datos", "OK");
                    return;
                }

                int idCliente = Convert.ToInt32(txtId.Text);

                var clienteExistente = await FirebaseHelper.GetCliente(idCliente);
                if (clienteExistente == null)
                {
                    await DisplayAlert("Error", "El cliente que intenta actualizar no existe", "OK");
                    return;
                }

                string nombre = clienteExistente.Cliente_N;
                int numero = clienteExistente.Cliente_Num_Compras;
                string direccion = clienteExistente.Cliente_Dir;

                if (!string.IsNullOrEmpty(txtName.Text) && !regex.IsMatch(txtName.Text))
                {
                    await DisplayAlert("Error", "Ingrese un nombre valido", "OK");
                    return;
                }

                if (!string.IsNullOrEmpty(txtName.Text))
                {
                    nombre = txtName.Text; 
                }

                
                if (!string.IsNullOrEmpty(txtNum.Text))
                {
                    if (!Regex.IsMatch(txtNum.Text, @"^\d+$"))
                    {
                        await DisplayAlert("Error", "Ingrese un número válido", "OK");
                        return;
                    }
                    numero = Convert.ToInt32(txtNum.Text);
                }
                
               
                if (!string.IsNullOrEmpty(txtDireccion.Text))
                {
                    direccion = txtDireccion.Text;
                }

                await FirebaseHelper.UpdateCliente(idCliente, nombre, numero, direccion);

                txtId.Text = string.Empty;
                txtName.Text = string.Empty;
                txtNum.Text = string.Empty;
                txtDireccion.Text = string.Empty;

                await DisplayAlert("FIN DEL PROCESO", "Los datos del cliente han sido actualizados con éxito", "OK");

                var allPersons = await FirebaseHelper.GetAllClientes();
                lstPersons.ItemsSource = allPersons;
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Verifique que el ID esté compuesto solo de caracteres numéricos", "Aceptar");
            }
        }



        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtId.Text))
                    await App.Current.MainPage.DisplayAlert("Error", "Porfavor ingrese datos", "OK");
                else
                {
                    int idCliente = Convert.ToInt32(txtId.Text);


                    var clienteExistente = await FirebaseHelper.GetCliente(idCliente);
                    if (clienteExistente == null)
                    {
                        await DisplayAlert("Error", "El cliente que intenta eliminar no existe", "OK");
                        return;
                    }

                    await FirebaseHelper.DeleteCliente(Convert.ToInt32(txtId.Text));
                    await DisplayAlert("FIN DEL PROCESO", "El cliente ha sido eliminado con exito", "OK");
                    var allPersons = await FirebaseHelper.GetAllClientes();
                    lstPersons.ItemsSource = allPersons;
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Verifique que el ID este compuesto solo de caracteres numericos", "Aceptar");
            }
        }
    }
}