using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkOracle
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<OracleDbContext>());

            using (var ctx = new OracleDbContext())
            {
                var emp = new Employee
                {
                    HireDate = DateTime.Now,
                    Name = "Tom"
                };

                ctx.Employee.Add(emp);
                ctx.SaveChanges();

                var dept = new Department
                {
                    Name = "Accounting",
                    ManagerId = emp.Id
                };

                ctx.Department.Add(dept);
                ctx.SaveChanges();
            }

            Console.Write("Oracle!");
            Console.ReadKey();
        }
    }

    class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public Employee Manager { get; set; }
    }

    class Employee
    {
        public int Id { get; set; }
        public DateTime HireDate { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    class OracleDbContext : DbContext
    {
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("HR");
        }
    }
}
