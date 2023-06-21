using System;
using View.Page;
using ViewModel;

namespace View.ModelViewPage
{
	public class ChampionDetailViewM
	{
        public ChampionDetailViewM(ChampionManagerVM manager, ChampionVm championVm)
        {
            ChampionVM = championVm;
            EditChampionCommand = new Command(EditChampion);
            Manager = manager;
        }
        public ChampionVm ChampionVM { get; }
        private ChampionManagerVM Manager;
        public Command EditChampionCommand { get; }

        private async void EditChampion()
        {
            await Shell.Current.Navigation.PushAsync(new AddChampionPage(new EditChampionViewM(Manager, new EditChampionVm(ChampionVM), ChampionVM)));
        }
    }
}

