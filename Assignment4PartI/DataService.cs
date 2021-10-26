using Assignment4PartI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment4PartI
{
    public interface IDataService
    {
        IList<Category> GetCategories();
        Category GetCategory(int id);
        bool CreateCategory(Category category);
        Category CreateCategory(string name, string description);

        bool DeleteCategory(int id);
        bool UpdateCategory(int id, string name, string description);

        IList<Product> GetProducts();
        Product GetProduct(int id);
        IList<Product> GetProductByCategory(int categoryId);
        IList<Product> GetProductByName(string name);

        Order GetOrder(int id);
        IList<Order> GetOrders();

        IList<OrderDetails> GetOrderDetailsByOrderId(int orderId);
        IList<OrderDetails> GetOrderDetailsByProductId(int productId);
    }
    public class DataService : IDataService
    {
        public bool CreateCategory(Category category)
        {
            var ctx = new NorthwindContext();
            category.Id = ctx.Categories.Max(x => x.Id) + 1;
            ctx.Add(category);
            return ctx.SaveChanges() > 0;
        }
        public Category CreateCategory(string name, string description)
        {
            var ctx = new NorthwindContext();
            var newId = ctx.Categories.Max(x => x.Id) + 1;
            Category category = new Category
            {
                Id = newId,
                Name = name,
                Description = description
            };
            return category;
        }

        public bool DeleteCategory(int id)
        {
            return true;
        }

        public bool UpdateCategory(int id, string name, string description)
        {
            return true;
        }

        public Category GetCategory(int id)
        {
            return new Category();
        }

        public IList<Category> GetCategories()
        {
            var ctx = new NorthwindContext();
            return ctx.Categories.ToList();
        }

        public Product GetProduct(int id)
        {
            return new Product();
        }

        public IList<Product> GetProductByCategory(int categoryId)
        {
            var ctx = new NorthwindContext();
            return ctx.Products.ToList();
        }

        public IList<Product> GetProductByName(string name)
        {
            var ctx = new NorthwindContext();
            return ctx.Products.ToList();
        }

        public IList<Product> GetProducts()
        {
            var ctx = new NorthwindContext();
            return ctx.Products.ToList();
        }

        public Order GetOrder(int id)
        {
            return new Order();
        }

        public IList<Order> GetOrders()
        {
            return new List<Order>();
        }

        public IList<OrderDetails> GetOrderDetailsByOrderId(int orderId)
        {
            return new List<OrderDetails>();
        }

        public IList<OrderDetails> GetOrderDetailsByProductId(int productId)
        {
            return new List<OrderDetails>();
        }
    }
}
