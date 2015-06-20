using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using eco_design.BLL;
using HelixToolkit.Wpf;
using System.Windows.Forms;

namespace eco_design
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int MaxDistance = 30;
        private const int Range = 20;

        private double[] x1 { get; set; } //x1 == X1 = [X11, X12]

        private double[] X1 { get; set; } //X1 == X'1 = [X'11,X'12]

        private double[] x2 { get; set; } //x2 == X2 = [X21, X22]

        private double[] X2 { get; set; } //X2 == X'2 = [X'21; X'22]


        public double[,] A { get; set; } // 2 x 3

        public double[,] N { get; set; } // 2 x 3

        public double[,] T { get; set; } // 2 x 4

        public double[,] K { get; set; } // 2 x 8

        public double a12, aa12, aaa12;                                // a12=a12' aa12=a''12...предприятие 1
        public double a21, aa21, aaa21;                                // same предприятие 2
        public double nns, nfm, nim;                                   // предприятие 1   
        public double nns1, nfm1, nim1;                                // предприятие 2
        public double t11, t12, T21, T22;                              // t11=T11, T21=T'21..предприятие 1  
        public double t21, t22, T11, T12;                              // t21=T21, T11=T'11..предприятие 2
        public double k11, K11, k12, K12, k21, K21, k22, K22;          // k11=K-11, K11=K+11
        public double kk21, KK21, kk22, KK22, kk11, KK11, kk12, KK12;  // k11=K-11, K11=K+11

        public double x11, x12, X11max, X12max, X11min, X12min;
        public double x21, x22, X21max, X22max, X21min, X22min;

        private Algorithm _algorithm = new Algorithm();

        public MainWindow()
        {
            InitializeComponent();

            DrawCoordinates();

            DrawWire();
        }
            
        private void DrawWire()
            {
            for (int i = -MaxDistance; i <= MaxDistance; i++)
            {
                var tube = new TubeVisual3D();
                tube.Path = new Point3DCollection
                {
                    new Point3D(i, -MaxDistance, 0), 
                    new Point3D(i, MaxDistance, 0)
                };

                tube.Diameter = 0.02;
                tube.Fill = Brushes.DarkGray;
                tube.IsPathClosed = false;

                HelixViewport3D.Children.Add(tube);

                tube = new TubeVisual3D();
                tube.Path = new Point3DCollection
                {
                    new Point3D(-MaxDistance, i, 0), 
                    new Point3D(MaxDistance, i, 0)
                };

                tube.Diameter = 0.02;
                tube.Fill = Brushes.DarkGray;
                tube.IsPathClosed = false;

                HelixViewport3D.Children.Add(tube);
            }
        }

        private void DrawGraph(Func<double, double, double> function, Color functionColor,
            double xMin, double xMax, double yMin, double yMax)
        {
            var mas = new TubeVisual3D[2*Range];

            for (int x = -Range; x < Range; x++)
            {
                var point3DCollection = new Point3DCollection();

                for (int y = -Range; y < Range; y++)
                {
                    point3DCollection.Add(new Point3D(x, y, function(x, y)));
                }

                mas[x + Range] = new TubeVisual3D {Path = point3DCollection};
                mas[x + Range].Material = new DiffuseMaterial(new SolidColorBrush(functionColor));
                mas[x + Range].Diameter = 0.1;
            }

            foreach (var item in mas)
            {
                HelixViewport3D.Children.Add(item);
            }

            mas = new TubeVisual3D[40];

            for (int y = -Range; y < Range; y++)
            {
                var point3DCollection = new Point3DCollection();

                for (int x = -Range; x < Range; x++)
                {
                    point3DCollection.Add(new Point3D(x, y, function(x, y)));
                }

                mas[y + Range] = new TubeVisual3D {Path = point3DCollection};
                mas[y + Range].Material = new DiffuseMaterial(new SolidColorBrush(functionColor));
                mas[y + Range].Diameter = 0.1;
            }

            foreach (var item in mas)
            {
                HelixViewport3D.Children.Add(item);
            }
        }

        public void DrawAllGraphs()
        {
            HelixViewport3D.Children.Remove(HelixViewport3D.Children.Last());
            HelixViewport3D.Children.Remove(HelixViewport3D.Children.Last());

            DrawGraph(FSum12(GraphVariant.MaxAndMax), Colors.OrangeRed, 0, Range, 0, Range);
                // TODO change it to min and max
            DrawGraph(FSum21(GraphVariant.MaxAndMax), Colors.Cyan, 0, Range, 0, Range);
        }

        public Func<double, double, double> FSum12(GraphVariant variant)
        {
            Func<double, double, double> res = null;

            switch (variant) //TODO change to min and max
            {
                case GraphVariant.MinAndMin:
                    res = (x, y) =>
                    {
                        var xx1 = new[] {x11, x};
                        var xxs2 = new[] {X21, y};

                        return _algorithm.SumF(xx1, xxs2, Parameter.X12);
                    };
                    break;

                case GraphVariant.MinAndMax:
                    res = (x, y) =>
                    {
                        var xx1 = new[] {x11, x};
                        var xxs2 = new[] {X21, y};

                        return _algorithm.SumF(xx1, xxs2, Parameter.X12);
                    };
                    break;

                case GraphVariant.MaxAndMin:
                    res = (x, y) =>
                    {
                        var xx1 = new[] {x11, x};
                        var xxs2 = new[] {X21, y};

                        return _algorithm.SumF(xx1, xxs2, Parameter.X12);
                    };
                    break;

                case GraphVariant.MaxAndMax:
                    res = (x, y) =>
                    {
                        var xx1 = new[] {x11, x};
                        var xxs2 = new[] {X21, y};

                        return _algorithm.SumF(xx1, xxs2, Parameter.X12);
                    };
                    break;
            }

            return res;
        }

        public Func<double, double, double> FSum21(GraphVariant variant)
        {
            Func<double, double, double> res = null;

            switch (variant) //TODO change to min and max
            {
                case GraphVariant.MinAndMin:
                    res = (x, y) =>
                    {
                        var xx1 = new[] {x21, x};
                        var xxs2 = new[] {X11, y};

                        return _algorithm.SumF(xx1, xxs2, Parameter.X21);
                    };
                    break;

                case GraphVariant.MinAndMax:
                    res = (x, y) =>
                    {
                        var xx1 = new[] {x21, x};
                        var xxs2 = new[] {X11, y};

                        return _algorithm.SumF(xx1, xxs2, Parameter.X21);
                    };
                    break;

                case GraphVariant.MaxAndMin:
                    res = (x, y) =>
                    {
                        var xx1 = new[] { x21, x };
                        var xxs2 = new[] { X11, y };

                        return _algorithm.SumF(xx1, xxs2, Parameter.X21);
                    };
                    break;

                case GraphVariant.MaxAndMax:
                    res = (x, y) =>
                    {
                        var xx1 = new[] { x21, x };
                        var xxs2 = new[] { X11, y };

                        return _algorithm.SumF(xx1, xxs2, Parameter.X21);
                    };
                    break;
            }

            return res;
        }

        private void DrawCoordinates()
        {
            TubeVisual3D tube = new TubeVisual3D
            {
                Path = new Point3DCollection
                {
                    new Point3D(-MaxDistance, 0, 0),
                    new Point3D(MaxDistance, 0, 0)
                },
                Diameter = 0.1,
                Fill = Brushes.Red,
                IsPathClosed = false
            };

            HelixViewport3D.Children.Add(tube);

            tube = new TubeVisual3D
            {
                Path = new Point3DCollection
                {
                    new Point3D(0, -MaxDistance, 0),
                    new Point3D(0, MaxDistance, 0)
                },
                Diameter = 0.1,
                Fill = Brushes.Green,
                IsPathClosed = false
            };

            HelixViewport3D.Children.Add(tube);

            tube = new TubeVisual3D
            {
                Path = new Point3DCollection
        {
                    new Point3D(0, 0, -MaxDistance),
                    new Point3D(0, 0, MaxDistance)
                },
                Diameter = 0.1,
                Fill = Brushes.Blue,
                IsPathClosed = false
            };

            HelixViewport3D.Children.Add(tube);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                x11 = Convert.ToDouble(textBoxx11.Text);
                x12 = Convert.ToDouble(textBoxx12.Text);
                X11max = Convert.ToDouble(textBoxX11max.Text);
                X12max = Convert.ToDouble(textBoxX12max.Text);
                X11min = Convert.ToDouble(textBoxX11min.Text);
                X12min = Convert.ToDouble(textBoxX12min.Text);

                //if (x11 > 0 && x12 > 0 && X11max > 0 && X12max > 0 && X12min > 0 && X12min > 0)
                //{
                //    x1 = new[] { x11, x12 };
                //    X1 = new[] { X11, X12 };
                //}
                //else
                //    System.Windows.MessageBox.Show("Введите верные X11, X12, X'11, X'12");

                x21 = Convert.ToDouble(textBoxx21.Text);
                x22 = Convert.ToDouble(textBoxx22.Text);
                X21max = Convert.ToDouble(textBoxX21max.Text);
                X22max = Convert.ToDouble(textBoxX22max.Text);
                X21min = Convert.ToDouble(textBoxX21min.Text);
                X22min = Convert.ToDouble(textBoxX22min.Text);

                //if (x21 > 0 && x22 > 0 && X21 > 0 && X22 > 0)
                //{
                //    x2 = new[] { x21, x22 };
                //    X2 = new[] { X21, X22 };
                //}
                //else
                //    System.Windows.MessageBox.Show("Введите верные X11, X12, X'11, X'12");

                //предприятие 1
                a12 = Convert.ToDouble(textBoxA12.Text);
                aa12 = Convert.ToDouble(textBoxAA12.Text);
                aaa12 = Convert.ToDouble(textBoxAAA12.Text);
                //предприятие 2
                a21 = Convert.ToDouble(textBoxA21.Text);
                aa21 = Convert.ToDouble(textBoxAA21.Text);
                aaa21 = Convert.ToDouble(textBoxAAA21.Text);


                if (a12 > 0 && aa12 > 0 && aaa12 > 0 && a21 > 0 && aa21 > 0 && aaa21 > 0)
                {
                    A = new[,] {{a12, aa12, aaa12}, {a21, aa21, aaa21}};
                }
                else
                    System.Windows.MessageBox.Show("Введите верные A'21, A22...");
                
                //предприятие 1
                nns = Convert.ToDouble(textBoxNns.Text);
                nfm = Convert.ToDouble(textBoxNfm.Text);
                nim = Convert.ToDouble(textBoxNim.Text);
                //предприятие 2
                nns1 = Convert.ToDouble(textBoNns.Text);
                nfm1 = Convert.ToDouble(textBoNfm.Text);
                nim1 = Convert.ToDouble(textBoNim.Text);

                if (nns > 0 && nfm > 0 && nim > 0 && nns1 > 0 && nfm1 > 0 && nim1 > 0)
                {
                    N = new[,] {{nns, nfm, nim}, {nns1, nfm1, nim1}};
                }
                else
                    System.Windows.MessageBox.Show("Введите верные Nns, Nfm, Nim");
                
                //предприятие 1
                t11 = Convert.ToDouble(textBoxT11.Text);
                t12 = Convert.ToDouble(textBoxT12.Text);
                T21 = Convert.ToDouble(textBoxT21.Text);
                T22 = Convert.ToDouble(textBoxT22.Text);
                //предприятие 2
                t21 = Convert.ToDouble(textBoT21.Text);
                t22 = Convert.ToDouble(textBoT22.Text);
                T11 = Convert.ToDouble(textBoT11.Text);
                T12 = Convert.ToDouble(textBoT12.Text);

                if (t11 > 0 && t12 > 0 && T21 > 0 && T22 > 0 && t21 > 0 && t22 > 0 && T11 > 0 && T12 > 0)
                {
                    T = new[,] {{t11, t12, T21, T22}, {t21, t22, T11, T12}};
                }
                else
                    System.Windows.MessageBox.Show("Введите верные T");
                

                //предприятие 1
                k11 = Convert.ToDouble(textBoxk11.Text);
                K11 = Convert.ToDouble(textBoxk11p.Text);
                k12 = Convert.ToDouble(textBoxk12.Text);
                K12 = Convert.ToDouble(textBoxk12p.Text);
                k21 = Convert.ToDouble(textBoxk21.Text);
                K21 = Convert.ToDouble(textBoxk21p.Text);
                k22 = Convert.ToDouble(textBoxk22.Text);
                K22 = Convert.ToDouble(textBoxk22p.Text);

                //предприятие 2
                kk11 = Convert.ToDouble(textBok11.Text);
                KK11 = Convert.ToDouble(textBok11p.Text);
                kk12 = Convert.ToDouble(textBok12.Text);
                KK12 = Convert.ToDouble(textBok12p.Text);
                kk21 = Convert.ToDouble(textBok21.Text);
                KK21 = Convert.ToDouble(textBok21p.Text);
                kk22 = Convert.ToDouble(textBok22.Text);
                KK22 = Convert.ToDouble(textBok22p.Text);

                if (k11 > 0 && K11 > 0 && k12 > 0 && k21 > 0 && K21 > 0 && k22 > 0 && K22 > 0 
                    && kk21 > 0 && KK21 > 0 && kk22 > 0 && KK22 > 0 && kk11 > 0 && KK11 > 0 && kk12 > 0 && KK12 > 0)
                {
                    K = new[,]
                    {
                        {k11, K11, k12, K12, k21, K21, k22, K22},
                        {kk21, KK21, kk22, KK22, kk11, KK11, kk12, KK12}
                    };
                }
                else
                    System.Windows.MessageBox.Show("Введите верные K");


                _algorithm.T = T;
                _algorithm.A = A;
                _algorithm.N = N;
                _algorithm.K = K;

                DrawAllGraphs();
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Введите верные данные");
            }
        }
        }

    public enum GraphVariant
    {
        MinAndMin,
        MinAndMax,
        MaxAndMin,
        MaxAndMax
    }
}
