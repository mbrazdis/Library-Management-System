using LibraryMB3.Commands;
using LibraryMB3.Models;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LibraryMB3.Views
{
    /// <summary>
    /// Interaction logic for PublisherList.xaml
    /// </summary>
    public partial class PublisherList : UserControl
    {
        public PublisherList()
        {
            InitializeComponent();
            using (DbLibraryMbContext db = new DbLibraryMbContext())
            {
                List<Publisher> list = db.Publishers.ToList();
                GridPublisher.ItemsSource = list;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            PublisherAddUpdate Page = new PublisherAddUpdate();
            Page.ShowDialog();
            using (DbLibraryMbContext db = new DbLibraryMbContext())
            {
                List<Publisher> list = db.Publishers.ToList();
                GridPublisher.ItemsSource = list;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Publisher pt = (Publisher)GridPublisher.SelectedItem;
            PublisherAddUpdate page = new PublisherAddUpdate();
            page.publisher = pt;
            page.ShowDialog();
            using (DbLibraryMbContext db = new DbLibraryMbContext())
            {
                GridPublisher.ItemsSource = db.Publishers.OrderBy(x => x.PublisherId).ToList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning)
                == MessageBoxResult.Yes)
            {
                Publisher pt = (Publisher)GridPublisher.SelectedItem;
                using (DbLibraryMbContext db = new DbLibraryMbContext())
                {
                    pt.Active = false;
                    db.Publishers.Update(pt);
                    db.SaveChanges();
                    GridPublisher.ItemsSource = db.Publishers.OrderBy(x => x.PublisherId).ToList();
                }
            }
        }

    }
}
