﻿namespace Entities;

public class User
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public DateTime Joined { get; set; }
    public List<int>? Subscribes  { get; set; }
}