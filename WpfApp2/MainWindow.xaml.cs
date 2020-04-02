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
        public MainWindow()
        {
            
            InitializeComponent();
            sq.OpenConnection(sq.qwetext);
            DataTable data = new DataTable();
            data = sq.GetAllAsDataTable("select * from category");
            Grid1.ItemsSource = data.DefaultView;
            Category[] category;

            category = new Category[data.Rows.Count];
            for (int i = 0; i < data.Rows.Count; i++)
            {
                category[i] = new Category(int.Parse(data.Rows[i][0].ToString()), data.Rows[i][1].ToString(), int.Parse(data.Rows[i][2].ToString())); 
            }

            CategoryCollection categoryCollection = new CategoryCollection(category);

            foreach (Category item in categoryCollection)
            {
                item.initPath(categoryCollection);
                if (item.Path == "0")
                {
                    treeView1.Items.Add(item.getTree);
                }

            }
          

        }

        private void Window_Initialized(object sender, EventArgs e)
        {

        }
    }
}
