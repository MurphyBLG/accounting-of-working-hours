using System.Text.Json;
using API.Models;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
public class PositionController : Controller
{
    private readonly AccountingOfWorkingHoursContext _context;

    public PositionController(AccountingOfWorkingHoursContext context)
    {
        this._context = context;
    }

    [HttpPost]
    public IActionResult CreatePosition([FromBody] PositionPostDTO positionPostDTO)
    {
        try
        {
            _context.Positions.Add(new Position
            {
                PositionId = Guid.NewGuid(),
                Name = positionPostDTO.Name,
                Salary = positionPostDTO.Salary,
                QuarterlyBonus = positionPostDTO.QuarterlyBonus,
                InterfaceAccesses = JsonSerializer.Serialize(positionPostDTO.InterfaceAccesses)
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        _context.SaveChanges();

        return Ok();
    }
}