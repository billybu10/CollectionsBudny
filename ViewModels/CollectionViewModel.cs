using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Compatibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace CollectionsBudny.ViewModels
{
    public class CollectionViewModel : ObservableObject, IQueryAttributable
    {
        public override bool Equals(System.Object obj)
        {

            var CollectionVMToCompare = obj as CollectionViewModel;

            if (CollectionVMToCompare == null)
            {
                // If it is null then it is not equal to this instance.
                return false;
            }
            else
            {
                bool allEqual = this.ID == CollectionVMToCompare.ID && this.Name == CollectionVMToCompare.Name && this.Description == CollectionVMToCompare.Description;
                return allEqual;
            }
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode() ^ Name.GetHashCode() ^ Description.GetHashCode();
        }

        private Models.Collection _collection;

        public string ID
        {
            get => _collection.ID;

        }

        public string Name
        {
            get => _collection.Name;
            set
            {
                if (_collection.Name != value)
                {
                    _collection.Name = value;
                    OnPropertyChanged();
                    CheckData();
                }
            }
        }

        public string Description
        {
            get => _collection.Description;
            set
            {
                if (_collection.Description != value)
                {
                    _collection.Description = value;
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

        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand SummaryCommand { get; private set; }

        public CollectionViewModel()
        {
            _collection = new Models.Collection();
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
            EditCommand = new AsyncRelayCommand(Edit);
            SummaryCommand = new AsyncRelayCommand(Summary);
        }

        public CollectionViewModel(Models.Collection collection)
        {
            _collection = collection;
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
            EditCommand = new AsyncRelayCommand(Edit);
            SummaryCommand = new AsyncRelayCommand(Summary);
        }

        private void CheckData()
        {
            IncorrectDataString = "";
            IsAllOk = true;

            if (String.IsNullOrWhiteSpace(_collection.Name) || (new Regex(@"\s")).IsMatch(_collection.Name))
            {
                IsAllOk = false;
                IncorrectDataString += "Incorrect Name \n";
            }
            else if (Directory.GetFiles(FileSystem.AppDataDirectory, "*.collection.txt").Where(f => Path.GetFileNameWithoutExtension(f).Split(".").First() != ID).Select(file => File.ReadLines(file).First()).Any(x => x == _collection.Name))
            {
                IsAllOk = false;
                IncorrectDataString += "Collection already exists \n";
            }
        }

        private async Task Edit()
        {
            if (ID != null)
                await Shell.Current.GoToAsync($"{nameof(Pages.CollectionPage)}?load={ID}");
        }

        private async Task Save()
        {
            _collection.Save();
            await Shell.Current.GoToAsync($"..?saved={_collection.Name}");

        }

        private async Task Delete()
        {
            _collection.Delete();
            await Shell.Current.GoToAsync($"..?deleted={_collection.Name}");
        }

        private async Task Summary()
        {
            await Shell.Current.GoToAsync($"{nameof(Pages.SummaryPage)}?load={_collection.ID}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("load"))
            {
                _collection = Models.Collection.Load(query["load"].ToString());
                RefreshProperties();
            }
        }

        public void Reload()
        {
            _collection = Models.Collection.Load(_collection.Name);
            RefreshProperties();
        }

        private void RefreshProperties()
        {
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Description));
        }

    }
}
