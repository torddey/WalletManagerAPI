using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WalletManagementApi.Models;
using WalletManagementApi.Repositories;

namespace WalletManagementApi.Services


{
   /// <summary>
   /// Provides business logic for managing wallets, including adding, deleting, and retrieving wallets.
   /// </summary>
    public class WalletService
    {
        private readonly IWalletRepository _walletRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WalletService"/> class with the specified repository.
        /// </summary>
        /// <param name="walletRepository"> The repository to manage wallet data.</param>
        public WalletService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }


        /// <summary>
        /// Adds a new wallet to the system.
        /// </summary>
        /// <param name="wallet">The wallet to be added.</param>
        /// <returns> Operation's success status, message, and the created wallet.</returns>
        public async Task<ServiceResult<Wallet>> AddWalletAsync(Wallet wallet)
        {
            // Validate wallet data
            if (wallet == null || string.IsNullOrWhiteSpace(wallet.Owner) || string.IsNullOrWhiteSpace(wallet.AccountNumber))
                return ServiceResult<Wallet>.FailureResult("Invalid wallet data.");

            // Check for duplicate wallet
            if (await _walletRepository.WalletExistsAsync(wallet.Owner, wallet.AccountNumber))
                return ServiceResult<Wallet>.FailureResult("Duplicate wallet is not allowed.");

            // Check for wallet limit
            if (await _walletRepository.CountWalletsByOwnerAsync(wallet.Owner) >= 5)
                return ServiceResult<Wallet>.FailureResult("A user cannot have more than 5 wallets.");

            // Mask card numbers
            if (wallet.Type == "card" && wallet.AccountNumber.Length >= 6)
                wallet.AccountNumber = wallet.AccountNumber.Substring(0, 6);

            await _walletRepository.AddWalletAsync(wallet);
            return ServiceResult<Wallet>.SuccessResult(wallet, "Wallet created successfully.");
        }

        /// <summary>
        /// Deletes a wallet by its unique identifier.
        /// </summary>
        /// <param name="id"> The unique identifier of the wallet to be deleted.</param>
        /// <returns> <c>true</c> if the wallet was successfully deleted; otherwise, <c>false</c>. </returns>

        public async Task<bool> DeleteWalletAsync(Guid id)
        {
            var wallet = await _walletRepository.GetWalletByIdAsync(id);
            if (wallet == null) return false;

            await _walletRepository.DeleteWalletAsync(id);
            return true;
        }

        /// <summary>
        /// Retrieves a wallet by its unique identifier.
        /// </summary>
        /// <param name="id"> The unique identifier of the wallet to retrieve. </param>
        /// <returns> The wallet associated with the specified identifier, or <c>null</c> if not found. </returns>

        public async Task<Wallet> GetWalletByIdAsync(Guid id)
        {
            return await _walletRepository.GetWalletByIdAsync(id);
        }

                
        /// <summary>
        /// Retrieves all wallets belonging to a specific owner.
        /// </summary>
        /// <param name="owner"> The owner of the wallets to retrieve. </param>
        /// <returns> A collection of wallets owned by the specified owner. </returns>
        public async Task<IEnumerable<Wallet>> GetAllWalletsAsync(string owner)
        {
            return await _walletRepository.GetAllWalletsAsync(owner);
        }
    }
}

/// <summary>
/// Represents the result of a service operation.
/// </summary>
/// <typeparam name="T"> The type of data returned by the service operation. </typeparam>
public class ServiceResult<T>
{

    /// <summary>
    /// Gets or sets a value indicating whether the operation was successful.
    /// </summary>
    public bool Success { get; set; }


    /// <summary>
    /// Gets or sets the message providing additional information about the operation's result.
    /// </summary>
    public required string Message { get; set; }

    /// <summary>
    /// Gets or sets the data returned by the service operation.
    /// </summary>
    public T? Data { get; set; }

    
    /// <summary>
    /// Creates a successful service result.
    /// </summary>
    /// <param name="data"> The data associated with the successful operation. </param>
    /// <param name="message"> An optional message describing the success. </param>
    /// <returns></returns>
    public static ServiceResult<T> SuccessResult(T data, string message = "")
    {
        return new ServiceResult<T> { Success = true, Message = message, Data = data };
    }



    /// <summary>
    /// Creates a failed service result.
    /// </summary>
    /// <param name="message"> A message describing the reason for failure. </param>
    /// <returns> A <see cref="ServiceResult{T}"/> representing a failed operation. </returns>
    public static ServiceResult<T> FailureResult(string message)
    {
        return new ServiceResult<T> { Success = false, Message = message, Data = default };
    }
}
