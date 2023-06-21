using System;
using ViewModel;
using View.Page;
using Model;
using System.Windows.Input;
using static StubLib.StubData;

namespace View.ModelViewPage
{
	public class ChampionsViewM
	{
        public ICommand NextPageCommand { get; private set; }
        public Command PreviousPageCommand { get; }
        public Command EditChampionCommand { get; }
        public ChampionManagerVM championManagerVm { get; }

        public ChampionsViewM(ChampionManagerVM championManager)
        {
            championManagerVm = championManager;
            PushToDetailCommand = new Command<ChampionVm>(PushToDetail);
            NextPageCommand = new Command(NextPage);
            PreviousPageCommand = new Command(PreviousPage, CanExecutePrevious);
            AddChampionCommand = new Command(Addchampion);

        }
        private void NextPage()
        {
            championManagerVm.Index++;
            RefreshCanExecute();

        }
        private void PreviousPage()
        {
            championManagerVm.Index--;
            RefreshCanExecute();
        }
        private bool CanExecutePrevious()
        {
            return championManagerVm.Index > 1;
        }
        void RefreshCanExecute()
        {

            PreviousPageCommand.ChangeCanExecute();

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

    }
}

