using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataClustering.Algorithms.KMeans;

public class DataClusterizer<T, K> : IDataClusterizer<T, K>
    where K : ICluster<T>
    where T : INumber<T>, IDivisionOperators<T, int, T>
{
    private const int _maxIterations = 100;
    private const int _minMovability = 1;

    private IList<IMeasurable<T>> _data;
    private ICluster<T>[] _clusters;
    public ICluster<T>[] Clusters => _clusters;

    public DataClusterizer(IList<IMeasurable<T>> data, int defaultClustersCount)
    {
        _data = data;
        _clusters = GetRandomCentroids(data, defaultClustersCount);
    }

    public DataClusterizer(IList<IMeasurable<T>> data, ICluster<T>[] defaultClusters) : this(data, defaultClusters.Length)
    {
        _clusters = defaultClusters;
    }

    private ICluster<T>[] GetRandomCentroids(IList<IMeasurable<T>> data, int count)
    {
        if (data.Count < count) throw new ArgumentException("The number of data elements is less than the number of clusters.");
        var random = new Random();
        return data
            .OrderBy(_ => random.Next())
            .Take(count)
            .Select(item => new Cluster<T>(item))
            .ToArray();
    }

    public void Compute(IList<IMeasurable<T>> data)
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

    private ICluster<T> GetClosestCluster(IMeasurable<T> unitData)
    {
        return _clusters
            .Select(item => new { measure = unitData.Measure(item), cluster = item })
            .MinBy(item => item.measure)
            !.cluster;
    }
}
