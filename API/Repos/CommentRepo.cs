using System;
using api.Data;
using API.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using API.DTOs.Comment;
using API.Mappers;

namespace API.Repos
{
    public class CommentRepo : ICommentRepo
    {
        private readonly AppDBContext _context;
        public CommentRepo(AppDBContext context) 
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null)
            {
                return null;
            }
            return comment;
        }

        public async Task<List<Comment>?> GetAllByStockAsync(int id)
        {
            //make sure stock ID is valid
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stock == null)
            {
                return null;
            }
            var list = await _context.Comments
                .Where(c => c.StockId == id)
                .ToListAsync();
            return list;
        }

        public async Task<Comment?> CreateAsync(CreateCommentDTO createCommentDTO)
        {
            //make sure stock ID is valid
            var stockId = createCommentDTO.StockId;
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == stockId);
            if (stock == null)
            {
                return null;
            }
            //create and add comment
            var newComment = createCommentDTO.ToComment();
            await _context.Comments.AddAsync(newComment);
            await _context.SaveChangesAsync();
            return newComment;
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentDTO update)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null)
            {
                return null;
            }

            comment.Title = update.Title;
            comment.Content = update.Content;

            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null)
            {
                return null;
            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}
