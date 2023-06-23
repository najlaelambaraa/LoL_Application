using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using ViewModels;

namespace View.ModelViewPage
{
	public partial class EditChampionViewM
	{

        public EditChampionViewM(ChampionManagerVM manager, EditChampionVm aditableChampion,ChampionVm championVM)
        {
            Manager = manager;
            EditableChampion = aditableChampion;
            ChampionVM = championVM;
            Title = aditableChampion.IsNew ? "Create" : "Update";
        }

        public string Title { get; }

        private ChampionManagerVM Manager;
        public EditChampionVm EditableChampion { get; }
        private ChampionVm ChampionVM;
       


        [RelayCommand]
        private async void SaveChampion()
        {
            Manager.SaveChampion(EditableChampion, ChampionVM);
            await Shell.Current.Navigation.PopAsync();
        }
        public ReadOnlyDictionary<string, int> Characteristics
        {
            get => Characteristics;
        }


        [RelayCommand]
        private async Task PickIcon()
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Pick an icon",

            });
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                var bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, (int)stream.Length);
                EditableChampion.Icon = Convert.ToBase64String(bytes);
            }
        }

        [RelayCommand]
        private async Task PickImage()
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Pick an image",

            });
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                var bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, (int)stream.Length);
                EditableChampion.Image = Convert.ToBase64String(bytes);
            }
        }

    }
}

