using API.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

public class Dictionarizator : IDictionarizatorService
{
    public Dictionary<string, int> DictionarizeStocks(Employee currentEmployee, AccountingOfWorkingHoursContext context)
    {
        List<int> stocks = JsonConvert.DeserializeObject<List<int>>(currentEmployee.Stocks!)!;
        Dictionary<string, int> stocksWithNames = new();
        foreach (int stockId in stocks)
        {
            Stock currentStock = context.Stocks.Find(stockId)!;

            string stockTitle = currentStock.StockName;
            stocksWithNames[stockTitle] = stockId;
        }

        return stocksWithNames;
    }
}