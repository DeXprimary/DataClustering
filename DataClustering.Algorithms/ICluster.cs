using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClustering.Algorithms;

public interface ICluster<T>
{
    public IMeasurable<T> Centroid { get; }
    public IList<IMeasurable<T>> Components { get; }
    public double UpdateCentroid();
}
