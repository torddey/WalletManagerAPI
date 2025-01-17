using System;
using System.ComponentModel.DataAnnotations;

namespace WalletManagementApi.Models
{

    /// <summary>
    /// Represents a wallet entity containing details such as owner, type, account information, and more.
    /// </summary>
    public class Wallet
    {
        /// <summary>
        /// Gets or sets the unique identifier for the wallet.
        /// Defaults to a new GUID value.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();


        /// <summary>
        /// Gets or sets the name of the wallet.
        /// This field is required and must not exceed 100 characters.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public required string Name { get; set; }


        /// <summary>
        /// Gets or sets the type of the wallet.
        /// This field is required and must be either 'momo' or 'card'.
        /// </summary>
        [Required(ErrorMessage = "Type is required")]
        [RegularExpression("momo|card", ErrorMessage = "Type must be either 'momo' or 'card'")]
        public required string Type { get; set; }


        /// <summary>
        /// Gets or sets the account number associated with the wallet.
        /// This field is required and must be between 6 and 16 characters in length.
        /// </summary>
        [Required(ErrorMessage = "Account Number is required")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Account Number must be between 6 and 16 characters")]
        public required string AccountNumber { get; set; }


        /// <summary>
        /// Gets or sets the account scheme associated with the wallet.
        /// This field is required and must be one of the following: 'visa', 'mastercard', 'mtn', 'vodafone', or 'airteltigo'.
        /// </summary>
        [Required(ErrorMessage = "Account Scheme is required")]
        [RegularExpression("visa|mastercard|mtn|vodafone|airteltigo", ErrorMessage = "Invalid Account Scheme")]
        public required string AccountScheme { get; set; }



        /// <summary>
        /// Gets or sets the date and time when the wallet was created.
        /// Defaults to the current UTC date and time.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



        /// <summary>
        ///  Gets or sets the owner's contact information, which must be a valid phone number.
        /// This field is required.
        /// </summary>
        [Required(ErrorMessage = "Owner is required")]
        [Phone(ErrorMessage = "Owner must be a valid phone number")]
        public required string Owner { get; set; }
    }
}
