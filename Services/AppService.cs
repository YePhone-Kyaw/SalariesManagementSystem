using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SalariesManagementSystem.Models;

namespace SalariesManagementSystem.Services
{
    public class AppService : IAppService
    {
        private readonly string _filePath = @"C:\SAIT SD TERM 2\C#\ASSIGNMENT\FINAL PROJECT\SalariesManagementSystem\Data\UserCredentials.txt";

        public async Task<string> AuthenticateUser(LoginModel loginModel)
        {
            var credentials = await ReadCredentialsFromFile();
            var matchedCredential = credentials.FirstOrDefault(c => c.Key == loginModel.UserName && c.Value == loginModel.Password);

            if (matchedCredential.Key != null)
            {
                return "Login successful.";
            }
            else
            {
                return "Invalid username or password.";
            }
        }

        private async Task<Dictionary<string, string>> ReadCredentialsFromFile()
        {
            var credentials = new Dictionary<string, string>();

            if (File.Exists(_filePath))
            {
                var lines = await File.ReadAllLinesAsync(_filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length >= 5)
                    {
                        credentials[parts[0].Trim()] = parts[1].Trim();
                    }
                }
            }

            return credentials;
        }

        public async Task RegisterUser(RegistrationModel userModel)
        {
            string newUserLine = $"{userModel.Email}, {userModel.Password}, {userModel.FirstName}, {userModel.LastName}, {userModel.Gender}\n";
            await File.AppendAllTextAsync(_filePath, newUserLine);
        }

    }
}
