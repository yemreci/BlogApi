using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogApi.Models;
using BlogApi.Data;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly BlogContext _context;

        public BlogPostsController(BlogContext context)
        {
            _context = context;
        }

        // GET: api/BlogPosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetBlogPosts()
        {
            return await _context.BlogPosts.ToListAsync();
        }

        // GET: api/BlogPosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> GetBlogPost(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            return blogPost;
        }

        // PUT: api/BlogPosts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogPost(int id, BlogPostPutDTO blogPostPutDTO)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            blogPost.UserId = blogPostPutDTO.UserId;
            blogPost.Content = blogPostPutDTO.Content;
            blogPost.DateTime = DateTime.Now;
            _context.BlogPosts.Update(blogPost);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/BlogPosts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BlogPost>> PostBlogPost(BlogPostPostDTO blogPostDTO)
        {
            var post = new BlogPost
            {
                UserId = blogPostDTO.UserId,
                Id = blogPostDTO.Id,
                Content = blogPostDTO.Content,
                DateTime = DateTime.Now
            };
            _context.BlogPosts.Add(post);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBlogPost", new { id = post.Id }, post);
        }

        // DELETE: api/BlogPosts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            if (_context.BlogPosts == null)
            {
                return NotFound();
            }
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            _context.BlogPosts.Remove(blogPost);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogPostExists(int id)
        {
            return (_context.BlogPosts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
