using System.Text.Json;
using System.Text;
using API.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;

namespace API.Utils.Implementations
{
    public class APIHelper
    {
        public static TransactionDTO PostApiData(string url, PaymentDTO payment)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var content = JsonConvert.SerializeObject(payment);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);

                    HttpResponseMessage response = httpClient.PostAsync(url, byteContent).GetAwaiter().GetResult();
                    response.EnsureSuccessStatusCode();

                    string responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    var result = System.Text.Json.JsonSerializer.Deserialize<TransactionDTO>(responseBody, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en POST: {ex.Message}");
                    return null;
                }
            }
        }
    }
}
