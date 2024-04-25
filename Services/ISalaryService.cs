using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalariesManagementSystem.Data;


namespace SalariesManagementSystem.Services
{
    public interface ISalaryService
    {
        Task<List<Salary>> GetAllSalariesAsync();
        Task<bool> EmployeeExists(int employeeID);
        Task AddSalaryAsync(Salary salary);

        Task<Salary>GetSalaryByIdAsync(int id);
        Task UpdateSalaryAsync(Salary salary);
        Task DeleteSalaryAsync(int id);
        Task ClearSalariesAsync();
        Task<List<Salary>> FilterSalariesAsync(string orderByOption);
    }
}
