using exercise_app.ViewModels;

namespace exercise_app.Views;

public partial class VitalsInputView : ContentPage
{
    public VitalsInputView(VitalsInputViewModel vitalsInputViewModel)
    {
        InitializeComponent();
        BindingContext = vitalsInputViewModel;
    }
}