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
using System.Diagnostics;
using System.Threading;

namespace Integral
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool flag = true;
        public MainWindow()
        {
            InitializeComponent();
            foto.Visibility = Visibility.Hidden;

        }
        int n;
        double a;
        double b;

        public bool Read_n()
        {
            n = Convert.ToInt32(this._n.Text);

            try
            {
                Convert.ToDouble(_n.Text);
            }
            catch
            {
                MessageBox.Show("Введите целое число");
                return false;
            }

            if (Convert.ToDouble(_n.Text) <= 0)
            {
                MessageBox.Show("Введите n больше 0");
                return false;
            }
            else
            {
                return true;
            }
        }


        public bool Read_A_B()
        {
            a = Convert.ToInt32(this._a.Text);
            b = Convert.ToInt32(this._b.Text);
            if (Convert.ToDouble(_a.Text) >= Convert.ToDouble(_b.Text))
            {
                //Здесь был cepera1029. Это типо коментарий...
                MessageBox.Show("Нижняя граница не должна превышать или быть равной верхней");
                return false;
            }
            else
            {
                return true;
            }
        }



        //Тут еще один коментарий...
        void Default()
        {
            _resultText.Text = "";
            _timeText.Text = "";


        }


        void Calculate()
        {
            ButtonCulc.IsEnabled = false;
            foto.Visibility = Visibility.Visible;
            Task task;
            Integral_calculate integral = new Integral_calculate();
            double In;
            Stopwatch timer = new Stopwatch();
            if (_check.IsChecked.Value == true)
            {
                task = Task.Run(() =>
                {
                    timer.Start();
                    In = integral.calcParr(n, a, b, x => 32 * x - Math.Log10(2 * x) - 41);
                    timer.Stop();

                    Dispatcher.Invoke(() =>
                    {
                        this._resultText.Text = Convert.ToString(In);
                        this._timeText.Text = Convert.ToString(timer.ElapsedMilliseconds);
                        ButtonCulc.IsEnabled = true;
                        foto.Visibility = Visibility.Hidden;
                    });

                    timer.Reset();
                });

            }
            if (_check.IsChecked.Value == false)
            {
                task = Task.Run(() =>
                {
                    timer.Start();
                    In = integral.calcPosl(n, a, b, x => 32 * x - Math.Log10(2 * x) - 41);
                    timer.Stop();
                    Dispatcher.Invoke(() =>
                    {
                        this._resultText.Text = Convert.ToString(In);
                        this._timeText.Text = Convert.ToString(timer.ElapsedMilliseconds);
                        ButtonCulc.IsEnabled = true;
                        foto.Visibility = Visibility.Hidden;
                    });

                    timer.Reset();
                });

            }
        }

        private void ButtonCulc_Click(object sender, RoutedEventArgs e)
        {
            if (Read_A_B() == false)
            {
                return;
            }
            if (Read_n() == false)
            {
                return;
            }
            Default();
            Calculate();
        }
        Posl_graph posl = new Posl_graph();
        Paral_graph paral = new Paral_graph();
        Bars bar = new Bars();


        private void PoslGraph_Click(object sender, RoutedEventArgs e)
        {
            if (Read_A_B() == false)
            {
                return;
            }
            if (Read_n() == false)
            {
                return;
            }
            posl.culc(a, b);
            posl.Show();

        }

        private void ParalGraph_Click(object sender, RoutedEventArgs e)
        {
            if (Read_A_B() == false)
            {
                return;
            }
            if (Read_n() == false)
            {
                return;
            }
            paral.culc(a, b);
            paral.Show();
        }

        private void barGraph_Click(object sender, RoutedEventArgs e)
        {
            if (Read_A_B() == false)
            {
                return;
            }
            if (Read_n() == false)
            {
                return;
            }
            bar.culc(a, b);
            bar.Show();

        }
    }
}
