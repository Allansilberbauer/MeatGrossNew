using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BIZ;
using Repository;

namespace UnitTestMeatGross
{
    [TestClass]
    public class UnitTestBeregnPris
    {
        [TestMethod]
        public void TestMethodPriceInEUR()
        {
            // Arange
            ClassOrder co = new ClassOrder();
            co.orderMeat = new ClassMeat();
            co.orderCustomer = new ClassCustomer();
            co.orderCustomer.country.valutaRate = 0.1300D;
            co.orderMeat.price = 10;
            co.orderMeat.stock = 10;

            double ExpRes = 13D;

            // Act
            co.orderWeight = 10;

            //Assert
            Assert.AreEqual(ExpRes, co.orderPriceValuta);
        }
    }
}
