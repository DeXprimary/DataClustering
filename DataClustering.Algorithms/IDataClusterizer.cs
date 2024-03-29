﻿using DataClustering.Algorithms.KMeans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClustering.Algorithms;

public interface IDataClusterizer<T, K>
    where K : ICluster<T>
{
    public ICluster<T>[] Clusters { get; }
    public void Compute(IList<IMeasurable<T>> data);
}
