using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ParticleSimulator.Particles {
    public class ParticleSystem {

        public IList<Particle> Particles { get; }

        public Size Size { get; }        

        public ParticleSystem(int n, Canvas canvas) {
            Size = canvas.RenderSize;
            Particles = new List<Particle>(n);
            for (int i = 0; i < n; i++) {
                Particles.Add(new Particle());
            }
            Randomize();
        }

        public void Randomize() {
            Random rnd = new Random();

            // initialize each particle in the system
            for (int i = 0; i < Particles.Count; i++) {
                Particle p = Particles[i];

                // assign a random starting position to each particle, ensuring that:
                // 1. the particle falls entirely inside the system region
                // 2. the particle does not intersect other particle already generated
                bool valid;
                do {
                    // generate the random position
                    p.position = new Vector2(
                        (float)(rnd.NextDouble() * Size.Width),
                        (float)(rnd.NextDouble() * Size.Height)
                        );

                    valid = true;
                    // test collision with previously generated
                    for (int j = 0; j < i; j++) {
                        if (p.CollidesWith(Particles[j])) {
                            valid = false; 
                            break;
                        }
                    }
                    // test for boundaries falling entirely within system region
                    if (p.IsOutsideOf(this)) 
                        valid = false;

                } while (!valid);                

                // assign a random direction vector to each particle
                do {
                    p.direction = new Vector2(
                        (float)(rnd.NextDouble() - 0.5f),
                        (float)(rnd.NextDouble() - 0.5f)
                        );
                } while (p.direction.Length() == 0); // prevent 0,0 dir. vector

                // normalize the direction vector (so all particles have same speed)
                p.direction = Vector2.Normalize(p.direction);
            }
        }

        internal Rect GetBoundingBox() {
            return new Rect(0, 0, (float) Size.Width, (float) Size.Height);
        }
    }
}
