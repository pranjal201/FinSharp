using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDTO toStockDTO(this Stock stock)
        {
            return new StockDTO
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                Company = stock.Company,
                Purchase = stock.Purchase,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap
           };

        }
        public static Stock toCreateStockDTO(this CreatStockDTO stock)
        {
            return new Stock
            {
                Symbol = stock.Symbol,
                Company = stock.Company,
                Purchase = stock.Purchase,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap
           };

        }
        
    }
}