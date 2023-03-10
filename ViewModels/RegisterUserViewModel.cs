using DatabaseHelper.Models;
using System;

namespace DatabaseHelper.ViewModels
{
    internal class RegisterUserViewModel
    {
        public bool RegisterUser(string username, string password)
        {
            try
            {
                using (var context = new Context.Context())
                {
                    context.Users.Add(new User { Login = username, Password = password, Role = "admin" });
                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
