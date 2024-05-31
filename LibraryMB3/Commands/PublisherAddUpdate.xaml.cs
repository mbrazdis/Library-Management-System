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
using System.Windows.Shapes;
using LibraryMB3.Views;

namespace LibraryMB3.Commands
{
    /// <summary>
    /// Interaction logic for PublisherAddUpdate.xaml
    /// </summary>
    public partial class PublisherAddUpdate : Window
    {
        public PublisherAddUpdate()
        {
            InitializeComponent();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public Publisher publisher;
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Please fill all areas!");
            }
            else
            {
                using (DbLibraryMbContext db = new DbLibraryMbContext())
                {
                    if (publisher != null && publisher.PublisherId != 0)
                    {
                        Publisher update = new Publisher();
                        update.PublisherId = publisher.PublisherId;
                        update.Name = txtName.Text;
                        update.Adress = txtAdress.Text;
                        update.Active = true;
                        db.Publishers.Update(update);
                        db.SaveChanges();
                        MessageBox.Show("Publisher was updated succesfully");
                        this.Close();

                    }
                    else
                    {
                        Publisher pt = new Publisher();
                        pt.Name = txtName.Text;
                        pt.Adress = txtAdress.Text;
                        pt.Active = true;
                        db.Publishers.Add(pt);
                        db.SaveChanges();
                        new List<TextBox> { txtName, txtAdress }.ForEach(tb => tb.Clear());
                        MessageBox.Show("Book was added succesfully!");
                        this.Close();
                    }
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (publisher != null && publisher.PublisherId != 0)
            {
                txtName.Text = publisher.Name.ToString();
                txtAdress.Text = publisher.Adress.ToString();

            }
        }
    }
}
