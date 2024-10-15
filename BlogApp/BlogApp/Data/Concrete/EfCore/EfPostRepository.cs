using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete
{
    public class EfPostRepository : IPostRepository
    {
        //  Bu sınıf, veritabanıyla iletişim kurarak Post nesneleriyle ilgili işlemler yapıyor. Sınıf, IPostRepository arayüzünü (interface) uyguluyor, yani IPostRepository'de tanımlanmış olan işlevleri kullanıcılara sunuyor.
        private BlogContext _context; //  BlogContext türünde bir özel alan (private field). Veritabanı işlemleri yapabilmek için kullanılıyor.
        public EfPostRepository(BlogContext context)
        {
            // Bu sınıfın her örneği, bir veritabanı bağlamı (DbContext) kullanarak çalışır. Böylece repository sınıfı, veritabanıyla iletişim kurabilir.
            _context = context;
        }
        public IQueryable<Post> Posts => _context.Posts; // IQueryable, bu tablonun sorgulanabilir bir versiyonunu temsil eder. Yani, filtreleme, sıralama gibi işlemleri veritabanına gitmeden önce yapabilmenizi sağlar.

        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }
    }
}