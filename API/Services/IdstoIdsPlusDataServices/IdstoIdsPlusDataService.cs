using API.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

public class IdstoIdsPlusData : IIdstoIdsPlusDataService
{
    public List<StockDTO> StocksToList(Employee currentEmployee, AccountingOfWorkingHoursContext context)
    {
        List<int> stocks = JsonConvert.DeserializeObject<List<int>>(currentEmployee.Stocks!)!;
        List<StockDTO> stocksResult = new();
        foreach (int stockId in stocks)
        {
            Stock currentStock = context.Stocks.Find(stockId)!;

            string stockTitle = currentStock.StockName;
            stocksResult.Add(new StockDTO 
            {
                StockId = stockId,
                StockName = stockTitle
            });
        }

        return stocksResult;
    }
}