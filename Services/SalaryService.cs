using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SalariesManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SalariesManagementSystem.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly string _connectionString;

        public SalaryService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Salary>> GetAllSalariesAsync()
        {
            List<Salary> salaries = new List<Salary>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = "SELECT * FROM salary";
                using (var command = new MySqlCommand(sql, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            salaries.Add(new Salary()
                            {
                                EmployeeID = reader.GetInt32("EmployeeID"),
                                EmployeeName = reader.GetString("EmployeeName"),
                                Career = reader.GetString("Career"),
                                Sal = reader.GetDouble("Salary")
                            });
                        }
                    }
                }
            }

            return salaries;
        }

        public async Task<bool> EmployeeExists(int employeeID)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("SELECT COUNT(1) FROM salary WHERE EmployeeID = @EmployeeID", connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeID);
                    var result = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return result > 0;
                }
            }
        }

        public async Task AddSalaryAsync(Salary salary)
        {
            if (await EmployeeExists(salary.EmployeeID))
            {
                throw new InvalidOperationException($"Your employee ID {salary.EmployeeID} already exists in our system. \n Please choose another ID.");
            }

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = "INSERT INTO salary (EmployeeID, EmployeeName, Career, Salary) VALUES (@EmployeeID, @EmployeeName, @Career, @Salary)";
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeID", salary.EmployeeID);
                        command.Parameters.AddWithValue("@EmployeeName", salary.EmployeeName);
                        command.Parameters.AddWithValue("@Career", salary.Career);
                        command.Parameters.AddWithValue("@Salary", salary.Sal);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<Salary> GetSalaryByIdAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = "SELECT * FROM salary WHERE EmployeeID = @EmployeeID";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new Salary()
                            {
                                EmployeeID = reader.GetInt32("EmployeeID"),
                                EmployeeName = reader.GetString("EmployeeName"),
                                Career = reader.GetString("Career"),
                                Sal = reader.GetDouble("Salary")
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public async Task UpdateSalaryAsync(Salary salary)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var existsCommand = new MySqlCommand("SELECT COUNT(1) FROM salary WHERE EmployeeID = @EmployeeID", connection);
                existsCommand.Parameters.AddWithValue("@EmployeeID", salary.EmployeeID);
                var exists = Convert.ToInt32(await existsCommand.ExecuteScalarAsync()) > 0;
                if (!exists)
                {
                    throw new InvalidOperationException("No salary entry exists with the given Employee ID.");
                }

                string sql = "UPDATE salary SET EmployeeName = @EmployeeName, Career = @Career, Salary = @Salary WHERE EmployeeID = @EmployeeID";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", salary.EmployeeID);
                    command.Parameters.AddWithValue("@EmployeeName", salary.EmployeeName);
                    command.Parameters.AddWithValue("@Career", salary.Career);
                    command.Parameters.AddWithValue("@Salary", salary.Sal);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        Task<Salary> ISalaryService.GetSalaryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteSalaryAsync(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string sql = "DELETE FROM salary WHERE EmployeeID = @EmployeeID";
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeID", id);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting salary: {ex.Message}");
                throw;
            }
        }

        public async Task ClearSalariesAsync()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string sql = "DELETE FROM salary";
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while clearing salaries: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Salary>> FilterSalariesAsync(string orderByOption)
        {
            List<Salary> salaries = new List<Salary>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = orderByOption == "Salary" ?
                    "SELECT * FROM salary ORDER BY Salary" :
                    "SELECT * FROM salary ORDER BY EmployeeName";

                using (var command = new MySqlCommand(sql, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            salaries.Add(new Salary()
                            {
                                EmployeeID = reader.GetInt32("EmployeeID"),
                                EmployeeName = reader.GetString("EmployeeName"),
                                Career = reader.GetString("Career"),
                                Sal = reader.GetDouble("Salary")
                            });
                        }
                    }
                }
            }

            return salaries;
        }
    }
}
