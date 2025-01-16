using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WalletManagementApi.Models;
using WalletManagementApi.Services;

[ApiController]
[Route("api/[controller]")]
public class WalletsController : ControllerBase
{
    private readonly WalletService _walletService;

    public WalletsController(WalletService walletService)
    {
        _walletService = walletService;
    }

    [HttpPost]
    public async Task<IActionResult> AddWallet([FromBody] Wallet wallet)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var createdWallet = await _walletService.AddWalletAsync(wallet);
            return CreatedAtAction(nameof(GetWalletById), new { id = createdWallet.Id }, createdWallet);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWallet(Guid id)
    {
        var success = await _walletService.DeleteWalletAsync(id);
        return success ? NoContent() : NotFound();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWalletById(Guid id)
    {
        var wallet = await _walletService.GetWalletByIdAsync(id);
        return wallet != null ? Ok(wallet) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWallets([FromQuery] string owner)
    {
        var wallets = await _walletService.GetAllWalletsAsync(owner);
        return Ok(wallets);
    }
}
