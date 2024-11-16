using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Comment
{
    private Comment()
    {
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get;  set; }

    [StringLength(1000)]
    public string? Body { get;  set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int PostId { get; set; }

    [Required]
    public DateTime CreatedAt { get;  set; }

    [Required]
    public int Upvotes { get;  set; }

    [Required]
    public int Downvotes { get;  set; }

    [StringLength(500)]
    public string? TextContent { get;  set; }

    // Navigation properties
    [ForeignKey("UserId")]
    public User? User { get; private set; }

    [ForeignKey("PostId")]
    public Post? Post { get; private set; }

    public static Comment Create(int userId, int postId, string? body, string? textContent)
    {
        return new Comment
        {
            UserId = userId,
            PostId = postId,
            Body = body,
            TextContent = textContent,
            CreatedAt = DateTime.UtcNow,
            Upvotes = 0,
            Downvotes = 0
        };
    }

    public void UpdateContent(string? body, string? textContent)
    {
        Body = body;
        TextContent = textContent;
    }

    public void IncrementUpvotes()
    {
        Upvotes++;
    }

    public void IncrementDownvotes()
    {
        Downvotes++;
    }
}