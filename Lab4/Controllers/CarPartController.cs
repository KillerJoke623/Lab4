using Lab4.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using Lab4.Data.Models;
using Lab4.Data.Services;

namespace Lab4.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CarPartController : ControllerBase
{
    private readonly CarPartService _contextC;

    public CarPartController(CarPartService context)
    {
        _contextC = context;
    }
    
    //Get CarParts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CarPart>>> GetCarParts()
    {
        return await _contextC.GetCarParts();
    }
    
    //Get CarPart by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<CarPart>> GetCarPart(int id)
    {
        var seller = await _contextC.GetCarPart(id);

        if (seller == null)
        {
            return NotFound();
        }

        return seller;
    }
    
    //Put change carp
    [HttpPut("{id}")]
    public async Task<ActionResult<CarPart>> PutCarPart(int id, [FromBody] CarPart seller)
    {
        var result = await _contextC.UpdateCarPart(id, seller);
        if (result==null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    //Post add carp
    [HttpPost]
    public async Task<ActionResult<CarPart>> PostCarPart([FromBody] CarPartDTO carPart)
    {
        var result = await _contextC.AddCarPart(carPart);
        if (result==null)
        {
            BadRequest();
        }

        return Ok(result);
    }
    
    //Delete carp
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCarPart(int id)
    {
        var seller = await _contextC.DeleteCarPart(id);
        if (seller)
        {
            return Ok();
        }

        return BadRequest();
    }
}