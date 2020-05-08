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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        CategoryCollection categoryCollection;
        Category myItem = null;
        Category parentItem = null;
        int myID;
        int lastID;
        SqlConn sq = new SqlConn();
        TreeViewItem tree;
        string myText;

        public Window1(CategoryCollection categoryCollection)
        {
            InitializeComponent();
            this.categoryCollection = categoryCollection;
            sq.OpenConnection(sq.qwetext);
            lastID = this.categoryCollection.lastID;
            foreach (Category item in categoryCollection)
            {
                //item.initPath(categoryCollection);
                if (item.Parent == 0)
                {
                    treeView1.Items.Add(item.getTree);
                }
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (myItem != null && textBox1.Text != "")
            {
                myText = string.Format("insert into Category (Category_Name, Parent) values ('{0}', {1})", textBox1.Text, myItem.ID);
                sq.Sqltext(myText);
                tree = new TreeViewItem();
                tree.Header = textBox1.Text;
                lastID++;
                Category category = new Category(lastID, textBox1.Text, myItem.ID)
                {
                    setTree = tree
                };
                myItem.getTree.Items.Add(tree);
                //myItem.addItem(category);
                categoryCollection.addCategory(category);
                listView1.Items.Clear();
                foreach (Category item in categoryCollection)
                {
                    listView1.Items.Add(string.Format("{0}\t{1}\t{2}", item.ID, item.Name, item.Parent));
                }
            }

        }

        private void TreeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is TreeViewItem)
            {
                foreach (Category item in categoryCollection)
                {
                    if (item.getTree == ((TreeViewItem)e.NewValue))
                    {
                        myItem = item;
                        myID = item.ID;
                        textBox1.Text = string.Format("{0} + {1}", myItem.Name.ToString(), myID);
                        foreach (Category item2 in categoryCollection)
                        {
                            parentItem = null;
                            if (myItem.Parent == item2.ID)
                            {
                                parentItem = item2;
                                break;
                            }
                        } 
                        break;
                    }
                   
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            sq.CloseConnection();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (myItem != null)
            {
                if (parentItem != null)
                {
                    parentItem.dropTree(myItem);
                    myText = string.Format("delete from category where Category_Name = '{0}' and Parent = {1}", myItem.Name, myItem.Parent);
                    sq.Sqltext(myText);
                }
                else
                {
                    MessageBox.Show(string.Format("Нельзя удалить каталог: {0}", myItem.Name));
                }


                //myText = string.Format("delete from category where Category_Name = '{0}' and Parent = {1}", myItem.Name, myItem.Parent);
                //sq.Sqltext(myText);
                //if (parentItem == null)
                //{
                //    treeView1.Items.Remove(myItem.getTree);
                //    textBox1.Text = "yes";
                //}
                //else
                //{
                //    try
                //    {
                //        textBox1.Text = parentItem.Name;
                //        parentItem.dropTree(myItem);
                //    }
                //    catch (Exception)
                //    {
                //        textBox1.Text = "Error";
                //    }
                //}

            }
        }
    }
}
