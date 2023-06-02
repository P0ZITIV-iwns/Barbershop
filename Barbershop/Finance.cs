using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbershop
{
    public class Finance
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public int ID_entry { get; set; }
        public int ID_product { get; set; }
        public Entry EntryEntity { get; set; }
        public Product ProductEntity { get; set; }

    }
}

