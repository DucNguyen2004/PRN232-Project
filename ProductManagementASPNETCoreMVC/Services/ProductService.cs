using BusinessObjects;
using Repositories;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository iProductRepository;

        public ProductService()
        {
            iProductRepository = new ProductRepository();
        }
        public void DeleteProduct(Product p)
        {
            iProductRepository.DeleteProduct(p);
        }

        public List<Product> FilterPrice(int start, int end)
        {
            return iProductRepository.FilterPrice(start, end);
        }

        public Product GetProductById(int id)
        {
            return iProductRepository.GetProductById(id);
        }

        public List<Product> GetProducts()
        {
            return iProductRepository.GetProducts();
        }

        public void SaveProduct(Product p)
        {
            iProductRepository.SaveProduct(p);
        }

        public void UpdateProduct(Product p)
        {
            iProductRepository.UpdateProduct(p);
        }
    }
}
