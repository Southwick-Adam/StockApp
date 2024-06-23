using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;
using API.Mappers;
using API.DTOs.Stock;
using API.Interfaces;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepo _stockRepo;
        public StockController(AppDBContext context, IStockRepo stockRepo)
        {
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetAllAsync();
            var stockDtoList = stocks.Select(s => s.ToStockDTO());
            return Ok(stockDtoList);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            if(stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDTO CsrDto)
        {
            //creates a new stock model from the input CreateStockRequestDTO
            var stockModel = CsrDto.ToStockFromCreateDTO();
            //Adds and saves to DB
            await _stockRepo.CreateAsync(stockModel);
            //Returns the success code with the id of the stock model as well as a new stockDTO from the new Model
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDTO());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody] CreateStockRequestDTO CsrDto, [FromRoute] int id)
        {
            var stockModel = await _stockRepo.UpdateAsync(id, CsrDto);
            if (stockModel == null) 
            {
                return NotFound();
            }
            //Returns the success code with the id of the stock model as well as a new stockDTO from the new Model
            return Ok(stockModel.ToStockDTO());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockDelete = await _stockRepo.DeleteAsync(id);
            if (stockDelete == null) 
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}