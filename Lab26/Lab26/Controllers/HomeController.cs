using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab26.Models;

namespace Lab26.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
		[Authorize]
		public ActionResult Menu()
		{
			CoffeeShopEntities db = new CoffeeShopEntities();
			List<Item> items = db.Items.ToList();
			ViewBag.Items = items;
			return View();
		}

		public ActionResult MenuSorted(string column)
		{
			CoffeeShopEntities db = new CoffeeShopEntities();

			if (column == "Name")
			{
				ViewBag.Items = (from i in db.Items
								 orderby i.Name
								 select i).ToList();
			}
			else if (column == "Description")
			{
				ViewBag.Items = (from i in db.Items
								 orderby i.Description
								 select i).ToList();
			}
			else if (column == "Stock")
			{
				ViewBag.Items = (from i in db.Items
								 orderby i.Stock
								 select i).ToList();
			}
			else if (column == "Price")
			{
				ViewBag.Items = (from i in db.Items
								 orderby i.Price
								 select i).ToList();
			}
			return View("Menu");
		}
		public ActionResult Add(int? id)
		{
			CoffeeShopEntities db = new CoffeeShopEntities();
			if (Session["Cart"] == null)
			{

				List<Item> cart = new List<Item>();
				
				cart.Add((from i in db.Items
						  where i.ID == id
						  select i).Single());
				
				Session.Add("Cart", cart);
			}
			else
			{
				
				List<Item> cart = (List<Item>)(Session["Cart"]);
				
				cart.Add((from i in db.Items
						  where i.ID == id
						  select i).Single());
			}
			return View("Add");
		}
	}
}