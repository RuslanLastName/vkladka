using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Category
    {
        int id;
        string name;
        int parent;
        string path = "";

        public Category(int id, string name, int parent)
        {
            this.id = id;
            this.name = name;
            this.parent = parent;
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

        public string Path
        {
            get
            {
                return path;
            }
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
            if (parent == 0)
            {
                path += parent.ToString();
            }
            else
            {
                foreach (Category item in collection)
                {
                    if (item.id == parent)
                    {
                        path = item.path+ '/' + parent;
                        break;
                    }

                }
            }

        }
    }

    public class CategoryCollection : IEnumerable
    {

        private Category[] _Category;
        public CategoryCollection(Category[] categories)
        {
            _Category = new Category[categories.Length];
            for (int i = 0; i < categories.Length; i++)
            {
                _Category[i] = categories[i];
            }
        }


        public IEnumerator GetEnumerator()
        {
            return new CategoryEnum(_Category);
        }
    }

    public class CategoryEnum: IEnumerator
    {
        public Category[] _Category;

        int position = -1;

        public CategoryEnum(Category[] list)
        {
            _Category = list;
        }

        public object Current
        {
            get
            {
                if (position == -1 || position > _Category.Length)
                    throw new InvalidOperationException();
                return _Category[position];
            }
        }

        public bool MoveNext()
        {
            position++;
            return (position < _Category.Length);
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
