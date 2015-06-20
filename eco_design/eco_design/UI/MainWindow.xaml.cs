using eco_design.DAL;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HelixToolkit.Wpf;

namespace eco_design
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //HelixViewport3D.ResetCamera();

            //HelixViewport3D.Camera = new PerspectiveCamera
            //{
            //    Position = new Point3D(100, 100, 100),
            //    LookDirection = new Vector3D(-100, -100, -100),
            //    UpDirection = new Vector3D(0, 0, 1)
            //};

            TubeVisual3D tube = new TubeVisual3D();
            tube.Path = new Point3DCollection
            {
                new Point3D(0, 0, 0), 
                new Point3D(50, 0, 0)
            };
            tube.Diameter = 0.1;
            tube.Fill = Brushes.Red;
            tube.IsPathClosed = false;

            HelixViewport3D.Children.Add(tube);
            
            tube = new TubeVisual3D();
            tube.Path = new Point3DCollection
            {
                new Point3D(0, 0, 0), 
                new Point3D(0, 50, 0)
            };
            tube.Diameter = 0.1;
            tube.Fill = Brushes.Blue;
            tube.IsPathClosed = false;

            HelixViewport3D.Children.Add(tube);

            tube = new TubeVisual3D();
            tube.Path = new Point3DCollection
            {
                new Point3D(0, 0, 0), 
                new Point3D(0, 0, 50)
            };
            tube.Diameter = 0.1;
            tube.Fill = Brushes.Green;
            tube.IsPathClosed = false;

            HelixViewport3D.Children.Add(tube);

            for (int i = 0; i <= 50; i++)
            {
                tube = new TubeVisual3D();
                tube.Path = new Point3DCollection
                {
                    new Point3D(0, 0, 0), 
                    new Point3D(0, 0, 50)
                };

                tube.Diameter = 0.1;
                tube.Fill = Brushes.DarkGray;
                tube.IsPathClosed = false;

                HelixViewport3D.Children.Add(tube);

                tube = new TubeVisual3D();
                tube.Path = new Point3DCollection
                {
                    new Point3D(0, 0, 0), 
                    new Point3D(0, 0, 50)
                };

                tube.Diameter = 0.1;
                tube.Fill = Brushes.DarkGray;
                tube.IsPathClosed = false;

                HelixViewport3D.Children.Add(tube);

                tube = new TubeVisual3D();
                tube.Path = new Point3DCollection
                {
                    new Point3D(0, 0, 0), 
                    new Point3D(0, 0, 50)
                };

                tube.Diameter = 0.1;
                tube.Fill = Brushes.DarkGray;
                tube.IsPathClosed = false;

                HelixViewport3D.Children.Add(tube);
            }
        }
    }
}
