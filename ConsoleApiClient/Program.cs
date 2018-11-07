using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ConsoleApiClient.Models;

namespace ConsoleApiClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient client=new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44323/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            
            HttpResponseMessage response = await client.GetAsync("api/products");
            if (response.IsSuccessStatusCode)
            {
                var products= await response.Content.ReadAsAsync<List<ProductModel>>();
                Console.WriteLine("|Product Id|Product Name|");

                foreach (var productModel in products)
                {
                    Console.WriteLine($"|{productModel.ProductId}|{productModel.ProductName}|");
                }
               
            }

            Console.WriteLine();

            response = await client.GetAsync("api/categories");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("|Category Id|Category Name|");

                var categoryModels = await response.Content.ReadAsAsync<List<CategoryModel>>();

                foreach (var cat in categoryModels)
                {
                    Console.WriteLine($"|{cat.Id}|{cat.Name}|");
                }

            }



            Console.ReadLine();            
        }
    }
}
