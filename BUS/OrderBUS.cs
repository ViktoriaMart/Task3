using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DAO;
using Models;

namespace BUS
{
    public class OrderBUS
    {
        OrderDAO orderDAO;

        public OrderBUS()
        {
            orderDAO = new OrderDAO();
        }

        public ObservableCollection<OrderProduct> GetOrders()
        {
            var ordersCollection = orderDAO.ReadOrders();
            ObservableCollection<OrderProduct> collection = new ObservableCollection<OrderProduct>();

            foreach (var item in ordersCollection)
            {
                collection.Add(item);
            }

            if (collection.Count == 0)
            {
                throw new Exception("Немає активних замовлень");
            }
            return collection;
        }

        public double GetSum(ObservableCollection<OrderProduct> collection)
        {
            double sum = 0;
            
            foreach (var item in collection)
            {
                sum += item.ProductPrice;
            }

            if (sum <= 0)
            {
                throw new Exception("Неправильно введені дані");
            }

            return sum;
        }

        public void RemoveAll(IList list)
        {
            while (list.Count > 0)
            {
                list.RemoveAt(list.Count - 1);
            }
        }
    }
}
