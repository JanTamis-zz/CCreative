using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CCreative
{
    class NeuralNetwork
    {
        int inputNodes;
        int hiddenNodes;
        int outputNode;

        public NeuralNetwork(int inputNum, int hiddenNum, int outputNum)
        {
            inputNodes = inputNum;
            hiddenNodes = hiddenNum;
            outputNode = outputNum;
        }
    }

    //class Matrix
    //{
    //    int rows;
    //    int cols;
    //    double[][] matrix;

    //    Matrix(int rows, int cols)
    //    {
    //        this.rows = rows;
    //        this.cols = cols;
    //        matrix = new double[rows][];

    //        for (int i = 0; i < rows; i++)
    //        {
    //            matrix[i] = new double[cols];

    //            for (int j = 0; j < cols; j++)
    //            {
    //                matrix[i][j] = 0;
    //            }
    //        }
    //    }

    //    public void multiply(double multFactor)
    //    {
    //        for (int i = 0; i < rows; i++)
    //        {
    //            for (int j = 0; j < cols; j++)
    //            {
    //                matrix[i][j] *= multFactor;
    //            }
    //        }
    //    }
    //    public void add(double addFactor)
    //    {
    //        for (int i = 0; i < rows; i++)
    //        {
    //            for (int j = 0; j < cols; j++)
    //            {
    //                matrix[i][j] += addFactor;
    //            }  
    //        }
    //    }

    //    public void add(Matrix addFactor)
    //    {
    //        if (addFactor.cols == this.cols && addFactor.rows == this.rows)
    //        {
    //            for (int i = 0; i < rows; i++)
    //            {
    //                for (int j = 0; j < cols; j++)
    //                {
    //                    matrix[i][j] += addFactor.matrix[i][j];
    //                }
    //            }
    //        }
    //    }

    //    public void randomize(double addFactor)
    //    {
    //        Random rand = new Random();

    //        for (int i = 0; i < rows; i++)
    //        {
    //            for (int j = 0; j < cols; j++)
    //            {
    //                matrix[i][j] = rand.NextDouble() * 10;
    //            }
    //        }
    //    }
    //}
}