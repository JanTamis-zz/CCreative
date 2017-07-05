using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;

namespace CCreative
{
    public class PVector
    {
        public float X, Y, Z;

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

        private void addTranslation()
        {
            x += Drawing.translate().X;
            y += Drawing.translate().Y;
        }

        public PVector()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public PVector(float x, float y)
        {
            this.x = x;
            this.y = y;

            addTranslation();
        }

        #region set
        public PVector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

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
            Random rand = new Random();
            x = map(rand.Next(-100, 100), -100, 100, -1, 1);
            y = map(rand.Next(-100, 100), -100, 100, -1, 1);

            return this;
        }

        public PVector random3D()
        {
            Random rand = new Random();
            x = map(rand.Next(-100, 100), -100, 100, -1, 1);
            y = map(rand.Next(-100, 100), -100, 100, -1, 1);
            z = map(rand.Next(-100, 100), -100, 100, -1, 1);
            

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

            addTranslation();
            return this;
        }

        public PVector add(float x, float y, float z)
        {
            this.x += x;
            this.y += y;
            this.z += z;
            addTranslation();

            return this;
        }

        public PVector add(PVector vector)
        {
            x += vector.x;
            y += vector.y;
            z += vector.z;
            addTranslation();

            return this;
        }

        public PVector add(PVector vector1, PVector vector2)
        {
            x += vector1.x;
            y += vector1.y;
            z += vector1.z;

            x += vector2.x;
            y += vector2.y;
            z += vector2.z;

            addTranslation();

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
            addTranslation();
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
            addTranslation();
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

        public PVector sub(PVector vector1, PVector vector2)
        {
            return vector1.set(vector1.x - vector2.x, vector1.y - vector2.y);
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

        public PVector limit(float Limit)
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
            return (this.x * vector.x) + (this.y * vector.y) + (this.z * vector.z);
        }

        public float dot(float x, float y, float z)
        {
            return (this.x * x) + (this.y * y) + (this.z * z);
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
            double x, y, z;
            x = this.y * vector.z - vector.y * this.z;
            y = (this.x * vector.z - vector.x * this.z) * -1;
            z = this.x * vector.y - vector.x * this.y;
            
            return this;
        }

        public PVector cross(PVector vector1, PVector vector2)
        {
            x = vector1.y * vector2.z - vector2.y * vector1.z;
            y = (vector1.x * vector2.z - vector2.x * vector1.z) * -1;
            z = vector1.x * vector2.y - vector2.x * vector1.y;

            return this;
        }

        public PVector cross(PVector vector1, PVector vector2, PVector safeVector)
        {
            x = vector1.y * vector2.z - vector2.y * vector1.z;
            y = (vector1.x * vector2.z - vector2.x * vector1.z) * -1;
            z = vector1.x * vector2.y - vector2.x * vector1.y;

            safeVector = this;

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
    }
}