using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClustering.Algorithms;

public interface IMeasurable<T>
{
    public double Measure(T a, T b);
}
