using System;
using System.ComponentModel.DataAnnotations;

namespace WalletManagementApi.Models
{
    public class Wallet
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Type is required")]
        [RegularExpression("momo|card", ErrorMessage = "Type must be either 'momo' or 'card'")]
        public required string Type { get; set; }

        [Required(ErrorMessage = "Account Number is required")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Account Number must be between 6 and 16 characters")]
        public required string AccountNumber { get; set; }

        [Required(ErrorMessage = "Account Scheme is required")]
        [RegularExpression("visa|mastercard|mtn|vodafone|airteltigo", ErrorMessage = "Invalid Account Scheme")]
        public required string AccountScheme { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Owner is required")]
        [Phone(ErrorMessage = "Owner must be a valid phone number")]
        public required string Owner { get; set; }
    }
}
