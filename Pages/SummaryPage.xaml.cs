namespace CollectionsBudny.Pages;

public partial class SummaryPage : ContentPage, IQueryAttributable
{
    public ViewModels.CollectionSummaryViewModel CollectionSummaryViewModel { get; set; }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("load"))
        {
            var temp = Models.Collection.Load(query["load"].ToString());
            CollectionSummaryViewModel = new ViewModels.CollectionSummaryViewModel(new ViewModels.CollectionViewModel(temp));
            BindingContext = this;
            OnPropertyChanged(nameof(CollectionSummaryViewModel));
            InvalidateMeasure();
            InitializeComponent();
        }
    }

    public SummaryPage()
	{
	    CollectionSummaryViewModel = new ViewModels.CollectionSummaryViewModel();
        BindingContext = this;
        
    }
}