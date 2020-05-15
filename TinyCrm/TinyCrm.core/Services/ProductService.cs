using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;
using TinyCrm.Core.Services.Options;

namespace TinyCrm
{
    class ProductService : IProductService
    {
        private TinyCrmDbContext context;

        public ProductService(TinyCrmDbContext contextByProgram)
        {
            context = contextByProgram;
        }

        public Product CreateProduct(
            CreateProductOptions options) {

            if (options == null)
            {
                return null;
            }

            var product = new Product() { 
                ProductId = options.ProductId,
                Description = options.Description,
                Name = options.Name,
                Category = options.Category,
                Price = options.Price                
            };

            context.Add(product);

            if (context.SaveChanges() > 0)
            {
                return product;
            }

            return null;
        }

        public IQueryable<Product> SearchProducts(
            SearchProductOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var query = context
                .Set<Product>()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.ProductId))
            {
                query = query.Where(prod => prod.ProductId == options.ProductId);
            }

            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                query = query.Where(prod => prod.Description == options.Description);
            }

            if (!string.IsNullOrWhiteSpace(options.Name))
            {
                query = query.Where(prod => prod.Name == options.Name);
            }

            if (options.PriceFrom != null)
            {
                query = query.Where(prod => prod.Price >= options.PriceFrom);
            }

            if (options.PriceTo != null)
            {
                query = query.Where(prod => prod.Price <= options.PriceTo);
            }

            if (options.Category != null)
            {
                query = query.Where(prod => prod.Category == options.Category);
            }

            query = query.Take(500);

            return query;
        }

        public bool UpdateProduct(
            UpdateProductOptions options)
        {
            if (options == null) {
                return false;
            }

            var product = SearchProducts(
                new SearchProductOptions(){ 
                   ProductId = options.ProductId
            }).SingleOrDefault();

            if (!string.IsNullOrWhiteSpace(options.Description)) 
            {
                product.Description = options.Description;
            }

            if (!string.IsNullOrWhiteSpace(options.Name))
            {
                product.Name = options.Name;
            }

            if (options.Price != null)
            {
                product.Price = options.Price;
            }
                        
            if (options.Category != null)
            {
                product.Category = options.Category;
            }

            if (context.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        public Product GetProductById(
            GetProductByIdOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var product = context
                .Set<Product>()
                .Where(prod => prod.ProductId == options.ProductId)
                .SingleOrDefault();
            if (product != null)
            {
                return product;
            }
            return null;
        }

    }
}
