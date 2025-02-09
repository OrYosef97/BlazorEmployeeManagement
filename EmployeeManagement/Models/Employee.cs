using System.ComponentModel.DataAnnotations;
namespace EmployeeManagement.Models;
public class Employee
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; } = string.Empty;

    [Range(1, 1000000, ErrorMessage = "Salary must be between 1 and 1,000,000.")]
    public decimal Salary { get; set; }

    public string ProfileImage { get; set; } = "default.png";
}
