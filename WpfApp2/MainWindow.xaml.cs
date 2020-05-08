using System;
using System.Collections.Generic;
using System.Data;
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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConn sq = new SqlConn();
        DataTable data = new DataTable();
        Category[] category;
        CategoryCollection categoryCollection;
        bool winOpen = false;
        public MainWindow()
        {

            InitializeComponent();
            FillTreeView();

        }

        private void FillTreeView()
        {
            sq.OpenConnection(sq.qwetext);
            data = sq.GetAllAsDataTable("select * from category");
            Grid1.ItemsSource = data.DefaultView;

            category = new Category[data.Rows.Count];
            for (int i = 0; i < data.Rows.Count; i++)
            {
                category[i] = new Category(int.Parse(data.Rows[i][0].ToString()), data.Rows[i][1].ToString(), int.Parse(data.Rows[i][2].ToString()));
            }

            categoryCollection = new CategoryCollection(category);

            foreach (Category item in categoryCollection)
            {

                item.initPath(categoryCollection);
                if (item.Parent == 0)
                {
                    treeView1.Items.Add(item.getTree);
                }

            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {

        }

        private void TreeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is TreeViewItem)
            {
                Label1.Content = ((TreeViewItem)e.NewValue).Header.ToString();

                foreach (Category item in categoryCollection)
                {
                    if (item.getTree == ((TreeViewItem)e.NewValue))
                    {
                        Label2.Content = string.Format("{0} => {1}", item.ID, item.Parent);
                    }
                }
            }

        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            treeView1.Items.Clear();
            Window1 window1 = new Window1(categoryCollection);
            winOpen = true;
            window1.Show();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if (winOpen)
            {
                FillTreeView();
                winOpen = false;
            }
        }
    }
}
