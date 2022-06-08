using Lab4.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using Lab4.Data.Models;
using Lab4.Data.Services;

namespace Lab4.Controllers;

[Route("api/[controller]")]
[ApiController]

public class SellerController : ControllerBase
{
    private readonly SellerService _context;

    public SellerController(SellerService context)
    {
        _context = context;
    }
    
    //Get sellers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Seller>>> GetSeller()
    {
        return await _context.GetSellers();
    }
    
    //Gets sellers incomlete
    [HttpGet("/incomplete")]
    public async Task<ActionResult<IEnumerable<IncompleteSellerDTO>>> GetSellerIncomplete()
    {
        return await _context.GetSellersIncomplete();
    }
    
    //Get seller by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Seller>> GetSeller(int id)
    {
        var seller = await _context.GetSeller(id);

        if (seller == null)
        {
            return NotFound();
        }

        return seller;
    }
    
    //Put change seller
    [HttpPut("{id}")]
    public async Task<ActionResult<Seller>> PutSeller(int id, [FromBody] Seller seller)
    {
        var result = await _context.UpdateSeller(id, seller);
        if (result==null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    //Post add seller
    [HttpPost]
    public async Task<ActionResult<Seller>> PostSeller([FromBody] SellerDTO seller)
    {
        var result = await _context.AddSeller(seller);
        if (result==null)
        {
            BadRequest();
        }

        return Ok(result);
    }

    //Delete seller
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSeller(int id)
    {
        var seller = await _context.DeleteSeller(id);
        if (seller)
        {
            return Ok();
        }

        return BadRequest();
    }
}