using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WalletManagementApi.Models;
using WalletManagementApi.Services;

/// <summary>
/// Handles API requests for wallet management operations, including adding, retrieving, and deleting wallets.
/// </summary>
[ApiController]
[Route("api/[controller]")]



public class WalletsController : ControllerBase
{
    private readonly WalletService _walletService;


    /// <summary>
    /// Initializes a new instance of the <see cref="WalletsController"/> class with the specified wallet service.
    /// </summary>
    /// <param name="walletService"> The service used to manage wallet operations. </param>
    public WalletsController(WalletService walletService)
    {
        _walletService = walletService;
    }


    /// <summary>
    /// Adds a new wallet to the system
    /// </summary>
    /// <param name="wallet"> The wallet to be added </param>
    /// <returns>
    /// A <see cref="CreatedAtActionResult"/> containing the newly created wallet if successful.
    /// Returns <see cref="BadRequestObjectResult"/> if the data is invalid or a limit is reached.
    /// Returns <see cref="ConflictObjectResult"/> if a duplicate wallet exists.
    /// Returns <see cref="StatusCodeResult"/> for server errors.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> AddWallet([FromBody] Wallet wallet)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid wallet data provided.");

        try
        {
            var result = await _walletService.AddWalletAsync(wallet);

            if (!result.Success)
            {
                if (result.Message.Contains("Duplicate"))
                    return Conflict(result.Message);

                if (result.Message.Contains("5 wallets"))
                    return BadRequest(result.Message);

                return BadRequest(result.Message);
            }

        #pragma warning disable CS8602 // Dereference of a possibly null reference.
                    return CreatedAtAction(nameof(GetWalletById), new { id = result.Data.Id }, result.Data);
        #pragma warning restore CS8602 // Dereference of a possibly null reference.
                }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


    /// <summary>
    /// Deletes a wallet by its unique identifier
    /// </summary>
    /// <param name="id"> The unique identifier of the wallet to delete.</param>
    /// <returns>
    /// Returns <see cref="NoContentResult"/> if the wallet is deleted successfully.
    /// Returns <see cref="NotFoundObjectResult"/> if the wallet is not found.
    /// Returns <see cref="StatusCodeResult"/> for server errors.
    /// </returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWallet(Guid id)
    {
        try
        {
            var success = await _walletService.DeleteWalletAsync(id);

            if (!success)
            {
                return NotFound("Wallet not found.");
            }

            return NoContent(); // Successfully deleted
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a wallet by its unique identifier.
    /// </summary>
    /// <param name="id"> The unique identifier of the wallet to retrieve. </param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> containing the wallet if found.
    /// Returns <see cref="NotFoundObjectResult"/> if the wallet does not exist.
    /// Returns <see cref="StatusCodeResult"/> for server errors.
    /// </returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetWalletById(Guid id)
    {
        try
        {
            var wallet = await _walletService.GetWalletByIdAsync(id);

            if (wallet == null)
            {
                return NotFound("Wallet not found.");
            }

            return Ok(wallet);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves all wallets for a specific owner.
    /// </summary>
    /// <param name="owner">The owner of the wallets to retrieve.</param>
    /// <returns>
    /// Returns <see cref="OkObjectResult"/> containing a list of wallets if found.
    /// Returns <see cref="NoContentResult"/> if no wallets are found for the specified owner.
    /// Returns <see cref="StatusCodeResult"/> for server errors.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> GetAllWallets([FromQuery] string owner)
    {
        try
{
            var wallets = await _walletService.GetAllWalletsAsync(owner);

           
            if (wallets == null || !wallets.Any())
            {
                return NoContent(); // No wallets found
            }

            return Ok(wallets); // Return wallets if found
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
