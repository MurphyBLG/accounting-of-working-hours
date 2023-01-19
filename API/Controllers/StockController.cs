using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[Route("[controller]")]
[Authorize]
public class StockController : Controller
{
    private readonly AccountingOfWorkingHoursContext _context;
    private readonly IHttpClientFactory _httpClientFactory;

    public StockController(IHttpClientFactory httpClientFactory, AccountingOfWorkingHoursContext context)
    {
        this._context = context;
        this._httpClientFactory = httpClientFactory;
    }

    [HttpPost]
    public async Task<IActionResult> UpdateStocks()
    {
        HttpClient httpClient = _httpClientFactory.CreateClient();

        var response = await httpClient.GetStringAsync("https://b2b.otr-it.ru/api/public/storages");

        List<StockFromAPI>? stocks = JsonConvert.DeserializeObject<List<StockFromAPI>>(response);

        if (stocks is null)
        {
            return BadRequest("Не возможно получить список складов!");
        }

        List<Task> tasks = new();
        foreach (StockFromAPI stockFromAPI in stocks)
        {
            Stock? stock = await _context.Stocks.FindAsync(stockFromAPI.Id);

            if (stock is null)
            {
                await _context.Stocks.AddAsync(new Stock
                {
                    StockId = stockFromAPI.Id,
                    StockName = stockFromAPI.FullTitle,
                    Links = "{\"links\": []}"
                });

                continue;
            }

            stock.StockName = stockFromAPI.FullTitle;
        }


        await _context.SaveChangesAsync();

        return Ok();
    }
}