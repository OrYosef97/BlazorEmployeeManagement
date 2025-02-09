using System.Text.Json;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public class EmployeeService
    {
        private const string DefaultFilePath = "employees.json";
        private readonly string _filePath;
        private List<Employee> employees = new();

        public EmployeeService()
        {
            _filePath = DefaultFilePath;
            LoadEmployees();
        }

        public EmployeeService(IWebHostEnvironment env)
        {
            // This will store/load employees.json in the Data folder of your project root
            var dataFolder = Path.Combine(env.ContentRootPath, "Data");
            Directory.CreateDirectory(dataFolder); // Ensure the folder exists

            _filePath = Path.Combine(dataFolder, "employees.json");
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                employees = JsonSerializer.Deserialize<List<Employee>>(json) ?? new List<Employee>();
            }
        }

        private void SaveEmployees()
        {
            string json = JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public List<Employee> GetEmployees() => employees;

        public Employee? GetEmployeeById(int id) => employees.FirstOrDefault(e => e.Id == id);

        public void AddEmployee(Employee employee)
        {
            employee.Id = employees.Count > 0 ? employees.Max(e => e.Id) + 1 : 1;
            employees.Add(employee);
            SaveEmployees();
        }

        public void UpdateEmployee(Employee employee)
        {
            var existing = employees.FirstOrDefault(e => e.Id == employee.Id);
            if (existing != null)
            {
                existing.Name = employee.Name;
                existing.Title = employee.Title;
                existing.Salary = employee.Salary;
                existing.ProfileImage = employee.ProfileImage;
                SaveEmployees();
            }
        }

        public void DeleteEmployee(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                employees.Remove(employee);
                SaveEmployees();
            }
        }
    }
}
