using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using API.DTOs.Stock;

namespace API.Interfaces
{
    public interface IStockRepo
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, CreateStockRequestDTO CsrDto);
        Task<Stock?> DeleteAsync(int id);
    }
}
