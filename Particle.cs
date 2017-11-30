using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using static CCreative.Colors;
using static CCreative.Math;
using static CCreative.Drawing;
using static CCreative.General;
using static CCreative.Data;
using CCreative;

namespace CCreative
{
    public class Particle
    {
        PVector pos;
        PVector vel;
        PVector acc;

        public Particle(float x, float y)
        {
            pos = new PVector(x, y);
            vel = new PVector(0, 0);
            acc = new PVector(0, 0);
        }

        public void applyForce(PVector force)
        {
            acc.add(force);
        }

        public void update()
        {
            vel.add(acc);
            pos.add(vel);
            acc.mult(0);
        }

        public void show()
        {
            point(pos.x, pos.y);
        }

        public void render()
        {
            update();
            show();
        }
    }
}
