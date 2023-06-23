using System;
using ViewModels;
using View.Page;
using Model;
using System.Windows.Input;
using static StubLib.StubData;
using System.Collections.ObjectModel;

namespace View.ModelViewPage
{
	public class ChampionsViewM
	{
        public Command EditChampionCommand { get; }
        public ChampionManagerVM championManagerVm { get; }
        

        public ChampionsViewM(ChampionManagerVM championManager)

        {
            championManagerVm = championManager;
            PushToDetailCommand = new Command<ChampionVm>(PushToDetail);
            
            AddChampionCommand = new Command(Addchampion);
            EditChampionCommand = new Command<ChampionVm>(EditChampion);

        }
        

        public Command PushToDetailCommand { get; }
        public Command AddChampionCommand { get; }

        private void PushToDetail(ChampionVm champion)
        {
            Shell.Current.Navigation.PushAsync(new DetailChampion(new ChampionDetailViewM(championManagerVm, champion)));
        }

        private void Addchampion()
        {
            Shell.Current.Navigation.PushAsync(page: new AddChampionPage(new EditChampionViewM(championManagerVm, new EditChampionVm(null), null)));
        }

        private async void EditChampion(ChampionVm championVM)
        {
            await Shell.Current.Navigation.PushAsync(new AddChampionPage(new EditChampionViewM(championManagerVm, new EditChampionVm(championVM), championVM)));
        }

    }
}

