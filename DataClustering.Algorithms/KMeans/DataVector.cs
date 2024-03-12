using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataClustering.Algorithms.KMeans;

public class DataVector<T> : IMeasurable<T>
    where T : INumber<T>, IDivisionOperators<T, int, T>
{
    private T[] _values;
    public T[] Values => _values;

    public DataVector(T[] values)
    {
        _values = values;
    }

    public DataVector(DataVector<T> vector)
    {
        _values = vector._values;
    }

    public T Measure(ICluster<T> cluster)
    {
        return (this - (DataVector<T>)(cluster.Centroid)).Values.Aggregate((sum, num) => ((num * num) + sum));
    }

    public static DataVector<T> GetZero(int dimension)
    {
        return new DataVector<T>(new T[dimension].Select(item => item = T.Zero).ToArray());
    }

    public static DataVector<T> operator +(DataVector<T> a, DataVector<T> b)
    {
        var numberArray = new T[a._values.Length];
        for (int k = 0; k < a._values.Length; k++)
            numberArray[k] = a._values[k] + b._values[k];
        return new DataVector<T>(numberArray);
    }

    public static DataVector<T> operator -(DataVector<T> a, DataVector<T> b)
    {
        var numberArray = new T[a._values.Length];
        for (int k = 0; k < a._values.Length; k++)
            numberArray[k] = a._values[k] - b._values[k];
        return new DataVector<T>(numberArray);
    }

    public static DataVector<T> operator *(DataVector<T> a, DataVector<T> b)
    {
        var numberArray = new T[a._values.Length];
        for (int k = 0; k < a._values.Length; k++)
            numberArray[k] = a._values[k] * b._values[k];
        return new DataVector<T>(numberArray);
    }

    public static DataVector<T> operator /(DataVector<T> a, int b)
    {
        var numberArray = new T[a._values.Length];
        for (int k = 0; k < a._values.Length; k++)
            numberArray[k] = a._values[k] / b;
        return new DataVector<T>(numberArray);
    }
}
