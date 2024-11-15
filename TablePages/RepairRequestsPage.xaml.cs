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
        UP1Entities UP = UP1Entities.GetContext();
        AddRequestWindow addRequestWindow;
        User _curuser;
        public RepairRequestsPage(User user)
        {
            InitializeComponent();
            _curuser = user;
            CheckPositionWithUpdating();
        }

        private void bEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckPositionWithUpdating()
        {
            DGridRequests.Items.Clear();
            if (_curuser.Role.Name == "Менеджер" || _curuser.Role.Name == "Директор")
            {
                foreach (var req in UP.RepairRequests)
                {
                    DGridRequests.Items.Add(req);
                }
            }
            else if (_curuser.Role.Name == "Мастер")
            {
                //AddRequest.Visibility = Visibility.Hidden;
                //Delete.Visibility = Visibility.Hidden;
                foreach (var req in UP.RepairRequests)
                {
                    if (req.Employee == _curuser)
                    {
                        DGridRequests.Items.Add(req);
                    }
                }
                Buttons.Visibility = Visibility.Hidden;
            }
            else if (_curuser.Role.Name == "Клиент")
            {
                foreach(var req in UP.RepairRequests)
                {
                    if (req.Client == _curuser)
                    {
                        DGridRequests.Items.Add(req);
                    }
                }
                Buttons.Visibility = Visibility.Hidden;
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UP.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            CheckPositionWithUpdating();
        }

        private void AddRequest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
