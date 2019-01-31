using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.IO;

namespace DAO
{
    public class OrderDAO
    {
        public List<OrderProduct> ReadOrders()
        {
            string line;
            List<OrderProduct> collection = new List<OrderProduct>();

            using (StreamReader sr = new StreamReader("orders.txt", System.Text.Encoding.Default))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    collection.Add((new OrderProduct().ReadOrder(line)) as OrderProduct);
                }
            }

            return collection;
        }
    }
}
