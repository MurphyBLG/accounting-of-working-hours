using System.Text.Json;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[Authorize]
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

        try
        {
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }

    [HttpGet("{PositionId}")]
    public IActionResult GetPosition(string positionId)
    {
        Position? position = _context.Positions.Find(new Guid(positionId));

        if (position == null)
        {
            return BadRequest("Такая должность не существует");
        }

        return Ok(new PositionGetDTO
        {
            PositionId = position.PositionId,
            Name = position.Name,
            Salary = position.Salary,
            QuarterlyBonus = position.QuarterlyBonus,
            InterfaceAccesses = position.InterfaceAccesses
        });
    }

    [HttpGet]
    public IActionResult GetAllPositions()
    {
        IEnumerable<PositionGetAllDTO>? result = from p in _context.Positions
                                                 select new PositionGetAllDTO
                                                 {
                                                     PositionId = p.PositionId,
                                                     Name = p.Name
                                                 };

        return Ok(result);
    }
}