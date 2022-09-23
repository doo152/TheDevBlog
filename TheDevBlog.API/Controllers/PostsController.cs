using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheDevBlog.API.Data;
using TheDevBlog.API.Models.DTO;
using TheDevBlog.API.Models.Entities;

namespace TheDevBlog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly TheDevBlogDbContext dbContext;

        public PostsController(TheDevBlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            return Ok(await dbContext.Posts.ToListAsync());
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetPost")]
        public async Task<IActionResult> GetPost(Guid id)
        {
            var post = await dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null) return BadRequest("Not found this ID");

            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostRequest addPostRequest)
        {
            //Convert DTO to Entity
            var post = new Post() {
                Title = addPostRequest.Title,
                Content = addPostRequest.Content,
                PublishDate = addPostRequest.PublishDate,
                UpdateDate = addPostRequest.UpdateDate,
                Summary = addPostRequest.Summary,
                Visible = addPostRequest.Visible,
                UrlHandle=addPostRequest.UrlHandle
            };
            post.Id = Guid.NewGuid();
            await dbContext.Posts.AddAsync(post);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction("GetPost", new { id = post.Id }, post );
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdatePost([FromRoute] Guid id, UpdatePostRequest updatePostRequest)
        {
            //Check if post to be updated is exist:
            var existingPost = await dbContext.Posts.FindAsync(id);

            if( existingPost !=null )
            {
                existingPost.Title = updatePostRequest.Title;
                existingPost.Content = updatePostRequest.Content;
                existingPost.PublishDate = updatePostRequest.PublishDate;
                existingPost.UpdateDate = updatePostRequest.UpdateDate;
                existingPost.Summary = updatePostRequest.Summary;
                existingPost.Visible = updatePostRequest.Visible;
                existingPost.UrlHandle = updatePostRequest.UrlHandle;

                await dbContext.SaveChangesAsync();
                return Ok(existingPost);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var existingPost = await dbContext.Posts.FindAsync(id);
            if (existingPost != null)
            {
                dbContext.Remove(existingPost);
                await dbContext.SaveChangesAsync();
                return Ok(existingPost);
            }
            return NotFound();
        }
    }
}
