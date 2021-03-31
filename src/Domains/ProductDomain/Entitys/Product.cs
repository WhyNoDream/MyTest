using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace ProductDomain.Entitys
{
    public class Product  : AggregateRoot<long>
    {
        public string ProductNo { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Brand { get; set; }
        [NotMapped]
        public List<ProductSku> ProductSku { get; set; }
        public string Remark { get; set; }

    }
}
