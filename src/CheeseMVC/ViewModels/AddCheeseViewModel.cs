using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddCheeseViewModel
    {
        [Required]
        [Display(Name = "Cheese Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must give your cheese a description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        public IList<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        public AddCheeseViewModel() { }
        public AddCheeseViewModel(IList<CheeseCategory> types)
        {


            foreach (CheeseCategory type in types)
            {

               Categories.Add(new SelectListItem
                {
                    Value = type.ID.ToString(),
                    Text = type.Name.ToString()
                });
            }

            //CheeseTypes.Add(new SelectListItem
            //{
            //    Value = ((int)CheeseType.Soft).ToString(),
            //    Text = CheeseType.Soft.ToString()
            //});

            //CheeseTypes.Add(new SelectListItem
            //{
            //    Value = ((int)CheeseType.Fake).ToString(),
            //    Text = CheeseType.Fake.ToString()
            //});

        }
    }
}
