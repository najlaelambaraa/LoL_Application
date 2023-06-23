using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using Model;
namespace ViewModels
{
    public class ChampionVm : ObservableObject
    {
        public ChampionVm(Champion champion) => Model = champion;

        private Champion model;
        public ReadOnlyDictionary<string, int> Characteristics
        {
            get => Model.Characteristics;
        }
        public Champion Model
        {
            get => model;
            set
            {
                model = value;
            }
        }

        public string Name
        {
            get => Model.Name;
        }

        public string Icon
        {
            get => Model.Icon;
            set => SetProperty(Model.Icon, value, Model, (mod, val) => model.Icon = val);
        }

        public LargeImage Image
        {
            get => Model.Image;
            set=> SetProperty(Model.Image, value, Model, (mod, val) => model.Image = val);
        }
        public ChampionClass Class
        {
            get => Model.Class;
            set=> SetProperty(Model.Class, value, Model, (mod, val) => model.Class = val);
        }
        public string Bio
        {
            get => Model.Bio;
            set=> SetProperty(Model.Bio, value, Model, (mod, val) => model.Bio = val);
        }
        private ObservableCollection<Skill> skills;
        public HashSet<Skill> Skills
        {
            get => Model.Skills.ToHashSet();
            set
            {
                if (value.Equals(skills)) return;
                skills = new ObservableCollection<Skill>(Skills);
                OnPropertyChanged();
            }
        }
    }
}

