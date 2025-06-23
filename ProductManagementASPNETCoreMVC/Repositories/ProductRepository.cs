using BusinessObjects;
using DataAccessObjects;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        public void DeleteProduct(Product p)
        {
            ProductDAO.DeleteProduct(p);
        }

        public List<Product> FilterPrice(int start, int end)
        {
            return ProductDAO.FilterPrice(start, end);
        }

        public Product GetProductById(int id)
        {
            return ProductDAO.GetProductById(id);
        }

        public List<Product> GetProducts()
        {
            return ProductDAO.GetProducts();
        }

        public void SaveProduct(Product p)
        {
            ProductDAO.SaveProduct(p);
        }

        public void UpdateProduct(Product p)
        {
            ProductDAO.UpdateProduct(p);
        }
    }
}
