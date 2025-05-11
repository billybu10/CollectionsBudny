using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CollectionsBudny.ViewModels
{
    public class ItemsViewModel : IQueryAttributable
    {
        public ObservableCollection<ViewModels.ItemViewModel> AllItems { get; }
        public ICommand NewCommand { get; }

        public ItemsViewModel()
        {
            AllItems = new ObservableCollection<ItemViewModel>(Models.Item.LoadAll().Select(n => new ItemViewModel(n)));
            NewCommand = new AsyncRelayCommand(NewItemAsync);
        }

        private async Task NewItemAsync()
        {
            await Shell.Current.GoToAsync(nameof(Pages.ItemPage));
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("deleted"))
            {
                string itemId = query["deleted"].ToString();
                ItemViewModel matchedItem = AllItems.Where((n) => n.ID == itemId).FirstOrDefault();

                if (matchedItem != null)
                    AllItems.Remove(matchedItem);
            }
            else if (query.ContainsKey("saved"))
            {
                string itemId = query["saved"].ToString();
                ItemViewModel matchedItem = AllItems.Where((n) => n.ID == itemId).FirstOrDefault();


                if (matchedItem != null)
                {
                    matchedItem.Reload();
                }

                else
                    AllItems.Insert(0, new ItemViewModel(Models.Item.Load(itemId)));
            }
        }
    }
}
