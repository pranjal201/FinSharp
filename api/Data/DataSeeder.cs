using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading.Tasks;
using api.Migrations;
using api.Models;
using Bogus;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace api.Data
{
    public static class DataSeeder
    {
        public static string GenerateSymbol(int Index)
        {
            string symbol = String.Empty;
            while(Index > 0)
            {
                Index--;
                symbol = (char)('A'+(Index % 26)) + symbol;
                Index = Index/26;
            }
            return symbol;
        }
        
        public static void SeedStocks(ApplicationDBContext dBContext,int limit)
        {
            if (dBContext.Stock.Any())
            {
                return;
            }
            var fStock = new Faker<Stock>()
            .RuleFor(s => s.Company,f => f.Company.CompanyName())
            .RuleFor(s => s.Purchase, f => Math.Round(f.Random.Decimal(50, 500), 2))
            .RuleFor(s => s.LastDiv, f => Math.Round(f.Random.Decimal(0, 10), 2))
            .RuleFor(s => s.Industry, f => f.Commerce.Department())
            .RuleFor(s => s.MarketCap, f => f.Random.Long(1_000_000_000, 500_000_000_000));

            var stocks = Enumerable.Range(1,limit)
                            .Select(i =>
                            {
                                var stock = fStock.Generate();
                                stock.Symbol = DataSeeder.GenerateSymbol(i);
                                return stock;
                            }).ToList();


            dBContext.Stock.AddRange(stocks);
            dBContext.SaveChanges();
        }

        public static void SeedComments(ApplicationDBContext dBContext)
        {
            if (dBContext.Comment.Any())
            {
                return;
            }
            var fComments = new Faker<Comment>()
            .RuleFor(c => c.StockId , f => f.Random.Number(1,1_00_000))
            .RuleFor(c => c.Title,f => f.Company.CompanyName())
            .RuleFor(c => c.Content, f => f.Lorem.Sentence())
            .RuleFor(c => c.CreatedOn, f => f.Date.Past(1));

            var comments = fComments.Generate(2_00_000);
            dBContext.Comment.AddRange(comments);
            dBContext.SaveChanges();
        }
    }
}