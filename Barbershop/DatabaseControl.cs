using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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
                List<Service> _services = ctx.Service.ToList();
                _services.Insert(0, new Service { Category = "Мужская" });
                _services.Insert(1, new Service { Category = "Женская" });
                return _services;
                //return ctx.Service.Include(p => p.EntryEntities).ToList();
            }
        }
        public static List<Employee> GetEmployees()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Employee.ToList();
                //return ctx.Employee.Include(p => p.EntryEntities).ToList();
            }
        }
        public static List<Product> GetProducts()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                List<Product> _products = ctx.Product.ToList();
                _products.Insert(0, new Product { Category = "Все" });
                _products.Insert(1, new Product { Category = "Воск" });
                _products.Insert(2, new Product { Category = "Лак" });
                _products.Insert(3, new Product { Category = "Гель" });
                _products.Insert(4, new Product { Category = "Шампунь" });
                _products.Insert(5, new Product { Category = "Краска" });
                _products.Insert(6, new Product { Category = "Кондиционер" });
                return _products;
            }
        }
        public static List<Client> GetClients()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Client.ToList();
                //return ctx.Client.Include(p => p.EntryEntities).ToList();
            }
        }

        public static List<Entry> GetEntries()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                List<Entry> _entries = ctx.Entry.Include(p => p.ClientEntity).Include(p => p.EmployeeEntity).Include(p => p.ServiceEntity).ToList();
                _entries.Insert(0, new Entry { Status = "Согласование" });
                _entries.Insert(1, new Entry { Status = "Назначена" });
                _entries.Insert(2, new Entry { Status = "В процессе" });
                _entries.Insert(3, new Entry { Status = "Завершена" });
                _entries.Insert(4, new Entry { Status = "Отменена" });
                return _entries;
            }
        }

        public static List<Finance> GetFinances()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Finance.Include(p => p.EntryEntity).ToList();
            }
        }
        // УСЛУГИ (ДОБАЛВЕНИЕ, РЕДАКТИРОВАНИЕ, УДАЛЕНИЕ)
        public static void AddService(Service service)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Service.Add(service);
                ctx.SaveChanges();
            }
        }
        public static void UpdateService(Service service)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                Service _service = ctx.Service.FirstOrDefault(p => p.Id == service.Id);
                if (_service == null)
                {
                    return;
                }
                _service.Name = service.Name;
                _service.Category = service.Category;
                _service.Duration = service.Duration;
                _service.Price = service.Price;
                _service.Id = service.Id;

                ctx.SaveChanges();
            }
        }
        public static void RemoveService(Service service)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Service.Remove(service);
                ctx.SaveChanges();
            }
        }

        // ТОВАРЫ (ДОБАВЛЕНИЕ, РЕДАКТИРОВАНИЕ, УДАЛЕНИЕ)
        public static void AddProduct(Product product)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Product.Add(product);
                ctx.SaveChanges();
            }
        }
        public static void UpdateProduct(Product product)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                Product _product = ctx.Product.FirstOrDefault(p => p.Id == product.Id);

                if (_product == null)
                {
                    return;
                }
                _product.Name = product.Name;
                _product.Category = product.Category;
                _product.Description = product.Description;
                _product.Price = product.Price;
                _product.Id = product.Id;
                _product.Image = product.Image;

                ctx.SaveChanges();
            }
        }
        public static void RemoveProduct(Product product)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Product.Remove(product);
                ctx.SaveChanges();
            }
            if (product.Image != "Images/Products/noProductImage.png")
            {
                File.Delete(product.FullPathToImage);
            }
        }

        // КЛИЕНТЫ (ДОБАВЛЕНИЕ, РЕДАКТИРОВАНИЕ, УДАЛЕНИЕ)
        public static void AddClient(Client client)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Client.Add(client);
                ctx.SaveChanges();
            }
        }
        public static void UpdateClient(Client client)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                Client _client = ctx.Client.FirstOrDefault(p => p.Id == client.Id);

                if (_client == null)
                {
                    return;
                }
                _client.FirstName = client.FirstName;
                _client.LastName = client.LastName;
                _client.Phone = client.Phone;
                _client.Id = client.Id;

                ctx.SaveChanges();
            }
        }
        public static void RemoveClient(Client client)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Client.Remove(client);
                ctx.SaveChanges();
            }
        }

        // ЗАПИСИ(ДОБАВЛЕНИЕ, РЕДАКТИРОВАНИЕ, УДАЛЕНИЕ)
        public static void AddEntry(Entry entry)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Entry.Add(entry);
                ctx.SaveChanges();
            }
        }
        public static void UpdateEntry(Entry entry)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                Entry _entry = ctx.Entry.FirstOrDefault(p => p.Id == entry.Id);

                if (_entry == null)
                {
                    return;
                }
                _entry.DateTime = entry.DateTime;
                _entry.Status = entry.Status;
                _entry.ID_employee = entry.ID_employee;
                _entry.ID_client = entry.ID_client;
                _entry.ID_service = entry.ID_service;
                _entry.Id = entry.Id;
                ctx.SaveChanges();
            }
        }
        public static void RemoveEntry(Entry entry)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Entry.Remove(entry);
                ctx.SaveChanges();
            }
        }

        // ФИНАНСЫ(ДОБАЛВЕНИЕ, ПРЕДСТАВЛЕНИЕ)
        public static void AddFinance(Finance finance)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Finance.Add(finance);
                ctx.SaveChanges();
            }
        }
    }
}
