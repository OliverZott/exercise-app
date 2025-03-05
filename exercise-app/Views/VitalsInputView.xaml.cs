using exercise_app.ViewModels;

namespace exercise_app.Views;

public partial class VitalsInputView : ContentPage
{
    private readonly VitalsInputViewModel vitalsInputViewModel;

    public VitalsInputView(VitalsInputViewModel vitalsInputViewModel)
    {
        InitializeComponent();
        BindingContext = vitalsInputViewModel;
        this.vitalsInputViewModel = vitalsInputViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        vitalsInputViewModel.SelectedDate = DateTime.Today;
        vitalsInputViewModel.SelectedTime = DateTime.Now.TimeOfDay;
    }
}