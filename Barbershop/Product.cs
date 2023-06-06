using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbershop
{
    public class Product
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        //public List<Finance> FinanceEntities { get; set; }

        public string FullPathToImage => Environment.CurrentDirectory + "\\..\\..\\..\\" + Image;
    }
}
