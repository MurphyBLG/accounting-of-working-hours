using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[Route("api/[controller]")]
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

    // Нужно оптимизировать
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

        List<Stock> stocksToAdd = new();
        foreach (StockFromAPI stockFromAPI in stocks)
        {
            Stock? stock = await _context.Stocks.FindAsync(stockFromAPI.Id);

            if (stock is null)
            {
                await _context.Stocks.AddAsync(new Stock
                {
                    StockId = stockFromAPI.Id,
                    StockName = stockFromAPI.FullTitle,
                    Links = "[]"
                });

                continue;
            }

            stock.StockName = stockFromAPI.FullTitle;
        }


        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("{stockId}")]
    public async Task<IActionResult> AddLinkToStock(int stockId, [FromBody] LinkAddDTO linkAddDTO)
    {
        Stock? currentStock = await _context.Stocks.FindAsync(stockId);

        if (currentStock is null)
        {
            return BadRequest("Склад не найден!");
        }

        SortedSet<string>? links = JsonConvert.DeserializeObject<SortedSet<string>>(currentStock.Links);

        if (links is null)
        {
            return BadRequest("У данного склада нет списка звеньев! / Ошибка десериализации");
        }

        if (links.Contains(linkAddDTO.Name))
        {
            return BadRequest("Это звено уже существует!");
        }

        links.Add(linkAddDTO.Name);

        string json = JsonConvert.SerializeObject(links);

        currentStock.Links = json;

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("{stockId}")]
    public async Task<IActionResult> GetLinks(int stockId)
    {
        Stock? stock = await _context.Stocks.FindAsync(stockId);

        if (stock is null)
        {
            return NotFound($"Склад {stockId} не найден");
        }

        List<string> links = JsonConvert.DeserializeObject<List<string>>(stock.Links!)!; 

        return Ok(links);
    }
}