using Microsoft.EntityFrameworkCore;
using Moq;
using ProductAPIEFCore;
using ProductAPIEFCore.Controllers;
using ProductAPIEFCore.Model;
using ProductAPIEFCore.Repository;

namespace ProductTest
{
    public class Tests
    {
        //Arrange
        
        List<Product> Products = new List<Product>();
        IQueryable<Product> billingdata;
        Mock<DbSet<Product>> mockSet;
        Mock<BirlasoftdbContext> billcontextmock;
        Mock<IProdRepo<Product>> prodrepo;
        
        [SetUp]
        public void Setup()
        {
            Products = new List<Product>()
            {
                new Product{Pid=1,Pname="Toy",Price=34 },
                new Product{Pid=2,Pname="Biscuits",Price=34 }

             };

            billingdata = Products.AsQueryable();
            mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(billingdata.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(billingdata.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(billingdata.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(billingdata.GetEnumerator());
            var p = new DbContextOptions<BirlasoftdbContext>();
            billcontextmock = new Mock<BirlasoftdbContext>(p);
            billcontextmock.Setup(x => x.Products).Returns(mockSet.Object);
            prodrepo = new Mock<IProdRepo<Product>>();
         
        }

        //[Test] //test method
        //public void TestAddMethod()
        //{
        //    //3A's - Assign, Act, Assert
        //    product.Setup(x => x.check(9)).Returns(true); //moq
        //    int actualresult=p.add(9, 6); //call the method
        //    int expectedresult = 15;
        //    Assert.AreEqual(expectedresult,actualresult);
        //}

        [Test]
        public void TestChecktrue()
        {
            ProdRepo obj = new ProdRepo(billcontextmock.Object);
            bool actualresult = obj.check(10);
            Assert.AreEqual(true, actualresult);
        }

        [Test]
        public void TestCheckfalse()
        {
            ProdRepo obj = new ProdRepo(billcontextmock.Object);
            bool actualresult = obj.check(3);
            Assert.AreEqual(false, actualresult);
        }

        [Test]
        public void TestGetAllProducts()
        {

            ProdRepo obj = new ProdRepo(billcontextmock.Object);
            var actualresult=obj.GetAllProducts();
            // var products = actualresult.Result;
            int count = actualresult.Count;
            Assert.AreEqual(2, count);
        }






    }
}