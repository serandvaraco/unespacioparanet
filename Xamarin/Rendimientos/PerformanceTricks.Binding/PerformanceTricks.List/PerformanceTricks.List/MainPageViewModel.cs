using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PerformanceTricks.List
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly MoviesLoader _moviesLoader = new MoviesLoader();

        private IList<Movie> _movies;
        public IList<Movie> Movies
        {
            get { return _movies; }
            set
            {
                _movies = value;
                OnPropertyChanged();
            }
        }

        Stopwatch sw; 
        public MainPageViewModel()
        {
            sw = new Stopwatch();
            sw.Start(); 
        }
        
        public async Task OnAppearingAsync()
        {
            Movies = await _moviesLoader.LoadMoviesAsync();
            sw.Stop();
            Debug.WriteLine($"Elapsed Time {sw.ElapsedMilliseconds} ms");
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        
    }
}
