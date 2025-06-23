using BusinessObjects;

namespace Services
{
    public interface IProductService
    {
        void SaveProduct(Product p);
        void DeleteProduct(Product p);
        void UpdateProduct(Product p);
        List<Product> GetProducts();
        Product GetProductById(int id);
        List<Product> FilterPrice(int start, int end);
    }
}
