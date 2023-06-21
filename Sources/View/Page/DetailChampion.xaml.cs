using View.ModelViewPage;
using ViewModel;
namespace View.Page;

public partial class DetailChampion : ContentPage
{
	public DetailChampion(ChampionDetailViewM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
