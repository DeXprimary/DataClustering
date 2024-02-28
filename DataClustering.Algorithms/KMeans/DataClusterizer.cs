using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClustering.Algorithms.KMeans;

internal class DataClusterizer<T, K> : IDataClusterizer<T, K>
    where K : ICluster<T>
{
    private const int _maxIterations = 100;
    private const int _minMovability = 1;

    private IList<T> _data;
    private Func<T, T, double> _funcComparison;
    private ICluster<T>[] _clusters;
    public ICluster<T>[] Clusters => _clusters;

    DataClusterizer(T[] data, ICluster<T>[] defaultClusters, Func<T, T, double> funcComparison)
    {
        _data = data;
        _funcComparison = funcComparison;
        _clusters = defaultClusters;
        Initialize(data);
    }

    public void Initialize(IList<T> data)
    {
        for (int k = 0; k < data.Count; k++)
        {
            GetClosestCluster(data[k]).Components.Add(data[k]);
        }
    }

    public void Compute(IList<T> data)
    {
        for (int k = 0; k < _maxIterations; k++)
        {
            foreach (var cluster in _clusters)
            {
                cluster.Components.Clear();
            }

            foreach (var vector in data)
            {
                GetClosestCluster(vector).Components.Add(vector);
            }

            var distanceChangeTotal = 0d;
            foreach (var cluster in _clusters)
            {
                distanceChangeTotal += cluster.UpdateCentroid();
            }

            if (distanceChangeTotal < _minMovability)
            {
                break;
            }
        }
    }

    private ICluster<T> GetClosestCluster(T unitData)
    {
        return _clusters
            .Select(item => new { measure = _funcComparison(item.Centroid, unitData), cluster = item })
            .MinBy(item => item.measure)
            !.cluster;
    }
}
