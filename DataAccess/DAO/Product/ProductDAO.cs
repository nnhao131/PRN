using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccess.DAO.Product
{
    public class ProductDAO
    {   
        
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                }
                return instance;
            }
        }
        private int GenerateRandomId()
        {
            // Sử dụng thư viện System.Random để tạo số ngẫu nhiên
            Random random = new Random();
            return random.Next(10000, 99999); 
        }
        public async Task<List<Category>> GetAllCategories()
        {
            using (var context = new DbBookStoreContext())
            {
                return await context.Categories.ToListAsync();
            }
        }
        public async Task<BusinessObject.Models.Product> UpdateProduct(BusinessObject.Models.Product product)
        {
            try
                {
                using(var contex = new DbBookStoreContext())
                {

                    var Uproduct = contex.Products.Find(product.Id);
                    if (Uproduct != null)
                    {
                        Uproduct.ProductName = product.ProductName;
                        Uproduct.Description = product.Description;
                        Uproduct.Quantity = product.Quantity;
                        Uproduct.IdCategory = product.IdCategory;
                       
                    }
                    if(product.Image!= null )
                    {
                        Uproduct.Image =product.Image;
                    }
                    await contex.SaveChangesAsync();
                    return Uproduct;
                }
                return null;
                
                    
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> deleteProduct(int id)
        {
            try
            {
                using(var context = new DbBookStoreContext())
                {
                    var product = await context.Products.SingleOrDefaultAsync(c=>c.Id == id);
                    if (product != null)
                    {
                        context.Products.Remove(product);
                        await context.SaveChangesAsync();
                        return true;
                    }
                }
            }
            catch
            {
                throw;
            }
            return false;
        }
        public async Task<BusinessObject.Models.Product> CreateProduct(BusinessObject.Models.Product product)
        {
            try
            {
                using(var context = new DbBookStoreContext())
                {
                    product.Status = true;
                    product.Id = GenerateRandomId();
                    context.Products.Add(product);
                    await context.SaveChangesAsync();
                    return product;
                }
            }
            catch
            {
                throw;
            }
        }
       
        public async Task<List<ProductDTO>> getallProduct()
        {
            try
            {
                    using(var context = new DbBookStoreContext())
                {
                    ;
                    var products = await context.Products
                        .Include(p => p.IdCategoryNavigation).Where(c=>c.Status!=null)
                        .Select(p => new ProductDTO
                        {
                            Id = p.Id,
                            ProductName = p.ProductName,
                            Description = p.Description,
                            Price = p.Price,
                            Quantity = p.Quantity,
                            Image = p.Image,
                            CategoryName = p.IdCategoryNavigation.CategoryName


                        }).ToListAsync();
                    return  products;
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task<ProductDTO> GetProsutByID(int id)
        {
            try
            {
                using(var contex = new DbBookStoreContext())
                {
                    var products = await contex.Products.Include(p=>p.IdCategoryNavigation).SingleOrDefaultAsync(i => i.Id == id);
                    if(products is not null)
                    {
                        var productDTO = new ProductDTO
                        {
                            Id = products.Id,
                            ProductName = products.ProductName,
                            Description = products.Description,
                            Price = products.Price,
                            Image = products.Image,
                            Quantity = products.Quantity,
                            CategoryName =products.IdCategoryNavigation.CategoryName,
                            cateid =products.IdCategory
                        };
                        

                        return productDTO;
                    }
                    return null;
                }

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<PagedList<ProductDTO>> GetallProduct(string searchTerm ,int pageNumber, int pageSize,string sortOrder)
        {
            
            try
            {
                using (var context = new DbBookStoreContext())
                {

                        var products = context.Products
                        .Include(p => p.IdCategoryNavigation)
                        .Select(p => new ProductDTO
                        {
                            Id = p.Id,
                            ProductName = p.ProductName,
                            Description = p.Description,
                            Price = p.Price,
                            Quantity = p.Quantity,
                            Image = p.Image,
                            CategoryName = p.IdCategoryNavigation.CategoryName


                        });

                    if(!string.IsNullOrEmpty(searchTerm))
                      {
                        products = products.Where(p =>
                            p.ProductName.Contains(searchTerm) ||
                            p.CategoryName.Contains(searchTerm)
                        );
                    }
                    switch (sortOrder)
                    {
                        case "PriceLowToHigh":
                            products = products.OrderBy(p => p.Price);
                            break;

                        case "PriceHighToLow":
                            products = products.OrderByDescending(p => p.Price);
                            break;

                        default:
                            // Không thực hiện sắp xếp
                            break;
                    }
                    var totalCount = await products.CountAsync();
                    var items = await products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();


                    return new PagedList<ProductDTO>(items, totalCount, pageNumber, pageSize);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                
            }
        }
        public async Task<List<ProductDTO>> GetcateName(string nameCate)
        {
            try
            {
                using(var context = new DbBookStoreContext())
                {
                    var products = await context.Products
                 .Include(p => p.IdCategoryNavigation)
                 .Where(p => p.IdCategoryNavigation.CategoryName == nameCate)
                 .Take(6)
                 .Select(p => new ProductDTO
                 {
                     Id = p.Id,
                     ProductName = p.ProductName,
                     Description = p.Description,
                     Price = p.Price,
                     Quantity = p.Quantity,
                     Image = p.Image,
                     CategoryName = p.IdCategoryNavigation.CategoryName
                 })
                 .ToListAsync();

                    return products;
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<ProductDTO>> Hotdeals(int number)
        {
            try
            {
                using (var context = new DbBookStoreContext())
                {
                    var products = await context.Products.Include(c=>c.IdCategoryNavigation).OrderBy(p => p.Quantity).Take(number)
                        .Select(p => new ProductDTO
                        {
                            Id = p.Id,
                            ProductName = p.ProductName,
                            Description = p.Description,
                            Price = p.Price,
                            Quantity = p.Quantity,
                            Image = p.Image,
                            CategoryName = p.IdCategoryNavigation.CategoryName
                        })
                        
                        .ToListAsync();
                    return products;
                }
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<ProductDTO>> GetProductsSortedByPrice(string sortOrder)
        {
            try
            {
                using (var context = new DbBookStoreContext())
                {

                    var products = await context.Products
                        .Include(p => p.IdCategoryNavigation)
                        .Select(p => new ProductDTO
                        {
                            Id = p.Id,
                            ProductName = p.ProductName,
                            Description = p.Description,
                            Price = p.Price,
                            Quantity = p.Quantity,
                            Image = p.Image,
                            CategoryName = p.IdCategoryNavigation.CategoryName


                        })
                        .ToListAsync();

                    switch (sortOrder)
                    {
                        case "PriceLowToHigh":
                            return  products.OrderBy(p => p.Price).ToList();

                        case "PriceHighToLow":
                            return products.OrderByDescending(p => p.Price).ToList();

                        default:
                            return products;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
