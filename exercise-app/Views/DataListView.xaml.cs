using exercise_app.ViewModels;

namespace exercise_app.Views;

public partial class DataListView : ContentPage
{
    private readonly DataListViewModel _listViewModel;

    public DataListView(DataListViewModel listViewModel)
    {
        InitializeComponent();
        BindingContext = listViewModel;
        _listViewModel = listViewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _listViewModel.GetDataList();
    }
}