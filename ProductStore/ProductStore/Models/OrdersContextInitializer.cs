using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProductStore.Models
{
    public class OrdersContextInitializer : DropCreateDatabaseIfModelChanges<OrdersContext>
    {
        protected override void Seed(OrdersContext context)
        {
            var products = new List<Product>()            
            {
                new Product() { Name = "ShuXue", PageCount = 100, ActualCost = .99M },
                new Product() { Name = "YuWen", PageCount = 150, ActualCost = 10 },
                new Product() { Name = "ZhengZhi", PageCount = 200, ActualCost = 2.05M }
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();

            var order = new Order() { Customer = "Bob" };
            var od = new List<OrderDetail>()
            {
                new OrderDetail() { Product = products[0], Quantity = 2, Order = order},
                new OrderDetail() { Product = products[1], Quantity = 4, Order = order }
            };
            context.Orders.Add(order);
            od.ForEach(o => context.OrderDetails.Add(o));

            context.SaveChanges();
        }
    }
}