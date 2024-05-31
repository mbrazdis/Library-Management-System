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

namespace LibraryMB3.Commands
{
    /// <summary>
    /// Interaction logic for AuthorAddUpdate.xaml
    /// </summary>
    public partial class AuthorAddUpdate : Window
    {
        public AuthorAddUpdate()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public Author author;
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtFirstName.Text.Trim() == "")
            {
                MessageBox.Show("Please fill the Book Type name area!");
            }
            else
            {
                using (DbLibraryMbContext db = new DbLibraryMbContext())
                {
                    if (author != null && author.AuthorId != 0)
                    {
                        Author update = new Author();
                        update.AuthorId = author.AuthorId;
                        update.FirstName = txtFirstName.Text;
                        update.LastName = txtLastName.Text;
                        update.Birthdate = DateOnly.Parse(txtBirthDate.Text);
                        update.Active = true;
                        db.Authors.Update(update);
                        db.SaveChanges();
                        MessageBox.Show("Author was updated succesfully");
                        this.Close();

                    }
                    else
                    {
                        Author at = new Author();
                        at.FirstName = txtFirstName.Text;
                        at.LastName = txtLastName.Text;
                        at.Birthdate = DateOnly.Parse(txtBirthDate.Text);
                        at.Active = true;
                        db.Authors.Add(at);
                        db.SaveChanges();
                        new List<TextBox> { txtFirstName, txtLastName, txtBirthDate }.ForEach(tb => tb.Clear());
                        MessageBox.Show("Author was added succesfully!");
                        this.Close();
                    }
                }
            }
        }

        private void WindowAuthor_Loaded(object sender, RoutedEventArgs e)
        {
            if (author != null && author.AuthorId != 0)
            {
                txtFirstName.Text = author.FirstName.ToString();
                txtLastName.Text = author.LastName.ToString();
                txtBirthDate.Text = author.Birthdate.ToString();

            }
        }
    }
}
