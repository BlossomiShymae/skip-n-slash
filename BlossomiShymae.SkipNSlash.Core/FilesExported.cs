using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FastCache;

namespace BlossomiShymae.SkipNSlash.Core
{
    public static class FilesExported
    {
        public record Text(string TextData)
        {
           public List<string> Search(string query)
           {
                return Regex
                    .Matches(TextData, $"^.*{query}.*$", RegexOptions.Multiline)
                    .Select(m => m.Value)
                    .ToList();
           }
        }

        public static async Task<Text> GetAsync(string version, HttpClient httpClient)
        {
            if (Cached<Text>.TryGet(version, out var data))
                return data;
            
            var res = await httpClient.GetStringAsync($"https://raw.communitydragon.org/{version}/cdragon/files.exported.txt")
                ?? throw new NullReferenceException();
            var file = new Text(res);
            
            return data.Save(file, TimeSpan.FromHours(1));
        }
    }
}