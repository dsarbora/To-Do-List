using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace ToDoList.Tests
{
    [TestClass]
    public class CategoryTest : IDisposable
    {
        public CategoryTest()
        {
        DBConfiguration.ConnectionString = "server=localhost;user id=root; password=root;port=8889;database=to_do_list_test;";
        }

        public void Dispose()
        {
            Category.ClearAll();
            Item.ClearAll();
        }
        // [TestMethod]
        // public void CategoryConstructor_CanCreateInstanceOfCategory_True()
        // {
        //     Category testCategory = new Category("test category");
        //     Assert.AreEqual(typeof(Category), testCategory.GetType());
        // }

        // [TestMethod]
        // public void GetName_ReturnsName_String()
        // {
        //     string testString = "test category";
        //     Category testCategory = new Category(testString);
        //     Assert.AreEqual (testString, testCategory.GetName());
        // }
        // // [TestMethod]
        // // public void GetId_ReturnsId_Int()
        // // {
        // //     Category testCategory = new Category("test");
        // //     Assert.AreEqual (1, testCategory.GetId());
        // // }
        // [TestMethod]
        // public void GetAll_ReturnsAllCategoryObjects_CategoryList()
        // {
        //     string name01 = "school";
        //     string name02 = "work";
        //     Category testCategory = new Category(name01);
        //     Category testCategory2 = new Category(name02);
        //     testCategory.Save();
        //     testCategory2.Save();
        //     List<Category> testList= new List<Category>(){testCategory, testCategory2};
        //     List<Category> result = Category.GetAll();
        //     CollectionAssert.AreEqual (result, testList);
        // }
        // [TestMethod]
        // public void Find_ReturnsCorrectCategoryFromDatabase_Category()
        // {
        //     string name1 = "Work";
        //     Category newCat1 = new Category(name1);
        //     newCat1.Save();
        //     Category foundCategory = Category.Find(newCat1.GetId());
        //     Assert.AreEqual (newCat1, foundCategory);
        // }

        // // [TestMethod]
        // // public void GetItems_ReturnsEmptyListItem_ItemList()
        // // {
        // //     string name="work";
        // //     Category newCategory = new Category(name);
        // //     List<Item> newList = new List<Item>{};
        // //     List<Item> result = newCategory.GetItems();
        // //     Assert.AreEqual (result, newList);
        // // }
        // [TestMethod]
        // public void GetItems_RetrievesAllItemsWithCategory_ItemList()
        // {
        //     Category testCategory = new Category("Household chores");
        //     testCategory.Save();
        //     Item testItem1 = new Item("Mow the lawn", testCategory.GetId());
        //     testItem1.Save();
        //     Item testItem2 = new Item("Clean the gutters", testCategory.GetId());
        //     testItem2.Save();
        //     List<Item> testList = new List<Item>{testItem1, testItem2};
        //     List<Item> result = testCategory.GetItems();
        //     CollectionAssert.AreEqual(testList, result);
        // }

        // [TestMethod]
        // public void GetAll_CategoriesEmptyAtFirst_List()
        // {
        //     int result=Category.GetAll().Count;
        //     Assert.AreEqual (0, result);
        // }
        // [TestMethod]
        // public void Equals_ReturnsTrueIfNamesAreTheSame_Category()
        // {
        //     Category firstCategory = new Category("Household chores");
        //     Category secondCategory = new Category("Household chores");
        //     Assert.AreEqual (firstCategory, secondCategory);
        // }
        // [TestMethod]
        // public void Save_SavesCategoryToDatabase_CategoryList()
        // {
        //     Category testCategory = new Category("Household chores");
        //     testCategory.Save();

        //     List<Category> result = Category.GetAll();
        //     List<Category> testList = new List<Category>{testCategory};

        //     CollectionAssert.AreEqual (testList, result);
        // }
        // [TestMethod]
        // public void Save_DatabaseAssignsIdToCategory_Id()
        // {
        //     Category testCategory = new Category("Household chores");
        //     testCategory.Save();
        //     Category savedCategory = Category.GetAll()[0];
        //     int result = savedCategory.GetId();
        //     int testId = testCategory.GetId();
        //     Assert.AreEqual(testId, result);
        // }
        [TestMethod]
        public void Delete_DeletesCategoryAssociationFromDatabase_CategoryList()
        {
            Item testItem = new Item("Walk the dog");
            testItem.Save();
            string testName="Home";
            Category testCategory = new Category(testName);
            testCategory.Save();
            testCategory.AddItem(testItem);
            testCategory.Delete();
            List<Category> resultItemCategories = new List<Category>{};
            List<Category> testItemCategories = testItem.GetCategories();
            CollectionAssert.AreEqual(resultItemCategories, testItemCategories);
        }

        [TestMethod]
        public void AddItem_AddsItemToCategory_ItemList()
        {
            Category testCategory = new Category("Home");
            testCategory.Save();
            Item testItem = new Item("Mow the lawn");
            testItem.Save();
            Item testItem2 = new Item("Water the garden");
            testItem2.Save();
            testCategory.AddItem(testItem);
            testCategory.AddItem(testItem2);
            List<Item> result = testCategory.GetItems();
            List<Item> testList = new List<Item>{testItem, testItem2};
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void GetItems_ReturnsAllCategory_ItemList()
        {
            Category testCategory = new Category("Household chores");
            testCategory.Save();
            Item testItem1 = new Item("Mow the lawn");
            testItem1.Save();
            Item testItem2 = new Item("Buy plane ticket");
            testItem2.Save();
            testCategory.AddItem(testItem1);
            List<Item> savedItems = testCategory.GetItems();
            List<Item> testList = new List<Item>{testItem1};
            CollectionAssert.AreEqual(testList, savedItems);
        }
    }
}