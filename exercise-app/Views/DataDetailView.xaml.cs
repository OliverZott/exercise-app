using exercise_app.ViewModels;

namespace exercise_app.Views;

public partial class DataDetailView : ContentPage
{
    public DataDetailView(DataDetailViewModel dataDetailViewModel)
    {
        InitializeComponent();
        BindingContext = dataDetailViewModel;
    }
}