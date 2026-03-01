using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO
{
    public record StockDTO
    {
        
        public int Id { get; set; }
        public string Symbol { get; set; } = String.Empty;
        public string Company { get; set; } = String.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = String.Empty;

        public long MarketCap {get;set;}

    }

    public record CreatStockDTO
    {
        public string Symbol { get; set; } = String.Empty;
        public string Company { get; set; } = String.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = String.Empty;

        public long MarketCap {get;set;}

    }
    public record UpdateStockDTO
    {
        public string Symbol { get; set; } = String.Empty;
        public string Company { get; set; } = String.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = String.Empty;

        public long MarketCap {get;set;}

    }
}