using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PerformanceTricks.List
{
    public class MoviesLoader
    {
        private const string FileName = "movies.json";

        public async Task<IList<Movie>> LoadMoviesAsync()
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream($"PerformanceTricks.List.UWP.{FileName}"))
            using (var reader = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<List<Movie>>(await reader.ReadToEndAsync());
            }
        }
    }
}
