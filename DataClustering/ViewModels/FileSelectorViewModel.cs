using DataClustering.Models;
using ReactiveUI;
using System.Windows.Input;

namespace DataClustering.ViewModels;

public class FileSelectorViewModel
{
    public ICommand CreateClusterizer { get; }

    public FileSelectorViewModel()
    {
        CreateClusterizer = ReactiveCommand.Create<string> ((rawData) =>
        {
            KMeansClustering.CreateNewClusterizer(ConvertToDataVector.FromCSV(rawData), 3);
        });
    }
}
