using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using System.Collections.Generic;
using System;

namespace ToDoList.Tests
{
  [TestClass]
  public class ItemTest : IDisposable
  {

    public void Dispose()
    {
      Item.ClearAll();
    }

    public ItemTest()
    {
      DBConfiguration.ConnectionString = "server=localhost; user id=root; password=root; port=8889; database=to_do_list_test;";
    }

    [TestMethod]
    public void ItemConstructor_CreatesInstanceOfItem_Item()
    {
      Item newItem = new Item("test");
      Assert.AreEqual(typeof(Item), newItem.GetType());
    }

    [TestMethod]
    public void GetDescription_ReturnsDescription_String()
    {
      //Arrange
      string description = "Walk the dog.";
      Item newItem = new Item(description);

      //Act
      string result = newItem.GetDescription();

      //Assert
      Assert.AreEqual(description, result);
    }

    [TestMethod]
    public void SetDescription_SetDescription_String()
    {
      //Arrange
      string description = "Walk the dog.";
      Item newItem = new Item(description);

      //Act
      string updatedDescription = "Do the dishes";
      newItem.SetDescription(updatedDescription);
      string result = newItem.GetDescription();

      //Assert
      Assert.AreEqual(updatedDescription, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyList_ItemList()
    {
      //Arrange
      List<Item> newList = new List<Item> { };

      //Act
      List<Item> result = Item.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsItems_ItemList()
    {
      //Arrange
      string description01 = "Walk the dog";
      string description02 = "Wash the dishes";
      Item newItem1 = new Item(description01);
      newItem1.Save();
      Item newItem2 = new Item(description02);
      newItem2.Save();
      List<Item> newList = new List<Item> { newItem1, newItem2 };

      //Act
      List<Item> result = Item.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    // [TestMethod]
    // public void GetId_ItemsInstantiateWithAnIdAndGetterReturns_True()
    // {
    //   string description = "walk the dog";
    //   Item item = new Item(description);
    //   Assert.AreEqual (1, item.GetId());
    // }
    [TestMethod]
    public void Find_ReturnsCorrectItem_True()
    {
      string description1 = "walk the dog";
      Item item1 = new Item(description1);
      item1.Save();
      Item result = Item.Find(item1.GetId());
      Assert.AreEqual (result, item1);
    }
    [TestMethod]
    public void Equals_ReturnsTrueIfItemDescriptionsAreTheSame_Item()
    {
      Item firstItem = new Item("this item");
      Item secondItem = new Item("this item");

      Assert.AreEqual(firstItem, secondItem);
    }
    [TestMethod]
    public void Save_SavesToDatabase_ItemList()
    {
      Item testItem = new Item("Mow the lawn");
      testItem.Save();
      List<Item> result = Item.GetAll();
      List<Item> testList = new List<Item>{testItem};
      CollectionAssert.AreEqual (testList, result);
    }
    [TestMethod]
    public void Save_AssignsId_Id()
    {
      Item testItem = new Item("Mow the lawn");
      testItem.Save();
      Item savedItem = Item.GetAll()[0];
      int result = savedItem.GetId();
      int testId = testItem.GetId();

      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void Edit_UpdatesItem_String()
    {
      Item newItem = new Item("Walk the dog");
      newItem.Save();
      string newDescription = "Walk the cat";
      newItem.Edit(newDescription);
      string result = Item.Find(newItem.GetId()).GetDescription();
      Assert.AreEqual(newDescription, result);
    }
    // [TestMethod]
    // public void GetCategoryId_ReturnsParentCategoryId_Int()
    // {
    //   Category newCategory = new Category("Home tastks");
    //   Item newItem = new Item("Walk the dog", newCategory.GetId());
    //   int result = newItem.GetCategoryId();
    //   Assert.AreEqual (newCategory.GetId(), result);

    // }
    [TestMethod]
    public void GetCategories_ReturnsAllItemCategories_CategoryList()
    {
      Item testItem = new Item("Mow the lawn");
      testItem.Save();
      Category testCategory1 = new Category("Home");
      testCategory1.Save();
      Category testCategory2 = new Category("Work");
      testCategory2.Save();
      testItem.AddCategory(testCategory1);
      List<Category> result=new List<Category>{testCategory1};
      List<Category> testList = testItem.GetCategories();
      CollectionAssert.AreEqual(result, testList);
    }

    [TestMethod]
    public void AddCategory_AddsCategoryToItem_CategoryList()
    {
      Item testItem = new Item("Mow the lawn");
      testItem.Save();
      Category testCategory = new Category("Home");
      testCategory.Save();
      testItem.AddCategory(testCategory);
      List<Category> result = new List<Category>{testCategory};
      List<Category> testList = testItem.GetCategories();
      CollectionAssert.AreEqual(result, testList);
    }

    [TestMethod]
    public void Delete_DeletesItemAssociationsFromDatabase_ItemList()
    {
      Category testCategory = new Category("Home");
      testCategory.Save();
      string testDescription = "Walk the dog";
      Item testItem = new Item(testDescription);
      testItem.Save();
      testItem.AddCategory(testCategory);
      testItem.Delete();
      List<Item> result = new List<Item>{};
      List<Item> testList = testCategory.GetItems();
      CollectionAssert.AreEqual(result, testList);
    }

  }
}
