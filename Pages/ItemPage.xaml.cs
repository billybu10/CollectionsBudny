using CollectionsBudny.Models;
using CollectionsBudny.ViewModels;
using System.Diagnostics;

namespace CollectionsBudny.Pages;

public partial class ItemPage : ContentPage, IQueryAttributable
{
    public ViewModels.ItemViewModel ItemViewModel { get; set; }
    public ViewModels.CollectionsViewModel CollectionsViewModel { get; set; }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("edit"))
        {
            ItemViewModel = new ViewModels.ItemViewModel(Models.Item.Load(query["edit"].ToString()));
            BindingContext = this;
            OnPropertyChanged(nameof(ItemViewModel));
            InvalidateMeasure();
        }
    }

    private async Task<FileResult> PickImage()
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Please select an image",
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                return result;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error picking image: {ex.Message}");
        }

        return null;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        ItemsViewModel a = new ItemsViewModel();
        if(a.AllItems.Any(x => x.Name == ItemViewModel.Name && x.ID != ItemViewModel.ID))
        {
            bool answer = await DisplayAlert("Item exists", "Item with this name exists, do you want to proceed?", "Yes", "No");
            if (answer)
            {
                ItemViewModel.SaveCommand.Execute(null);
            }
            else
            {
                return;
            }
        }
        else
        {
            ItemViewModel.SaveCommand.Execute(null);
        }
    }

    private async void ImagePickerButton_Clicked(object sender, EventArgs e)
    {
        FileResult? file = await PickImage();
        if (file != null)
        {
            ItemViewModel.Image = file.FullPath;
        }
        else
        {
            await DisplayAlert("Error", "Failed to pick image.", "OK");
        }
    }

    public ItemPage()
	{
        CollectionsViewModel = new ViewModels.CollectionsViewModel();
        ItemViewModel = new ViewModels.ItemViewModel();
        BindingContext = this;
        InitializeComponent();
	}
}