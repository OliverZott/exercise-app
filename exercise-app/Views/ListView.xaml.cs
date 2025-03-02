using exercise_app.ViewModels;

namespace exercise_app.Views;

public partial class ListView : ContentPage
{
    private readonly ListViewModel _listViewModel;

    public ListView(ListViewModel listViewModel)
    {
        InitializeComponent();
        BindingContext = listViewModel;
        _listViewModel = listViewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _listViewModel.GetExerciseList();
    }
}