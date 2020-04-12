using ParticleSimulator.Particles.Simulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ParticleSimulator {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
        }

        private void Button_Start_Click(object sender, RoutedEventArgs e) {

            // setup the simulator
            var count = (int)Slider_ParticleCount.Value;
            var system = new Particles.ParticleSystem(count, CanvasView);
            var simulator = new DefaultSimulator(system);
            
            // start the simulation
            CanvasView.Run(simulator);
        }

        private void Button_Stop_Click(object sender, RoutedEventArgs e) {
            CanvasView.Stop();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            CanvasView.Stop();
        }
    }
}
