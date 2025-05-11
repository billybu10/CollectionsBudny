using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsBudny.ViewModels
{
    public class CollectionSummaryViewModel
    {
        public CollectionViewModel CollectionViewModel { get; }
        public CollectionItemsViewModel CollectionItemsViewModel { get; private set; }

        public string Name
        {
            get => CollectionItemsViewModel.Collection.Name;
        }

        public int NumberOwned
        {
            get => CollectionItemsViewModel.Items.Count(c => c.State == "New" || c.State == "Used");
        }

        public int NumberForSale
        {
            get => CollectionItemsViewModel.Items.Count(c => c.State == "For Sale");
        }

        public int NumberSold
        {
            get => CollectionItemsViewModel.Items.Count(c => c.State == "Sold");
        }

        public CollectionSummaryViewModel(CollectionViewModel collectionViewModel)
        {
            this.CollectionViewModel = collectionViewModel;
            this.CollectionItemsViewModel = new CollectionItemsViewModel(collectionViewModel);
        }
        
        public CollectionSummaryViewModel()
        {
        }

        public void Reload()
        {
            this.CollectionItemsViewModel = new CollectionItemsViewModel(CollectionViewModel);
        }
    }
}
