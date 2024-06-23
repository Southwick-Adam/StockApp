using System;

namespace API.DTOs.Comment
{
    public class UpdateCommentDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
