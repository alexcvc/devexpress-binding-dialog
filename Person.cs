using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevExpressBindingDialog
{
    public class Person : INotifyPropertyChanged, ICloneable
    {
        private string _name = string.Empty;
        private int _age;
        private DateTime _startDate = DateTime.Today;
        private int? _countryId;
        private bool _isActive;

        [Required, StringLength(100)]
        public string Name { get => _name; set { if (_name != value) { _name = value; OnPropertyChanged(nameof(Name)); } } }

        [Range(0, 150)]
        public int Age { get => _age; set { if (_age != value) { _age = value; OnPropertyChanged(nameof(Age)); } } }

        public DateTime StartDate { get => _startDate; set { if (_startDate != value) { _startDate = value; OnPropertyChanged(nameof(StartDate)); } } }

        public int? CountryId { get => _countryId; set { if (_countryId != value) { _countryId = value; OnPropertyChanged(nameof(CountryId)); } } }

        public bool IsActive { get => _isActive; set { if (_isActive != value) { _isActive = value; OnPropertyChanged(nameof(IsActive)); } } }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public object Clone() => MemberwiseClone();

        public void CopyFrom(Person other)
        {
            Name = other.Name;
            Age = other.Age;
            StartDate = other.StartDate;
            CountryId = other.CountryId;
            IsActive = other.IsActive;
        }

        public override string ToString()
            => $"Name={Name}, Age={Age}, StartDate={StartDate:d}, CountryId={CountryId}, IsActive={IsActive}";
    }
}
