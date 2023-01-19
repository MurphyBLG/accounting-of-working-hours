using System.Text.Json;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            _context.Positions.Add(new Position(positionPostDTO));
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
    public async Task<IActionResult> GetPosition(string positionId)
    {
        Position? position = await _context.Positions.FindAsync(new Guid(positionId));

        if (position == null)
        {
            return BadRequest("Такая должность не существует");
        }

        return Ok(new PositionGetDTO(position)); // need to be tested
    }

    [HttpGet]
    public IActionResult GetAllPositions()
    {
        IEnumerable<PositionGetAllDTO>? result = from p in _context.Positions
                                                 select new PositionGetAllDTO
                                                 {
                                                     PositionId = p.PositionId,
                                                     Name = p.Name,
                                                    //  Salary = p.Salary,
                                                    //  QuarterlyBonus = p.QuarterlyBonus,
                                                    //  InterfaceAccesses = JsonConvert.DeserializeObject<InterfaceAccesses>(p.InterfaceAccesses)!
                                                 };

        return Ok(result);
    }

    [HttpPut] 
    public IActionResult UpdatePosition([FromBody] PositionUpdateDTO positionUpdateDTO)
    {
        Position? positionToUpdate = _context.Positions.Find(positionUpdateDTO.PositionId);

        if (positionToUpdate == null)
        {
            return BadRequest("Такая должность не существует");
        }
        
        positionToUpdate.PositionId = positionUpdateDTO.PositionId;
        positionToUpdate.Name = positionUpdateDTO.Name;
        positionToUpdate.Salary = positionUpdateDTO.Salary;
        positionToUpdate.QuarterlyBonus = positionUpdateDTO.QuarterlyBonus;
        positionToUpdate.InterfaceAccesses = positionUpdateDTO.InterfaceAccesses;

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
}