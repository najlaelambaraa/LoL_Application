using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Model;


namespace ViewModels
{
	public class ChampionManagerVM : INotifyPropertyChanged
    {
            public ObservableCollection<ChampionVm> Champions { get; }
            public event PropertyChangedEventHandler? PropertyChanged;
            public Command NextPageCommand { get; private set; }
            public Command PreviousPageCommand { get; }
            public Command DeleChampionCommand { get; }
            public ICommand DeleteChampionCommand { get; }

            public IDataManager DataManager
            {
                get => _dataManager;
                set
                {
                    if (_dataManager == value) return;
                    _dataManager = value;
                    OnPropertyChanged();
                }
            }
            private IDataManager _dataManager { get; set; }

            public ChampionManagerVM(IDataManager dataManager)
            {
                DataManager = dataManager;
                Champions = new ObservableCollection<ChampionVm>();
            DeleteChampionCommand = new Command<ChampionVm>(async (ChampionVm obj) => await DeleteChampion(obj));
            NextPageCommand = new Command(NextPage, CanExecuteNext);
            PreviousPageCommand = new Command(PreviousPage, CanExecutePrevious);

            LoadChampions(index, Count).ConfigureAwait(false);
                PropertyChanged += ChampionManagerVM_PropertyChanged;
                Total = this.DataManager.ChampionsMgr.GetNbItems().Result;
            }

            private async void ChampionManagerVM_PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(Index))
                {
                    Champions.Clear();
                    await LoadChampions(index, Count);
                }
            }

        private void NextPage()
        {
            Index++;
            RefreshCanExecute();

        }
        private void PreviousPage()
        {
            Index--;
            RefreshCanExecute();
        }
        private bool CanExecutePrevious()
        {
            return Index > 1;
        }
        private bool CanExecuteNext()
        {
            var val = (this.Index) < this.PageTotale;
            return val;
        }
        void RefreshCanExecute()
        {
            PreviousPageCommand.ChangeCanExecute();
            NextPageCommand.ChangeCanExecute();
        }

        public int Index
            {
                get => index;
                set
                {
                    if (index == value) return;
                    index = value;
                    OnPropertyChanged();
                }
            }
            private int index;

            public int Count
            {
                get;
                set;
            } = 5;
            public int PageTotale { get { return this.total / Count + ((this.total % Count) > 0 ? 1 : 0); } }

            private int total;

            public int Total
            {
                get => total;
                private set
                {
                    total = value;
                    OnPropertyChanged();
                }
            }
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            private async Task LoadChampions(int index, int count)
            {
                Champions.Clear();

                var modelChampions = await DataManager.ChampionsMgr.GetItems(Index - 1, Count);

                foreach (var champion in modelChampions)
                {
                    Champions.Add(new ChampionVm(champion));
                }
            }

            public async void SaveChampion(EditChampionVm editableChampionVM, ChampionVm? championVM)
            {
                if (editableChampionVM is null) return;
                if (!editableChampionVM.IsNew)
                {
                    editableChampionVM.SaveChampion();
                    var champ = await DataManager.ChampionsMgr.UpdateItem(championVM.Model, editableChampionVM.Model.Model);
                }
                else
                {
                    editableChampionVM.SaveChampion();
                    var champ = await DataManager.ChampionsMgr.AddItem(editableChampionVM.Model.Model);
                    if (champ is null)
                    {
                        var result = "nll";
                    }
                    updatePagination();
                }

            }


           

            private async void updatePagination()
            {


                await LoadChampions(this.Index, Count);
                if (Champions.Count == 0)
                {
                    this.Index = this.Index - 1;
                    await LoadChampions(this.Index, Count);
                }
                this.Total = this.DataManager.ChampionsMgr.GetNbItems().Result;
                OnPropertyChanged(nameof(this.Champions));

                OnPropertyChanged(nameof(PageTotale));

            }
        public async Task DeleteChampion(ChampionVm champion)
        {
            if (champion is null) return;
            if (!Champions.Contains(champion)) return;
            await DataManager.ChampionsMgr.DeleteItem(champion.Model);
            updatePagination();

        }

    }
    
}

