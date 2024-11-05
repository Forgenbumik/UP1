using UP1.AddWindows;

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


namespace UP1
{
    /// <summary>
    /// Логика взаимодействия для RepairRequestsPage.xaml
    /// </summary>
    public partial class RepairRequestsPage : Page
    {
        UP1Entities SE = UP1Entities.GetContext();
        AddRequestWindow AddRequestWindow;
        User _curuser;
        public RepairRequestsPage(User user)
        {
            InitializeComponent();
            _curuser = user;
        }

        private void bEdit_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
