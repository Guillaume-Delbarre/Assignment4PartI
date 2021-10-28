using Assignment4PartI.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Assignment4PartI
{
    public interface IDataService
    {
        IList<Category> GetCategories();
        Category GetCategory(int id);
        Category CreateCategory(string name, string description);

        bool DeleteCategory(int id);
        bool UpdateCategory(int id, string name, string description);

        Product GetProduct(int id);
        IList<Product> GetProductByCategory(int categoryId);
        IList<Product> GetProductByName(string name);

        Order GetOrder(int id);
        IList<Order> GetOrders();
        IList<Order> GetOrderByShippingName(string name);

        IList<OrderDetails> GetOrderDetailsByOrderId(int orderId);
        IList<OrderDetails> GetOrderDetailsByProductId(int productId);
    }
    public class DataService : IDataService
    {
        /* Categories */
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

        /* Products */
        public Product GetProduct(int id)
        {
            var ctx = new NorthwindContext();
            return ctx.Products.Where(x => x.Id == id).Select(x => new Product
            {
                Name = x.Name,
                UnitPrice = x.UnitPrice,
                Category = ctx.Categories.Where(c => c.Id == x.CategoryId).Select(c => new Category
                {
                    Name = c.Name
                }).FirstOrDefault()
            }).FirstOrDefault();
        }
        public IList<Product> GetProductByName(string name)
        {
            var ctx = new NorthwindContext();
            return ctx.Products.Where(x => x.Name.Contains(name)).Select(x => new Product
            {
                ProductName = x.Name,
                Category = ctx.Categories.Where(c => c.Id == x.CategoryId).Select(c => new Category
                {
                    Name = c.Name
                }).FirstOrDefault()
            }).ToList();
        }

        public IList<Product> GetProductByCategory(int categoryId)
        {
            var ctx = new NorthwindContext();
            return ctx.Products.Where(x => x.CategoryId == categoryId).Select(x => new Product
            {
                Name = x.Name,
                UnitPrice = x.UnitPrice,
                CategoryName = ctx.Categories.Where(c => c.Id == x.CategoryId).FirstOrDefault().Name
            }).ToList();
        }

        /* Orders */
        public Order GetOrder(int id)
        {
            var ctx = new NorthwindContext();
            var order = ctx.Orders.Find(id);
            return new Order
            {
                Id = order.Id,
                Date = order.Date,
                Required = order.Required,
                Shipped = order.Shipped,
                Freight = order.Freight,
                ShipName = order.ShipName,
                ShipCity = order.ShipCity,
                OrderDetails = ctx.OrderDetails.Where(x => x.OrderId == id).Select(x => new OrderDetails
                {
                    Product = ctx.Products.Where(p => p.Id == x.ProductId).Select(p => new Product
                    {
                        Name = p.Name,
                        Category = ctx.Categories.Where(c => c.Id == p.CategoryId).FirstOrDefault()
                    }).FirstOrDefault()
                }).ToList()
            };
        }

        public IList<Order> GetOrderByShippingName(string name)
        {
            var ctx = new NorthwindContext();
            return ctx.Orders.Where(x => x.ShipName.Contains(name)).Select(x => new Order
            {
                Id = x.Id, 
                Date = x.Date, 
                ShipName = x.ShipName, 
                ShipCity = x.ShipCity
            }).ToList();
        }

        public IList<Order> GetOrders()
        {
            var ctx = new NorthwindContext();
            return ctx.Orders.Select(x => new Order
            {
                Id = x.Id,
                Date = x.Date,
                ShipName = x.ShipName,
                ShipCity = x.ShipCity
            }).ToList();
        }

        /* Order Details */
        public IList<OrderDetails> GetOrderDetailsByOrderId(int orderId)
        {
            var ctx = new NorthwindContext();
            return ctx.OrderDetails.Where(x => x.OrderId == orderId).Select(x => new OrderDetails
            {
                Product = ctx.Products.Where(p => p.Id == x.ProductId).Select(p => new Product
                {
                    Name = p.Name
                }).FirstOrDefault(),
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity
            }).ToList();
        }

        public IList<OrderDetails> GetOrderDetailsByProductId(int productId)
        {
            var ctx = new NorthwindContext();
            return ctx.OrderDetails.Where(x => x.ProductId == productId).Select(x => new OrderDetails
            {
                Order = ctx.Orders.Where(o => o.Id == x.OrderId).Select(o => new Order
                {
                    Date = o.Date
                }).FirstOrDefault(),
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity,
                OrderId = x.OrderId
            }).ToList();
        }
    }
}
