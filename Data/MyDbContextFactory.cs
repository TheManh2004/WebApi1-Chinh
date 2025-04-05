using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebApi1.Data
{
    public class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseSqlServer("Data Source=manh\\sqlexpress;Initial Catalog=FreeCourseWebAPI;Integrated Security=True;Trust Server Certificate=True");

            return new MyDbContext(optionsBuilder.Options);
        }
    }
}
