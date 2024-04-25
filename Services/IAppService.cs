using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalariesManagementSystem.Models;

namespace SalariesManagementSystem.Services
{
    public interface IAppService
    {
        Task<string> AuthenticateUser(LoginModel loginModel);
        Task RegisterUser(RegistrationModel registrationModel);
    }
}
