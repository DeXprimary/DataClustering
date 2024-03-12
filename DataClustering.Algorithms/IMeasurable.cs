using DataClustering.Algorithms.KMeans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataClustering.Algorithms;

public interface IMeasurable<T>
{
    public T[] Values { get; }
    public T Measure(ICluster<T> targetCluster);
}
