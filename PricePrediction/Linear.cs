using PricePrediction.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PricePrediction
{
    public class Linear: HomeController
    {
        

        public void Main(String[] args)
        {
            double[] a = { 1, 2, 3, 4, 5, 6, 7, 8 };
            double[] b = { 100, 100, 100, 100, 100, 100, 100, 100 };
            if (a.Length != b.Length)
            {
                throw new Exception("Length of dependent and independent variable must be equal");
            }
            double SumX = 0;
            double SumY = 0;
            double SumXSq = 0;
            double SumXY = 0;
            for (var i = 0; i < a.Length; i++)
            {
                var x = a[i];
                var y = b[i];
                SumXY += x * y;
                SumX += x;
                SumY += y;
                SumXSq += x * x;
            }
            var Count = a.Length;
            var meanX = SumX / Count;
            var meanY = SumY / Count;
            var meanXSq = meanX * meanX;
            var Slope = (SumXY - (Count * meanX * meanY)) / (SumXSq - (Count * meanXSq));
            var C = meanY - (meanX * Slope);
            var Y = (Slope * (Count + 1)) + C;
        }
    
    }
}