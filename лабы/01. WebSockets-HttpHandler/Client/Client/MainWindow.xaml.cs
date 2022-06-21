using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            result.Content = "";
            int x = int.Parse(X.Text);
            int y = int.Parse(Y.Text);

            result.Content = $"X + Y = {Get_Summ(x, y)}";
        }

        private static int Get_Summ(int x, int y)
        {
            int sum;
            WebRequest request = WebRequest.Create($"https://localhost:44340/mka/task4?X={x}&Y={y}");
            request.Method = "POST";
            request.ContentLength = 0;

            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    sum = int.Parse(reader.ReadToEnd());
                }
            }

            response.Close();

            return sum;
        }
    }
}
