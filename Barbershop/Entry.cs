using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbershop
{
    public class Entry
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
        public int ID_client { get; set; }
        public int ID_employee { get; set; }
        public int ID_service { get; set; }
        public Client ClientEntity { get; set; }
        public Employee EmployeeEntity { get; set; }
        public Service ServiceEntity { get; set; }
        public List<Finance> FinanceEntities { get; set; }
    }
}
