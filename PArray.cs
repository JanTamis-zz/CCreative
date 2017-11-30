using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CCreative.Colors;
using static CCreative.Math;
using static CCreative.Drawing;
using static CCreative.General;
using static CCreative.Data;
using CCreative;

namespace CCreative
{
    public class PArray<T> : List<T>
    {
        public int Lenght
        {
            get { return base.Count; }
        }

        public PArray() : base()
        {

        }

        public PArray(int size) : base(size)
        {

        }

        public PArray(IEnumerable<T> enumerable) : base(enumerable)
        {

        }

        public void Push(T value)
        {
            base.Add(value);
        }

        public T Pop()
        {
            T value = base[Lenght];
            base.RemoveAt(Lenght);
            return value;
        }

        public T Peek()
        {
            return base[Lenght];
        }

        public T Dequeue()
        {
            T value = base[Lenght];
            base.RemoveAt(0);
            return value;
        } 

        public void Enqueue(T value)
        {
            base.Add(value);
        }

        public T this[int index]
        {
            get { return base[index]; }
            set { base[index] = value; }
        }
    }
}
