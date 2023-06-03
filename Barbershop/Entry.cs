using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbershop
{
    public class Entry
    {
        [Key] public int Id { get; set; }
        public string DateTime { get; set; }
        public string Status { get; set; }
        [ForeignKey("ClientEntity")] public int ID_client { get; set; }
        [ForeignKey("EmployeeEntity")] public int ID_employee { get; set; }
        [ForeignKey("ServiceEntity")]  public int ID_service { get; set; }
        public Client ClientEntity { get; set; }
        public Employee EmployeeEntity { get; set; }
        public Service ServiceEntity { get; set; }
        public List<Finance> FinanceEntities { get; set; }
    }
}
