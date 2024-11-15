using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
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
using System.Xml.Linq;
using UP1.AddWindows;

namespace UP1.TablePages
{
    /// <summary>
    /// Логика взаимодействия для RepairReportsPage.xaml
    /// </summary>
    public partial class RepairReportsPage : Page
    {
        UP1Entities UP = UP1Entities.GetContext();
        User _curuser;
        string name;
        AddReportWindow addReportWindow;
        public RepairReportsPage(User user)
        {
            InitializeComponent();
            DGridReports.Items.Clear();
            _curuser = user;
            name = _curuser.Role.Name;
            DGridReports.ItemsSource = UP.RepairReports.ToList();
        }

        private void CheckPositionWithUpdating()
        {
            DGridReports.ItemsSource = null;

            switch (name)
            {
                case "Менеджер":
                    AddReport.Visibility = Visibility.Hidden;
                    Delete.Visibility = Visibility.Hidden;
                    DGridReports.ItemsSource = UP.RepairWorks.ToList();
                    Buttons.Visibility = Visibility.Hidden;
                    break;
                case "Мастер":
                    foreach (var rep in UP.RepairReports)
                    {
                        if (rep.RepairWork.RepairRequest.Employee == _curuser)
                        {
                            DGridReports.Items.Add(rep);
                        }
                    }
                    break;
                case "Директор":
                    DGridReports.ItemsSource = UP.RepairReports.ToList();
                    break;
                case "Клиент":
                    foreach (var rep in UP.RepairReports)
                    {
                        if (rep.RepairWork.RepairRequest.Client == _curuser)
                        {
                            DGridReports.Items.Add(rep);
                        }
                    }
                    break;
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UP.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            CheckPositionWithUpdating();
        }

        private void AddReport_Click(object sender, RoutedEventArgs e)
        {
            addReportWindow = new AddReportWindow(null);
            addReportWindow.Show();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = DGridReports.SelectedItems.Cast<RepairReport>().ToList();
            if (selectedItems.Any())
            {
                foreach (var item in selectedItems.Select(itm => itm))
                {
                    UP.RepairReports.Remove(item);
                }
                UP.SaveChanges();
                CheckPositionWithUpdating();
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            UP.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            CheckPositionWithUpdating();
        }

        private void bEdit_Click(object sender, RoutedEventArgs e)
        {
            addReportWindow = new AddReportWindow((sender as Button).DataContext as RepairReport);
            addReportWindow.Show();
        }
    }
}
