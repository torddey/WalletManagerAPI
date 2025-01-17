using Microsoft.EntityFrameworkCore;
using WalletManagementApi.Data;
using WalletManagementApi.Models;

namespace WalletManagementApi.Repositories
{

    /// <summary>
    /// Implements the IWalletRepository interface to interact with the wallet data stored in the database.
    /// </summary>
    public class WalletRepository : IWalletRepository
    {
        private readonly WalletDbContext _context;


        /// <summary>
        /// Initializes a new instance of the <see cref="WalletRepository"/> class.
        /// </summary>
        /// <param name="context"> The database context to interact with the wallet data. </param>
        public WalletRepository(WalletDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Retrieves all wallets for a specific owner from the database asynchronously.
        /// </summary>
        /// <param name="owner"> The owner's identifier </param>
        /// <returns> A collection of wallets associated with the owner. </returns>
        public async Task<IEnumerable<Wallet>> GetAllWalletsAsync(string owner)
        {
            return await _context.Wallets.Where(w => w.Owner == owner).ToListAsync();
        }



        /// <summary>
        /// Retrieves a specific wallet by its ID from the database asynchronously.
        /// </summary>
        /// <param name="id"> The unique identifier of the wallet. </param>
        /// <returns> The wallet with the specified ID, or null if not found. </returns>
        public async Task<Wallet> GetWalletByIdAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Wallets.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }



        /// <summary>
        /// Adds a new wallet to the database asynchronously.
        /// </summary>
        /// <param name="wallet"> The wallet to be added. </param>
        /// <returns> A task representing the asynchronous operation. </returns>
        public async Task AddWalletAsync(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Deletes a wallet from the database by its ID asynchronously.
        /// </summary>
        /// <param name="id"> The unique identifier of the wallet to be deleted. </param>
        /// <returns> A task representing the asynchronous operation. </returns>
        public async Task DeleteWalletAsync(Guid id)
        {
            var wallet = await _context.Wallets.FindAsync(id);
            if (wallet != null)
            {
                _context.Wallets.Remove(wallet);
                await _context.SaveChangesAsync();
            }
        }


        /// <summary>
        /// Checks whether a wallet exists for a specific owner and account number asynchronously.
        /// </summary>
        /// <param name="owner"> The owner's identifier. </param>
        /// <param name="accountNumber"> The account number associated with the wallet. </param>
        /// <returns> A boolean value indicating whether the wallet exists. </returns>
        public async Task<bool> WalletExistsAsync(string owner, string accountNumber)
        {
            return await _context.Wallets.AnyAsync(w => w.Owner == owner && w.AccountNumber == accountNumber);
        }


        /// <summary>
        /// Counts the number of wallets associated with a specific owner in the database asynchronously.
        /// </summary>
        /// <param name="owner"> The owner's identifier </param>
        /// <returns> The count of wallets owned by the specified owner. </returns>
        public async Task<int> CountWalletsByOwnerAsync(string owner)
        {
            return await _context.Wallets.CountAsync(w => w.Owner == owner);
        }
    }
}
