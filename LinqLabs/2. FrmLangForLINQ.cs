using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLangForLINQ : Form
    {
        public FrmLangForLINQ()
        {
            InitializeComponent();
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int n = 100, n1 = 200;
            MessageBox.Show(n + " , " + n1);

            swap(ref n,ref n1);
            MessageBox.Show(n + " , " + n1);

            string s = "aaa", s1 = "bbb";
            MessageBox.Show(s + " , " + s1);

            swap(ref s, ref s1);
            MessageBox.Show(s + " , " + s1);
        }

        private void swap<T>(ref T n, ref T n1)
        {
            T a = n1;
            n1 = n;
            n = a;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //C# 1.0 具名方法
            //buttonX.Click += ButtonX_Click;
            buttonX.Click += new EventHandler(aaa);
            buttonX.Click += bbb;

            //C# 2.0 匿名方法
            buttonX.Click += delegate (object sender1, EventArgs e1)
              {
                  MessageBox.Show("C# 2.0 匿名方法");
              } ;

            //C# 3.0 匿名方法 lambda運算式
            buttonX.Click += (object sender1, EventArgs e1) =>
              {
                  MessageBox.Show("C# 3.0 lambda運算式");
              };
        }
        private void aaa(object sender, EventArgs e)
        {
            MessageBox.Show("aaa");
        }
        private void bbb(object sender, EventArgs e)
        {
            MessageBox.Show("bbb");
        }

        private void ButtonX_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ButtonX Click");
        }

        bool test(int n)
        {
            return n > 5;
        }
        bool test1(int n)
        {
            return n % 2 == 0;
        }

        //Step 1: create delegate 型別
        //Step 2: create delegate Object (new...)
        //Step 3: invoke / call method

        delegate bool MyDelegate(int n);
        private void button9_Click(object sender, EventArgs e)
        {
            bool result = test(4);
            MessageBox.Show("result = " + result);

            MyDelegate delegateObj = new MyDelegate(test);
            result= delegateObj.Invoke(7);
            MessageBox.Show("result = " + result);

            delegateObj = test1;
            result= delegateObj(3);
            MessageBox.Show("result = " + result);

            //C# 3.0 匿名方法 lambda運算式
            delegateObj = n => n > 5;
            result = delegateObj(1);
            MessageBox.Show("result = " + result);

        }
        List<int> MyWhere(int[] nums,MyDelegate delegateObj)
        {
            List<int> list = new List<int>();
            foreach(int n in nums)
            {
                if (delegateObj(n))
                {
                    list.Add(n);
                }
            }
            return list;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> list = MyWhere(nums, test1);
            string s = "";
            foreach(int n in list)
            {
                s += n.ToString()+" ";
            }
            MessageBox.Show(s);

            //===================
            List<int> oddList = MyWhere(nums, n => n % 2 == 1);
            List<int> evenList = MyWhere(nums, n => n % 2 == 0);
            foreach (int n in oddList)
            {
                listBox1.Items.Add(n);
            }
            foreach (int n in evenList)
            {
                listBox2.Items.Add(n);
            }
        }
        IEnumerable<int> MyIterator(int[] nums, MyDelegate delegateObj)
        {
            foreach (int n in nums)
            {
                if (delegateObj(n))
                {
                    yield return n;
                }
            }
        }
        private void button13_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            IEnumerable<int> q = MyIterator(nums, n => n > 5); //只是定義 並未call方法
            foreach(int n in q) //列舉時才去call方法
            {
                listBox1.Items.Add(n);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            IEnumerable<int> q = nums.Where(n => n > 5); 
            foreach (int n in q) 
            {
                listBox1.Items.Add(n);
            }
            //===================================================
            string[] words = { "aaa", "bbbbbbbbbb", "ccccccccc" };
            var q1 = words.Where(w => w.Length > 3);
            foreach(string w in q1)
            {
                listBox2.Items.Add(w);
            }
            dataGridView1.DataSource = q1.ToList();
            //===================================================
            productsTableAdapter1.Fill(nwDataSet1.Products);
            var q2 = nwDataSet1.Products.Where(r => r.UnitPrice > 30);
            dataGridView2.DataSource = q2.ToList();
        }

        private void button45_Click(object sender, EventArgs e)
        {

        }

        private void button41_Click(object sender, EventArgs e)
        {
            MyPoint pt1 = new MyPoint();
            pt1.P1 = 100;
            int w = pt1.P1;
            List<MyPoint> list = new List<MyPoint>();
            list.Add(new MyPoint(10));
            list.Add(new MyPoint(99, 888));

            list.Add(new MyPoint { P1 = 207, P2 = 818 });
            list.Add(new MyPoint { P2 = 319 });

            dataGridView1.DataSource = list;

            List<MyPoint> list2 = new List<MyPoint>()
            {
                new MyPoint()
                {
                    P1=389,
                    P2=425
                }
            };
            dataGridView2.DataSource = list2;
        }

        private void button43_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //var q = from n in nums
            //        where n > 5
            //        select new { N = n, S = n * n };
            var q = nums.Where(n => n > 5).Select(n => new { X = n, X2 = n * n, X3 = n * n * n });
            dataGridView1.DataSource = q.ToList();
            //==========================================

            productsTableAdapter1.Fill(nwDataSet1.Products);
            var q1 = from p in nwDataSet1.Products
                     where p.UnitPrice > 30
                     select new
                     {
                         ID = p.ProductID,
                         產品名稱 = p.ProductName,
                         p.UnitPrice,
                         p.UnitsInStock,//系統自動辨別欄位名
                         TotalPrice = $"{p.UnitPrice * p.UnitsInStock:c2}" //運算欄位或是格式化的欄位就需自訂欄位名, 系統無法辨別
                     };
            dataGridView2.DataSource = q1.ToList();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            string s1 = "abcd";
            int n1 = s1.WordCount();
            MessageBox.Show("WordCount = " + n1);

            string s2 = "123456789";
            int n2 = s2.WordCount();
            MessageBox.Show("WordCount = " + n2);
            //======================================

            MessageBox.Show("Char = " + s2.Chars(3));
        }
    }
    public static class MyString
    {
        //擴充方法必須定義在最上層靜態類別中, 不可寫在巢狀類別中
        public static int WordCount(this string s) //this代表誰call進這個方法
        {
            return s.Length;
        } 
        public static char Chars(this string s,int index) //第一個參數必須寫要擴充的型別
        {
            return s[index];
        }
    }
    public class MyPoint
    { 
        public MyPoint()
        {

        }
        public MyPoint(int p1)
        {
            this.P1 = p1;
        }
        public MyPoint(int p1,int p2)
        {
            this.P1 = p1;
            this.P2 = p2;
        }
        public MyPoint(string s)
        {
            
        }
        private int m_p1;
        public int P1
        {
            get
            {
                return m_p1;
            }
            set
            {
                m_p1 = value;
            }
        }
        public int P2 { get; set; }
    }

}
