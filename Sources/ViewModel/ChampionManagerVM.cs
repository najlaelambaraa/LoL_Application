using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Model;

namespace ViewModel
{
    public class ChampionManagerVM : INotifyPropertyChanged
    {
        
        public ObservableCollection<ChampionVm> Champions { get; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand NextPageCommand { get; private set; }

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
        private IDataManager _dataManager;

        public  ChampionManagerVM(IDataManager dataManager)
        {
            DataManager = dataManager;
            Champions = new ObservableCollection<ChampionVm>();
            LoadChampions(index, Count).ConfigureAwait(false);
            PropertyChanged += ChampionManagerVM_PropertyChanged;
            PropertyChanged += ChampionMgrm_PropertyChanged;
        }

        private async void ChampionManagerVM_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Index))
            {
                await LoadChampions(index, Count);
            }
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

        

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task LoadChampions(int index, int count)
        {
            
            var modelChampions = await DataManager.ChampionsMgr.GetItems(Index - 1, Count);

            foreach (var champion in modelChampions)
            {
                Champions.Add(new ChampionVm(champion));
            }
        }

        private async void ChampionManager_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Index))
            {
                Champions.Clear();
                await LoadChampions(Index, Count);
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
                    var result = "null";
                }
            }

        }
        private async void ChampionMgrm_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Index))
            {
                Champions.Clear();
                await LoadChampions(Index, Count);
            }
        }


    }
}

