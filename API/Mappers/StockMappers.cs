using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Stock;
using api.Models;
using API.DTOs.Stock;

namespace API.Mappers
{
    public static class StockMappers
    {
        public static StockDTO ToStockDTO(this Stock stockModel)
        {
            //creates a DTO from an inputed stock model
            return new StockDTO
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap
            };
        }

        public static Stock ToStockFromStockDTO(this StockDTO stockDto)
        {
            //creates a complete Stock  Model from a CSR DTO
            return new Stock
            {
                Id = stockDto.Id,
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }

        public static StockDTO ToStockDTOFromCreateDTO(this CreateStockRequestDTO CsrDto, int id)
        {
            //creates a DTO from an inputed stock model
            return new StockDTO
            {
                Id = id,
                Symbol = CsrDto.Symbol,
                CompanyName = CsrDto.CompanyName,
                Purchase = CsrDto.Purchase,
                LastDiv = CsrDto.LastDiv,
                Industry = CsrDto.Industry,
                MarketCap = CsrDto.MarketCap
            };
        }

        public static Stock ToStockFromCreateDTO(this CreateStockRequestDTO CsrDto)
        {
            //creates a complete Stock  Model from a CSR DTO
            return new Stock
            {
                Symbol = CsrDto.Symbol,
                CompanyName = CsrDto.CompanyName,
                Purchase = CsrDto.Purchase,
                LastDiv = CsrDto.LastDiv,
                Industry = CsrDto.Industry,
                MarketCap = CsrDto.MarketCap
            };
        }
    }
}
