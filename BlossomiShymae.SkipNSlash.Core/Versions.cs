using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BlossomiShymae.SkipNSlash.Core.Models;

namespace BlossomiShymae.SkipNSlash.Core
{
    public static partial class Versions
    {
        public static async Task<IEnumerable<string>> GetAllAsync(HttpClient httpClient)
        {
            var res = await httpClient.GetFromJsonAsync<List<JsonFile>>("https://raw.communitydragon.org/json/")
                ?? throw new NullReferenceException();
            return res
                .Select(file => file.Name)
                .Where(version => version.Any(char.IsDigit)
                || version == "latest"
                || version == "pbe")
                .OrderBy(x => MyRegex().Replace(x, match => match.Value.PadLeft(10, '0')));
        }

        [GeneratedRegex("[0-9]+")]
        private static partial Regex MyRegex();
    }
}