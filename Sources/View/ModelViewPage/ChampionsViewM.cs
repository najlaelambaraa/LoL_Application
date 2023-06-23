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
        public Command NextPageCommand { get; private set; }
        public Command PreviousPageCommand { get; }
        public Command EditChampionCommand { get; }
        public ChampionManagerVM championManagerVm { get; }
        public Command DeleteChampionCommand { get; private set; }
        

        public ChampionsViewM(ChampionManagerVM championManager)

        {
            championManagerVm = championManager;
            PushToDetailCommand = new Command<ChampionVm>(PushToDetail);
            DeleteChampionCommand = new Command<ChampionVm>(async (ChampionVm obj) => await championManagerVm.DeleteChampion(obj));
            NextPageCommand = new Command(NextPage, CanExecuteNext);
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
        private bool CanExecuteNext()
        {
            var val = (this.championManagerVm.Index) < this.championManagerVm.PageTotale;
            return val;
        }
        void RefreshCanExecute()
        {

            PreviousPageCommand.ChangeCanExecute();
            NextPageCommand.ChangeCanExecute();
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

