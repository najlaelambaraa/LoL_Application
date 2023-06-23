using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model;

namespace ViewModels
{
    public class EditChampionVm : INotifyPropertyChanged
    {
        public ChampionVm Model { get; set; }
        public EditChampionVm(ChampionVm vm)
        {
            IsNew = vm is null;
            Model = IsNew ? null : vm;
            _bio = IsNew ? string.Empty : Model.Bio;
            icon = IsNew ? string.Empty : Model.Icon;
            _name = IsNew ? string.Empty : Model.Name;
            image = IsNew ? string.Empty : Model.Image;
            _classe = IsNew ? ChampionClass.Unknown : Model.Class;
            ListClasses = Enum.GetValues<ChampionClass>().Where(c => c != ChampionClass.Unknown).ToArray();
        }

        public bool IsNew { get; private set; }

        public IEnumerable<ChampionClass> ListClasses { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }
        private string icon;

        public string Icon
        {
            get => icon;
            set
            {
                if (icon == value) return;
                icon = value;
                OnPropertyChanged();
            }
        }
        private string _bio;
        public string Bio
        {
            get => _bio;
            set
            {
                if (_bio == value) return;
                _bio = value;
                OnPropertyChanged();
            }
        }



        private string image;
        public string Image
        {
            get => image;
            set
            {
                if (image == value) return;
                image = value;
                OnPropertyChanged();
            }
        }

        private ChampionClass _classe;
        public ChampionClass Class
        {
            get => _classe;
            set
            {
                if (_classe == null) return;
                _classe = value;
                OnPropertyChanged();
            }
        }

        private int index;
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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ReadOnlyDictionary<string, int> Characteristics
        {
            get => Model.Characteristics;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void SaveChampion()
        {
            if (!IsNew)
            {
                Model.Bio = Bio;
                Model.Icon = Icon;
                Model.Image = Image;
                Model.Class = Class;
            }
            else
            {
                
                Model = new ChampionVm(new Champion(Name, ChampionClass.Unknown, Icon, "", Bio));
                var data = "";
               

            }
        }
    }
}

