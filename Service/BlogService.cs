using Repository.Entity;
using Repository.Repository;
using Service.DTOs;
using Service.Interface;

namespace Service
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repo;
        public BlogService(IBlogRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<BlogReadDTO>> GetAllAsync()
        {
            var blogs = await _repo.GetAllAsync();
            return blogs.Select(b => new BlogReadDTO
            {
                BlogId = b.BlogId,
                Title = b.Title,
                Content = b.Content,
                Url = b.Url, // Assuming Url is a property of Blog
                UserId = b.UserId,
                PublishedDate = b.PublishedDate
            });
        }

        public async Task<BlogReadDTO> GetByIdAsync(int id)
        {
            var blog = await _repo.GetByIdAsync(id);
            if (blog == null) return null;

            return new BlogReadDTO
            {
                BlogId = blog.BlogId,
                Title = blog.Title,
                Content = blog.Content,
                UserId = blog.UserId,
                PublishedDate = blog.PublishedDate
            };
        }

        public async Task<BlogReadDTO> AddAsync(BlogCreateUpdateDTO dto)
        {
            var blog = new Blog
            {
                Title = dto.Title,
                Content = dto.Content,
                UserId = dto.UserId,
                Url = dto.Url,
                PublishedDate = DateTime.UtcNow
            };

            await _repo.AddAsync(blog);
            await _repo.SaveAsync();

            return new BlogReadDTO
            {
                BlogId = blog.BlogId,
                Title = blog.Title,
                Url = blog.Url, // Assuming Url is generated in the Blog entity
                Content = blog.Content,
                UserId = blog.UserId,
                PublishedDate = blog.PublishedDate
            };
        }

        public async Task<bool> UpdateAsync(int id, BlogCreateUpdateDTO dto)
        {
            var blog = await _repo.GetByIdAsync(id);
            if (blog == null) return false;

            blog.Title = dto.Title;
            blog.Content = dto.Content;
            blog.Url = dto.Url;
            // UserId maybe shouldn't be updated, but for now we will allow it.
            blog.UserId = dto.UserId;

            _repo.Update(blog);
            await _repo.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;
            _repo.Delete(entity);
            await _repo.SaveAsync();
            return true;
        }
    }
} 