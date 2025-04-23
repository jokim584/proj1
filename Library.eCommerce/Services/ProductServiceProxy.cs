using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;
using Library.eCommerce.DTO;
using Library.eCommerce.Models;
using Newtonsoft.Json;
using proj1.Models;

namespace Library.eCommerce.Services
{
    public class ProductServiceProxy
    {


         private ProductServiceProxy()
         {
            //var productPayload = WebRequestHandler().Get("\\Inventory");
            //Products = new List<Item?>();
            //Products = JsonConvert.DeserializeObject<List<Item>>(productPayload) ?? new List<Item?>();
            
            Products = new List<Item?>
            {
                new Item { Product = new ProductDTO { Id = 1, Name = "Product 1", Price = 1 }, Id = 1, Quantity = 1 },
                new Item { Product = new ProductDTO { Id = 2, Name = "Product 2", Price = 2 }, Id = 2, Quantity = 2 },
                new Item { Product = new ProductDTO { Id = 3, Name = "Product 3", Price = 3 }, Id = 3, Quantity = 3 },
              
            };
        }
        
        private int LastKey
        {
            get
            {
                if(!Products.Any()) //same as products.count==0
                {
                    return 0;
                }

                return Products.Select(p => p?.Id ?? 0).Max();
            }
        }
        private static ProductServiceProxy? instance; //? means it can be null
        private static object instanceLock = new object();
        public static ProductServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductServiceProxy();
                    }
                }

                return instance;    
            }
        }

        public List<Item?> Products { get; private set; }

        public Item AddOrUpdate(Item item)
        {
            if (item == null)
                return item;
            if (item.Id == 0)
            {
                item.Id = LastKey + 1;
                item.Product.Id = item.Id;
                //item.Product.Amount = (int)item.Quantity; 
                item.Product.Price = (decimal)item.Cost;
                Products.Add(item);
            }
            else
            {
                var existingItem = Products.FirstOrDefault(p => p.Id == item.Id);
               //added below
                existingItem.Product.Name = item.Product.Name;
                //existingItem.Product.Amount = (int)item.Quantity;
                existingItem.Product.Price = (decimal)item.Cost;
                existingItem.Quantity = item.Quantity;
               // added above bc the price and quantity wouldnt change*/
                var index = Products.IndexOf(existingItem);
                Products.RemoveAt(index);
                Products.Insert(index, new Item(item));
                
            }
                return item;
        }

        public Item? Delete(int id)
        {
            if(id==0)
            {
                return null;
            }
            Item? product = Products.FirstOrDefault(p => p.Id == id);
            Products.Remove(product);
            return product;
        }

        public Item PurchaseItem(Item item)
        {
            if(item.Id<=0||item==null)
            {
                return null; 
            }
            var itemToPurchase = GetById(item.Id);
            if (itemToPurchase != null)
            {
                itemToPurchase.Quantity--;
            }
            return itemToPurchase;
        }
        public Item? GetById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }   

    }
}
