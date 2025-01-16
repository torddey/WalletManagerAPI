using Microsoft.EntityFrameworkCore;
using WalletManagementApi.Data;
using WalletManagementApi.Models;

namespace WalletManagementApi.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly WalletDbContext _context;

        public WalletRepository(WalletDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Wallet>> GetAllWalletsAsync(string owner)
        {
            return await _context.Wallets.Where(w => w.Owner == owner).ToListAsync();
        }

        public async Task<Wallet> GetWalletByIdAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Wallets.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task AddWalletAsync(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWalletAsync(Guid id)
        {
            var wallet = await _context.Wallets.FindAsync(id);
            if (wallet != null)
            {
                _context.Wallets.Remove(wallet);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> WalletExistsAsync(string owner, string accountNumber)
        {
            return await _context.Wallets.AnyAsync(w => w.Owner == owner && w.AccountNumber == accountNumber);
        }

        public async Task<int> CountWalletsByOwnerAsync(string owner)
        {
            return await _context.Wallets.CountAsync(w => w.Owner == owner);
        }
    }
}
