using BlogApp.Entity;

namespace BlogApp.Data.Abstract
{
    public interface IPostRepository
    {
        // Bu interface'in amacı, alt sınıflarının ne tür işlevleri (methodlar) gerçekleştirebileceğini belirlemek.
        IQueryable<Post> Posts { get; }
        void CreatePost(Post post);
    }
}