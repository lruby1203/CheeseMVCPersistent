using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using CheeseMVC.Data;
using CheeseMVC.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CategoryController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<CheeseCategory> categories = context.Categories.ToList();

            return View(categories);
        }

        private readonly CheeseDbContext context;

        public CategoryController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Add()
        {
            AddCategoryViewModel newCategory = new AddCategoryViewModel();
            return View(newCategory);
        }

        [HttpPost]
        public IActionResult Add(AddCategoryViewModel addCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                CheeseCategory newCategory = new CheeseCategory()
                {
                    Name = addCategoryViewModel.Name
                };
                context.Categories.Add(newCategory);
                context.SaveChanges();
                return Redirect("/Category");
            }
            return View(addCategoryViewModel);
        }
    }
}
