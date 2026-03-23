using System.ComponentModel;
using System.Runtime.CompilerServices;
using CaisseClaire.Execution.Entities;

namespace CaisseClaire.Directive.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private readonly ProductEntity _entity;
        private bool _isMarkedForDeletion;

        public ProductViewModel(ProductEntity entity)
        {
            _entity = entity;
        }

        public string Code 
        { 
            get => _entity.Code; 
            set { _entity.Code = value; OnPropertyChanged(); } 
        }

        public string Name 
        { 
            get => _entity.Name; 
            set { _entity.Name = value; OnPropertyChanged(); } 
        }

        public decimal Price 
        { 
            get => _entity.Price; 
            set { _entity.Price = value; OnPropertyChanged(); } 
        }

        public bool IsMarkedForDeletion
        {
            get => _isMarkedForDeletion;
            set { _isMarkedForDeletion = value; OnPropertyChanged(); }
        }

        public ProductEntity GetEntity() => _entity;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
