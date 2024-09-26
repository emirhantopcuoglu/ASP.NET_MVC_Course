namespace FormsApp.Models
{
    public class Repository
    {
        private static readonly List<Product> _products = new();
        private static readonly List<Category> _categories = new();
        static Repository()
        {
            _categories.Add(new Category { CategoryId = 1, Name = "Telefon" });
            _categories.Add(new Category { CategoryId = 2, Name = "Bilgisayar" });

            _products.Add(new Product
            {
                ProductId = 1,
                Name = "IPhone 14",
                Price = 450000,
                IsActive = true,
                Image = "1.jpg",
                CategoryId = 1
            });
            _products.Add(new Product
            {
                ProductId = 2,
                Name = "IPhone 15",
                Price = 550000,
                IsActive = true,
                Image = "2.jpg",
                CategoryId = 1
            });
            _products.Add(new Product
            {
                ProductId = 3,
                Name = "Samsung S23",
                Price = 390000,
                IsActive = true,
                Image = "3.jpg",
                CategoryId = 1
            });
            _products.Add(new Product
            {
                ProductId = 4,
                Name = "Samsung S24",
                Price = 490000,
                IsActive = true,
                Image = "4.jpg",
                CategoryId = 1
            });

            _products.Add(new Product
            {
                ProductId = 2,
                Name = "Macbook Air",
                Price = 850000,
                IsActive = true,
                Image = "5.jpg",
                CategoryId = 2
            });
            _products.Add(new Product
            {
                ProductId = 2,
                Name = "Macbook Pro",
                Price = 950000,
                IsActive = true,
                Image = "6.jpg",
                CategoryId = 2
            });

        }
        public static List<Product> Products
        {
            get
            {
                return _products;
            }
        }

        public static void CreateProduct(Product entity)
        {
            _products.Add(entity);
        }

        public static void EditProduct(Product updatedProduct)
        {
            var entity = _products.FirstOrDefault(p => p.ProductId == updatedProduct.ProductId);
            if (entity != null)
            {
                entity.Name = updatedProduct.Name;
                entity.Price = updatedProduct.Price;
                entity.Image = updatedProduct.Image;
                entity.CategoryId = updatedProduct.CategoryId;
                entity.IsActive = updatedProduct.IsActive;
            }
        }

        public static void DeleteProduct(Product deletedProduct)
        {
            var entity = _products.FirstOrDefault(p => p.ProductId == deletedProduct.ProductId);
            if (entity != null)
            {
                _products.Remove(entity);
            }
        }
        public static List<Category> Categories
        {
            get
            {
                return _categories;
            }
        }
    }
}