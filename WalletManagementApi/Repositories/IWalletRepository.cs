using WalletManagementApi.Models;

namespace WalletManagementApi.Repositories
{
    public interface IWalletRepository
    {
        Task<IEnumerable<Wallet>> GetAllWalletsAsync(string owner);
        Task<Wallet> GetWalletByIdAsync(Guid id);
        Task AddWalletAsync(Wallet wallet);
        Task DeleteWalletAsync(Guid id);
        Task<bool> WalletExistsAsync(string owner, string accountNumber);
        Task<int> CountWalletsByOwnerAsync(string owner);
    }
}
