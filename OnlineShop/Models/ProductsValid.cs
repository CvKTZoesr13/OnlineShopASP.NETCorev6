namespace OnlineShop.Models
{
    public class ProductsValid : IProductsValid
    {
        public bool CheckProductName(Products product)
        {
            if (product.Name.Length== 0 || product.Name.Trim(' ').Length== 0)
                return false;
            else return true;
        }
    }
}
