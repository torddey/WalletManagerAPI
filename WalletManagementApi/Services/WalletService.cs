using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletManagementApi.Data;
using WalletManagementApi.Models;

namespace WalletManagementApi.Services
{
    public class WalletService
    {
        private readonly WalletDbContext _context;

        public WalletService(WalletDbContext context)
        {
            _context = context;
        }

        public async Task<Wallet> AddWalletAsync(Wallet wallet)
        {
            var existingWallets = _context.Wallets.Where(w => w.Owner == wallet.Owner).ToList();

            if (existingWallets.Any(w => w.AccountNumber == wallet.AccountNumber))
                throw new Exception("Duplicate wallet is not allowed.");

            if (existingWallets.Count >= 5)
                throw new Exception("A user cannot have more than 5 wallets.");

            if (wallet.Type == "card")
                wallet.AccountNumber = wallet.AccountNumber.Substring(0, 6);

            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();
            return wallet;
        }

        public async Task<bool> DeleteWalletAsync(Guid id)
        {
            var wallet = await _context.Wallets.FindAsync(id);
            if (wallet == null) return false;

            _context.Wallets.Remove(wallet);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Wallet> GetWalletByIdAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Wallets.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IEnumerable<Wallet>> GetAllWalletsAsync(string owner)
        {
            return await Task.FromResult(_context.Wallets.Where(w => w.Owner == owner).ToList());
        }
    }
}
