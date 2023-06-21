using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model;

namespace ViewModel;

public class ChampionVm : INotifyPropertyChanged
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
            if (value.Equals(model)) return;
            if (value == null) return;
            model = value;
            OnPropertyChanged();
        }
    }

    public string Name
    {
        get => Model.Name;
    }

    public string Icon
    {
        get => Model.Icon;
        set
        {
            if (model == null) return;
            Model.Icon = value;
            OnPropertyChanged();
        }
    }

    public LargeImage Image
    {
        get => Model.Image;
        set
        {
            if (model == null) return;
            Model.Image = value;
            OnPropertyChanged();
        }
    }
    public ChampionClass Class
    {
        get => Model.Class;
        set
        {
            if (model == null) return;
            Model.Class = value;
            OnPropertyChanged();
        }
    }
    public string Bio
    {
        get => Model.Bio;
        set
        {
            if (model == null) return;
            Model.Bio = value;
            OnPropertyChanged();
        }
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

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}