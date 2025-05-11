namespace CollectionsBudny
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Pages.AllItemsPage), typeof(Pages.AllItemsPage));
            Routing.RegisterRoute(nameof(Pages.CollectionPage), typeof(Pages.CollectionPage));
            Routing.RegisterRoute(nameof(Pages.SummaryPage), typeof(Pages.SummaryPage));

        }
    }
}
