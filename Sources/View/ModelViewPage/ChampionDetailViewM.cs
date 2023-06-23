using System;
using CommunityToolkit.Mvvm.Input;
using View.Page;
using ViewModels;

namespace View.ModelViewPage
{
	public partial class ChampionDetailViewM
	{
        public ChampionDetailViewM(ChampionManagerVM manager, ChampionVm championVm)
        {
            ChampionVM = championVm;
            Manager = manager;
        }
        public ChampionVm ChampionVM { get; }
        private ChampionManagerVM Manager;

        [RelayCommand]
        private async void EditChampion()
        {
            await Shell.Current.Navigation.PushAsync(new AddChampionPage(new EditChampionViewM(Manager, new EditChampionVm(ChampionVM), ChampionVM)));
        }
    }
}

