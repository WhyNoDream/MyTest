using Newtonsoft.Json;
using ProductDomain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace ProductDomain.Entitys
{
    public class ProductSku:Entity<long>
    {
        public ProductSku()
        {

        }
        public long ProductId { get; set; }
        public string SkuNo { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string SpecificationJson { get; set; }
        public List<Specification> Specification
        {
            get
            {
                try
                {
                    return JsonConvert.DeserializeObject<List<Specification>>(this.SpecificationJson);
                }
                catch (Exception ex)
                {
                    return new List<Specification>();
                }
            }
        }
        public string Remark { get; set; }
    }
}
