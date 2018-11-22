using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IronBlock;
using Newtonsoft.Json;

namespace IronDemo.Blocks
{
    public class TempBlock : IBlock
    {
        public override object Evaluate(Context context)
        {   
            var city = this.Values.FirstOrDefault(x => x.Name == "CITY").Block.Evaluate(context);

            var url = $@"http://api.openweathermap.org/data/2.5/weather?q={city}&appid=6b9c876af988b75ae2dfe34e8b6249c9";

            dynamic result = HttpGet(url).Result;

            return result.main.temp.ToObject<double>();
        }

        static async Task<object> HttpGet(string url)
        {
            using (var request = new HttpRequestMessage()) 
            {
                request.RequestUri = new Uri(url);
                request.Method = HttpMethod.Get;

                using (var response = await new HttpClient().SendAsync(request))
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                    return JsonConvert.DeserializeObject<dynamic>(content);
                }
            }
        }
    } 



}