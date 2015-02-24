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

            var row = dataTable.NewRow();
            row["Id"] = 1;
            row["Code"] = "999";
            row["Name"] = "bacon";
            row["Quantity"] = 100;
            row["Date"] = DateTime.Today;
            dataTable.Rows.Add(row);

            var mock = new Mock<IFoodItemRepository>();
            mock.Setup(m => m.GetFoodItems(It.IsAny<DateTime>(), It.IsAny<int>())).Returns(dataTable);

            var expected = new List<FoodItem>
            {
                new FoodItem
                {
                    Date = DateTime.Today,
                    Code = "999",
                    Id = 1,
                    Name = "bacon",
                    Quantity = 100
                }
            };

            var foodItemServices = new FoodItemServices(mock.Object, new FoodItemMapper(), new FavouriteRepository(), new FavouriteMapper());

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

            var row1 = dataTable.NewRow();
            row1["Id"] = 1;
            row1["Code"] = "999";
            row1["Name"] = "bacon";
            row1["Quantity"] = 100;
            row1["Date"] = DateTime.Today;
            dataTable.Rows.Add(row1);

            var row2 = dataTable.NewRow();
            row2["Id"] = 2;
            row2["Code"] = "1000";
            row2["Name"] = "eggs";
            row2["Quantity"] = 100;
            row2["Date"] = DateTime.Today;
            dataTable.Rows.Add(row2);

            var mock = new Mock<IFoodItemRepository>();
            mock.Setup(m => m.GetFoodItems(It.IsAny<DateTime>(), It.IsAny<int>())).Returns(dataTable);

            var expected = new List<FoodItem>
            {
                new FoodItem
                {
                    Date = DateTime.Today,
                    Code = "999",
                    Id = 1,
                    Name = "bacon",
                    Quantity = 100
                },

                new FoodItem
                {
                    Date = DateTime.Today,
                    Code = "1000",
                    Id = 2,
                    Name = "eggs",
                    Quantity = 100
                }
            };

            var foodItemServices = new FoodItemServices(mock.Object, new FoodItemMapper(), new FavouriteRepository(), new FavouriteMapper());

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
            var foodItemServices = new FoodItemServices(mock.Object, new FoodItemMapper(), new FavouriteRepository(), new FavouriteMapper());

            var expected = new List<FoodItem>();

            // act
            List<FoodItem> actual = foodItemServices.GetFoodItems(new DateTime(), 1).ToList();

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }



        [TestMethod]
        public void GetFavourites_OneItemExists()
        {
            // arrange 
            var dataTable = new DataTable();
            dataTable.Columns.Add("Code");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Quantity");

            var row = dataTable.NewRow();
            row["Code"] = "999";
            row["Name"] = "bacon";           
            row["Quantity"] = 100;            
            dataTable.Rows.Add(row);

            var mock = new Mock<IFavouriteRepository>();
            mock.Setup(m => m.GetFavourites(It.IsAny<int>())).Returns(dataTable);

            var expected = new List<Favourite>
            {
                new Favourite
                {                    
                    Code = "999",                 
                    Name = "bacon",
                    Quantity = 100
                }
            };

            var foodItemServices = new FoodItemServices(new FoodItemRepository(), new FoodItemMapper(), mock.Object, new FavouriteMapper());

            // act
            List<Favourite> actual = foodItemServices.GetFavourites(1).ToList();

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetFavourites_MultipleItemsExist()
        {
            // arrange 
            var dataTable = new DataTable();
            dataTable.Columns.Add("Code");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Quantity");

            var row1 = dataTable.NewRow();
            row1["Code"] = "999";
            row1["Name"] = "bacon";
            row1["Quantity"] = 100;
            dataTable.Rows.Add(row1);

            var row2 = dataTable.NewRow();
            row2["Code"] = "1000";
            row2["Name"] = "eggs";
            row2["Quantity"] = 50;
            dataTable.Rows.Add(row2);

            var mock = new Mock<IFavouriteRepository>();
            mock.Setup(m => m.GetFavourites(It.IsAny<int>())).Returns(dataTable);

            var expected = new List<Favourite>
            {
                new Favourite
                {                    
                    Code = "999",                 
                    Name = "bacon",
                    Quantity = 100
                },
                new Favourite
                {                    
                    Code = "1000",                 
                    Name = "eggs",
                    Quantity = 50
                }

            };

            var foodItemServices = new FoodItemServices(new FoodItemRepository(), new FoodItemMapper(), mock.Object, new FavouriteMapper());

            // act
            List<Favourite> actual = foodItemServices.GetFavourites(1).ToList();

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetFavourites_NoItemsExist()
        {
            // arrange 
            var dataTable = new DataTable();
            var mock = new Mock<IFavouriteRepository>();
            mock.Setup(m => m.GetFavourites(It.IsAny<int>())).Returns(dataTable);
            var foodItemServices = new FoodItemServices(new FoodItemRepository(), new FoodItemMapper(), mock.Object, new FavouriteMapper());

            var expected = new List<Favourite>();

            // act
            List<Favourite> actual = foodItemServices.GetFavourites(1).ToList();

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


    }
}
