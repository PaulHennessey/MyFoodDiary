using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyFoodDiary.Data.Abstract;
using MyFoodDiary.Data.Concrete;
using MyFoodDiary.Domain;
using MyFoodDiary.Services.Concrete;

namespace MyFoodDiary.Tests.Services
{
    [TestClass]
    public class ProductServicesTests
    {
        [TestMethod]
        public void GetProductsByUserId_OneItemExists()
        {
            // arrange 
            var dataTable = new DataTable();
            dataTable.Columns.Add("Code");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Protein");
            dataTable.Columns.Add("Carbohydrate");
            dataTable.Columns.Add("Fat");
            dataTable.Columns.Add("Calories");
            dataTable.Columns.Add("Alcohol");
            dataTable.Columns.Add("ValuesArePerItem");
            dataTable.Columns.Add("ServingSize");

            var row = dataTable.NewRow();
            row["Code"] = "XXX";
            row["Name"] = "Barney";
            row["Protein"] = 1;
            row["Carbohydrate"] = 1;
            row["Fat"] = 1;
            row["Calories"] = 1;
            row["Alcohol"] = 1;
            row["ValuesArePerItem"] = false;
            row["ServingSize"] = 1;
            dataTable.Rows.Add(row);

            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetProducts(It.IsAny<int>())).Returns(dataTable);

            var expected = new List<Product>
            {
                new Product
                {
                    Code = "XXX",
                    Name = "Barney",
                    Nutrients = new Dictionary<string, double>()
                    {
                        { "Protein", 1 },
                        { "Carbohydrates", 1 },
                        { "Fat", 1 },
                        { "Calories", 1 },
                        { "Alcohol", 1 }
                    },
                    ValuesArePerItem = false,
                    ServingSize = 1
                }
            };

            var productServices = new ProductServices(mock.Object, new ProductMapper());

            // act
            List<Product> actual = productServices.GetProducts(1).ToList();

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetProductsByUserId_MultipleItemsExist()
        {
            // arrange 
            var dataTable = new DataTable();
            dataTable.Columns.Add("Code");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Protein");
            dataTable.Columns.Add("Carbohydrate");
            dataTable.Columns.Add("Fat");
            dataTable.Columns.Add("Calories");
            dataTable.Columns.Add("Alcohol");
            dataTable.Columns.Add("ValuesArePerItem");
            dataTable.Columns.Add("ServingSize");

            var row1 = dataTable.NewRow();
            row1["Code"] = "XXX";
            row1["Name"] = "Barney";
            row1["Protein"] = 1;
            row1["Carbohydrate"] = 1;
            row1["Fat"] = 1;
            row1["Calories"] = 1;
            row1["Alcohol"] = 1;
            row1["ValuesArePerItem"] = false;
            row1["ServingSize"] = 1;
            dataTable.Rows.Add(row1);

            var row2 = dataTable.NewRow();
            row2["Code"] = "YYY";
            row2["Name"] = "Fred";
            row2["Protein"] = 1;
            row2["Carbohydrate"] = 1;
            row2["Fat"] = 1;
            row2["Calories"] = 1;
            row2["Alcohol"] = 1;
            row2["ValuesArePerItem"] = false;
            row2["ServingSize"] = 1;
            dataTable.Rows.Add(row2);

            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetProducts(It.IsAny<int>())).Returns(dataTable);

            var expected = new List<Product>
            {
                new Product
                {
                    Code = "XXX",
                    Name = "Barney",
                    Nutrients = new Dictionary<string, double>()
                    {
                        { "Protein", 1 },
                        { "Carbohydrates", 1 },
                        { "Fat", 1 },
                        { "Calories", 1 },
                        { "Alcohol", 1 }
                    },
                    ValuesArePerItem = false,
                    ServingSize = 1
                },
                new Product
                {
                    Code = "YYY",
                    Name = "Fred",
                    Nutrients = new Dictionary<string, double>()
                    {
                        { "Protein", 1 },
                        { "Carbohydrates", 1 },
                        { "Fat", 1 },
                        { "Calories", 1 },
                        { "Alcohol", 1 }
                    },
                    ValuesArePerItem = false,
                    ServingSize = 1
                }

            };

            var productServices = new ProductServices(mock.Object, new ProductMapper());

            // act
            List<Product> actual = productServices.GetProducts(1).ToList();

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetProductsByUserId_NoItemsExist()
        {
            // arrange 
            var dataTable = new DataTable();
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetProducts(It.IsAny<int>())).Returns(dataTable);

            var expected = new List<Product>();
            var productServices = new ProductServices(mock.Object, new ProductMapper());

            // act
            List<Product> actual = productServices.GetProducts(1).ToList();

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetProductsByFoodItems_OneItemExists()
        {
            // arrange 
            var dataTable = new DataTable();
            dataTable.Columns.Add("Code");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Protein");
            dataTable.Columns.Add("Carbohydrate");
            dataTable.Columns.Add("Fat");
            dataTable.Columns.Add("Calories");
            dataTable.Columns.Add("Alcohol");
            dataTable.Columns.Add("ValuesArePerItem");
            dataTable.Columns.Add("ServingSize");

            var row = dataTable.NewRow();
            row["Code"] = "XXX";
            row["Name"] = "Barney";
            row["Protein"] = 1;
            row["Carbohydrate"] = 1;
            row["Fat"] = 1;
            row["Calories"] = 1;
            row["Alcohol"] = 1;
            row["ValuesArePerItem"] = false;
            row["ServingSize"] = 1;
            dataTable.Rows.Add(row);

            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetProducts(It.IsAny<List<FoodItem>>())).Returns(dataTable);

            var expected = new List<Product>
            {
                new Product
                {
                    Code = "XXX",
                    Name = "Barney",
                    Nutrients = new Dictionary<string, double>()
                    {
                        { "Protein", 1 },
                        { "Carbohydrates", 1 },
                        { "Fat", 1 },
                        { "Calories", 1 },
                        { "Alcohol", 1 }
                    },
                    ValuesArePerItem = false,
                    ServingSize = 1
                }
            };

            var productServices = new ProductServices(mock.Object, new ProductMapper());

            // act
            List<Product> actual = productServices.GetProducts(new List<FoodItem>()).ToList();

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetProductsByFoodItems_MultipleItemsExist()
        {
            // arrange 
            var dataTable = new DataTable();
            dataTable.Columns.Add("Code");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Protein");
            dataTable.Columns.Add("Carbohydrate");
            dataTable.Columns.Add("Fat");
            dataTable.Columns.Add("Calories");
            dataTable.Columns.Add("Alcohol");
            dataTable.Columns.Add("ValuesArePerItem");
            dataTable.Columns.Add("ServingSize");

            var row1 = dataTable.NewRow();
            row1["Code"] = "XXX";
            row1["Name"] = "Barney";
            row1["Protein"] = 1;
            row1["Carbohydrate"] = 1;
            row1["Fat"] = 1;
            row1["Calories"] = 1;
            row1["Alcohol"] = 1;
            row1["ValuesArePerItem"] = false;
            row1["ServingSize"] = 1;
            dataTable.Rows.Add(row1);

            var row2 = dataTable.NewRow();
            row2["Code"] = "YYY";
            row2["Name"] = "Fred";
            row2["Protein"] = 1;
            row2["Carbohydrate"] = 1;
            row2["Fat"] = 1;
            row2["Calories"] = 1;
            row2["Alcohol"] = 1;
            row2["ValuesArePerItem"] = false;
            row2["ServingSize"] = 1;
            dataTable.Rows.Add(row2);

            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetProducts(It.IsAny<List<FoodItem>>())).Returns(dataTable);

            var expected = new List<Product>
            {
                new Product
                {
                    Code = "XXX",
                    Name = "Barney",
                    Nutrients = new Dictionary<string, double>()
                    {
                        { "Protein", 1 },
                        { "Carbohydrates", 1 },
                        { "Fat", 1 },
                        { "Calories", 1 },
                        { "Alcohol", 1 }
                    },
                    ValuesArePerItem = false,
                    ServingSize = 1
                },
                new Product
                {
                    Code = "YYY",
                    Name = "Fred",
                    Nutrients = new Dictionary<string, double>()
                    {
                        { "Protein", 1 },
                        { "Carbohydrates", 1 },
                        { "Fat", 1 },
                        { "Calories", 1 },
                        { "Alcohol", 1 }
                    },
                    ValuesArePerItem = false,
                    ServingSize = 1
                }

            };

            var productServices = new ProductServices(mock.Object, new ProductMapper());

            // act
            List<Product> actual = productServices.GetProducts(new List<FoodItem>()).ToList();

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetProductsByFoodItems_NoItemsExist()
        {
            // arrange 
            var dataTable = new DataTable();
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetProducts(It.IsAny<List<FoodItem>>())).Returns(dataTable);

            var expected = new List<Product>();
            var productServices = new ProductServices(mock.Object, new ProductMapper());

            // act
            List<Product> actual = productServices.GetProducts(new List<FoodItem>()).ToList();

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetProductByCode_OneItemExists()
        {
            // arrange 
            var dataTable = new DataTable();
            dataTable.Columns.Add("Code");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Protein");
            dataTable.Columns.Add("Carbohydrate");
            dataTable.Columns.Add("Fat");
            dataTable.Columns.Add("Calories");
            dataTable.Columns.Add("Alcohol");
            dataTable.Columns.Add("ValuesArePerItem");
            dataTable.Columns.Add("ServingSize");

            var row = dataTable.NewRow();
            row["Code"] = "XXX";
            row["Name"] = "Barney";
            row["Protein"] = 1;
            row["Carbohydrate"] = 1;
            row["Fat"] = 1;
            row["Calories"] = 1;
            row["Alcohol"] = 1;
            row["ValuesArePerItem"] = false;
            row["ServingSize"] = 1;
            dataTable.Rows.Add(row);

            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetProduct(It.IsAny<string>())).Returns(dataTable);

            var expected = new Product
            {
                Code = "XXX",
                Name = "Barney",
                Nutrients = new Dictionary<string, double>()
                                    {
                                        { "Protein", 1 },
                                        { "Carbohydrates", 1 },
                                        { "Fat", 1 },
                                        { "Calories", 1 },
                                        { "Alcohol", 1 }
                                    },
                ValuesArePerItem = false,
                ServingSize = 1
            };

            var productServices = new ProductServices(mock.Object, new ProductMapper());

            // act
            Product actual = productServices.GetProduct("XXX");

            // assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetProductByCode_MultipleItemsExist()
        {
            // arrange 
            var dataTable = new DataTable();
            dataTable.Columns.Add("Code");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Protein");
            dataTable.Columns.Add("Carbohydrate");
            dataTable.Columns.Add("Fat");
            dataTable.Columns.Add("Calories");
            dataTable.Columns.Add("Alcohol");
            dataTable.Columns.Add("ValuesArePerItem");
            dataTable.Columns.Add("ServingSize");

            var row1 = dataTable.NewRow();
            row1["Code"] = "XXX";
            row1["Name"] = "Barney";
            row1["Protein"] = 1;
            row1["Carbohydrate"] = 1;
            row1["Fat"] = 1;
            row1["Calories"] = 1;
            row1["Alcohol"] = 1;
            row1["ValuesArePerItem"] = false;
            row1["ServingSize"] = 1;
            dataTable.Rows.Add(row1);

            var row2 = dataTable.NewRow();
            row2["Code"] = "YYY";
            row2["Name"] = "Fred";
            row2["Protein"] = 1;
            row2["Carbohydrate"] = 1;
            row2["Fat"] = 1;
            row2["Calories"] = 1;
            row2["Alcohol"] = 1;
            row2["ValuesArePerItem"] = false;
            row2["ServingSize"] = 1;
            dataTable.Rows.Add(row2);

            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetProduct(It.IsAny<string>())).Returns(dataTable);

            var expected = new Product
            {
                Code = "XXX",
                Name = "Barney",
                Nutrients = new Dictionary<string, double>()
                                    {
                                        { "Protein", 1 },
                                        { "Carbohydrates", 1 },
                                        { "Fat", 1 },
                                        { "Calories", 1 },
                                        { "Alcohol", 1 }
                                    },
                ValuesArePerItem = false,
                ServingSize = 1
            };

            var productServices = new ProductServices(mock.Object, new ProductMapper());

            // act
            Product actual = productServices.GetProduct("XXX");

            // assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetProductByCode_NoItemsExist()
        {
            // arrange 
            var dataTable = new DataTable();
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetProduct(It.IsAny<string>())).Returns(dataTable);

            Product expected = null;
            var productServices = new ProductServices(mock.Object, new ProductMapper());

            // act
            Product actual = productServices.GetProduct("XXX");

            // assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetCustomProductsByUserId_OneItemExists()
        {
            // arrange 
            var dataTable = new DataTable();
            dataTable.Columns.Add("Code");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Protein");
            dataTable.Columns.Add("Carbohydrate");
            dataTable.Columns.Add("Fat");
            dataTable.Columns.Add("Calories");
            dataTable.Columns.Add("Alcohol");
            dataTable.Columns.Add("ValuesArePerItem");
            dataTable.Columns.Add("ServingSize");

            var row = dataTable.NewRow();
            row["Code"] = "XXX";
            row["Name"] = "Barney";
            row["Protein"] = 1;
            row["Carbohydrate"] = 1;
            row["Fat"] = 1;
            row["Calories"] = 1;
            row["Alcohol"] = 1;
            row["ValuesArePerItem"] = false;
            row["ServingSize"] = 1;
            dataTable.Rows.Add(row);

            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetCustomProducts(It.IsAny<int>())).Returns(dataTable);

            var expected = new List<Product>
            {
                new Product
                {
                    Code = "XXX",
                    Name = "Barney",
                    Nutrients = new Dictionary<string, double>()
                    {
                        { "Protein", 1 },
                        { "Carbohydrates", 1 },
                        { "Fat", 1 },
                        { "Calories", 1 },
                        { "Alcohol", 1 }
                    },
                    ValuesArePerItem = false,
                    ServingSize = 1
                }
            };

            var productServices = new ProductServices(mock.Object, new ProductMapper());

            // act
            List<Product> actual = productServices.GetCustomProducts(1).ToList();

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetCustomProductsByUserId_MultipleItemsExist()
        {
            // arrange 
            var dataTable = new DataTable();
            dataTable.Columns.Add("Code");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Protein");
            dataTable.Columns.Add("Carbohydrate");
            dataTable.Columns.Add("Fat");
            dataTable.Columns.Add("Calories");
            dataTable.Columns.Add("Alcohol");
            dataTable.Columns.Add("ValuesArePerItem");
            dataTable.Columns.Add("ServingSize");

            var row1 = dataTable.NewRow();
            row1["Code"] = "XXX";
            row1["Name"] = "Barney";
            row1["Protein"] = 1;
            row1["Carbohydrate"] = 1;
            row1["Fat"] = 1;
            row1["Calories"] = 1;
            row1["Alcohol"] = 1;
            row1["ValuesArePerItem"] = false;
            row1["ServingSize"] = 1;
            dataTable.Rows.Add(row1);

            var row2 = dataTable.NewRow();
            row2["Code"] = "YYY";
            row2["Name"] = "Fred";
            row2["Protein"] = 1;
            row2["Carbohydrate"] = 1;
            row2["Fat"] = 1;
            row2["Calories"] = 1;
            row2["Alcohol"] = 1;
            row2["ValuesArePerItem"] = false;
            row2["ServingSize"] = 1;
            dataTable.Rows.Add(row2);

            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetCustomProducts(It.IsAny<int>())).Returns(dataTable);

            var expected = new List<Product>
            {
                new Product
                {
                    Code = "XXX",
                    Name = "Barney",
                    Nutrients = new Dictionary<string, double>()
                    {
                        { "Protein", 1 },
                        { "Carbohydrates", 1 },
                        { "Fat", 1 },
                        { "Calories", 1 },
                        { "Alcohol", 1 }
                    },
                    ValuesArePerItem = false,
                    ServingSize = 1
                },
                new Product
                {
                    Code = "YYY",
                    Name = "Fred",
                    Nutrients = new Dictionary<string, double>()
                    {
                        { "Protein", 1 },
                        { "Carbohydrates", 1 },
                        { "Fat", 1 },
                        { "Calories", 1 },
                        { "Alcohol", 1 }
                    },
                    ValuesArePerItem = false,
                    ServingSize = 1
                }

            };

            var productServices = new ProductServices(mock.Object, new ProductMapper());

            // act
            List<Product> actual = productServices.GetCustomProducts(1).ToList();

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetCustomProductsByUserId_NoItemsExist()
        {
            // arrange 
            var dataTable = new DataTable();
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetCustomProducts(It.IsAny<int>())).Returns(dataTable);

            var expected = new List<Product>();
            var productServices = new ProductServices(mock.Object, new ProductMapper());

            // act
            List<Product> actual = productServices.GetCustomProducts(1).ToList();

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }

    }
}
