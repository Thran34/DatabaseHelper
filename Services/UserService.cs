using DatabaseHelper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DatabaseHelper.Services
{
    internal class UserService
    {
        private readonly string _filePath;

        public UserService()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolderPath = Path.Combine(appDataPath, "DatabaseHelper");
            if (!Directory.Exists(appFolderPath))
            {
                Directory.CreateDirectory(appFolderPath);
            }
            _filePath = Path.Combine(appFolderPath, "users.json");
        }

        public bool RegisterUser(string username, string password)
        {
            User user = new User { Login = username, Password = password };
            var users = LoadUsers();
            users.Add(user);
            string jsonString = JsonSerializer.Serialize(users);
            File.WriteAllText(_filePath, jsonString);
            return true;
        }
        public bool LoginUser(string username, string password)
        {
            var users = LoadUsers();
            bool isValid = users.Any(user => username == user.Login && password == user.Password);

            if (isValid)
            {
                return true;
            }

            return false;
        }
        private List<User> LoadUsers()
        {
            if (!File.Exists(_filePath))
            {
                return new List<User>();
            }
            string jsonString = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<User>>(jsonString)!;
        }
    }
}
