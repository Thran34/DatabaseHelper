using DatabaseHelper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DatabaseHelper.Services
{
    public class UserService
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
            var users = LoadUsers();

            if (users.Any(u => u.Login == username))
            {
                return false;
            }

            User user = new User { Login = username, Password = password };
            users.Add(user);

            string jsonString = JsonSerializer.Serialize(users);
            using (var streamWriter = new StreamWriter(_filePath))
            {
                streamWriter.Write(jsonString);
                streamWriter.Close();
            }

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

            string jsonString;
            using (var reader = new StreamReader(_filePath))
            {
                jsonString = reader.ReadToEnd();
            }

            if (string.IsNullOrEmpty(jsonString))
            {
                return new List<User>();
            }

            return JsonSerializer.Deserialize<List<User>>(jsonString)!;
        }


    }
}
