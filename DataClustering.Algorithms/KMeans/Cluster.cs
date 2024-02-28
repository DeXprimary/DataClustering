using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

namespace DataClustering.Algorithms.KMeans;

internal class Cluster<TVector, KNumber> : ICluster<TVector>
    where TVector : DataVector<KNumber>
    where KNumber : INumber<KNumber>, IDivisionOperators<KNumber, int, KNumber>
{
    private TVector _lastCentroid;
    private TVector _centroid;
    public TVector Centroid => _centroid;

    private IList<TVector> _components = new List<TVector>();
    public IList<TVector> Components => _components;

    Cluster(TVector defaultCentroid)
    {
        _centroid = defaultCentroid;
        _lastCentroid = defaultCentroid;
    }

    public double GetDistanceSq(TVector a, TVector b)
    {
        return a.Values
            .Select((item, index) => Convert.ToDouble((b.Values[index] - a.Values[index]) * (b.Values[index] - a.Values[index])))
            .Aggregate((sum, item) => sum + item);
    }

    public double UpdateCentroid()
    {
        _lastCentroid = _centroid;
        var zeroVector = DataVector<KNumber>.GetZero(_centroid.Values.Length);
        _centroid = (TVector)Components.Aggregate(zeroVector, (sum, item) => sum + item, sum => sum / zeroVector.Values.Length);

        return GetDistanceSq(_centroid, _lastCentroid);
    }
}
