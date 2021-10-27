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
            ctx.Categories.Add(category);
            ctx.SaveChanges();
            return category;
        }

        public bool DeleteCategory(int id)
        {
            var ctx = new NorthwindContext();
            Category category = ctx.Categories.Find(id);
            if (category == null)
                return false;
            else
            {
                ctx.Categories.Remove(category);
                return ctx.SaveChanges() > 0;
            }
        }

        public bool UpdateCategory(int id, string name, string description)
        {
            var ctx = new NorthwindContext();
            Category category = ctx.Categories.Find(id);
            if (category == null)
                return false;
            else
            {
                category.Name = name;
                category.Description = description;
                return ctx.SaveChanges() > 0;
            }
        }

        public Category GetCategory(int id)
        {
            var ctx = new NorthwindContext();
            return ctx.Categories.Find(id);
        }

        public IList<Category> GetCategories()
        {
            var ctx = new NorthwindContext();
            return ctx.Categories.ToList();
        }

        public Product GetProduct(int id)
        {
            var ctx = new NorthwindContext();
            var product = ctx.Products.Find(id);
            return product;
        }

        public IList<Product> GetProductByCategory(int categoryId)
        {
            var ctx = new NorthwindContext();
            return ctx.Products.Where<Product>(x => x.CategoryId == categoryId).ToList();
        }

        public IList<Product> GetProductByName(string name)
        {
            var ctx = new NorthwindContext();
            return ctx.Products.Where<Product>(x => x.Name.Contains(name)).ToList();
        }

        public IList<Product> GetProducts()
        {
            var ctx = new NorthwindContext();
            return ctx.Products.ToList();
        }

        public Order GetOrder(int id)
        {
            var ctx = new NorthwindContext();
            return ctx.Orders.Find(id);
        }

        public IList<Order> GetOrders()
        {
            var ctx = new NorthwindContext();
            return ctx.Orders.ToList();
        }

        public IList<OrderDetails> GetOrderDetailsByOrderId(int orderId)
        {
            var ctx = new NorthwindContext();
            return ctx.OrderDetails.Where<OrderDetails>(x => x.OrderId == orderId).ToList();
        }

        public IList<OrderDetails> GetOrderDetailsByProductId(int productId)
        {
            var ctx = new NorthwindContext();
            return ctx.OrderDetails.Where<OrderDetails>(x => x.ProductId == productId).ToList();
        }
    }
}
