using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibraryMB3.Models;
using LibraryMB3.ViewModel;


namespace LibraryMB3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBookClick(object sender, RoutedEventArgs e)
        {
            lblWindowName.Content = "Book List";
            DataContext = new BookViewModel();
        }

        private void btnPublisher_Click(object sender, RoutedEventArgs e)
        {
            lblWindowName.Content = "Publisher List";
            DataContext = new PublisherViewModel();
        }

        private void btnAuthor_Click(object sender, RoutedEventArgs e)
        {
            lblWindowName.Content = "Author List";
            DataContext = new AuthorViewModel();
        }

        private void btnBookTypeClick(object sender, RoutedEventArgs e)
        {
            lblWindowName.Content = "Book Types List";
            DataContext = new BookTypeViewModel();
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            lblWindowName.Content = "User List";
            DataContext = new UserViewModel();
        }
    }

}