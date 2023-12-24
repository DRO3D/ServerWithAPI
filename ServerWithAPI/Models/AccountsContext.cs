using Microsoft.EntityFrameworkCore;

namespace ServerWithAPI.Models
{
    public class AccountsContext : DbContext
    {
        public AccountsContext(DbContextOptions<AccountsContext> options) : base(options)
        {

        }


        public DbSet<AccountsModel> AccountsModels { get; set; } = null!;
    }
}
