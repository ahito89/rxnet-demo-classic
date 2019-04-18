using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace desktop_example.Pages
{
    public class Product
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Price { get; set; }
    }

    public static class ProductProvider
    {
        private static List<Product> _all;
        public static List<Product> All => _all ?? (_all = Load());

        private static List<Product> Load()
        {
            var productPath = Path.Combine(Environment.CurrentDirectory, "products.json");
            using (StreamReader r = new StreamReader(productPath))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Product>>(json);
            }
        }
    }
}
