using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WalletManagementApi.Models;
using WalletManagementApi.Repositories;

namespace WalletManagementApi.Services
{
    public class WalletService
    {
        private readonly IWalletRepository _walletRepository;

        public WalletService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<Wallet> AddWalletAsync(Wallet wallet)
        {
            // Check for duplicate wallet
            if (await _walletRepository.WalletExistsAsync(wallet.Owner, wallet.AccountNumber))
                throw new Exception("Duplicate wallet is not allowed.");

            // Check for wallet limit
            if (await _walletRepository.CountWalletsByOwnerAsync(wallet.Owner) >= 5)
                throw new Exception("A user cannot have more than 5 wallets.");

            // Mask card numbers
            if (wallet.Type == "card")
                wallet.AccountNumber = wallet.AccountNumber.Substring(0, 6);

            await _walletRepository.AddWalletAsync(wallet);
            return wallet;
        }

        public async Task<bool> DeleteWalletAsync(Guid id)
        {
            var wallet = await _walletRepository.GetWalletByIdAsync(id);
            if (wallet == null) return false;

            await _walletRepository.DeleteWalletAsync(id);
            return true;
        }

        public async Task<Wallet> GetWalletByIdAsync(Guid id)
        {
            return await _walletRepository.GetWalletByIdAsync(id);
        }

        public async Task<IEnumerable<Wallet>> GetAllWalletsAsync(string owner)
        {
            return await _walletRepository.GetAllWalletsAsync(owner);
        }
    }
}
