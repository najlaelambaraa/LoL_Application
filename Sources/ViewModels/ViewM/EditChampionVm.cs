using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using Model;

namespace ViewModels
{
    public partial class EditChampionVm : ObservableObject
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
            _class = IsNew ? ChampionClass.Unknown : Model.Class;
            ListClasses = Enum.GetValues<ChampionClass>().Where(c => c != ChampionClass.Unknown).ToArray();
        }

        public bool IsNew { get; private set; }

        public IEnumerable<ChampionClass> ListClasses { get; }

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string icon;

        [ObservableProperty]
        private string _bio;



        [ObservableProperty]
        private string image;

        [ObservableProperty]
        private ChampionClass _class;

        [ObservableProperty]
        private int index;
        

        public ReadOnlyDictionary<string, int> Characteristics
        {
            get => Model.Characteristics;
        }

        public void SaveChampion()
        {
            if (!IsNew)
            {
                Model.Bio = Bio;
                Model.Icon = Icon;
                Model.Image.Base64 = Image;
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

