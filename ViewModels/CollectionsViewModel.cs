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
    public class CollectionsViewModel : IQueryAttributable
    {
        public ObservableCollection<ViewModels.CollectionViewModel> AllCollections { get; }
        public ICommand NewCommand { get; }

        public CollectionsViewModel()
        {
            AllCollections = new ObservableCollection<CollectionViewModel>(Models.Collection.LoadAll().Select(n => new CollectionViewModel(n)));
            NewCommand = new AsyncRelayCommand(NewCollectionAsync);
        }

        public void Reload()
        {
            AllCollections.Clear();
            foreach (var collectionViewModel in Models.Collection.LoadAll().Select(n => new CollectionViewModel(n))) AllCollections.Add(collectionViewModel);
        }

        private async Task NewCollectionAsync()
        {
            await Shell.Current.GoToAsync(nameof(Pages.CollectionPage));
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("deleted"))
            {
                string collectionName = query["deleted"].ToString();
                CollectionViewModel matchedCollection = AllCollections.Where((n) => n.ID == collectionName).FirstOrDefault();

                if (matchedCollection != null)
                    AllCollections.Remove(matchedCollection);
            }
            else if (query.ContainsKey("saved"))
            {
                string collectionName = query["saved"].ToString();
                CollectionViewModel matchedCollection = AllCollections.Where((n) => n.ID == collectionName).FirstOrDefault();


                if (matchedCollection != null)
                {
                    matchedCollection.Reload();
                }

                else
                    AllCollections.Insert(0, new CollectionViewModel(Models.Collection.Load(collectionName)));
            }
        }
    }
}
