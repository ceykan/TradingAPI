using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TradingAPI.Core
{
    public class CommonHelper
    {
        public static async Task<IEnumerable<string[]>> ReadFromFile(string filePath, bool headerExists)
        {
            var path = Path.Combine(AppContext.BaseDirectory
                , filePath);
            var linesAsync = await File.ReadAllLinesAsync(path);
            var lines = linesAsync
                      .Where(row => row.Length > 0)
                      .Select(i => Regex.Split(i, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"))
                      .ToList();
            if (headerExists) { lines = lines.Skip(1).ToList(); }

            return lines;
        }

        public static async Task<List<dynamic>> ReadFromUrl(string uri)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(uri))
                {
                    string resultContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(resultContent);
                    List<dynamic> list = result.ToList();
                    return list;
                }
            }
        }
    }

}
