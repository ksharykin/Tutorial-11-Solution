using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnonPosters.API.DAL;
using AnonPosters.API.DTOs.Posts;
using AnonPosters.API.DTOs.Users;
using AnonPosters.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace AnonPosters.API.Controllers
{
    [Route("api/posts")]
    [Authorize]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly AnonPostersContext _context;

        public PostsController(AnonPostersContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPost()
        {
            var posts = await _context.Post.Include(p => p.User).ToListAsync();
            var mappedPosts = posts.Select(p => new PostDto {Id = p.Id, Username = p.User.Username});
            return Ok(mappedPosts);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PostInfoDto>> GetPost(int id)
        {
            var post = await _context.Post.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            var mappedPost = new PostInfoDto
            {
                User = new UserDto
                {
                    Id = post.User.Id,
                    Username = post.User.Username,
                },
                CreatedAt = post.CreatedAt,
                Content = post.Content
            };

            return Ok(mappedPost);
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, CreatePostDto updatePost)
        {
            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            
            post.UserId = updatePost.UserId;
            post.Content = updatePost.Content;

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostPost(CreatePostDto createPost)
        {
            var post = new Post
            {
                CreatedAt = DateTime.Now,
                Content = createPost.Content,
                UserId = createPost.UserId
            };
            _context.Post.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.Id });
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}
