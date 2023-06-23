using System;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ViewModel
{
    public class DataManagerVM : INotifyPropertyChanged
    {
        private ObservableCollection<ChampionVm> ChampionsObs { get; set; } = new ObservableCollection<ChampionVm>();
        public ReadOnlyObservableCollection<ChampionVm> ChampionVMs { get; private set; }
        public int PageId
        {
            get => _pageId;
            set
            {
                if (_pageId != value)
                {
                    _pageId = value;
                    OnPropertyChanged();
                    (PreviousPageCommand as Command).ChangeCanExecute();
                    (NextPageCommand as Command).ChangeCanExecute();
                    LoadData();
                }
            }

        }
        public int _pageId = 1;
        public int PageSize { get; set; } = 5;
        public int NbItem
        {
            get => _nbItem;
            set
            {
                if (_nbItem != value)
                {
                    _nbItem = value;
                    OnPropertyChanged();
                }
            }
        }
        public int _nbItem;

        public IDataManager DataManager
        {
            get => _dataManager;
            set
            {
                if (_dataManager == value) return;
                _dataManager = value;
                OnPropertyChanged();
                LoadData();
            }
        }
        private IDataManager _dataManager;

        public ICommand NextPageCommand { get; private set; }
        public ICommand PreviousPageCommand { get; private set; }

        public DataManagerVM(IDataManager dataManager)
        {
            DataManager = dataManager;
            ChampionVMs = new ReadOnlyObservableCollection<ChampionVm>(ChampionsObs);

            NextPageCommand = new Command(
                execute: () => { PageId++; },
                canExecute: () => { return NbItem - (PageSize * (PageId - 1)) > PageSize; }
                );
            PreviousPageCommand = new Command(
                execute: () => { PageId--; },
                canExecute: () => { return PageId > 1; }
                );
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
          => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public event PropertyChangedEventHandler PropertyChanged;

        public async void LoadData()
        {
            ChampionsObs.Clear();
            NbItem = await DataManager.ChampionsMgr.GetNbItems();
            IEnumerable<Champion> champions = await DataManager.ChampionsMgr.GetItems(PageId - 1, PageSize);
            foreach (var item in champions)
            {
                ChampionsObs.Add(new ChampionVm(item));
            }
        }
    }
}

