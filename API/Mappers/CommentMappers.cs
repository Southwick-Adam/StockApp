using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Comment;
using api.Models;

namespace API.Mappers
{
    public static class CommentMappers
    {
        public static CommentDTO ToCommentDTO (this Comment comment)
        {
            return new CommentDTO
            {
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId,
            };
        }

        public static Comment ToComment (this CreateCommentDTO createdComment)
        {
            return new Comment
            {
                Title = createdComment.Title,
                Content = createdComment.Content,
                CreatedOn = createdComment.CreatedOn,
                StockId = createdComment.StockId,
            };
        }
    }
}
