﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbershop
{
    public class Service
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public List<Entry> EntryEntities { get; set; }
    }
}
