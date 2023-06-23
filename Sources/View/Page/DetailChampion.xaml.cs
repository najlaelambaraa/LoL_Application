using View.ModelViewPage;
using ViewModels;
namespace View.Page;

public partial class DetailChampion : ContentPage
{
	public DetailChampion(ChampionDetailViewM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
