using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using System.Collections.Generic;
using System;

namespace ToDoList.Tests
{
    [TestClass]
    public class CategoryTests : IDisposable
    {

        public void Dispose()
        {
            Category.ClearAll();
        }
        [TestMethod]
        public void CategoryConstructor_CanCreateInstanceOfCategory_True()
        {
            Category testCategory = new Category("test category");
            Assert.AreEqual(typeof(Category), testCategory.GetType());
        }

        [TestMethod]
        public void GetName_ReturnsName_String()
        {
            string testString = "test category";
            Category testCategory = new Category(testString);
            Assert.AreEqual (testString, testCategory.GetName());
        }
        [TestMethod]
        public void GetId_ReturnsId_Int()
        {
            Category testCategory = new Category("test");
            Assert.AreEqual (1, testCategory.GetId());
        }
        [TestMethod]
        public void GetAll_ReturnsAllCategoryObjects_CategoryList()
        {
            string name01 = "school";
            string name02 = "work";
            Category testCategory = new Category(name01);
            Category testCategory2 = new Category(name02);
            List<Category> testList= new List<Category>(){testCategory, testCategory2};
            List<Category> result = Category.GetAll();
            CollectionAssert.AreEqual (result, testList);
        }
        [TestMethod]
        public void Find_ReturnsCorrectCategory_Category()
        {
            string name1 = "Work";
            string name2 = "School";
            Category newCat1 = new Category(name1);
            Category newCat2 = new Category(name2);
            Category result = Category.Find(2);
            Assert.AreEqual (result, newCat2);
        }

        //[TestMethod]
        // public void GetItems_ReturnsEmptyListItem_ItemList()
        // {
        //     string name="work";
        //     Category newCategory = new Category(name);
        //     List<Item> newList = new List<Item>{};
        //     List<Item> result = newCategory.GetItems();
        //     Assert.AreEqual (result, newList);
        // }
        // [TestMethod]
        // public void AddItem_AssociatesItemWithCategory_ItemList()
        // {
        //     string description = "walk the dog";
        //     Item newItem = new Item(description);
        //     List<Item> newList = new List<Item>{newItem};
        //     string name ="Work";
        //     Category newCategory = new Category(name);
        //     newCategory.AddItem(newItem);
        //     List<Item> result = newCategory.GetItems();
        //     Assert.AreEqual (result, newList);
        // }
    }
}