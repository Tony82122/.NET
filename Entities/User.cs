using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string UserName { get; set; }

    [Required]
    [StringLength(100)]
    public string Password { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    public DateTime Joined { get; set; } = DateTime.UtcNow;

    // Changed to a separate table for many-to-many relationship
    public ICollection<UserSubscription>? Subscriptions { get; set; }

    // Navigation properties
    public ICollection<Post>? Posts { get; set; }
    public ICollection<Comment>? Comments { get; set; }
}

// New class for handling subscriptions
public class UserSubscription
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }

    public int SubscribedToUserId { get; set; }
    [ForeignKey("SubscribedToUserId")]
    public User SubscribedToUser { get; set; }
}