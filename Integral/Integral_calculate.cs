using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral
{
    class Integral_calculate
    {
        double f(double x)
        {
            return 32 * x - Math.Log10(2 * x) - 41;
        }
        public double I(double a, double b, double n, double y)
        {
            return ((b - a) / (2 * n) * y);
        }

        /*double a = 1;
        double b = 100000;*/
        public double y = 0, dy;
        double In;

        //Func<double,double>func = x => x*x;

        public double calcPosl(int n, double a, double b, Func<double, double> f)// calculating Integral.single process variant 
        {
            y = 0;
            dy = (b - a) / n;
            y += f(a) + f(b);
            /* Parallel.For(1, (int)n, i=> {
                  y += 2 * (f(a + dy * i));
             });*/
            for (int i = 1; i < n; i++)
            {
                y += 2 * (f(a + dy * i));
            }
            return In = I(a, b, n, y);
        }

        public double calcParr(int n, double a, double b, Func<double, double> f)// calculating Integral.multy process variant 
        {
            object monitor = new object();
            y = 0;
            dy = (b - a) / n;
            y += (f(a) + f(b)) / 2;
            /* for (int i = 1; i < n; i++)
             {
                 y += 2 * (f(a + dy * i));
             }*/
            // Parallel.For()
            Parallel.For(0, (int)n, () => 0.0, (i, state, local) =>
            {

                local += f(a + dy * i);
                return local;
            }, local => { lock (monitor) y += local; });

            return y *= dy;
            //return In;
            //return In = I(a, b, n, y);
        }
       
    }
}
