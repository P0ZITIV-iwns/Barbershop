using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbershop
{
    public static class DatabaseControl
    {
        public static List<Service> GetServices()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Service.ToList();
            }
        }
        public static List<Employee> GetEmployees()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Employee.ToList();
            }
        }
        public static List<Product> GetProducts()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                List<Product> _products = ctx.Product.ToList();
                _products.Insert(0, new Product { Category = "Все" });
                return _products;
            }
        }
        public static List<Client> GetClients()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Client.ToList();
            }
        }

        public static List<Entry> GetEntries()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Entry.Include(p => p.ClientEntity).Include(p => p.EmployeeEntity).Include(p => p.ServiceEntity).ToList();
            }
        }

        public static List<Finance> GetFinances()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Finance.Include(p => p.EntryEntity).Include(p => p.ProductEntity).ToList();
            }
        }
    }
}
