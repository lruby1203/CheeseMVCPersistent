using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using CheeseMVC.Data;
using CheeseMVC.ViewModels;
using Microsoft.EntityFrameworkCore;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Menu> menus = context.Menus.ToList();
            return View(menus);
        }
        private readonly CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Title = "New Menu";
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();
            return View(addMenuViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu
                {
                    Name = addMenuViewModel.Name
                };

                context.Menus.Add(newMenu);
                context.SaveChanges();

                return Redirect("/Menu/ViewMenu/" + newMenu.ID);
            }
            return View(addMenuViewModel);
        }

        [HttpGet]
        public IActionResult ViewMenu(int id)
        {
            Menu theMenu = context.Menus.Single(c => c.ID == id);
            List<CheeseMenu> items = context.CheeseMenus.Include(item => item.Cheese)
                .Where(cm => cm.MenuID == id).ToList();
            ViewMenuViewModel newViewMenuViewModel = new ViewMenuViewModel
            {
                Menu = theMenu,
                Items = items
            };

            return View(newViewMenuViewModel);

        }

        [HttpGet]
        public IActionResult AddItem(int id)
        {
            List<Cheese> cheeses = context.Cheeses.ToList();
            Menu theMenu = context.Menus.Single(c => c.ID == id);
            AddMenuItemViewModel addMenuItemViewModel = new AddMenuItemViewModel(theMenu, cheeses);
            return View(addMenuItemViewModel);

        }

        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
        {
            if (ModelState.IsValid)
            {
                IList<CheeseMenu> existingItems = context.CheeseMenus
                    .Where(cm => cm.CheeseID == addMenuItemViewModel.cheeseID)
                    .Where(cm => cm.MenuID == addMenuItemViewModel.menuID).ToList();
                if (existingItems.Count == 0)
                {
                    CheeseMenu cheeseMenu = new CheeseMenu
                    {
                        MenuID = addMenuItemViewModel.menuID,
                        CheeseID = addMenuItemViewModel.cheeseID
                    };
                    context.CheeseMenus.Add(cheeseMenu);
                    context.SaveChanges();
                    return Redirect("/Menu/ViewMenu/" + cheeseMenu.MenuID);
                }

            }
            return View(addMenuItemViewModel);
        }
    }
}
