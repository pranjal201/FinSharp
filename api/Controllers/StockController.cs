using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ILogger<StockController> _logger;
        private readonly ApplicationDBContext _context;

        public StockController(ILogger<StockController> logger,ApplicationDBContext dBContext)
        {
            _logger = logger;
            _context = dBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetStokes()
        {
            var stocks = await _context.Stock.ToListAsync();
            var stocksDto = stocks.Select(x => x.toStockDTO());
            return Ok(stocksDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
            var stock = await _context.Stock.FindAsync(id);
            if(stock == null)
            {
                return NotFound();
            }
            return Ok(stock.toStockDTO());
        }
        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreatStockDTO creatStockDTO)
        {
            var stockModel = creatStockDTO.toCreateStockDTO();
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStockById),new {id = stockModel.Id},stockModel.toStockDTO()); 

        }
        [HttpGet]
        [Route("total")]
        public async Task<IActionResult> GetTotalStocks()
        {
            var total = await _context.Stock.CountAsync(); 
            return Ok(total);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute]int id, [FromBody] UpdateStockDTO updateStockDto)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if(stock == null ) return NotFound();
            stock.Company = updateStockDto.Company;
            stock.Symbol = updateStockDto.Symbol;
            stock.LastDiv = updateStockDto.LastDiv;
            stock.Purchase = updateStockDto.Purchase;
            stock.MarketCap = updateStockDto.MarketCap;
            stock.Industry = updateStockDto.Industry;

            await _context.SaveChangesAsync();

            return Ok(stock.toStockDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var stock = await _context.Stock.FindAsync(id);
            if(stock == null) return NotFound();
            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}