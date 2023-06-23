using System;
using ViewModels;
using View.Page;
using Model;
using System.Windows.Input;
using static StubLib.StubData;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace View.ModelViewPage
{
	public partial class ChampionsViewM
	{
        public ChampionManagerVM championManagerVm { get; }
        

        public ChampionsViewM(ChampionManagerVM championManager)

        {
            championManagerVm = championManager;
        }

        [RelayCommand]
        private void PushToDetail(ChampionVm champion)
        {
            Shell.Current.Navigation.PushAsync(new DetailChampion(new ChampionDetailViewM(championManagerVm, champion)));
        }

        [RelayCommand]
        private void AddChampion()
        {
            Shell.Current.Navigation.PushAsync(page: new AddChampionPage(new EditChampionViewM(championManagerVm, new EditChampionVm(null), null)));
        }

        [RelayCommand]
        private async void EditChampion(ChampionVm championVM)
        {
            await Shell.Current.Navigation.PushAsync(new AddChampionPage(new EditChampionViewM(championManagerVm, new EditChampionVm(championVM), championVM)));
        }

    }
}

