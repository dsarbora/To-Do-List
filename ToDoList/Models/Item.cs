using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ToDoList.Models
{
  public class Item
  {
    private string _description;
    private int _id;


    public Item (string description, int id=0)
    {
      _description = description;
      _id = id;
      }

    public string GetDescription()
    {
      return _description;
    }

    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }

    public static List<Item> GetAll()
    {
      List<Item> allItems = new List<Item>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        Item newItem = new Item(itemDescription, itemId);
        allItems.Add(newItem);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allItems;
    }

    public static void ClearAll()
    {
       MySqlConnection conn = DB.Connection();
       conn.Open();
       var cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText=@"DELETE FROM items;";
       cmd.ExecuteNonQuery();
       conn.Close();
       if(conn!=null)
       {
         conn.Dispose();
       }
    }


    public int GetId()
    {
      return _id;
    }

     public static Item Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `items` WHERE id =@thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value=id;
      cmd.Parameters.Add(thisId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int itemId = 0;
      string description = "";
      while(rdr.Read())
      {
        itemId = rdr.GetInt32(0);
        description = rdr.GetString(1);
      }
      Item foundItem = new Item(description, itemId);

      conn.Close();
      if (conn!=null)
      {
        conn.Dispose();
      }
      return foundItem;
    }

    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Item))
      {
        return false;
      }
      else
      {
        Item newItem = (Item)otherItem;
        bool idEquality = (this.GetId()==newItem.GetId());
        bool descriptionEquality=(this.GetDescription()==newItem.GetDescription());
        return (descriptionEquality && idEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO items (description) VALUES (@ItemDescription);";
      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@ItemDescription";
      description.Value=this._description;
      cmd.Parameters.Add(description);
      cmd.ExecuteNonQuery();
      _id = (int)cmd.LastInsertedId;

      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
    }

    public void Edit(string newDescription)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE items SET description=@newDescription WHERE id=@searchId;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName="@searchId";
      searchId.Value=_id;
      cmd.Parameters.Add(searchId);
      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@newDescription";
      description.Value=newDescription;
      cmd.Parameters.Add(description);
      cmd.ExecuteNonQuery();
      _description=newDescription;

      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }

    }

    public List<Category> GetCategories()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT category_id FROM categories_items WHERE item_id=@Item_Id;";
      MySqlParameter itemIdParameter = new MySqlParameter();
      itemIdParameter.ParameterName = "@Item_Id";
      itemIdParameter.Value = _id;
      cmd.Parameters.Add(itemIdParameter);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<int> categoryIds = new List<int>{};
      while(rdr.Read())
      {
        int categoryId=rdr.GetInt32(0);
        categoryIds.Add(categoryId);
      }
      rdr.Dispose();
      List<Category> categories = new List<Category>{};
      foreach(int categoryId in categoryIds)
      {
        var categoryQuery=conn.CreateCommand() as MySqlCommand;
        categoryQuery.CommandText = @"SELECT * FROM categories WHERE id=@CategoryId;";
        MySqlParameter categoryIdParameter = new MySqlParameter();
        categoryIdParameter.ParameterName = "@CategoryId";
        categoryIdParameter.Value = categoryId;
        categoryQuery.Parameters.Add(categoryIdParameter);
        var categoryQueryRdr = categoryQuery.ExecuteReader() as MySqlDataReader;
        while(categoryQueryRdr.Read())
        {
          int thisCategoryId=categoryQueryRdr.GetInt32(0);
          string categoryName=categoryQueryRdr.GetString(1);
          Category foundCategory = new Category(categoryName, thisCategoryId);
          categories.Add(foundCategory);
        }
        categoryQueryRdr.Dispose();
      }
      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
      return categories;
    }

    public void AddCategory(Category newCategory)
    {
      MySqlConnection conn=DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText=@"INSERT INTO categories_items (category_id, item_id) VALUES (@Category_Id, @Item_Id);";
      MySqlParameter category_id = new MySqlParameter();
      category_id.ParameterName="@Category_Id";
      category_id.Value = newCategory.GetId();
      cmd.Parameters.Add(category_id);
      MySqlParameter item_id = new MySqlParameter();
      item_id.ParameterName="@Item_Id";
      item_id.Value=_id;
      cmd.Parameters.Add(item_id);
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd=conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items WHERE id=@ItemId; DELETE FROM categories_items WHERE item_id=@ItemId;";
      MySqlParameter itemIdParameter = new MySqlParameter();
      itemIdParameter.ParameterName = "@ItemId";
      itemIdParameter.Value=_id;
      cmd.Parameters.Add(itemIdParameter);
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
    }

  }
}
