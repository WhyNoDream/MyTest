using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace ProductDomain.Entitys
{
    public class Product: AggregateRoot<long>
    {
        public  Product()
        {

        }
        public string ProductNo { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Brand { get; set; }
        public List<ProductSku> ProductSku { get; set; }
        public string Remark { get; set; }

    }
}
