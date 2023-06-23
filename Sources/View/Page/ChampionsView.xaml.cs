namespace View.Page;
using ModelViewPage;
using ViewModels;

public partial class ChampionsView : ContentPage
{
    public ChampionsViewM ChampionsViewModel { get; }
    public ChampionsView(ChampionsViewM vm)
	{
		InitializeComponent();
        BindingContext = vm;
        ChampionsViewModel = vm;
    }
}
