using API.Models;
using Microsoft.EntityFrameworkCore;

public interface IIdstoIdsPlusDataService
{
    public List<StockDTO> StocksToList(Employee currentEmployee, AccountingOfWorkingHoursContext context);
}