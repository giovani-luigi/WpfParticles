using ParticleSimulator.Particles;
using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace ParticleSimulator.Controls {
    /// <summary>
    /// Interaction logic for ParticleCanvas.xaml
    /// </summary>
    public partial class ParticleCanvas : Canvas {

        private ParticleSystemSimulator simulator;

        public ParticleCanvas() {
            InitializeComponent();
        }

        public void Run(ParticleSystemSimulator simulator) {
            this.simulator = simulator;
            simulator.NewStep += OnNewSimulationStep;
            simulator.Start();
        }

        public void Stop() {
            if (simulator == null) return;
            simulator.Stop();
            simulator.NewStep -= OnNewSimulationStep;
            simulator = null;
        }        

        /// <summary>
        /// This is called every time a new step of the simulation executes, 
        /// so we can update the screen
        /// </summary>
        private void OnNewSimulationStep(object sender, EventArgs e) {
            Dispatcher.Invoke(new Action(() => { // dispatch to the UI thread
                InvalidateVisual();
            }));
        }

        protected override void OnRender(DrawingContext dc) {
            base.OnRender(dc);
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            if (simulator != null) {
                foreach (var p in simulator.ParticleSystem.Particles) {
                    p.Draw(dc);
                }
            }            
        }

    }
}
