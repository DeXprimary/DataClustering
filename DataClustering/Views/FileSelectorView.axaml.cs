using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using System.Collections.Generic;
using System.IO;
using DataClustering.ViewModels;

namespace DataClustering.Views
{
    public partial class FileSelectorView : UserControl
    {
        public FileSelectorView()
        {
            InitializeComponent();
            DataContext = new FileSelectorViewModel();
        }

        private async void OpenFileButton_Clicked(object sender, RoutedEventArgs args)
        {
            // Get top level from the current control. Alternatively, you can use Window reference instead.
            var topLevel = TopLevel.GetTopLevel(this);

            // Start async operation to open the dialog.
            var files = await topLevel!.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                FileTypeFilter = new[] { new FilePickerFileType("Select Excel file") { Patterns = new[] { "*.csv" } } },
                Title = "Open Text File",
                AllowMultiple = false
            });

            if (files.Count >= 1)
            {
                // Open reading stream from the first file.
                await using var stream = await files[0].OpenReadAsync();
                using var streamReader = new StreamReader(stream);
                // Reads all the content of file as a text.
                var fileContent = await streamReader.ReadToEndAsync();

                (DataContext as FileSelectorViewModel)!.CreateClusterizer.Execute(fileContent);
            }
        }
    }
}
