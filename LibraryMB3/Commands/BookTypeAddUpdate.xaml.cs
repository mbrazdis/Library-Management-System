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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LibraryMB3.Commands
{
    /// <summary>
    /// Interaction logic for BookTypeAddUpdate.xaml
    /// </summary>
    public partial class BookTypeAddUpdate : Window
    {
        public BookTypeAddUpdate()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public BookType bookType;
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Please fill the Book Type name area!");
            }
            else
            {
                using (DbLibraryMbContext db = new DbLibraryMbContext())
                {
                    if (bookType != null && bookType.BookTypeId != 0)
                    {
                        BookType update = new BookType();
                        update.BookTypeId = bookType.BookTypeId;
                        update.Name = txtName.Text;
                        update.Active = true;
                        db.BookTypes.Update(update);
                        db.SaveChanges();
                        MessageBox.Show("Book Type was updated succesfully");
                        this.Close();

                    }
                    else
                    {
                        BookType btt = new BookType();
                        btt.Name = txtName.Text;
                        btt.Active = true;
                        db.BookTypes.Add(btt);
                        db.SaveChanges();
                        new List<TextBox> { txtName }.ForEach(tb => tb.Clear());
                        MessageBox.Show("Book Type was added succesfully!");
                        this.Close();
                    }
                }
            }
        }

        private void WindowBookType_Loaded(object sender, RoutedEventArgs e)
        {
            if (bookType != null && bookType.BookTypeId != 0)
            {
                txtName.Text = bookType.Name.ToString();
            }
        }
    }
}

