using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using System;
using System.Collections.Generic;

namespace ToDoList.Tests
{
    [TestClass]
    public class ItemTest : IDisposable
    {
        public void Dispose()
        {
            Item.ClearAll();
        }
        [TestMethod]
        public void Item_CanBeCreated_True()
        {
            Item testItem = new Item("test");
            Assert.AreEqual (typeof(Item), testItem.GetType());
        }
        [TestMethod]
        public void GetDescription_ReturnsDescription_String()
        {
            //Arrange
            string description = "Walk the dog.";
            Item testItem = new Item(description);
            //Act
            string result = testItem.GetDescription();
            //Assert
            Assert.AreEqual (description, result);
        }
        [TestMethod]
        public void SetDescription_SetsDescription_String()
        {
            //Arrange
            string description = "Walk the dog.";
            Item testItem = new Item(description);
            //Act
            string newDescription = "Fold the laundry.";
            testItem.SetDescription(newDescription);
            string result = testItem.GetDescription();
            //Assert
            Assert.AreEqual (newDescription, result);
        }
        [TestMethod]
        public void GetAll_ReturnsEmptyList_ItemList()
        {
            //Arrange
            List<Item> testList = new List<Item> {};
            //Act
            List<Item> result = Item.GetAll();
            foreach (Item thisItem in result)
            {
                Console.WriteLine("Output of empty list is " + thisItem.GetDescription());
            }
            //Assert
            CollectionAssert.AreEqual (testList, result);
        }
        [TestMethod]
        public void GetAll_ReturnsItems_ItemList()
        {
            //Arrange
            string description1 = "Walk the dog.";
            string description2 = "Fold the laundry";
            Item walkDog = new Item(description1);
            Item laundry = new Item(description2);
            List<Item> testList = new List<Item>{walkDog, laundry};
            //Act
            List<Item> result = Item.GetAll();
            foreach (Item thisItem in result)
            {
                Console.WriteLine("Output of filled list is " + thisItem.GetDescription());
            }
            //Assert
            CollectionAssert.AreEqual (testList, result);
        }
    }
}