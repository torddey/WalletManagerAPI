using WalletManagementApi.Models;

namespace WalletManagementApi.Repositories
{


    /// <summary>
    ///  Defines the methods for interacting with wallet data.
    /// </summary>
    public interface IWalletRepository
    {

        /// <summary>
        /// Retrieves all wallets for a specific owner asynchronously.
        /// </summary>
        /// <param name="owner">The owner's identifier.</param>
        /// <returns> A collection of wallets associated with the owner. </returns>
        Task<IEnumerable<Wallet>> GetAllWalletsAsync(string owner);


        /// <summary>
        /// Retrieves a specific wallet by its ID asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the wallet.</param>
        /// <returns>The wallet with the specified ID.</returns>
        Task<Wallet> GetWalletByIdAsync(Guid id);


        /// <summary>
        /// Adds a new wallet to the repository asynchronously.
        /// </summary>
        /// <param name="wallet">The wallet to be added.</param>
        /// <returns> A task representing the asynchronous operation.</returns>
        Task AddWalletAsync(Wallet wallet);


        /// <summary>
        /// Deletes a wallet from the repository by its ID asynchronously.
        /// </summary>
        /// <param name="id"> The unique identifier of the wallet to be deleted.</param>
        /// <returns> A task representing the asynchronous operation. </returns>
        Task DeleteWalletAsync(Guid id);


        /// <summary>
        /// Checks whether a wallet exists for a specific owner and account number asynchronously.
        /// </summary>
        /// <param name="owner"> The owner's identifier.</param>
        /// <param name="accountNumber"> The account number associated with the wallet.</param>
        /// <returns> A boolean value indicating whether the wallet exists. </returns>
        Task<bool> WalletExistsAsync(string owner, string accountNumber);


        /// <summary>
        /// Counts the number of wallets associated with a specific owner asynchronously.
        /// </summary>
        /// <param name="owner"> The owner's identifier. </param>
        /// <returns> The count of wallets owned by the specified owner.</returns>
        Task<int> CountWalletsByOwnerAsync(string owner);
    }
}
