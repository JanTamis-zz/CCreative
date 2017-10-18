using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using static CCreative.Math;

namespace CCreative
{
    public class PVector
    {
        float X, Y, Z;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The x component of the vector. </summary>
        ///
        /// <value> The x coordinate. </value>

        public float x
        {
            set { X = value; }
            get { return X; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The y component of the vector. </summary>
        ///
        /// <value> The y coordinate. </value>

        public float y
        {
            set { Y = value; }
            get { return Y; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The z component of the vector. </summary>
        ///
        /// <value> The z coordinate. </value>

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

        public PVector(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public PVector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        #region set

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets the x, y, and z component of the vector using three separate variables. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="x">    The x coordinate. </param>
        /// <param name="y">    The y coordinate. </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector set(float x, float y)
        {
            this.x = x;
            this.y = y;

            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets the x, y, and z component of the vector using three separate variables.  </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="x">    The x coordinate. </param>
        /// <param name="y">    The y coordinate. </param>
        /// <param name="z">    The z coordinate. </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector set(float x, float y, float z)
        {
            this.x = x;
            this.y = x;
            this.z = z;

            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets the x, y, and z component of the vector using three separate variables.  </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="vector">   The vector to set. </param>
        ///
        /// <returns>   this PVector. </returns>

        public PVector set(PVector vector)
        {
            this.x = vector.x;
            this.y = vector.y;
            this.z = vector.z;

            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Sets the x, y, and z component of the vector using three separate variables.
        /// </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="vector">   The point to set. </param>
        ///
        /// <returns>   this PVector. </returns>

        public PVector set(PointF vector)
        {
            this.x = vector.X;
            this.y = vector.Y;

            return this;
        }
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns a 2D unit vector with a random direction. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   A PVector. </returns>

        public PVector random2D()
        {
            x = random(float.MinValue, float.MaxValue);
            y = random(float.MinValue, float.MaxValue);

            normalize();

            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns a 3D unit vector with a random direction. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   A PVector. </returns>

        public PVector random3D()
        {
            x = random(float.MinValue, float.MaxValue);
            y = random(float.MinValue, float.MaxValue);
            z = random(float.MinValue, float.MaxValue);
            
            return this;
        }

        private float map(float value, float least, float max, float toMinimum, float toMaximum)
        {
            return toMinimum + (value - least) * (toMaximum - toMinimum) / (max - least);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Normalize the vector to length 1 (make it a unit vector). </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   A PVector. </returns>

        public PVector normalize()
        {
            float size = mag();

            x = x / size;
            y = y / size;
            z = z / size;

            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates and returns a new 2D unit vector from the specified angle value (in radians). </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="angle">    the angle in radians. </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector fromAngle(double angle)
        {
            x = (float)(1 * System.Math.Cos(angle * 2 * System.Math.PI / 360));
            y = (float)(1 * System.Math.Sin(angle * 2 * System.Math.PI / 360));
            
            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates and returns a new 2D unit vector from the specified angle value (in radians).  </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="angle">    the angle in radians. </param>
        /// <param name="vector">   the target vector. </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector fromAngle(float angle, PVector vector)
        {
            vector.x += (float)(mag() * System.Math.Cos(angle * 2 * System.Math.PI / 360));
            vector.y += (float)(mag() * System.Math.Sin(angle * 2 * System.Math.PI / 360));
            
            set(vector);

            return this;
        }

        #region add

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Adds x and y components to a vector, adds one vector to another. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="x">    The x coordinate. </param>
        /// <param name="y">    The y coordinate. </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector add(float x, float y)
        {
            this.x += x;
            this.y += y;
            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Adds x, y, and z components to a vector, adds one vector to another. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="x">    The x coordinate. </param>
        /// <param name="y">    The y coordinate. </param>
        /// <param name="z">    The z coordinate. </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector add(float x, float y, float z)
        {
            this.x += x;
            this.y += y;
            this.z += z;

            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Adds a vector to this vector. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="vector">   The vector to add. </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector add(PVector vector)
        {
            x += vector.x;
            y += vector.y;
            z += vector.z;

            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Adds two vectors to this vector. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="vector1">  The first vector. </param>
        /// <param name="vector2">  The second vector. </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector add(PVector vector1, PVector vector2)
        {
            x += vector1.x;
            y += vector1.y;
            z += vector1.z;

            x += vector2.x;
            y += vector2.y;
            z += vector2.z;
            

            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Adds a array of vectors to this vector. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="vectors">  The vectors to add. </param>
        ///
        /// <returns>   A PVector. </returns>

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

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Adds a list of vectors to the current vector. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="vectors">  The vectors to add. </param>
        ///
        /// <returns>   A PVector. </returns>

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

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Subtracts x and y components from a vector. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="x">    The x coordinate. </param>
        /// <param name="y">    The y coordinate. </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector sub(float x, float y)
        {
            this.x -= x;
            this.y -= y;

            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Subtracts x, y and z components from a vector. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="x">    The x coordinate. </param>
        /// <param name="y">    The y coordinate. </param>
        /// <param name="z">    The z coordinate. </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector sub(float x, float y, float z)
        {
            this.x -= x;
            this.y -= y;
            this.z -= z;

            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Subtracts a vector to this vector. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="vector">   The vector to set. </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector sub(PVector vector)
        {
            x -= vector.x;
            y -= vector.y;
            z -= vector.z;

            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Subtracts two vectors to this vector. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="vector1">  The first vector. </param>
        /// <param name="vector2">  The second vector. </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector sub(PVector vector1, PVector vector2)
        {
            x -= vector1.x;
            y -= vector1.y;
            z -= vector1.z;

            x -= vector2.x;
            y -= vector2.y;
            z -= vector2.z;


            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Subtracts a array of vector to this vector. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="vectors">  The vectors to add. </param>
        ///
        /// <returns>   A PVector. </returns>

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

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Subtracts a list of vectors to this vector. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="vectors">  The vectors to add. </param>
        ///
        /// <returns>   A PVector. </returns>

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

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Multiplies this vector by a scalar. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="multi">    The number to multiply with the vector. </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector mult(double multi)
        {
            this.x *= (float)multi;
            this.y *= (float)multi;
            this.z *= (float)multi;

            return this;
        }
        #endregion

        #region div

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Divides a vector by a scalar. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="multi">    The number to divide with the vector. </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector div(double multi)
        {
            this.x /= x;
            this.y /= y;

            return this;
        }
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the angle of rotation for a vector (2D vectors only). </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   The angle (in degrees). </returns>

        public float heading()
        {
            Vector vector1 = new Vector(0, 0);
            Vector vector2 = new Vector(x, y);
            float angle = Math.degrees((float)(System.Math.Atan2(vector2.Y, vector2.X) - System.Math.Atan2(vector1.Y, vector1.X)));
            if (angle < 0)
                angle = (angle + 180) + 180;
            return angle;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the Euclidean distance. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="vector">   The vector to calculate the distance with. </param>
        ///
        /// <returns>   A float. </returns>

        public float dist(PVector vector)
        {
            return (float)(System.Math.Sqrt(System.Math.Pow(this.x - vector.x, 2) + System.Math.Pow(this.y - vector.y, 2)));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the Euclidean distance. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="vector">   The point to calculate the distance with. </param>
        ///
        /// <returns>   A float. </returns>

        public float dist(PointF vector)
        {
            return (float)(System.Math.Sqrt(System.Math.Pow(this.x - vector.X, 2) + System.Math.Pow(this.y - vector.Y, 2)));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the magnitude (length) of the vector. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   A float. </returns>

        public float mag()
        {
            return sqrt(x * x + y * y + z * z);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Set the magnitude of this vector. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="len">  The set magnitude. </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector setMag(double len)
        {
            normalize();
            mult(len);
            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Set the magnitude of the given vector. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="target">   Target for the. </param>
        /// <param name="len">      The set magnitude. </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector setMag(PVector target, double len)
        {
            target.normalize();
            target.mult(len);
            set(target);

            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Limit the magnitude of this vector to the value used for the limit parameter. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="Limit">    The limit. </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector limit(float Limit)
        {
            if (mag() > Limit)
            {
                normalize();
                mult(Limit);
            }
            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates linear interpolation from one vector to another vector. (Just like regular lerp(), but for vectors). </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="toVector"> The vector to chance. </param>
        /// <param name="atm">      The amount of interpolation; some value between 0.0 (old vector) and 1.0 (new vector). </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector lerp(PVector toVector, float atm)
        {
            if (atm > 1) { atm = 1; }
            else if (atm < 0) { atm = 0; }

            x = (1 - atm) * this.x + atm * toVector.x;
            y = (1 - atm) * this.y + atm * toVector.y;
            z = (1 - atm) * this.z + atm * toVector.z;

            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>    Calculates linear interpolation for this vector (Just like regular lerp(), but for vectors). </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="x">    The x coordinate. </param>
        /// <param name="y">    The y coordinate. </param>
        /// <param name="z">    The z coordinate. </param>
        /// <param name="atm">  The amount of interpolation; some value between 0.0 (old vector) and 1.0 (new vector). </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector lerp(float x, float y, float z, float atm)
        {
            if (atm > 1) { atm = 1; }
            else if (atm < 0) { atm = 0; }

            x = (1 - atm) * this.x + atm * x;
            y = (1 - atm) * this.y + atm * y;
            z = (1 - atm) * this.z + atm * z;

            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates linear interpolation from one vector to another vector. (Just like regular lerp(), but for vectors).  </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="fromVector">   From vector. </param>
        /// <param name="toVector">     To vector. </param>
        /// <param name="atm">          The amount of interpolation; some value between 0.0 (old vector) and 1.0 (new vector). </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector lerp(PVector fromVector, PVector toVector, double atm)
        {
            if (atm > 1) { atm = 1; }
            else if (atm < 0) { atm = 0; }

            x = (float)((1 - atm) * fromVector.x + atm * toVector.x);
            y = (float)((1 - atm) * fromVector.y + atm * toVector.y);
            z = (float)((1 - atm) * fromVector.z + atm * toVector.z);

            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the dot product of this vector. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="vector">   The vector to calculate the dot product with. </param>
        ///
        /// <returns>   A float. </returns>

        public float dot(PVector vector)
        {
            return (this.x * vector.x) + (this.y * vector.y) + (this.z * vector.z);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the dot product of this vector. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="x">    The x coordinate. </param>
        /// <param name="y">    The y coordinate. </param>
        /// <param name="z">    The z coordinate. </param>
        ///
        /// <returns>   A float. </returns>

        public float dot(float x, float y, float z)
        {
            return (this.x * x) + (this.y * y) + (this.z * z);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Copies the components of the vector and returns the result as a PVector. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   A PVector. </returns>

        public PVector copy()
        {
            return new PVector(x, y, z);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates the magnitude (length) of the vector, squared. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   A float. </returns>

        public float magsq()
        {
            float Mag = mag();
            return (Mag * Mag);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Rotates a vector by the specified angle (2D vectors only), while maintaining the same magnitude. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="theta">    the angle of rotation. </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector rotate(double theta)
        {
            fromAngle(heading() + theta);
            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns a array of the x-y-z components. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   A array of the x-y-z component of this vector. </returns>

        public float[] array()
        {
            return new float[]
            {
                x, y, z
            };
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets the components to the cross product of this vector and the given vector. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="vector">   the vector to calculate the cross product with. </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector cross(PVector vector)
        {
            double x, y, z;
            x = this.y * vector.z - vector.y * this.z;
            y = (this.x * vector.z - vector.x * this.z) * -1;
            z = this.x * vector.y - vector.x * this.y;
            
            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets the components to the cross product of this vector and the given two vectors  </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="vector1">  The first vector. </param>
        /// <param name="vector2">  The second vector. </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector cross(PVector vector1, PVector vector2)
        {
            x = vector1.y * vector2.z - vector2.y * vector1.z;
            y = (vector1.x * vector2.z - vector2.x * vector1.z) * -1;
            z = vector1.x * vector2.y - vector2.x * vector1.y;

            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Calculates and returns a vector composed of the cross product between two vectors  </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <param name="vector1">      The first vector. </param>
        /// <param name="vector2">      The second vector. </param>
        /// <param name="safeVector">   PVector to store the result. </param>
        ///
        /// <returns>   A PVector. </returns>

        public PVector cross(PVector vector1, PVector vector2, PVector safeVector)
        {
            x = vector1.y * vector2.z - vector2.y * vector1.z;
            y = (vector1.x * vector2.z - vector2.x * vector1.z) * -1;
            z = vector1.x * vector2.y - vector2.x * vector1.y;

            safeVector = this;

            return this;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts this vector to a point. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   This vector as a System.Drawing.Point. </returns>

        public System.Drawing.Point toPoint()
        {
            return new System.Drawing.Point((int)x, (int)y);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts this vector to a pointF. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   This vector as a PointF. </returns>

        public PointF toPointF()
        {
            return new PointF(x, y);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts this vector to a WindowsBase vector. </summary>
        ///
        /// <remarks>   Jan Tamis, 29-8-2017. </remarks>
        ///
        /// <returns>   This object as a Vector. </returns>

        public Vector toVector()
        {
            return new Vector(x, y);
        }
    }
}