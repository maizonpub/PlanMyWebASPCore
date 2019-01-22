using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooCommerceNET.WooCommerce.v2;

namespace ImportFromWordPress
{
    public class WoocommerceService
    {
        private readonly WooCommerceNET.WooCommerce.v2.WCObject wc;
        public WoocommerceService()
        {
            WooCommerceNET.RestAPI rest = new WooCommerceNET.RestAPI(Statics.WooApi, Statics.ConsumerKey, Statics.ConsumerSecret);
            wc = new WooCommerceNET.WooCommerce.v2.WCObject(rest);
        }
        public async Task<List<Product>> GetProductsByCategory(string id)
        {
            var p = wc.Product;
            var dic = new Dictionary<string, string>();
            dic.Add("category", id);
            dic.Add("per_page", "100");
            var products = await p.GetAll(dic);
            return products;
        }
        public async Task<ProductCategory> GetProductCategories(string search)
        {
            var p = wc.Category;
            var dic = new Dictionary<string, string>();
            dic.Add("search", search);
            dic.Add("per_page", "100");
            var products = await p.GetAll(dic);
            return products.FirstOrDefault();
        }
    }
}
