using System;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.Mappers;
using API.DTOs.Comment;

namespace API.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepo _commentRepo;
        public CommentController(ICommentRepo commentRepo)
        {
            _commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            var commentDtoList = comments.Select(c => c.ToCommentDTO());
            return Ok(commentDtoList);
        }

        [HttpGet]
        [Route ("{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpGet]
        [Route ("{stockId}/stockId")]
        public async Task<IActionResult> GetCommentByStock(int stockId)
        {
            var list = await _commentRepo.GetAllByStockAsync(stockId);
            if (list == null)
            {
                return BadRequest("Stock ID invalid");
            }
            if (list.Count == 0)
            {
                return Ok("No comments for this stock were found. =)");
            }
            var DTOlist = list.Select(c => c.ToCommentDTO());
            return Ok(DTOlist);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDTO createCommentDTO)
        {
            var newComment = await _commentRepo.CreateAsync(createCommentDTO);
            if (newComment == null)
            {
                return BadRequest("Stock ID invalid");
            }
            return CreatedAtAction(nameof(GetComment), new { id = newComment.Id }, newComment.ToCommentDTO());
        }

        [HttpPut]
        [Route ("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentDTO createCommentDTO)
        {
            var update = await _commentRepo.UpdateAsync(id, createCommentDTO);
            if (update == null)
            {
                return NotFound();
            }
            return Ok(update.ToCommentDTO());
        }

        [HttpDelete]
        [Route ("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _commentRepo.DeleteAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
