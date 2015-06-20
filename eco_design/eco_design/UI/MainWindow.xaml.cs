﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using eco_design.BLL;
using HelixToolkit.Wpf;

namespace eco_design
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int MaxDistance = 30;
        private const int Range = 20;

        private double[] x1 { get; set; }      //x1 == X1 = [X11, X12]

        private double[] X1 { get; set; }      //X1 == X'1 = [X'11,X'12]

        private double[] x2 { get; set; }      //x2 == X2 = [X21, X22]

        private double[] X2 { get; set; }      //X2 == X'2 = [X'21; X'22]


        public double[,] A { get; set; }                                // 2 x 3

        public double[,] N { get; set; }                                // 2 x 3

        public double[,] T { get; set; }                                // 2 x 4

        public double[,] K { get; set; }                                // 2 x 8

        private double a12, aa12, aaa12;                                // a12=a12' aa12=a''12...предприятие 1
        private double a21, aa21, aaa21;                                // same предприятие 2
        private double nns, nfm, nim;                                   // предприятие 1   
        private double nns1, nfm1, nim1;                                // предприятие 2
        private double t11, t12, T21, T22;                              // t11=T11, T21=T'21..предприятие 1  
        private double t21, t22, T11, T12;                              // t21=T21, T11=T'11..предприятие 2
        private double k11, K11, k12, K12, k21, K21, k22, K22;          // k11=K-11, K11=K+11
        private double kk21, KK21, kk22, KK22, kk11, KK11, kk12, KK12;  // k11=K-11, K11=K+11
        private double x11, x12, X11, X12;
        private double x21, x22, X21, X22;

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
            DrawGraph(FSum12(GraphVariant.MaxAndMax), Colors.OrangeRed, 0, Range, 0, Range);     // TODO change it to min and max
            DrawGraph(FSum21(GraphVariant.MaxAndMax), Colors.Cyan, 0, Range, 0, Range);
        }

        public Func<double, double, double> FSum12(GraphVariant variant)
        {
            Func<double, double, double> res = null;

            switch (variant)                                                    //TODO change to min and max
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
                        var xx1 = new[] { x11, x };
                        var xxs2 = new[] { X21, y };

                        return _algorithm.SumF(xx1, xxs2, Parameter.X12);
                    };
                    break;

                case GraphVariant.MaxAndMin:
                    res = (x, y) =>
                    {
                        var xx1 = new[] { x11, x };
                        var xxs2 = new[] { X21, y };

                        return _algorithm.SumF(xx1, xxs2, Parameter.X12);
                    };
                    break;

                case GraphVariant.MaxAndMax:
                    res = (x, y) =>
                    {
                        var xx1 = new[] { x11, x };
                        var xxs2 = new[] { X21, y };

                        return _algorithm.SumF(xx1, xxs2, Parameter.X12);
                    };
                    break;
            }

            return res;
        }

        public Func<double, double, double> FSum21(GraphVariant variant)
        {
            Func<double, double, double> res = null;

            switch (variant)                                                    //TODO change to min and max
            {
                case GraphVariant.MinAndMin:
                    res = (x, y) =>
                    {
                        var xx1 = new[] { x21, x };
                        var xxs2 = new[] { X11, y };

                        return _algorithm.SumF(xx1, xxs2, Parameter.X21);
                    };
                    break;

                case GraphVariant.MinAndMax:
                    res = (x, y) =>
                    {
                        var xx1 = new[] { x21, x };
                        var xxs2 = new[] { X11, y };

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
            TubeVisual3D tube = new TubeVisual3D();
            tube.Path = new Point3DCollection
            {
                new Point3D(-MaxDistance, 0, 0),
                new Point3D(MaxDistance, 0, 0)
            };
            tube.Diameter = 0.1;
            tube.Fill = Brushes.Red;
            tube.IsPathClosed = false;

            HelixViewport3D.Children.Add(tube);

            tube = new TubeVisual3D();
            tube.Path = new Point3DCollection
            {
                new Point3D(0, -MaxDistance, 0),
                new Point3D(0, MaxDistance, 0)
            };
            tube.Diameter = 0.1;
            tube.Fill = Brushes.Green;
            tube.IsPathClosed = false;

            HelixViewport3D.Children.Add(tube);

            tube = new TubeVisual3D();
            tube.Path = new Point3DCollection
            {
                new Point3D(0, 0, -MaxDistance),
                new Point3D(0, 0, MaxDistance)
            };
            tube.Diameter = 0.1;
            tube.Fill = Brushes.Blue;
            tube.IsPathClosed = false;

            HelixViewport3D.Children.Add(tube);
        }

        private double GetFunction(double x, double y)
        {

            return Math.Abs(0.1*(x*x - y*y));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            //if (textBoxA12 != null && textBoxAA12 != null && textBoxAA12 != null &&
            //    textBoxAAA12 != null && textBoxA21 != null && textBoxAA21 != null && textBoxAAA21 != null)

            x11 = Convert.ToDouble(textBoxx11.Text);
            x12 = Convert.ToDouble(textBoxx12.Text);
            X11 = Convert.ToDouble(textBoxX11.Text);
            X12 = Convert.ToDouble(textBoxX12.Text);

            x1 = new[] { x11, x12 };
            X1 = new[] { X11, X12 };

            x21 = Convert.ToDouble(textBoxx21.Text);
            x22 = Convert.ToDouble(textBoxx22.Text);
            X21 = Convert.ToDouble(textBoxX21.Text);
            X22 = Convert.ToDouble(textBoxX22.Text);

            x2 = new[] { x21, x22 };
            X2 = new[] { X21, X22 };

            //предприятие 1
            a12 = Convert.ToDouble(textBoxA12.Text);
            aa12 = Convert.ToDouble(textBoxAA12.Text);
            aaa12 = Convert.ToDouble(textBoxAAA12.Text);
            //предприятие 2
            a21 = Convert.ToDouble(textBoxA21.Text);
            aa21 = Convert.ToDouble(textBoxAA21.Text);
            aaa21 = Convert.ToDouble(textBoxAAA21.Text);

            A = new[,] { { a12, aa12, aaa12 }, { a21, aa21, aaa21 } };

            //предприятие 1
            nns = Convert.ToDouble(textBoxNns.Text);
            nfm = Convert.ToDouble(textBoxNfm.Text);
            nim = Convert.ToDouble(textBoxNim.Text);
            //предприятие 2
            nns1 = Convert.ToDouble(textBoNns.Text);
            nfm1 = Convert.ToDouble(textBoNfm.Text);
            nim1 = Convert.ToDouble(textBoNim.Text);

            N = new[,] { { nns, nfm, nim }, { nns1, nfm1, nim1 } };

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

            T = new[,] { { t11, t12, T21, T22 }, { t21, t22, T11, T12 } };

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

            K = new[,] { { k11, K11, k12, K12, k21, K21, k22, K22 },
                { kk21, KK21, kk22, KK22, kk11, KK11, kk12, KK12 } };

            _algorithm.T = T;
            _algorithm.A = A;
            _algorithm.N = N;
            _algorithm.K = K;

            DrawAllGraphs();
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
