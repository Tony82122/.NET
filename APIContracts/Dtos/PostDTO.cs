namespace ApiContracts.DTOs;

public class postDTO
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; } = string.Empty; 
    public string UserId { get; set; }
    public int? UpVotes { get; set; }
    public int? DownVotes { get; set; }
    public string? Body { get; set; }
    public List<CommentDTO> Comments { get; set; } = new List<CommentDTO>(); 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}