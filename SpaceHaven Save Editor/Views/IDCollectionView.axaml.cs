using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using SpaceHaven_Save_Editor.ViewModels;

namespace SpaceHaven_Save_Editor.Views
{
    public partial class IdCollectionView : ReactiveWindow<IdCollectionViewModel>
    {
        public IdCollectionView()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new IdCollectionViewModel();

            this.WhenActivated(d =>
            {
                if (ViewModel != null) d(ViewModel.ShowSaveFileDialog.RegisterHandler(ShowSaveFileDialog));
            });
        }

        public async Task ShowSaveFileDialog(InteractionContext<Unit, string?> interaction)
        {
            var file = await this.StorageProvider.SaveFilePickerAsync(new Avalonia.Platform.Storage.FilePickerSaveOptions
            {
                Title = "Save ID List as...",
                DefaultExtension = "txt",
                SuggestedFileName = "id list"
            });
            interaction.SetOutput(file?.Path.LocalPath);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
