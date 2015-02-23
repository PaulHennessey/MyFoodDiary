using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyFoodDiary.Domain;
using MyFoodDiary.Services.Concrete;

namespace MyFoodDiary.Tests.Services
{
    [TestClass]
    public class ChartServicesTests
    {
        [TestMethod]
        public void GetBarNames_OneDay_OneItem()
        {
            // arrange 
            var foodItems = new List<FoodItem>
            {
                new FoodItem{Code = "1", Name = "XXX"}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems}
            };

            var expected = new List<string> { "XXX", "Total" };

            // act
            List<string> actual = new ChartServices().GetBarNames(days);

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetBarNames_OneDay_MultipleItemsSameName()
        {
            // arrange 
            var foodItems = new List<FoodItem>
            {
                new FoodItem{Code = "1", Name = "XXX"},
                new FoodItem{Code = "1", Name = "XXX"}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems}
            };

            var expected = new List<string> { "XXX", "Total" };

            // act
            List<string> actual = new ChartServices().GetBarNames(days);

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetBarNames_OneDay_MultipleItemsDifferentNames()
        {
            // arrange 
            var foodItems = new List<FoodItem>
            {
                new FoodItem{Code = "1", Name = "XXX"},
                new FoodItem{Code = "2", Name = "YYY"}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems}
            };

            var expected = new List<string> { "XXX", "YYY", "Total" };

            // act
            List<string> actual = new ChartServices().GetBarNames(days);

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetBarNames_MultipleDays_OneItem()
        {
            // arrange 
            var foodItems = new List<FoodItem>
            {
                new FoodItem{Code = "1", Name = "XXX"}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems},
                new Day {Date = new DateTime(2015, 1, 20), Food = foodItems}
            };

            var expected = new List<string> { "XXX", "Total" };

            // act
            List<string> actual = new ChartServices().GetBarNames(days);

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetBarNames_MultipleDays_MultipleItems()
        {
            // arrange 
            var foodItems = new List<FoodItem>
            {
                new FoodItem{Code = "1", Name = "XXX"},
                new FoodItem{Code = "2", Name = "YYY"}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems},
                new Day {Date = new DateTime(2015, 1, 20), Food = foodItems}
            };

            var expected = new List<string> { "XXX", "YYY", "Total" };

            // act
            List<string> actual = new ChartServices().GetBarNames(days);

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetBarNames_MultipleDays_MultipleDifferentItems()
        {
            // arrange 
            var foodItems1 = new List<FoodItem>
            {
                new FoodItem{Code = "1", Name = "AAA"},
                new FoodItem{Code = "2", Name = "BBB"}
            };

            var foodItems2 = new List<FoodItem>
            {
                new FoodItem{Code = "3", Name = "XXX"},
                new FoodItem{Code = "4", Name = "YYY"}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems1},
                new Day {Date = new DateTime(2015, 1, 20), Food = foodItems2}
            };

            var expected = new List<string> { "AAA", "BBB", "XXX", "YYY", "Total" };

            // act
            List<string> actual = new ChartServices().GetBarNames(days);

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CalculateNutrientByProduct_OneDay_OneItem()
        {
            // arrange 
            var foodItems = new List<FoodItem>
            {
                new FoodItem{Code = "1", Quantity = 1}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems}
            };

            var products = new List<Product>
            {
                new Product{Code = "1", Nutrients = new Dictionary<string, double>{{"Protein", 5.6}}, ServingSize = 100}
            };

            var expected = new List<double> { 5.6, 5.6 };

            // act
            List<double> actual = new ChartServices().CalculateNutrientByProduct(days, products, "Protein");

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CalculateNutrientByProduct_OneDay_OneItemRepeated()
        {
            // arrange 
            var foodItems = new List<FoodItem>
            {
                new FoodItem{Code = "1", Quantity = 1},
                new FoodItem{Code = "1", Quantity = 1}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems}
            };

            var products = new List<Product>
            {
                new Product{Code = "1", Nutrients = new Dictionary<string, double>{{"Protein", 5.6}}, ServingSize = 100}
            };

            var expected = new List<double> { 11.2, 11.2 };

            // act
            List<double> actual = new ChartServices().CalculateNutrientByProduct(days, products, "Protein");

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CalculateNutrientByProduct_OneDay_TwoItems()
        {
            // arrange 
            var foodItems = new List<FoodItem>
            {
                new FoodItem{Code = "1", Quantity = 1},
                new FoodItem{Code = "2", Quantity = 100}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems}
            };

            var products = new List<Product>
            {
                new Product{Code = "1", Nutrients = new Dictionary<string, double>{{"Protein", 5.6}}, ServingSize = 100},
                new Product{Code = "2", Nutrients = new Dictionary<string, double>{{"Protein", 1.4}}, ServingSize = 0}
            };

            var expected = new List<double>
            {   
                5.6,
                1.4,
                7.0
            };

            // act
            List<double> actual = new ChartServices().CalculateNutrientByProduct(days, products, "Protein");

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CalculateNutrientByProduct_OneDay_TwoItemsOneMultiple()
        {
            // arrange 
            var foodItems = new List<FoodItem>
            {
                new FoodItem{Code = "1", Quantity = 3},
                new FoodItem{Code = "2", Quantity = 100}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems}
            };

            var products = new List<Product>
            {
                new Product{Code = "1", Nutrients = new Dictionary<string, double>{{"Protein", 5.6}}, ServingSize = 100},
                new Product{Code = "2", Nutrients = new Dictionary<string, double>{{"Protein", 1.4}}, ServingSize = 0}
            };

            var expected = new List<double>
            {   
                16.8,
                1.4,
                18.2
            };

            // act
            List<double> actual = new ChartServices().CalculateNutrientByProduct(days, products, "Protein");

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }



        [TestMethod]
        public void CalculateNutrientByProduct_MultipleDays_OneItem()
        {
            // arrange 
            var foodItems = new List<FoodItem>
            {
                new FoodItem{Code = "1", Quantity = 1}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems},
                new Day {Date = new DateTime(2015, 1, 20), Food = foodItems}
            };

            var products = new List<Product>
            {
                new Product{Code = "1", Nutrients = new Dictionary<string, double>{{"Protein", 5.6}}, ServingSize = 100}
            };

            var expected = new List<double> { 11.2, 11.2 };

            // act
            List<double> actual = new ChartServices().CalculateNutrientByProduct(days, products, "Protein");

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetDates_OneDay()
        {
            // arrange
            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19) }
            };

            var expected = new List<string> { "19/01/2015" };

            // act
            List<string> actual = new ChartServices().GetDates(days);

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetDates_MultipleDays()
        {
            // arrange
            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19) },
                new Day {Date = new DateTime(2015, 1, 20) }
            };

            var expected = new List<string> { "19/01/2015", "20/01/2015" };

            // act
            List<string> actual = new ChartServices().GetDates(days);

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CalculateNutrientByDay_OneDay_OneItem()
        {
            // arrange 
            var foodItems = new List<FoodItem>
            {
                new FoodItem{Code = "1", Quantity = 1}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems}
            };

            var products = new List<Product>
            {
                new Product{Code = "1", Nutrients = new Dictionary<string, double>{{"Protein", 5.6}}, ServingSize = 100}
            };

            var expected = new List<double> { 5.6 };

            // act
            List<double> actual = new ChartServices().CalculateNutrientByDay(days, products, "Protein");

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CalculateNutrientByDay_OneDay_OneItemRepeated()
        {
            // arrange 
            var foodItems = new List<FoodItem>
            {
                new FoodItem{Code = "1", Quantity = 1},
                new FoodItem{Code = "1", Quantity = 1}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems}
            };

            var products = new List<Product>
            {
                new Product{Code = "1", Nutrients = new Dictionary<string, double>{{"Protein", 5.6}}, ServingSize = 100}
            };

            var expected = new List<double> { 11.2 };

            // act
            List<double> actual = new ChartServices().CalculateNutrientByDay(days, products, "Protein");

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CalculateNutrientByDay_OneDay_TwoItems()
        {
            // arrange 
            var foodItems = new List<FoodItem>
            {
                new FoodItem{Code = "1", Quantity = 1},
                new FoodItem{Code = "2", Quantity = 100}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems}
            };

            var products = new List<Product>
            {
                new Product{Code = "1", Nutrients = new Dictionary<string, double>{{"Protein", 5.6}}, ServingSize = 100},
                new Product{Code = "2", Nutrients = new Dictionary<string, double>{{"Protein", 1.4}}, ServingSize = 0}
            };

            var expected = new List<double> { 7.0 };

            // act
            List<double> actual = new ChartServices().CalculateNutrientByDay(days, products, "Protein");

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CalculateNutrientByDay_OneDay_TwoItemsOneMultiple()
        {
            // arrange 
            var foodItems = new List<FoodItem>
            {
                new FoodItem{Code = "1", Quantity = 3},
                new FoodItem{Code = "2", Quantity = 100}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems}
            };

            var products = new List<Product>
            {
                new Product{Code = "1", Nutrients = new Dictionary<string, double>{{"Protein", 5.6}}, ServingSize = 100},
                new Product{Code = "2", Nutrients = new Dictionary<string, double>{{"Protein", 1.4}}, ServingSize = 0}
            };

            var expected = new List<double> { 18.2 };

            // act
            List<double> actual = new ChartServices().CalculateNutrientByDay(days, products, "Protein");

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }




        [TestMethod]
        public void CalculateNutrientByDay_MultipleDays_OneItem()
        {
            // arrange 
            var foodItems = new List<FoodItem>
            {
                new FoodItem{Code = "1", Quantity = 1}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems},
                new Day {Date = new DateTime(2015, 1, 20), Food = foodItems}
            };

            var products = new List<Product>
            {
                new Product{Code = "1", Nutrients = new Dictionary<string, double>{{"Protein", 5.6}}, ServingSize = 100}
            };

            var expected = new List<double> { 5.6, 5.6 };

            // act
            List<double> actual = new ChartServices().CalculateNutrientByDay(days, products, "Protein");

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CalculateNutrientByDay_MultipleDays_MultipleItems()
        {
            // arrange 
            var foodItems1 = new List<FoodItem>
            {
                new FoodItem{Code = "1", Quantity = 1},
                new FoodItem{Code = "2", Quantity = 100}
            };

            var foodItems2 = new List<FoodItem>
            {
                new FoodItem{Code = "3", Quantity = 2},
                new FoodItem{Code = "4", Quantity = 10}
            };

            var foodItems3 = new List<FoodItem>
            {
                new FoodItem{Code = "3", Quantity = 1},
                new FoodItem{Code = "4", Quantity = 5}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems1},
                new Day {Date = new DateTime(2015, 1, 20), Food = foodItems2},
                new Day {Date = new DateTime(2015, 1, 21), Food = foodItems3}
            };

            var products = new List<Product>
            {
                new Product{Code = "1", Nutrients = new Dictionary<string, double>{{"Protein", 5}}, ServingSize = 100},
                new Product{Code = "2", Nutrients = new Dictionary<string, double>{{"Protein", 30}}, ServingSize = 0},
                new Product{Code = "3", Nutrients = new Dictionary<string, double>{{"Protein", 10}}, ServingSize = 100},
                new Product{Code = "4", Nutrients = new Dictionary<string, double>{{"Protein", 50}}, ServingSize = 0}
            };

            var expected = new List<double> { 35, 25, 12.5 };

            // act
            List<double> actual = new ChartServices().CalculateNutrientByDay(days, products, "Protein");

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }




        [TestMethod]
        public void CalculateTotalEnergyData_OneDay_OneItem()
        {
            // arrange 
            var foodItems = new List<FoodItem>
            {
                new FoodItem{Code = "1", Quantity = 1}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems}
            };

            var products = new List<Product>
            {
                new Product{Code = "1", Nutrients = new Dictionary<string, double>{{"Protein", 10}, {"Carbohydrates", 10}, {"Fat", 10}}, ServingSize = 100}
            };

            var expected = new List<double> { 23.5, 23.5, 52.9 };

            // act
            List<double> actual = new ChartServices().CalculateTotalEnergyData(days, products);

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CalculateTotalEnergyData_OneDay_MultipleItems()
        {
            // arrange 
            var foodItems = new List<FoodItem>
            {
                new FoodItem{Code = "1", Quantity = 1}, 
                new FoodItem{Code = "2", Quantity = 100}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems}
            };

            var products = new List<Product>
            {
                new Product{Code = "1", Nutrients = new Dictionary<string, double>{{"Protein", 10}, {"Carbohydrates", 10}, {"Fat", 10}}, ServingSize = 100},
                new Product{Code = "2", Nutrients = new Dictionary<string, double>{{"Protein", 10}, {"Carbohydrates", 10}, {"Fat", 10}}, ServingSize = 0}
            };

            var expected = new List<double> { 23.5, 23.5, 52.9 };

            // act
            List<double> actual = new ChartServices().CalculateTotalEnergyData(days, products);

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CalculateTotalEnergyData_MultipleDays_OneItem()
        {
            // arrange 
            var foodItems1 = new List<FoodItem>
            {
                new FoodItem{Code = "1", Quantity = 1}
            };

            var foodItems2 = new List<FoodItem>
            {
                new FoodItem{Code = "1", Quantity = 1}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems1},
                new Day {Date = new DateTime(2015, 1, 20), Food = foodItems2}
            };

            var products = new List<Product>
            {
                new Product{Code = "1", Nutrients = new Dictionary<string, double>{{"Protein", 10}, {"Carbohydrates", 10}, {"Fat", 10}}, ServingSize = 100},
                new Product{Code = "2", Nutrients = new Dictionary<string, double>{{"Protein", 10}, {"Carbohydrates", 10}, {"Fat", 10}}, ServingSize = 0}
            };

            var expected = new List<double> { 23.5, 23.5, 52.9 };

            // act
            List<double> actual = new ChartServices().CalculateTotalEnergyData(days, products);

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CalculateTotalEnergyData_MultipleDays_MultipleItems()
        {
            // arrange 
            var foodItems1 = new List<FoodItem>
            {
                new FoodItem{Code = "1", Quantity = 1},
                new FoodItem{Code = "2", Quantity = 100}
            };

            var foodItems2 = new List<FoodItem>
            {
                new FoodItem{Code = "3", Quantity = 1},
                new FoodItem{Code = "4", Quantity = 100}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems1},
                new Day {Date = new DateTime(2015, 1, 20), Food = foodItems2}
            };

            var products = new List<Product>
            {
                new Product{Code = "1", Nutrients = new Dictionary<string, double>{{"Protein", 10}, {"Carbohydrates", 10}, {"Fat", 10}}, ServingSize = 100},
                new Product{Code = "2", Nutrients = new Dictionary<string, double>{{"Protein", 10}, {"Carbohydrates", 10}, {"Fat", 10}}, ServingSize = 0},
                new Product{Code = "3", Nutrients = new Dictionary<string, double>{{"Protein", 10}, {"Carbohydrates", 10}, {"Fat", 10}}, ServingSize = 100},
                new Product{Code = "4", Nutrients = new Dictionary<string, double>{{"Protein", 10}, {"Carbohydrates", 10}, {"Fat", 10}}, ServingSize = 0}
            };

            var expected = new List<double> { 23.5, 23.5, 52.9 };

            // act
            List<double> actual = new ChartServices().CalculateTotalEnergyData(days, products);

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CalculateAlcoholByProduct_OneDay_OneItem()
        {
            // arrange 
            var foodItems = new List<FoodItem>
            {
                new FoodItem{Code = "1", Quantity = 568}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems}
            };

            var products = new List<Product>
            {
                new Product{Code = "1", Nutrients = new Dictionary<string, double>{{"Alcohol", 4.2}}, ServingSize = 0}
            };

            var expected = new List<double> { 2.4, 2.4 };

            // act
            List<double> actual = new ChartServices().CalculateAlcoholByProduct(days, products);

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CalculateAlcoholByProduct_OneDay_MultipleItems()
        {
            // arrange 
            var foodItems = new List<FoodItem>
            {
                new FoodItem{Code = "1", Quantity = 568},
                new FoodItem{Code = "2", Quantity = 2}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems}
            };

            var products = new List<Product>
            {
                new Product{Code = "1", Nutrients = new Dictionary<string, double>{{"Alcohol", 4.2}}, ServingSize = 0},
                new Product{Code = "2", Nutrients = new Dictionary<string, double>{{"Alcohol", 4.2}}, ServingSize = 568}
            };

            var expected = new List<double> { 2.4, 4.8, 7.2 };

            // act
            List<double> actual = new ChartServices().CalculateAlcoholByProduct(days, products);

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CalculateAlcoholByProduct_MultipleDays_MultipleItems()
        {
            // arrange 
            var foodItems1 = new List<FoodItem>
            {
                new FoodItem{Code = "1", Quantity = 568},
                new FoodItem{Code = "2", Quantity = 1}
            };

            var foodItems2 = new List<FoodItem>
            {
                new FoodItem{Code = "3", Quantity = 284},
                new FoodItem{Code = "4", Quantity = 3}
            };

            var days = new List<Day>
            {
                new Day {Date = new DateTime(2015, 1, 19), Food = foodItems1},
                new Day {Date = new DateTime(2015, 1, 20), Food = foodItems2}
            };

            var products = new List<Product>
            {
                new Product{Code = "1", Nutrients = new Dictionary<string, double>{{"Alcohol", 4.2}}, ServingSize = 0},
                new Product{Code = "2", Nutrients = new Dictionary<string, double>{{"Alcohol", 4.2}}, ServingSize = 568},
                new Product{Code = "3", Nutrients = new Dictionary<string, double>{{"Alcohol", 4.2}}, ServingSize = 0},
                new Product{Code = "4", Nutrients = new Dictionary<string, double>{{"Alcohol", 4.2}}, ServingSize = 284}
            };

            var expected = new List<double> { 2.4, 2.4, 1.2, 3.6, 9.6 };

            // act
            List<double> actual = new ChartServices().CalculateAlcoholByProduct(days, products);

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }

    }

}
