using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsBudny.ViewModels
{
    public class CollectionItemsViewModel
    {
        public ObservableCollection<ViewModels.ItemViewModel> Items { get; set; } = [];
        public ViewModels.CollectionViewModel Collection { get; private set; }

        public CollectionItemsViewModel(CollectionViewModel Collection)
        {
            this.Collection = Collection;
            Items = new ObservableCollection<ItemViewModel>((new ItemsViewModel()).AllItems.Where(n => n.Collection == Collection.ID).OrderBy(item => item.State == "Sold").ThenBy(item => item.Name));
        }

        public CollectionItemsViewModel()
        {
        }

        public void Reload()
        {
            Items.Clear();
            Items = new ObservableCollection<ItemViewModel>((new ItemsViewModel()).AllItems.Where(n => n.Collection == Collection.ID).OrderBy(item => item.State == "Sold").ThenBy(item => item.Name));

        }
    }
}
