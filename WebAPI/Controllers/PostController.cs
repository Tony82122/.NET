using ApiContracts.DTOs;
using Entities;
using EntityRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : Controller
{
    private readonly IPostRepo _postRepository;

    public PostController(IPostRepo postRepository)
    {
        _postRepository = postRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Post>> GetAllPosts()
    {
        var posts = _postRepository.GetAll();
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById(int id)
    {
        var post = await _postRepository.GetSingleAsync(id);
        if (post == null)
            return NotFound(); // 404 Not Found if the post doesn't exist
        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] postDTO postDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
    
        // Assuming there's a Create method in the Post class
        var newPost = Post.Create(
            title: postDto.Title ?? string.Empty,
            body: postDto.Body,
            userId: postDto.UserId,
            content: postDto.Content
        );
    
        // If AuthorId needs to be set separately and it's of type User
        // newPost.AuthorId = await _userRepository.GetUserById(postDto.UserId);
    
        await _postRepository.AddAsync(newPost);
        return CreatedAtAction(nameof(GetPostById), new { id = newPost.Id }, newPost);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] postDTO postDto)
    {
        var postToUpdate = await _postRepository.GetSingleAsync(id);
        if (postToUpdate == null)
            return NotFound();

        postToUpdate.Title = postDto.Title ?? string.Empty;
        postToUpdate.Body = postDto.Body;
        postToUpdate.Content = postDto.Content;

        await _postRepository.UpdateAsync(postToUpdate);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var post = await _postRepository.GetSingleAsync(id);
        if (post == null) return NotFound();

        await _postRepository.DeleteAsync(id);
        return NoContent();
    }
}