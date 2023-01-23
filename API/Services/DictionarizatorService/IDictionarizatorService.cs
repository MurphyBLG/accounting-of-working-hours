using API.Models;
using Microsoft.EntityFrameworkCore;

public interface IDictionarizatorService
{
    public Dictionary<string, int> DictionarizeStocks(Employee currentEmployee, AccountingOfWorkingHoursContext context);
}