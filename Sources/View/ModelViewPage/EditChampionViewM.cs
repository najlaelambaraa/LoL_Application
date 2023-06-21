using System;
using ViewModel;

namespace View.ModelViewPage
{
	public class EditChampionViewM
	{
        public Command PickIconCommand { get; }
        public Command PickImageCommand { get; }

        public EditChampionViewM(ChampionManagerVM manager, EditChampionVm aditableChampion,ChampionVm championVM)
        {
            Manager = manager;
            EditableChampion = aditableChampion;
            ChampionVM = championVM;
            SaveChampionCommand = new Command(SaveChampion);
            PickIconCommand = new Command(async () => await PickIcon());
            PickImageCommand = new Command(async () => await PickImage());
            Title = aditableChampion.IsNew ? "Create" : "Update";
        }

        public string Title { get; }

        private ChampionManagerVM Manager;
        public EditChampionVm EditableChampion { get; }
        private ChampionVm ChampionVM;

        public Command SaveChampionCommand { get; }

        private async void SaveChampion()
        {
            Manager.SaveChampion(EditableChampion, ChampionVM);
            await Shell.Current.Navigation.PopAsync();
        }



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

