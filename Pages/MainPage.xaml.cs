using CollectionsBudny.ViewModels;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace CollectionsBudny.Pages
{
    public partial class MainPage : ContentPage, IQueryAttributable
    {
        public CollectionsViewModel CollectionsViewModel { get; set; }
        public ICommand BrowseItemsCommand { get; }
        public ICommand AddCollectionCommand { get; }

        public MainPage()
        {
            BindingContext = this;
            CollectionsViewModel = new CollectionsViewModel();
            BrowseItemsCommand = new AsyncRelayCommand(BrowseItemsAsync);
            AddCollectionCommand = new AsyncRelayCommand(AddCollectionAsync);
            InitializeComponent();
        }

        private async Task BrowseItemsAsync()
        {
            await Shell.Current.GoToAsync(nameof(Pages.AllItemsPage));
        }

        private async Task AddCollectionAsync()
        {
            await Shell.Current.GoToAsync(nameof(Pages.CollectionPage));
        }


        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("deleted"))
            {
                CollectionsViewModel.Reload();
                
                InvalidateMeasure();

            }
            else if (query.ContainsKey("saved"))
            {
                CollectionsViewModel.Reload();
                InvalidateMeasure();
            }
        }
    }

}
