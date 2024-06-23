using System;
using api.Models;
using API.DTOs.Comment;


namespace API.Interfaces
{
    public interface ICommentRepo
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<List<Comment>?> GetAllByStockAsync(int id);
        Task<Comment?> CreateAsync(CreateCommentDTO createCommentDTO);
        Task<Comment?> UpdateAsync(int id, UpdateCommentDTO createCommentDTO);
        Task<Comment?> DeleteAsync(int id);
    }
}
