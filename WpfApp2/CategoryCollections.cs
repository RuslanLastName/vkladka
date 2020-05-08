using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp2
{
    public class Category
    {
        int id;
        string name;
        int parent;
        TreeViewItem tree;

        public Category(int id, string name, int parent)
        {
            this.id = id;
            this.name = name;
            this.parent = parent;
            tree = new TreeViewItem();
            tree.Header = name;
        }

        public int Parent
        {
            get
            {
                return parent;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }



        public TreeViewItem getTree
        {
            get
            {
                return tree;
            }
        }
        
        public TreeViewItem setTree
        {
            set
            {
                tree = value;
            }
        }

        public void addItem(Category newItem)
        {
            id = newItem.ID;
            name = newItem.Name;
            parent = newItem.Parent;
            getTree.Items.Add(newItem.getTree);
        }

        public void dropTree(Category newTree)
        {
            getTree.Items.Remove(newTree.getTree);
        }

        public int ID
        {
            get
            {
                return id;
            }
        }

        public void initPath(CategoryCollection collection)
        {
            if (parent != 0)
            {
                foreach (Category item in collection)
                {
                    if (item.id == parent)
                    {
                        item.getTree.Items.Add(tree);
                        break;
                    }

                }
            }
        }

    }

    public class CategoryCollection : IEnumerable
    {
        private int lastid;
        private int lastItem;
        private List<Category> _Category;
        public CategoryCollection(Category[] categories)
        {
            _Category = new List <Category>(categories.Length);
            for (int i = 0; i < categories.Length; i++)
            {
                _Category.Add(categories[i]);
                lastid = categories[i].ID;
                
            }
            lastItem = categories.Length;
            Category free = null;
            _Category.Add(free);
        }

        public void addCategory(Category category)
        {
            _Category[lastItem] = category;
            Category free = null;
            _Category.Add(free);
            lastItem++;
            //_Category.Add(category);
        }

        public int lastID
        {
            get
            {
                return lastid;
            }
        }
        public IEnumerator GetEnumerator()
        {
            return new CategoryEnum(_Category);
        }
    }

    public class CategoryEnum: IEnumerator
    {
        //public Category[] _Category;
        public List<Category> _Category;

        int position = -1;

        //public CategoryEnum(Category[] list)
        //{
        //    _Category = list;
        //}

        public CategoryEnum(List<Category> category)
        {
            _Category = category;
        }

        public object Current
        {
            get
            {
                if (position == -1 || _Category == null)
                    throw new InvalidOperationException();
                return _Category[position];
            }
        }

        public bool MoveNext()
        {
            position++;
            return (_Category[position] != null);
            //return (position < _Category.Length);
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
