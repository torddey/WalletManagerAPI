using Microsoft.EntityFrameworkCore;
using WalletManagementApi.Models;

namespace WalletManagementApi.Data
{
    public class WalletDbContext : DbContext
    {
        public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options)
        {
        }

        public DbSet<Wallet> Wallets { get; set; }
    }
}
