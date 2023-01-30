using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[Authorize]
public class WorkPlanController : Controller
{
    private readonly AccountingOfWorkingHoursContext _context;

    public WorkPlanController(AccountingOfWorkingHoursContext context)
    {
        this._context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddWrokPlan([FromQuery] Guid employeeId, [FromQuery] int year, [FromQuery] int month, [FromBody] WorkPlanAddDTO workPlanAddDTO)
    {
        WorkPlan? workPlan = _context.WorkPlans.Where(workPlan => workPlan.EmployeeId == employeeId
            && workPlan.Year == year
            && workPlan.Month == month).FirstOrDefault();

        if (workPlan is not null)
        {
            return BadRequest("Рабочий план для данного сотрудника на данный промежуток времени уже есть!");
        }

        try
        {
            await _context.WorkPlans.AddAsync(new WorkPlan
            {
                Year = year,
                Month = month,
                WorkPlanId = Guid.NewGuid(),
                EmployeeId = employeeId,
                NumberOfDayShifts = workPlanAddDTO.NumberOfDayShifts,
                NumberOfHoursPerDayShift = workPlanAddDTO.NumberOfHoursPerDayShift,
                NumberOfNightShifts = workPlanAddDTO.NumberOfNightShifts,
                NumberOfHoursPerNightShift = workPlanAddDTO.NumberOfHoursPerNightShift
            });

            await _context.SaveChangesAsync();
            
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


}