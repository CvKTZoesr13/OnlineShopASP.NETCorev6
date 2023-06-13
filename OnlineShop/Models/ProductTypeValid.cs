namespace OnlineShop.Models
{
    public class ProductTypeValid : IProductTypeValid
    {
        public ProductTypeValid() { }
        public bool CheckProductTypeName(ProductTypes productTypes)
        {
            if(productTypes.ProductType.Length== 0 || productTypes.ProductType.Trim(' ').Length== 0)
            return false;
            else return true;
        }
    }
}
