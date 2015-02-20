using System;
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
    public class FoodItemServicesTests
    {
        [TestMethod]
        public void GetFoodItems_OneItemExists()
        {
            // arrange 
            var dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Code");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Quantity");
            dataTable.Columns.Add("Date");
            dataTable.Columns.Add("ValuesArePerItem");

            var row = dataTable.NewRow();
            row["Id"] = 1;
            row["Code"] = "999";
            row["Name"] = "bacon";
            row["Quantity"] = 100;
            row["Date"] = DateTime.Today;
            row["ValuesArePerItem"] = false;
            dataTable.Rows.Add(row);

            var mock = new Mock<IFoodItemRepository>();
            mock.Setup(m => m.GetFoodItems(It.IsAny<DateTime>(), It.IsAny<int>())).Returns(dataTable);

            var expected = new List<FoodItem>
            {
                new FoodItem
                {
                    Date = DateTime.Today,
                    FoodCode = "999",
                    Id = 1,
                    Name = "bacon",
                    Quantity = 100
                    //ValuesArePerItem = false
                }
            };

            var foodItemServices = new FoodItemServices(mock.Object, new FoodItemMapper());

            // act
            List<FoodItem> actual = foodItemServices.GetFoodItems(new DateTime(), 1).ToList();

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetFoodItems_MultipleItemsExist()
        {
            // arrange 
            var dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Code");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Quantity");
            dataTable.Columns.Add("Date");
            dataTable.Columns.Add("ValuesArePerItem");

            var row1 = dataTable.NewRow();
            row1["Id"] = 1;
            row1["Code"] = "999";
            row1["Name"] = "bacon";
            row1["Quantity"] = 100;
            row1["Date"] = DateTime.Today;
            row1["ValuesArePerItem"] = false;
            dataTable.Rows.Add(row1);

            var row2 = dataTable.NewRow();
            row2["Id"] = 2;
            row2["Code"] = "1000";
            row2["Name"] = "eggs";
            row2["Quantity"] = 100;
            row2["Date"] = DateTime.Today;
            row2["ValuesArePerItem"] = false;
            dataTable.Rows.Add(row2);

            var mock = new Mock<IFoodItemRepository>();
            mock.Setup(m => m.GetFoodItems(It.IsAny<DateTime>(), It.IsAny<int>())).Returns(dataTable);

            var expected = new List<FoodItem>
            {
                new FoodItem
                {
                    Date = DateTime.Today,
                    FoodCode = "999",
                    Id = 1,
                    Name = "bacon",
                    Quantity = 100
                    //ValuesArePerItem = false
                },

                new FoodItem
                {
                    Date = DateTime.Today,
                    FoodCode = "1000",
                    Id = 2,
                    Name = "eggs",
                    Quantity = 100
                    //ValuesArePerItem = false
                }
            };

            var foodItemServices = new FoodItemServices(mock.Object, new FoodItemMapper());

            // act
            List<FoodItem> actual = foodItemServices.GetFoodItems(new DateTime(), 1).ToList();

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetFoodItems_NoItemsExist()
        {
            // arrange 
            var dataTable = new DataTable();
            var mock = new Mock<IFoodItemRepository>();
            mock.Setup(m => m.GetFoodItems(It.IsAny<DateTime>(), It.IsAny<int>())).Returns(dataTable);
            var foodItemServices = new FoodItemServices(mock.Object, new FoodItemMapper());

            var expected = new List<FoodItem>();

            // act
            List<FoodItem> actual = foodItemServices.GetFoodItems(new DateTime(), 1).ToList();

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }

    }
}
