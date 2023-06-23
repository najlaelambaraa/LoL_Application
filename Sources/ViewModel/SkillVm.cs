using System;
using Model;
using System.ComponentModel;

namespace ViewModel
{
    public class SkillVm : INotifyPropertyChanged
    {
        public SkillType Type { get => Model.Type; }

        public Skill Model
        {
            get => _model;
            set
            {
                if (value != _model)
                {
                    _model = value;
                    
                }
            }
        }
        private Skill _model;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get => Model.Name;
        }

        public string Description
        {
            get => Model.Description;
            set
            {
                if (value != Model.Description)
                {
                    Model.Description = value;
                   
                }
            }
        }

        public SkillVm(Skill model)
        {
            this.Model = model;
        }
    }
}

