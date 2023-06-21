using ViewModel;
using View.ModelViewPage;

namespace View.Page;

public partial class AddChampionPage : ContentPage
{
	public AddChampionPage(EditChampionViewM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}
