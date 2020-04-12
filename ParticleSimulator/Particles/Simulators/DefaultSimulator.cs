using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ParticleSimulator.Particles.Simulators {
    
    /// <summary>
    /// This simulator initializes the particle system using a random distribution into the
    /// systems available area, and assign a random normalized direction vector to each particle.
    /// At every simulation step, all particles are moved according to their direction vector.
    /// If the movement results in a collision with either another particle, or with the edges,
    /// the direction is flipped in both particles if the collision was between particles, and 
    /// all affected particles changes to its next 'state'. The system stops when all particles
    /// arrive at their final state.
    /// </summary>
    public class DefaultSimulator : ParticleSystemSimulator {
        
        public DefaultSimulator(ParticleSystem ParticleSystem) : base(ParticleSystem) {}

        /// <summary>
        /// This method is called every step of the simulation
        /// </summary>
        protected override void SimulationStep() {

            // move all particles first
            foreach (var p1 in ParticleSystem.Particles) {
                p1.position += p1.direction;
            }

            // now check for collisions between particles
            foreach (var p1 in ParticleSystem.Particles) {
                // check for collision
                if ( p1.IsOutsideOf(ParticleSystem) ) { // collision with system edges
                    // invert direction
                    p1.direction *= -1;
                } else { // if no collisions with edges, check between particles
                    foreach (var p2 in ParticleSystem.Particles) {
                        if (ReferenceEquals(p1, p2)) continue; // avoid self collision
                        if (p1.CollidesWith(p2)) {
                            // invert both particles directions
                            p1.direction *= -1;
                            // trigger a state change
                            p1.NextState();
                        }
                    }
                }
            }
        }

    }
}
