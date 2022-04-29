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
    public partial class FrmHelloLinq : Form
    {
        public FrmHelloLinq()
        {
            InitializeComponent();

            productsTableAdapter1.Fill(nwDataSet1.Products);
            ordersTableAdapter1.Fill(nwDataSet1.Orders);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            //public interface IEnumerable<T>
            //System.Collections.Generic 的成員
            //摘要:公開支援指定類型集合上簡單反覆運算的列舉值。

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //陣列和集合會時做此interface:IEnumerable<T>

            //語法糖:foreach
            foreach (int n in nums) //逐個取出陣列成員丟給變數n,in___一定要是陣列或集合才可列舉
            {
                listBox1.Items.Add(n);
            }

            //=============================================


            System.Collections.IEnumerator en = nums.GetEnumerator();  //取出此陣列的列舉值
            listBox1.Items.Add("GetEnumerator()↓ =====================");
            while (en.MoveNext())//MoveNext()回傳布林值 並讀下一行
            {
                listBox1.Items.Add(en.Current); //屬性Current{get;}
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            List<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach(int n in list)
            {
                this.listBox1.Items.Add(n);
            }
            //==============================================

            int a = 100;
            var a1 = 200;

            listBox1.Items.Add("GetEnumerator()↓ =====================");
            System.Collections.IEnumerator en = list.GetEnumerator();
            while (en.MoveNext())
            {
                listBox1.Items.Add(en.Current);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            //Step 1: define Data Source
            //Step 2: define query
            //Step 3: execute query

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<int> q = from n in nums
                                 where (n >= 5 && n <= 8) && (n % 2 == 0) //用&&第一項false第二項就不會執行, 用一個&兩邊條件都會執行
                                 select n;
            foreach(int n in q)
            {
                listBox1.Items.Add(n);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<int> q = from n in nums
                                 where IsEven(n)
                                 select n;
            foreach (int n in q)
            {
                listBox1.Items.Add(n);
            }
        }
        bool IsEven(int n)
        {
            return n % 2 == 0; //不用寫if/else敘述,本身結果就是true或false
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<Point> q = from n in nums
                                 where n>5
                                 select new Point(n,n*n);
            foreach (Point pt in q)
            {
                listBox1.Items.Add(pt.X+" , "+pt.Y);
            }

            //============================================

            List<Point> list = q.ToList();
            dataGridView1.DataSource = list;

            //===========================================

            chart1.DataSource = list;
            chart1.Series[0].XValueMember = "X";
            chart1.Series[0].YValueMembers = "Y";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string[] a = { "aaa", "Apple", "pineApple", "xxxapple" };
            IEnumerable<string> q = from w in a
                                    where w.ToLower().Contains("apple")// w.Contains("Apple")
                                    select w;
            foreach(string s in q)
            {
                listBox1.Items.Add(s);
            }

            dataGridView1.DataSource = q.ToList();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = nwDataSet1.Products;

            IEnumerable < global::LinqLabs.NWDataSet.ProductsRow > q= from p in nwDataSet1.Products
                                                                    where ! p.IsUnitPriceNull() && p.UnitPrice > 30 && p.ProductName.StartsWith("M")
                                                                    select p;
            dataGridView1.DataSource = q.ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = nwDataSet1.Orders;

            IEnumerable<global::LinqLabs.NWDataSet.OrdersRow> q = from o in nwDataSet1.Orders
                                                                    where o.OrderDate.Year==1997 && o.OrderDate.Month==1&&o.OrderDate.Month==2&&o.OrderDate.Month==3 //<4 
                                                                    orderby o.OrderDate descending
                                                                    select o;
            dataGridView1.DataSource = q.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
