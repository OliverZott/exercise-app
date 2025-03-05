namespace exercise_app.Views.Components;

public partial class TimeSpanPicker : ContentView
{
    public static readonly BindableProperty SelectedTimeSpanProperty =
        BindableProperty.Create(
            nameof(SelectedTimeSpan),
            typeof(TimeSpan),
            typeof(TimeSpanPicker),
            default(TimeSpan),
            BindingMode.TwoWay);

    public TimeSpan SelectedTimeSpan
    {
        get => (TimeSpan)GetValue(SelectedTimeSpanProperty);
        set => SetValue(SelectedTimeSpanProperty, value);
    }

    public TimeSpanPicker()
    {
        InitializeComponent();
        InitializePickers();

        HoursPicker.SelectedIndexChanged += OnPickerValueChanged;
        MinutesPicker.SelectedIndexChanged += OnPickerValueChanged;
    }

    public void InitializePickers()
    {
        for (int i = 0; i <= 23; i++) HoursPicker.Items.Add(i.ToString("00"));
        for (int i = 0; i <= 59; i++) MinutesPicker.Items.Add(i.ToString("00"));
    }

    private void OnPickerValueChanged(object sender, EventArgs e)
    {
        int hours = int.Parse(HoursPicker.SelectedItem?.ToString() ?? "0");
        int minutes = int.Parse(MinutesPicker.SelectedItem?.ToString() ?? "0");

        SelectedTimeSpan = new TimeSpan(hours, minutes, 0);
    }
}
