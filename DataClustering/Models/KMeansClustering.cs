using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using DataClustering.Algorithms.KMeans;
using DataClustering.Algorithms;

namespace DataClustering.Models;

public static class KMeansClustering
{
    public static DataClusterizer<int, Cluster<int>> DataClusterizer { get; private set; }

    public static DataClusterizer<int, Cluster<int>> CreateNewClusterizer(IList<IMeasurable<int>> data, int countClusters = 2)
    {
        DataClusterizer = new DataClusterizer<int, Cluster<int>>(data, countClusters);
        return DataClusterizer;
    }
}
