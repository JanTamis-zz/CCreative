using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using static CCreative.Math;
using static CCreative.General;

namespace CCreative
{
    public class PVector
    {
        float X, Y, Z;

        public float x
        {
            set { X = value; }
            get { return X; }
        }

        public float y
        {
            set { Y = value; }
            get { return Y; }
        }

        public float z
        {
            set { Z = value; }
            get { return Z; }
        }

        public PVector()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public PVector(double x, double y)
        {
            this.x = (float)x;
            this.y = (float)y;
        }

        
        public PVector(double x, double y, double z)
        {
            this.x = (float)x;
            this.y = (float)y;
            this.z = (float)z;
        }

        public PVector(double[] xyz)
        {
            if (xyz.Length == 3)
            {
                this.x = (float)xyz[0];
                this.y = (float)xyz[1];
                this.z = (float)xyz[2];
            }
            else
            {
                throw new ArgumentException("Array must contain exactly three components , (x,y,z)");
            }
        }

        public PVector(PVector v1)
        {
            this.x = v1.X;
            this.y = v1.Y;
            this.z = v1.Z;
        }

        public PVector(PointF point)
        {
            this.x = point.X;
            this.y = point.Y;
            this.z = 0;
        }

        #region set
        public PVector set(float x, float y)
        {
            this.x = x;
            this.y = y;

            return this;
        }

        public PVector set(float x, float y, float z)
        {
            this.x = x;
            this.y = x;
            this.z = z;

            return this;
        }

        public PVector set(PVector vector)
        {
            this.x = vector.x;
            this.y = vector.y;
            this.z = vector.z;

            return this;
        }

        public PVector set(PointF vector)
        {
            this.x = vector.X;
            this.y = vector.Y;

            return this;
        }
        #endregion

        public PVector random2D()
        {
            x = map(random(-100, 100), -100, 100, -1, 1);
            y = map(random(-100, 100), -100, 100, -1, 1);
            normalize();

            return this;
        }

        public PVector random3D()
        {
            x = map(random(-100, 100), -100, 100, -1, 1);
            y = map(random(-100, 100), -100, 100, -1, 1);
            z = map(random(-100, 100), -100, 100, -1, 1);

            normalize();

            return this;
        }

        private float map(float value, float least, float max, float toMinimum, float toMaximum)
        {
            return toMinimum + (value - least) * (toMaximum - toMinimum) / (max - least);
        }

        public PVector normalize()
        {
            Vector vec = new Vector(x, y);
            vec.Normalize();
            x = (float)vec.X;
            y = (float)vec.Y;
            //set(x / mag(), y / mag(), z / mag());
            return this;
        }

        public PVector fromAngle(double angle)
        {
            x = (float)(1 * System.Math.Cos(angle * 2 * System.Math.PI / 360));
            y = (float)(1 * System.Math.Sin(angle * 2 * System.Math.PI / 360));

            //addTranslation();
            return this;
        }

        public PVector fromAngle(float angle, PVector vector)
        {
            vector.x += (float)(mag() * System.Math.Cos(angle * 2 * System.Math.PI / 360));
            vector.y += (float)(mag() * System.Math.Sin(angle * 2 * System.Math.PI / 360));

            //addTranslation();
            set(vector);

            return this;
        }

        #region add
        public PVector add(float x, float y)
        {
            this.x += x;
            this.y += y;
            return this;
        }

        public PVector add(float x, float y, float z)
        {
            this.x += x;
            this.y += y;
            this.z += z;

            return this;
        }

        public PVector add(PVector vector)
        {
            x += vector.x;
            y += vector.y;
            z += vector.z;

            return this;
        }

        public PVector add(PVector[] vectors)
        {
            foreach (PVector items in vectors)
            {
                x += items.x;
                y += items.y;
                z += items.z;
            }
            return this;
        }

        public PVector add(List<PVector> vectors)
        {
            foreach (PVector items in vectors)
            {
                x += items.x;
                y += items.y;
                z += items.z;
            }
            return this;
        }
        #endregion

        #region sub
        public PVector sub(float x, float y)
        {
            this.x -= x;
            this.y -= y;

            return this;
        }

        public PVector sub(float x, float y, float z)
        {
            this.x -= x;
            this.y -= y;
            this.z -= z;

            return this;
        }

        public PVector sub(PVector vector)
        {
            x -= vector.x;
            y -= vector.y;
            z -= vector.z;

            return this;
        }

        public PVector sub(PVector[] vectors)
        {
            foreach (PVector items in vectors)
            {
                x -= items.x;
                y -= items.y;
                z -= items.z;
            }
            return this;
        }

        public PVector sub(List<PVector> vectors)
        {
            foreach (PVector items in vectors)
            {
                x -= items.x;
                y -= items.y;
                z -= items.z;
            }
            return this;
        }
        #endregion

        #region mult
        public PVector mult(double multi)
        {
            this.x *= (float)multi;
            this.y *= (float)multi;
            this.z *= (float)multi;

            return this;
        }
        #endregion

        #region div
        public PVector div(float x, float y)
        {
            this.x /= x;
            this.y /= y;

            return this;
        }

        public PVector div(float x, float y, float z)
        {
            this.x /= x;
            this.y /= y;
            this.z /= z;

            return this;
        }

        public PVector div(PVector vector)
        {
            x /= vector.x;
            y /= vector.y;
            z /= vector.z;

            return this;
        }

        public PVector div(PVector vector1, PVector vector2)
        {
            x /= vector1.x;
            y /= vector1.y;
            z /= vector1.z;

            x /= vector2.x;
            y /= vector2.y;
            z /= vector2.z;

            return this;
        }

        public PVector div(PVector[] vectors)
        {
            foreach (PVector items in vectors)
            {
                x /= items.x;
                y /= items.y;
                z /= items.z;
            }
            return this;
        }

        public PVector div(List<PVector> vectors)
        {
            foreach (PVector items in vectors)
            {
                x /= items.x;
                y /= items.y;
                z /= items.z;
            }
            return this;
        }
        #endregion

        public float heading()
        {
            Vector vector1 = new Vector(0, 0);
            Vector vector2 = new Vector(x, y);
            float angle = Math.degrees((float)(System.Math.Atan2(vector2.Y, vector2.X) - System.Math.Atan2(vector1.Y, vector1.X)));
            if (angle < 0)
                angle = (angle + 180) + 180;
            return angle;
        }

        public float dist(PVector vector)
        {
            return (float)(System.Math.Sqrt(System.Math.Pow(this.x - vector.x, 2) + System.Math.Pow(this.y - vector.y, 2)));
        }

        public float dist(PointF vector)
        {
            return (float)(System.Math.Sqrt(System.Math.Pow(this.x - vector.X, 2) + System.Math.Pow(this.y - vector.Y, 2)));
        }

        public float dist(System.Drawing.Point vector)
        {
            return (float)(System.Math.Sqrt(System.Math.Pow(this.x - vector.X, 2) + System.Math.Pow(this.y - vector.Y, 2)));
        }

        public float mag()
        {
            return (float)System.Math.Sqrt(x * x + y * y + z * z);
        }

        public PVector setMag(double len)
        {
            normalize();
            mult(len);
            return this;
        }

        public PVector setMag(PVector target, double len)
        {
            target.normalize();
            target.mult(len);
            set(target);

            return this;
        }

        public PVector limit(double Limit)
        {
            if (mag() > Limit)
            {
                normalize();
                mult(Limit);
            }
            return this;
        }

        public PVector lerp(PVector toVector, float atm)
        {
            if (atm > 1) { atm = 1; }
            else if (atm < 0) { atm = 0; }

            x = (1 - atm) * this.x + atm * toVector.x;
            y = (1 - atm) * this.y + atm * toVector.y;
            z = (1 - atm) * this.z + atm * toVector.z;

            return this;
        }

        public PVector lerp(float x, float y, float z, float atm)
        {
            if (atm > 1) { atm = 1; }
            else if (atm < 0) { atm = 0; }

            x = (1 - atm) * this.x + atm * x;
            y = (1 - atm) * this.y + atm * y;
            z = (1 - atm) * this.z + atm * z;

            return this;
        }

        public PVector lerp(PVector fromVector, PVector toVector, double atm)
        {
            if (atm > 1) { atm = 1; }
            else if (atm < 0) { atm = 0; }

            x = (float)((1 - atm) * fromVector.x + atm * toVector.x);
            y = (float)((1 - atm) * fromVector.y + atm * toVector.y);
            z = (float)((1 - atm) * fromVector.z + atm * toVector.z);

            return this;
        }

        public float dot(PVector vector)
        {
            return PVector.Dot(this, vector);
        }

        public float dot(float x, float y, float z)
        {
            return PVector.Dot(this, new PVector(x, y, z));
        }

        public PVector copy()
        {
            return new PVector(x, y, z);
        }

        public float magsq()
        {
            float Mag = mag();
            return (Mag * Mag);
        }

        public PVector rotate(double theta)
        {
            fromAngle(heading() + theta);
            return this;
        }
        public float[] array()
        {
            return new float[]
            {
                x, y, z
            };
        }

        public PVector cross(PVector vector)
        {
            PVector temp = PVector.Cross(this, vector);
            x = temp.x;
            y = temp.y;
            z = temp.z;
            
            return this;
        }

        public System.Drawing.Point toPoint()
        {
            return new System.Drawing.Point((int)x, (int)y);
        }

        public PointF toPointF()
        {
            return new PointF(x, y);
        }

        public Vector toVector()
        {
            return new Vector(x, y);
        }

        public float[] Array
        {
            get { return new float[] { x, y, z }; }
        }

        public static PVector Add(PVector v1, PVector v2)
        {
            return new PVector(
                v1.x + v2.x,
                v1.y + v2.y,
                v1.z + v2.z);
        }

        public static PVector Cross(PVector v1, PVector v2)
        {
            return
               new PVector
               (
                  v1.y * v2.z - v1.z * v2.y,
                  v1.z * v2.x - v1.x * v2.z,
                  v1.x * v2.y - v1.y * v2.x
               );
        }

        public static float Dot(PVector v1, PVector v2)
        {
            return
            (
               v1.x * v2.x +
               v1.y * v2.y +
               v1.z * v2.z
            );
        }

        public static float Dist(PVector v1, PVector v2)
        {
            return
               sqrt
               (
                   (v1.X - v2.X) * (v1.X - v2.X) +
                   (v1.Y - v2.Y) * (v1.Y - v2.Y) +
                   (v1.Z - v2.Z) * (v1.Z - v2.Z)
               );
        }

        public static PVector FromAngle(double angle)
        {
            return new PVector().fromAngle(angle);
        }

        #region operators
        public static bool operator <(PVector v1, PVector v2)
        {
            return v1.mag() < v2.mag();
        }

        public static bool operator <=(PVector v1, PVector v2)
        {
            return v1.mag() <= v2.mag();
        }

        public static bool operator >(PVector v1, PVector v2)
        {
            return v1.mag() > v2.mag();
        }

        public static bool operator >=(PVector v1, PVector v2)
        {
            return v1.mag() >= v2.mag();
        }

        //public static bool operator ==(PVector v1, PVector v2)
        //{
        //    if (v1 == null && v2 == null)
        //    {
        //        return true;
        //    }
        //    else if (v1 != null && v1 == null)
        //    {
        //        return false;
        //    }
        //    return
        //        v1.x == v2.x &&
        //        v1.y == v2.y &&
        //        v1.z == v2.z;
        //}

        //public static bool operator !=(PVector v1, PVector v2)
        //{
        //    return !(v1 == v2);
        //}

        public static PVector operator *(PVector v1, double s2)
        {
            return
               new PVector
               (
                  v1.x * s2,
                  v1.y * s2,
                  v1.z * s2
               );
        }

        public static PVector operator *(double s1, PVector v2)
        {
            return v2 * s1;
        }

        

        #endregion
    }
}