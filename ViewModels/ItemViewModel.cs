using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CollectionsBudny.ViewModels
{
    public class ItemViewModel: ObservableObject, IQueryAttributable
    {
        private Models.Item _item;

        public string ID
        {
            get => _item.ID;

        }

        public string Name
        {
            get => _item.Name;
            set
            {
                if (_item.Name != value)
                {
                    _item.Name = value;
                    OnPropertyChanged();
                    CheckData();
                }
            }
        }

        public float Price
        {
            get => _item.Price;
            set
            {
                if (_item.Price != value)
                {
                    _item.Price = value;
                    OnPropertyChanged();
                    CheckData();
                }
            }
        }

        public string State
        {
            get => _item.State;
            set
            {
                if (_item.State != value)
                {
                    _item.State = value;
                    OnPropertyChanged();
                    CheckData();
                }
            }
        }

        public int Rating
        {
            get => _item.Rating;
            set
            {
                if (_item.Rating != value)
                {
                    _item.Rating = value;
                    OnPropertyChanged();
                    CheckData();
                }
            }
        }

        public string Image
        {
            get => _item.Image;
            set
            {
                if (_item.Image != value)
                {
                    _item.Image = value;
                    OnPropertyChanged();
                    CheckData();
                }
            }
        }

        public string Collection
        {
            get => _item.Collection;
            set
            {
                if (_item.Collection != value)
                {
                    if(!String.IsNullOrWhiteSpace(_item.Collection)) _item.Delete();
                    _item.Collection = value;
                    OnPropertyChanged();
                    CheckData();
                }
            }
        }

        public string Comment
        {
            get => _item.Comment;
            set
            {
                if (_item.Comment != value)
                {
                    _item.Comment = value;
                    OnPropertyChanged();
                    CheckData();
                }
            }
        }

        private string InternalIncorrectDataString = "Input data";
        public string IncorrectDataString
        {
            get => InternalIncorrectDataString;
            set
            {
                if (InternalIncorrectDataString != value)
                {
                    InternalIncorrectDataString = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool InternalIsAllOk = false;
        public bool IsAllOk
        {
            get => InternalIsAllOk;
            set
            {
                if (InternalIsAllOk != value)
                {
                    InternalIsAllOk = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsSold
        {
            get => _item.State == "Sold";
        }

        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        public ItemViewModel()
        {
            _item = new Models.Item();
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
            EditCommand = new AsyncRelayCommand(Edit);
        }

        public ItemViewModel(Models.Item item)
        {
            _item = item;
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
            EditCommand = new AsyncRelayCommand(Edit);
        }

        private void CheckData()
        {
            IncorrectDataString = "";
            IsAllOk = true;
            if (String.IsNullOrWhiteSpace(_item.Name) || _item.Name.Contains(';'))
            {
                IsAllOk = false;
                IncorrectDataString += "Incorrect Name \n";
            }

            if (_item.Price < 0)
            {
                IsAllOk = false;
                IncorrectDataString += "Incorrect Number \n";
            }

            if (String.IsNullOrWhiteSpace(_item.State) || _item.State.Contains(';'))
            {
                IsAllOk = false;
                IncorrectDataString += "Incorrect State \n";
            }

            if (_item.Rating < 0 || _item.Rating > 10)
            {
                IsAllOk = false;
                IncorrectDataString += "Incorrect Rating \n";
            }

            if (String.IsNullOrWhiteSpace(_item.Collection))
            {
                IsAllOk = false;
                IncorrectDataString += "Incorrect Collection \n";
            }

            if ((!String.IsNullOrWhiteSpace(_item.Image) && !File.Exists(_item.Image)) || _item.Image.Contains(';'))
            {
                IsAllOk = false;
                IncorrectDataString += "Incorrect Image \n";
            }

            if (_item.Comment.Contains(';'))
            {
                IsAllOk = false;
                IncorrectDataString += "Incorrect Comment\n";
            }

        }


        private async Task Save()
        {
            _item.Save();
            await Shell.Current.GoToAsync($"..?saved={_item.ID}");
        }

        private async Task Delete()
        {
            _item.Delete();
            await Shell.Current.GoToAsync($"..?deleted={_item.ID}");
        }

        private async Task Edit()
        {
            if (this.ID != null)
                await Shell.Current.GoToAsync($"{nameof(Pages.ItemPage)}?edit={this.ID}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("load"))
            {
                _item = Models.Item.Load(query["load"].ToString());
                RefreshProperties();
            }
        }

        public void Reload()
        {
            _item = Models.Item.Load(_item.ID);
            RefreshProperties();
        }

        private void RefreshProperties()
        {
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(State));
            OnPropertyChanged(nameof(Rating));
            OnPropertyChanged(nameof(Image));
            OnPropertyChanged(nameof(Collection));
            OnPropertyChanged(nameof(Comment));
        }


    }
}
