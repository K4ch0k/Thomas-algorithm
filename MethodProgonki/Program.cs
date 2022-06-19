using System;
using System.IO;

namespace MethodProgonki
{
    class Program
    {
        static void Main(string[] args)
        {
            int rows = 0;
            int columns = 0;
            String line;

            //  Ru
            //  Алгоритм используется для решения систем линейных уравнений вида Ax=F, где A - трёхдиагональная матрица.
            //  В моём примере матрица была n X n
            //  Если в вашем файле числа разделены чем-то, отличным от таба - измените 3 строки .Split("\t")

            //  Eng
            //  The algorithm is used to solve systems of linear equations of the form Ax=F, where A is a tridiagonal matrix.
            //  In my example, the matrix was n X n
            //  If the numbers in your file are separated by something other than tab, change 3 lines .Split("\t")

            //  Файл с матрицей A 
            //  FIle with matrix A
            StreamReader srA = new StreamReader(@"C:\Users\A14.txt");
            //  Файл с матрицей F
            //  FIle with matrix F
            StreamReader srB = new StreamReader(@"C:\Users\B14.txt");

            try
            {
                line = srA.ReadLine();
                columns = line.Split("\t").Length;
                while (line != null)
                {
                    rows++;
                    line = srA.ReadLine();
                }
                double[,] mA = new double[rows, columns];
                double[] mB = new double[rows];
                double[] x = new double[rows];
                double[] y = new double[rows];
                double[] a = new double[rows];
                double[] b = new double[rows];

                srA.DiscardBufferedData();
                srA.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);

                for (int i = 0; i < rows; i++)
                {
                    var tline = srA.ReadLine().Split("\t");
                    for (int j = 0; j < columns; j++)
                    {
                        mA[i, j] = Convert.ToDouble(tline[j]);
                    }
                    tline = srB.ReadLine().Split("\t");
                    mB[i] = Convert.ToDouble(tline[0]);
                }

                y[0] = mA[0, 0];
                a[0] = -mA[0, 1] / y[0];
                b[0] = mB[0] / y[0];

                for (int i = 1; i < rows - 1; i++)
                {

                    y[i] = mA[i, i] + mA[i, i - 1] * a[i - 1];
                    a[i] = -mA[i, i + 1] / y[i];
                    b[i] = (mB[i] - (mA[i, i - 1] * b[i - 1])) / y[i];
                }

                y[rows - 1] = mA[rows - 1, columns - 1] + (mA[rows - 1, columns - 2] * a[rows - 2]);
                b[rows - 1] = (mB[rows - 1] - mA[rows - 1, columns - 2] * b[rows - 2]) / y[rows - 1];

                for (int i = rows - 1; i >= 0; i--)
                {
                    if (i == rows - 1)
                    {
                        x[i] = b[rows - 1];
                        continue;
                    }
                    x[i] = a[i] * x[i + 1] + b[i];
                }

                for (int i = 0; i <= rows - 1; i++)
                {
                    Console.WriteLine($"x[{i}] = {x[i]}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                srA.Close();
                srB.Close();
                Console.WriteLine("Executing finally block.");
            }
        }
    }
}
