using Microsoft.EntityFrameworkCore;
using WalletManagementApi.Models;

namespace WalletManagementApi.Data
{


    /// <summary>
    /// Represents the database context for managing wallet-related data.
    /// Provides access to the Wallets table and manages database operations.
    /// </summary>
    public class WalletDbContext : DbContext
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="WalletDbContext"/> class with specified options.
        /// </summary>
        /// <param name="options"> The options to configure the DbContext. </param>
        public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options)
        {
        }


        /// <summary>
        /// The options to configure the DbContext.
        /// </summary>
        public DbSet<Wallet> Wallets { get; set; }
    }
}
