namespace View.Page;
using ModelViewPage;
using ViewModel;

public partial class ChampionsView : ContentPage
{
	public ChampionsView(ChampionsViewM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}
