using DataClustering.Algorithms.KMeans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClustering.Models
{
    public static class ConvertToDataVector
    {
        public static DataVector<int>[] FromCSV(string rawData)
        {
            var q = rawData;
            var e = q
                .Split(Environment.NewLine);
            var r = e
                .Select(item => item.Split(";").ToArray());
            return rawData
                .Split(Environment.NewLine)
                .Where(line => line.Length > 0)
                .Select(item => new DataVector<int>(item.Split(";").Select(num => int.Parse(num)).ToArray()))
                .ToArray();
        }
    }
}
