using Microsoft.EntityFrameworkCore;
namespace SchoolManagementSystem.Models
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Student> Students { get; set; }

        // Retrieve all students
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await Students.FromSqlInterpolated($"CALL GetStudents()").ToListAsync();
        }

        // Add a student
        public async Task AddStudentAsync(string name, string address, DateTime dateOfBirth, string email, string contactNumber)
        {
            await Database.ExecuteSqlInterpolatedAsync($"CALL AddStudent({name}, {address}, {dateOfBirth}, {email}, {contactNumber})");
        }

        // Update a student
        public async Task UpdateStudentAsync(int id, string name, string address, DateTime dateOfBirth, string email, string contactNumber)
        {
            await Database.ExecuteSqlInterpolatedAsync($"CALL UpdateStudent({id}, {name}, {address}, {dateOfBirth}, {email}, {contactNumber})");
        }

        // Delete a student
        public async Task DeleteStudentAsync(int id)
        {
            await Database.ExecuteSqlInterpolatedAsync($"CALL DeleteStudent({id})");
        }
    }

}
