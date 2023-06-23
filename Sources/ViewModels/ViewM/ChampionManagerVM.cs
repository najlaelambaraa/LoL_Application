using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;


namespace ViewModels
{
	public partial class ChampionManagerVM : ObservableObject
    {
            public ObservableCollection<ChampionVm> Champions { get; }

            public IDataManager DataManager
            {
                get => _dataManager;
                set => _dataManager = value;
            }
            private IDataManager _dataManager { get; set; }

            public ChampionManagerVM(IDataManager dataManager)
            {
                DataManager = dataManager;
                Champions = new ObservableCollection<ChampionVm>();
               
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

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PreviousPageCommand),nameof(NextPageCommand))]
        private int index;

            public int Count
            {
                get;
                set;
            } = 5;

            public int PageTotale { get { return this.total / Count + ((this.total % Count) > 0 ? 1 : 0); } }

        [ObservableProperty]
        private int total;


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

        [RelayCommand(CanExecute =nameof(CanExecuteNext))]
        private void NextPage()
        {
            Index++;

        }

        [RelayCommand(CanExecute =nameof(CanExecutePrevious))]
        private void PreviousPage()
        {
            Index--;
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

        [RelayCommand]
        public async Task DeleteChampion(ChampionVm champion)
        {
            if (champion is null) return;
            if (!Champions.Contains(champion)) return;
            await DataManager.ChampionsMgr.DeleteItem(champion.Model);
            updatePagination();

        }

    }
    
}

