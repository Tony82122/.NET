using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Post
{ 
    private Post()
    {
        Title = string.Empty;
        UserId = string.Empty;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; }

    [StringLength(5000)]
    public string? Body { get; set; }

    [Required]
    public string UserId { get; set; }

    [Required]
    public int Upvotes { get; set; } = 0;

    [Required]
    public int Downvotes { get; set; } = 0;

    [StringLength(5000)]
    public string? Content { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("User")]
    public int AuthorId { get; set; }

    public User? User { get; set; }

    public ICollection<Comment>? Comments { get; set; }

    public static Post Create(string title, string? body, string userId, string? content)
    {
        return new Post
        {
            Title = title,
            Body = body,
            UserId = userId,
            Content = content,
            CreatedAt = DateTime.UtcNow
        };
    }
}