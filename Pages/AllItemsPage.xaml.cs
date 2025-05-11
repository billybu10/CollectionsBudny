using CollectionsBudny.ViewModels;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CollectionsBudny.Pages;

public partial class AllItemsPage : ContentPage, IQueryAttributable
{
    public CollectionsViewModel CollectionsViewModel { get; set; }
    public ObservableCollection<CollectionItemsViewModel> CollectionItemsViewModels { get; set; }
    public ObservableCollection<string> EmptyCollection { get; }
    public ICommand NewItemCommand { get; }

    public AllItemsPage()
	{
        BindingContext = this;
        CollectionsViewModel = new ViewModels.CollectionsViewModel();
        CollectionItemsViewModels = new ObservableCollection<CollectionItemsViewModel>();
        foreach (var collection in CollectionsViewModel.AllCollections)
        {
            var temp = new CollectionItemsViewModel(collection);
            CollectionItemsViewModels.Add(temp);
        }
        NewItemCommand = new AsyncRelayCommand(NewItemAsync);
        InitializeComponent();
        Routing.RegisterRoute(nameof(Pages.ItemPage), typeof(Pages.ItemPage));
    }

    private async Task NewItemAsync()
    {
        await Shell.Current.GoToAsync(nameof(Pages.ItemPage));
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("deleted"))
        {
            CollectionsViewModel.Reload();
            CollectionItemsViewModels.Clear();
            foreach (var collection in CollectionsViewModel.AllCollections)
            {
                CollectionItemsViewModels.Add(new CollectionItemsViewModel(collection));
            }
            InvalidateMeasure();

        }
        else if (query.ContainsKey("saved"))
        {
            CollectionsViewModel.Reload();
            CollectionItemsViewModels.Clear();
            foreach (var collection in CollectionsViewModel.AllCollections)
            {
                CollectionItemsViewModels.Add(new CollectionItemsViewModel(collection));
            }
            InvalidateMeasure();
        }
    }
}