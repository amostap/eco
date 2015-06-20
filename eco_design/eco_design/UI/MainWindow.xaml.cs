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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            CompanyContext c = new CompanyContext();
            c.Companies.Add(new Company() { CompanyName = "1" });
            c.SaveChanges();
        }

            private double[] x1 { get; set; }      //x1 == X1 = [X11, X12]

            private double[] X1 { get; set; }      //X1 == X'1 = [X'11,X'12]

            private double[] x2 { get; set; }      //x2 == X2 = [X21, X22]

            private double[] X2 { get; set; }      //X2 == X'2 = [X'21; X'22]

  
            public double[,] A { get; set; }

            public double[,] N { get; set; }                                // 2 x 3

            public double[,] T { get; set; }                                // 2 x 4

            public double[,] K { get; set; }                                // 2 x 8

            private double a12, aa12, aaa12;                                //a12=a12' aa12=a''12...предприятие 1
            private double a21, aa21, aaa21;                                //same предприятие 2
            private double nns, nfm, nim;                                   // предприятие 1   
            private double nns1, nfm1, nim1;                                // предприятие 2
            private double t11, t12, T21, T22;                              // t11=T11, T21=T'21..предприятие 1  
            private double t21, t22, T11, T12;                              // t21=T21, T11=T'11..предприятие 2
            private double k11, K11, k12, K12, k21, K21, k22, K22;          //k11=K-11, K11=K+11
            private double kk21, KK21, kk22, KK22, kk11, KK11, kk12, KK12;  //k11=K-11, K11=K+11
            private double x11, x12, X11, X12;
            private double x21, x22, X21, X22;


        private void button_Click(object sender, RoutedEventArgs e)
        {

            //if (textBoxA12 != null && textBoxAA12 != null && textBoxAA12 != null &&
            //    textBoxAAA12 != null && textBoxA21 != null && textBoxAA21 != null && textBoxAAA21 != null)
            //{
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

            a12 = Convert.ToDouble(textBoxA12.Text);
            aa12 = Convert.ToDouble(textBoxAA12.Text);
            aaa12 = Convert.ToDouble(textBoxAAA12.Text);
            a21 = Convert.ToDouble(textBoxA21.Text);
            aa21 = Convert.ToDouble(textBoxAA21.Text);
            aaa21 = Convert.ToDouble(textBoxAAA21.Text);

            A = new[,] { { a12, aa12, aaa12 }, { a21, aa21, aaa21 } };

            nns = Convert.ToDouble(textBoxNns.Text);
            nfm = Convert.ToDouble(textBoxNfm.Text);
            nim = Convert.ToDouble(textBoxNim.Text);
            nns1 = Convert.ToDouble(textBoNns.Text);
            nfm1 = Convert.ToDouble(textBoNfm.Text);
            nim1 = Convert.ToDouble(textBoNim.Text);

            N = new[,] { { nns, nfm, nim }, { nns1, nfm1, nim1 } };

            t11 = Convert.ToDouble(textBoxT11.Text);
            t12 = Convert.ToDouble(textBoxT12.Text);
            T21 = Convert.ToDouble(textBoxT21.Text);
            T22 = Convert.ToDouble(textBoxT22.Text);

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

        }
    }
}
