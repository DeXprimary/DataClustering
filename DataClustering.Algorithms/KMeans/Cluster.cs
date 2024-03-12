using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

namespace DataClustering.Algorithms.KMeans;

public class Cluster<T> : ICluster<T>
    where T : INumber<T>, IDivisionOperators<T, int, T>
{
    private IMeasurable<T> _lastCentroid;
    private IMeasurable<T> _centroid;
    public IMeasurable<T> Centroid => _centroid;

    private IList<IMeasurable<T>> _components = new List<IMeasurable<T>>();
    public IList<IMeasurable<T>> Components => _components;

    public Cluster(IMeasurable<T> defaultCentroid)
    {
        _centroid = defaultCentroid;
        _lastCentroid = defaultCentroid;
    }

    public double GetDistanceSq(DataVector<T> a, DataVector<T> b)
    {
        return Convert.ToDouble((a - b).Values.Aggregate((sum, num) => ((num * num) + sum)));
    }

    public double UpdateCentroid()
    {
        _lastCentroid = _centroid;
        var zeroVector = DataVector<T>.GetZero(_centroid.Values.Length);
        _centroid = Components.Aggregate(zeroVector, (sum, item) => sum + (DataVector<T>)item, sum => sum / zeroVector.Values.Length);

        return GetDistanceSq((DataVector<T>)_centroid, (DataVector<T>)_lastCentroid);
    }
}
