using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            var line = lineCollection.FirstOrDefault(i => i.Product.ProductID == product.ProductID);

            if (line == null)
                lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            else
                line.Quantity++;
        }

        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        public decimal TotalValue()
        {
            return lineCollection.Sum(line => line.Product.Price * line.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }
}
