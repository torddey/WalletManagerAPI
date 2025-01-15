using Microsoft.EntityFrameworkCore;
using WalletManagementApi.Models;

namespace WalletManagementApi.Data
{
    public class WalletDbContext : DbContext
    {
        public DbSet<Wallet> Wallets { get; set; } // Table for wallets

        public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options) { }
    }
}
