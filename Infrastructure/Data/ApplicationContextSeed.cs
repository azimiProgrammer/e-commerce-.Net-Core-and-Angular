using Core.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext dbContext, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<ILogger<ApplicationContextSeed>>();
            try
            {
                //brands
                if(!dbContext.ProductBrands.Any())
                {
                    logger.LogInformation("start seed brands");
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/ProductBrands.json");

                    var brands = JsonConvert.DeserializeObject<List<ProductBrand>>(brandsData);

                    foreach (var brand in brands)
                        dbContext.ProductBrands.Add(brand);

                    await dbContext.SaveChangesAsync();
                    logger.LogInformation("seed brands is done");
                }

                //types
                if (!dbContext.ProductTypes.Any())
                {
                    logger.LogInformation("start seed types");
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/ProductTypes.json");

                    var types = JsonConvert.DeserializeObject<List<ProductType>>(typesData);

                    foreach (var type in types)
                        dbContext.ProductTypes.Add(type);

                    await dbContext.SaveChangesAsync();
                    logger.LogInformation("seed types is done");
                }

                //products
                if (!dbContext.Products.Any())
                {
                    logger.LogInformation("start seed products");
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/Products.json");

                    var products = JsonConvert.DeserializeObject<List<Product>>(productsData);

                    foreach (var product in products)
                        dbContext.Products.Add(product);

                    await dbContext.SaveChangesAsync();
                    logger.LogInformation("seed products is done");
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An error occured sedd data");
            }
        }
    }
}
