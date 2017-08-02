using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace PerformanceTricks.Binding
{
    public class BindingModel : INotifyPropertyChanged
    {

        public BindingModel()
        {
            sw = new Stopwatch();
            sw.Start(); 

        }
        public void RecopiledInformationCalculate()
        {
            sw.Stop();
            Debug.WriteLine($"Time Elapsed: {sw.ElapsedMilliseconds}");
        }

        volatile int called = 0;
        public event PropertyChangedEventHandler PropertyChanged;
        Stopwatch sw;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            called ++; 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _welcome;
        public string Welcome
        {
            get
            {
                return _welcome;
            }
            set
            {
                _welcome = value;
                OnPropertyChanged();
            }
        }

    }
}
