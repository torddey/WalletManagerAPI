using System;

namespace WalletManagementApi.Models
{
    public class Wallet
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Unique identifier for each wallet
        public required string Name { get; set; } // Wallet name
        public required string Type { get; set; } // Wallet type: 'momo' or 'card'
        public required string AccountNumber { get; set; } // First 6 digits for cards or full number for momo
        public required string AccountScheme { get; set; } // Scheme: visa, mastercard, mtn, etc.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Wallet creation time
        public required string Owner { get; set; } // Phone number of wallet owner
    }
}
