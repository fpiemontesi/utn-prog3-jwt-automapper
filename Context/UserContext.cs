using Microsoft.EntityFrameworkCore;

namespace UserApi.Context;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions options)
        :base(options)
    {
    }

    public DbSet<User> User { get; set; }
}
