using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ParticleSimulator.Particles {
    public class Particle {

        ParticleStates state = ParticleStates.White;
        
        public Vector2 position = Vector2.Zero;
        
        public Vector2 direction = Vector2.Zero;

        public Size size = new Size(10, 10);

        public Particle() {
        }

        public Particle(float width, float height) {
            size = new Size(width, height);
        }

        private Rect GetBoundingBox() {
            return new Rect(new Point(position.X, position.Y), size);
        }

        public static Color GetColorForState(ParticleStates state) {
            switch (state) {
                case ParticleStates.White: 
                    return Color.FromRgb(0xFF, 0xFF, 0xFF);
                case ParticleStates.Yellow:
                    return Color.FromRgb(241, 235, 58);
                case ParticleStates.Orange:
                    return Color.FromRgb(241, 177, 58);
                case ParticleStates.Green:
                    return Color.FromRgb(83, 211, 51);
                case ParticleStates.Red:
                    return Color.FromRgb(230, 46, 46);
                case ParticleStates.Purple:
                    return Color.FromRgb(217, 52, 220);
                case ParticleStates.Violet:
                    return Color.FromRgb(141, 52, 220);
                default:
                    return Color.FromRgb(0, 0, 0);
            }
        }

        public virtual void Draw(DrawingContext dc) { // default implementation (draw circle)
            var center = new Point(position.X, position.Y);
            var color = GetColorForState(state);
            dc.DrawRectangle(new SolidColorBrush(color), null, GetBoundingBox());
        }

        public virtual bool CollidesWith(Particle other) {
            return GetBoundingBox().IntersectsWith(other.GetBoundingBox());
        }

        internal void NextState() {
            if (state < ParticleStates.Black) 
                state += 1;
        }

        /// <summary>
        /// Determines whether the particle is outside or is crossing the particle system edges
        /// </summary>
        internal bool IsOutsideOf(ParticleSystem particleSystem) {

            var particle = GetBoundingBox();
            var system = particleSystem.GetBoundingBox();

            if (particle.Top < system.Top) return true;
            if (particle.Bottom > system.Bottom) return true;
            if (particle.Left < system.Left) return true;
            if (particle.Right > system.Right) return true;

            return false;   
        }
    }
}
