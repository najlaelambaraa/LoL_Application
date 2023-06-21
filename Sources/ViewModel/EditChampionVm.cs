using System;
using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace ViewModel
{
    public class EditChampionVm
	{
        public ChampionVm Model { get; set; }
        public EditChampionVm(ChampionVm vm)
        {
            IsNew = vm is null;
            Model = IsNew ? null : vm;
            _bio = IsNew ? string.Empty : Model.Bio;
            icon = IsNew ? string.Empty : Model.Icon;
            _name = IsNew ? string.Empty : Model.Name;
            image = IsNew ? string.Empty : Model.Image.Base64;
        }

        public bool IsNew { get; private set; }

   

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



        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        public event PropertyChangedEventHandler PropertyChanged;

        public void SaveChampion()
        {
            if (!IsNew)
            {
                Model.Bio = Bio;
                Model.Icon = Icon;
            }
            else
            {
                //Model.Model = new Champion(Name,ChampionClass.Unknown,Icon,"",Bio);
                Model = new ChampionVm(new Champion(Name, ChampionClass.Unknown, Icon, "", Bio));
                var data = "";
                //foreach (KeyValuePair<string,int> c in Characteristique)
                //{
                //    Model.Model.AddCharacteristics(new Tuple<string, int>(c.Key, c.Value));
                //}

            }
        }
    }
}

