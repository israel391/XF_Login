using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XF_Login.Models;
using XF_Login.ViewModel;
namespace XF_Login.ViewModel
{
   public class FirebaseHelper
    {
      public static FirebaseClient firebase = new FirebaseClient("https://piaxm-a11c4-default-rtdb.firebaseio.com/");

        //Read All
        public static async Task<List<Users>> GetAllUser()
        {
            try
            {
                var userlist = (await firebase
                .Child("Users")
                .OnceAsync<Users>()).Select(item =>
                new Users
                {
                    Email = item.Object.Email,
                    Password = item.Object.Password
                }).ToList();
                return userlist;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Read 
        public static async Task<Users> GetUser(string email)
        {
            try
            {
                var allUsers = await GetAllUser();
                await firebase
                .Child("Users")
                .OnceAsync<Users>();
                return allUsers.Where(a => a.Email == email).FirstOrDefault();
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Inser a user
        public static async Task<bool> AddUser(string email,string password)
        {
            try
            {


                await firebase
                .Child("Users")
                .PostAsync(new Users() { Email = email, Password = password });
                return true;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        //Update 
        public static async Task<bool> UpdateUser(string email,string password)
        {
            try
            {


                var toUpdateUser = (await firebase
                .Child("Users")
                .OnceAsync<Users>()).Where(a => a.Object.Email == email).FirstOrDefault();
                await firebase
                .Child("Users")
                .Child(toUpdateUser.Key)
                .PutAsync(new Users() { Email = email, Password = password });
                return true;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        //Delete User
        public static async Task<bool> DeleteUser(string email)
        {
            try
            {


                var toDeletePerson = (await firebase
                .Child("Users")
                .OnceAsync<Users>()).Where(a => a.Object.Email == email).FirstOrDefault();
                await firebase.Child("Users").Child(toDeletePerson.Key).DeleteAsync();
                return true;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
        //Clientes
        public static async Task<List<Clientes>> GetAllClientes()
        {
            try
            {
             
                var clientlist = (await firebase
                .Child("Clientes")
                .OnceAsync<Clientes>()).Select(item =>
                new Clientes
                {
                    ClienteId = item.Object.ClienteId,
                    Cliente_N = item.Object.Cliente_N,
                    Cliente_Num_Compras = item.Object.Cliente_Num_Compras,
                    Cliente_Dir = item.Object.Cliente_Dir
                }).ToList();
                return clientlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        
        public static async Task<Clientes> GetCliente(int clienteid)
        {
            try
            {
                var allclientes = await GetAllClientes();
                await firebase
                .Child("Cliente")
                .OnceAsync<Clientes>();
                return allclientes.Where(a => a.ClienteId == clienteid).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        public static async Task<bool> AddCliente(int clienteid, string clienteN, int clienteNum, string ClienteDir)
        {
            try
            {


                await firebase
                .Child("Clientes")
                .PostAsync(new Clientes() { ClienteId = clienteid, Cliente_N = clienteN, Cliente_Num_Compras= clienteNum, Cliente_Dir=ClienteDir });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
 
        public static async Task<bool> UpdateCliente(int clienteid, string clienteN, int clienteNum, string clienteDir)
        {
            try
            {
                var toUpdateCliente = (await firebase
                    .Child("Clientes")
                    .OnceAsync<Clientes>())
                    .Where(a => a.Object.ClienteId == clienteid)
                    .FirstOrDefault();

                if (toUpdateCliente != null)
                {
                    await firebase
                        .Child("Clientes")
                        .Child(toUpdateCliente.Key)
                        .PutAsync(new Clientes()
                        {
                            ClienteId = clienteid,
                            Cliente_N = clienteN,
                            Cliente_Num_Compras = clienteNum,
                            Cliente_Dir = clienteDir
                        });

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }





        public static async Task<bool> DeleteCliente(int clienteid)
        {
            try
            {


                var toDeleteCliente = (await firebase
                .Child("Clientes")
                .OnceAsync<Clientes>()).Where(a => a.Object.ClienteId == clienteid).FirstOrDefault();
                await firebase.Child("Clientes").Child(toDeleteCliente.Key).DeleteAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

    }
}
