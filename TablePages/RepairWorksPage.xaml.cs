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
using UP1.AddWindows;

namespace UP1.TablePages
{
    /// <summary>
    /// Логика взаимодействия для RepairWorksPage.xaml
    /// </summary>
    public partial class RepairWorksPage : Page
    {
        UP1Entities UP = UP1Entities.GetContext();
        User _curuser;
        AddWorkWindow addWorkWindow;
        public RepairWorksPage(User user)
        {
            InitializeComponent();
            _curuser = user;
            DGridWorks.ItemsSource = UP.RepairWorks.ToList();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UP.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
        }

        private void CheckPositionWithUpdating()
        {
            switch (_curuser.Role.Name)
            {
                case "Менеджер":
                    //AddWork.Visibility = Visibility.Hidden; кнопка добавить работу
                    //Delete.Visibility = Visibility.Hidden; кнопка удалить работу
                    DGridWorks.Items.Clear();
                    foreach (var work in UP.RepairWorks)
                    {
                        DGridWorks.Items.Add(work);
                    }
                    //Buttons.Visibility = Visibility.Hidden; кнопки редактирования
                    break;
                case "Мастер":
                    DGridWorks.Items.Clear();
                    foreach (var work in UP.RepairWorks)
                    {
                        if (work.RepairRequest.Employee == _curuser)
                        {
                            DGridWorks.Items.Add(_curuser);
                        }
                    }
                    break;
                case "Директор":
                    DGridWorks.Items.Clear();
                    foreach (var work in UP.RepairWorks)
                    {
                        DGridWorks.Items.Add(work);
                    }
                    break;
                case "Клиент":
                    foreach (var work in UP.RepairWorks)
                    {
                        if (work.RepairRequest.Client == _curuser)
                        {
                            DGridWorks.Items.Add(_curuser);
                        }
                    }
                    break;
            }
        }
    }
}
