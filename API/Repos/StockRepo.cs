using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using API.DTOs.Stock;

namespace API.Repos
{
    public class StockRepo : IStockRepo
    {
        //dependancy injection
        private readonly AppDBContext _context;
        public StockRepo(AppDBContext context)
        {
            _context = context;
        }
        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if(stock == null)
            {
                return null;
            }
            return stock;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> UpdateAsync(int id, CreateStockRequestDTO CsrDto)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null) 
            {
                return null;
            }

            stockModel.Symbol = CsrDto.Symbol;
            stockModel.CompanyName = CsrDto.CompanyName;
            stockModel.Purchase = CsrDto.Purchase;
            stockModel.LastDiv = CsrDto.LastDiv;
            stockModel.Industry = CsrDto.Industry;
            stockModel.MarketCap = CsrDto.MarketCap;

            //Adds and saves to DB
            await _context.SaveChangesAsync();
            //Returns the success code with the id of the stock model as well as a new stockDTO from the new Model
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockDelete = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stockDelete == null) 
            {
                return null;
            }
            _context.Stocks.Remove(stockDelete);
            await _context.SaveChangesAsync();
            return stockDelete;
        }
    }
}
