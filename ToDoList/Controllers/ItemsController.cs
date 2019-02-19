using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic; //Dave

namespace ToDoList.Controllers //Dave
{
    public class ItemsController : Controller //Dave
    {
        
        [HttpGet("/items")]
        public ActionResult Index()
        {
            List<Item> allItems = Item.GetAll();  //Dave
            return View(allItems);
        }
        
        [HttpGet("/items/new")]
        public ActionResult CreateForm() //Dave
        {
            return View();
        }

        [HttpPost("/items")]
        public ActionResult Create(string description)
        {
            Item myItem = new Item(description); //Dave
            return RedirectToAction("Index");
        }
        
    }
}


//DAVE