using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SushiPay;
using System.Collections.ObjectModel;
using System.Windows.Automation;
using System.Windows.Media;
using Models;
using DAO;
using BUS;


namespace UnitTestSushiPay
{
    [TestClass]
    public class UnitTest1
    {
        OrderBUS orderBUS;
        OrderDAO orderDAO;
        public UnitTest1()
        {
            orderBUS = new OrderBUS();
            orderDAO = new OrderDAO();
        }

        [TestMethod]
        public void OrderConstructorTest1()
        {
            OrderProduct order = new OrderProduct();
            Assert.IsTrue(String.Empty == order.ProductName);
        }

        [TestMethod]
        public void OrderConstructorTest2()
        {
            OrderProduct order = new OrderProduct(145, "hanabi", 2, 145.5);
            Assert.IsTrue(145 == order.ProductId && "hanabi" == order.ProductName);
        }

        [TestMethod]
        public void RemoveAllTest()
        {
            OrderProduct order = new OrderProduct(145, "hanabi", 2, 145.5);
            OrderProduct order1 = new OrderProduct(145, "sakura", 2, 145.5);
            OrderProduct order2 = new OrderProduct(145, "kotuko", 2, 145.5);
            ObservableCollection<OrderProduct> collection = new ObservableCollection<OrderProduct>();
            collection.Add(order);
            collection.Add(order1);
            collection.Add(order2);

            orderBUS.RemoveAll(collection);

            int expected = 0;
            int actual = collection.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReadOrderTest()
        {
            string order = "145 hanabi 2 145";

            OrderProduct orderProductExpected = new OrderProduct(145, "hanabi", 2, 145);
            OrderProduct orderProductActual = (new OrderProduct()).ReadOrder(order);

            Assert.AreEqual(orderProductExpected.ProductName, orderProductActual.ProductName);
        }

        [TestMethod]
        public void GetOrdersTest()
        {
            var collection = orderBUS.GetOrders();

            int collectionCountExpected = 4;
            int collectionCountActual = collection.Count;

            Assert.AreEqual(collectionCountExpected, collectionCountActual);
        }

        [TestMethod]
        public void GetSumTest()
        {
            var collection = orderBUS.GetOrders();

            double sumExpected = 600;
            double sumActual = orderBUS.GetSum(collection);

            Assert.AreEqual(sumExpected, sumActual);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetSumExceptionTest()
        {
            OrderProduct order = new OrderProduct(145, "hanabi", 2, 0);
            OrderProduct order1 = new OrderProduct(145, "sakura", 2, 0);
            OrderProduct order2 = new OrderProduct(145, "kotuko", 2, 0);
            ObservableCollection<OrderProduct> collection = new ObservableCollection<OrderProduct>();

            double sumActual = orderBUS.GetSum(collection);
        }

        [TestMethod]
        public void IsTextAllowedTest_WhenDataIsCorrect()
        {
            string textData = "9565";

            bool expectedResult = true;
            bool actualResult = Rules.IsTextAllowed(textData);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void IsTextAllowedTest_WhenDataIsUncorrect()
        {
            string textData = "9565s";

            bool expectedResult = false;
            bool actualResult = Rules.IsTextAllowed(textData);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void IsCreditDataValid_WhenCreditDataIsCorrect()
        {
            string creditNumberData = "9565456695654566";
            string creditPasswordData = "0000";

            bool expectedResult = true;
            bool actualResult = Rules.IsCreditDataValid(creditNumberData, creditPasswordData);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IsCreditDataValid_WhenCreditNumberIsUncorrect()
        {
            string creditNumberWrongData = "8";
            string creditPasswordData = "0000";

            Rules.IsCreditDataValid(creditNumberWrongData, creditPasswordData);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IsCreditDataValid_WhenCreditDataIsUncorrect()
        {
            string creditNumberWrongData = "8";
            string creditPasswordWrongData = "00";

            Rules.IsCreditDataValid(creditNumberWrongData, creditPasswordWrongData);
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IsCreditDataValid_WhenCreditPasswordIsUncorrect()
        {
            string creditNumberData = "9565456695654566";
            string creditPasswordWrongData = "000";

            Rules.IsCreditDataValid(creditNumberData, creditPasswordWrongData);
        }

        [TestMethod]
        public void IsInputCashDataValid_WhenInputCashDataIsCorrect()
        {
            string inputCash = "100";
            double sum = 90;

            bool expectedResult = true;
            bool actualResult = Rules.IsInputCashDataValid(inputCash, sum);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IsCreditDataValid_WhenInputCashEmpty()
        {
            string inputCash = "";
            double sum = 90;

            Rules.IsInputCashDataValid(inputCash, sum);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IsCreditDataValid_WhenInputCashEmptyLessThanSum()
        {
            string inputCash = "80";
            double sum = 90;

            Rules.IsInputCashDataValid(inputCash, sum);
        }

        [TestMethod]
        public void ReadOrdersTest()
        {
            var collection = orderDAO.ReadOrders();

            int expectedCount = 4;
            int actualCount = collection.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
