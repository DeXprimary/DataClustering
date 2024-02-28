using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClustering.Algorithms;

public interface ICluster<T>
{
    public T Centroid { get; }
    public IList<T> Components { get; }
    public double UpdateCentroid();
}
