using Newtonsoft.Json;

namespace Services
{
    public class CurrencyService
    {
        // Your API key is: SLfE6cGHMGS7GN7xtJhkxWC9AWky7N
        // Please note that our Free Plan requires you to display the following message prominently with a backlink:
        //Powered by<a href="https://www.amdoren.com">Amdoren</a>
        private class CurrencyResult
        {
            public int Error { get; set; }
            public string ErrorMessage { get; set; }
            public decimal Amount { get; set; }
        }

        public async Task ChangeVallet(string fromOne, string toAnother)
        {
            if (fromOne != "" && toAnother != "")
            {
                // ключ с сайта
                string apiKey = "SLfE6cGHMGS7GN7xtJhkxWC9AWky7N";
                //UAH MDL RUB USD EUR

                string apiUrl = $"https://www.amdoren.com/api/Currency.php?api_key={apiKey}&from={fromOne}&to={toAnother}";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        //Console.WriteLine(result);
                        CurrencyResult currencyResult = JsonConvert.DeserializeObject<CurrencyResult>(result);
                        decimal amount = currencyResult.Amount;
                        Console.WriteLine($"{fromOne} к {toAnother} = {amount}"); // ошибка 400, лимит: 10 запросов в день
                    }
                    else
                    {
                       Console.WriteLine($"Ошибка: {response.StatusCode}");                        
                    }
                }
            }
            else
            {
                Console.WriteLine($"Ошибка: Не введены данные");
            }
        }
    }
}