using API.Data;
using Newtonsoft.Json;
using System.Text;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Utils.Implementations
{
    public class Gemini
    {
        
        public static string ObtenerRecomendacion(string prompt, ProjectlabContext db)
        {
            string API = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["GEMINI_API"];
            string API_KEY = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["GEMINI_API_KEY"];
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();

                List<Product> products = db.Products
                    .Where(p => p.IsActive == true)
                    .Include(p => p.ProductVariants)
                    .Include(p => p.Attributes)
                    .ToList();

                string productsText = ProductFormater(products);

                string fullPrompt = $"From this products:\n{productsText}\n\nbased on the following: \"{prompt}\"\n¿Can you recommend me the best product for my necesity? be concise and dont explain too much, recomend maximum 3 devices, dont show the specs of the devices, just a short sentence explaining why it is the best device for the client requirements and use as name only and only the name from the product+variant SubName";

                var body = new
                {
                    contents = new[]
                    {
                    new
                    {
                        parts = new[]
                        {
                            new { text = fullPrompt }
                        }
                    }
                }
                };

                string jsonBody = JsonConvert.SerializeObject(body);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = client.PostAsync($"{API}?key={API_KEY}", content).Result;

                if (!response.IsSuccessStatusCode)
                    return $"Error: {response.StatusCode}";

                string responseString = response.Content.ReadAsStringAsync().Result;
                dynamic json = JsonConvert.DeserializeObject(responseString);
                return json?.candidates?[0]?.content?.parts?[0]?.text ?? "No se pudo obtener una respuesta.";
            }
        }

        public static string ProductFormater(List<Product> products)
        {
            var sb = new StringBuilder();

            foreach (var product in products)
            {
                sb.AppendLine($"🧾 Product: {product.Name} (Brand: {product.Brand}, Family: {product.Family})");

                if (product.Attributes.Any())
                {
                    sb.AppendLine("  General Attributes:");
                    foreach (var attr in product.Attributes)
                    {
                        sb.AppendLine($"    - {attr.Field}: {attr.Value}");
                    }
                }

                foreach (var variante in product.ProductVariants)
                {
                    if (variante.IsActive != true) continue;

                    sb.AppendLine($"  🔹 Variant SubName: {variante.SubName} (SKU: {variante.Sku})");
                    sb.AppendLine($"    Price: ${variante.Price?.ToString("0.00") ?? "N/A"} | Stock: {variante.Stock}");

                    if (variante.Attributes.Any())
                    {
                        sb.AppendLine("    Atributos:");
                        foreach (var attr in variante.Attributes)
                        {
                            sb.AppendLine($"      - {attr.Field}: {attr.Value}");
                        }
                    }
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
