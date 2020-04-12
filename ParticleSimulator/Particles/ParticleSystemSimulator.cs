using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleSimulator.Particles {
    public abstract class ParticleSystemSimulator {

        public delegate void NewStepEventHandler(object sender, EventArgs e);

        public event NewStepEventHandler NewStep;
        
        public readonly ParticleSystem ParticleSystem;

        private System.Timers.Timer RunningTimer;
        
        private int interval = 25;

        public int StepInterval {
            get { return interval; }
            set {
                // validate interval
                if (interval < 25) return;
                
                interval = value;

                // update timer if it is running
                if (RunningTimer != null) {
                    RunningTimer.Interval = interval;
                }
            }
        }

        public ParticleSystemSimulator(ParticleSystem ps) {
            this.ParticleSystem = ps;
        }

        public void Start() {
            
            if (RunningTimer != null) return;
            
            RunningTimer = new System.Timers.Timer(StepInterval);
            RunningTimer.Elapsed += RunningTimer_Elapsed;
            RunningTimer.Start();

            SimulationStarted();
        }

        protected virtual void SimulationStarted() {}

        public virtual void Stop() {
            
            if (RunningTimer == null) return;

            RunningTimer.Stop();
            RunningTimer.Dispose();
            RunningTimer = null;

            SimulationStopped();
        }

        protected virtual void SimulationStopped() {}

        private void RunningTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {

            // run simulation step routine
            SimulationStep();

            // report new step
            NewStep(this, new EventArgs());
        }

        protected abstract void SimulationStep();

    }
}
