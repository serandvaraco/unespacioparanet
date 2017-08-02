using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace PerformanceTricks.Binding
{
    public class MainPageBinding : INotifyPropertyChanged
    {
        public MainPageBinding()
        {
            sw = new Stopwatch();
            sw.Start();
        }
        Stopwatch sw;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        public void RecopiledInformationCalculate()
        {
            sw.Stop();
            Debug.WriteLine($"Time Elapsed: {sw.ElapsedMilliseconds}");
        }

        #region Binding 's
        string _value1 = "Hello World!!";
        string _value2 = "Hello World!!";
        string _value3 = "Hello World!!";
        string _value4 = "Hello World!!";
        string _value5 = "Hello World!!";
        string _value6 = "Hello World!!";
        string _value7 = "Hello World!!";
        string _value8 = "Hello World!!";
        string _value9 = "Hello World!!";
        string _value10 = "Hello World!!";
        string _value11 = "Hello World!!";
        string _value12 = "Hello World!!";
        string _value13 = "Hello World!!";
        string _value14 = "Hello World!!";
        string _value15 = "Hello World!!";
        string _value16 = "Hello World!!";
        string _value17 = "Hello World!!";
        string _value18 = "Hello World!!";
        string _value19 = "Hello World!!";
        string _value20 = "Hello World!!";
        string _value21 = "Hello World!!";
        string _value22 = "Hello World!!";
        string _value23 = "Hello World!!";
        string _value24 = "Hello World!!";
        string _value25 = "Hello World!!";
        string _value26 = "Hello World!!";
        string _value27 = "Hello World!!";
        string _value28 = "Hello World!!";
        string _value29 = "Hello World!!";
        string _value30 = "Hello World!!";
        string _value31 = "Hello World!!";
        string _value32 = "Hello World!!";

        public string Value1
        {
            get { return _value1; }
            set
            {
                _value1 = value;
                OnPropertyChanged();
            }
        }

        public string Value2
        {
            get { return _value2; }
            set
            {
                _value2 = value;
                OnPropertyChanged();
            }
        }

        public string Value3
        {
            get { return _value3; }
            set
            {
                _value3 = value;
                OnPropertyChanged();
            }
        }

        public string Value4
        {
            get { return _value4; }
            set
            {
                _value4 = value;
                OnPropertyChanged();
            }
        }

        public string Value5
        {
            get { return _value5; }
            set
            {
                _value5 = value;
                OnPropertyChanged();
            }
        }

        public string Value6
        {
            get { return _value6; }
            set
            {
                _value6 = value;
                OnPropertyChanged();
            }
        }

        public string Value7
        {
            get { return _value7; }
            set
            {
                _value7 = value;
                OnPropertyChanged();
            }
        }

        public string Value8
        {
            get { return _value8; }
            set
            {
                _value8 = value;
                OnPropertyChanged();
            }
        }

        public string Value9
        {
            get { return _value9; }
            set
            {
                _value9 = value;
                OnPropertyChanged();
            }
        }

        public string Value10
        {
            get { return _value10; }
            set
            {
                _value10 = value;
                OnPropertyChanged();
            }
        }

        public string Value11
        {
            get { return _value11; }
            set
            {
                _value11 = value;
                OnPropertyChanged();
            }
        }

        public string Value12
        {
            get { return _value12; }
            set
            {
                _value12 = value;
                OnPropertyChanged();
            }
        }

        public string Value13
        {
            get { return _value13; }
            set
            {
                _value13 = value;
                OnPropertyChanged();
            }
        }

        public string Value14
        {
            get { return _value14; }
            set
            {
                _value14 = value;
                OnPropertyChanged();
            }
        }

        public string Value15
        {
            get { return _value15; }
            set
            {
                _value15 = value;
                OnPropertyChanged();
            }
        }

        public string Value16
        {
            get { return _value16; }
            set
            {
                _value16 = value;
                OnPropertyChanged();
            }
        }

        public string Value17
        {
            get { return _value17; }
            set
            {
                _value17 = value;
                OnPropertyChanged();
            }
        }

        public string Value18
        {
            get { return _value18; }
            set
            {
                _value18 = value;
                OnPropertyChanged();
            }
        }

        public string Value19
        {
            get { return _value19; }
            set
            {
                _value19 = value;
                OnPropertyChanged();
            }
        }

        public string Value20
        {
            get { return _value20; }
            set
            {
                _value20 = value;
                OnPropertyChanged();
            }
        }

        public string Value21
        {
            get { return _value21; }
            set
            {
                _value21 = value;
                OnPropertyChanged();
            }
        }

        public string Value22
        {
            get { return _value22; }
            set
            {
                _value22 = value;
                OnPropertyChanged();
            }
        }

        public string Value23
        {
            get { return _value23; }
            set
            {
                _value23 = value;
                OnPropertyChanged();
            }
        }

        public string Value24
        {
            get { return _value24; }
            set
            {
                _value24 = value;
                OnPropertyChanged();
            }
        }

        public string Value25
        {
            get { return _value25; }
            set
            {
                _value25 = value;
                OnPropertyChanged();
            }
        }

        public string Value26
        {
            get { return _value26; }
            set
            {
                _value26 = value;
                OnPropertyChanged();
            }
        }

        public string Value27
        {
            get { return _value27; }
            set
            {
                _value27 = value;
                OnPropertyChanged();
            }
        }

        public string Value28
        {
            get { return _value28; }
            set
            {
                _value28 = value;
                OnPropertyChanged();
            }
        }

        public string Value29
        {
            get { return _value29; }
            set
            {
                _value29 = value;
                OnPropertyChanged();
            }
        }

        public string Value30
        {
            get { return _value30; }
            set
            {
                _value30 = value;
                OnPropertyChanged();
            }
        }

        public string Value31
        {
            get { return _value31; }
            set
            {
                _value31 = value;
                OnPropertyChanged();
            }
        }

        public string Value32
        {
            get { return _value32; }
            set
            {
                _value32 = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
