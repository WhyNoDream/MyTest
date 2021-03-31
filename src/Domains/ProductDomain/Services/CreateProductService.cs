using ProductDomain.Entitys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace ProductDomain.Services
{
    public class CreateProductService: DomainService
    {
        private readonly IRepository<Product, long> _productRepository;
        public CreateProductService(IRepository<Product, long> productRepository) //注入默认仓储
        {
            _productRepository = productRepository;
        }

        public async Task CreateProduct(string productName)
        {
            Check.NotNullOrWhiteSpace(productName, nameof(productName));

            var book = new Product
            {
                Name = productName
            };

            await _productRepository.InsertAsync(book); //使用仓储提供的标准方法

        }

    }
}
