using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Starter
{
    public partial class FrmLINQ_To_XXX : Form
    {
        public FrmLINQ_To_XXX()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //IEnumerable<IGrouping<int, int>> q = from n in nums
            //                                     group n by n % 2;
            //加上Key的名稱
            //IEnumerable<IGrouping<string, int>> q = from n in nums
            //                                        group n by n % 2 == 0 ? "偶數" : "奇數";
            var q = nums.GroupBy(n => n % 2 == 0 ? "偶數" : "奇數");

            //DataGridView
            dataGridView1.DataSource = q.ToList();

            //TreeView
            foreach(var group in q)
            {
                TreeNode node = treeView1.Nodes.Add(group.Key.ToString());
                foreach(var items in group)
                {
                    node.Nodes.Add(items.ToString());
                }
            }

            //ListView
            foreach (var group in q)
            {
                ListViewGroup lvg = listView1.Groups.Add(group.Key.ToString(),group.Key.ToString());
                foreach (var items in group)
                {
                    listView1.Items.Add(items.ToString()).Group = lvg;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var q = from n in nums
                    group n by n % 2 == 0 ? "偶數" : "奇數" into g
                    select new
                    {
                        MyKey = g.Key,
                        MyMax = g.Max(),
                        MyMin = g.Min(),
                        MyCount = g.Count(),
                        MyAvg = g.Average(),
                        MySum = g.Sum(),
                        MyGroup = g
                    };

            //DataGridView
            dataGridView1.DataSource = q.ToList();

            //TreeView
            foreach (var group in q)
            {
                string s = $"{group.MyKey}({group.MyCount})";
                TreeNode node = treeView1.Nodes.Add(group.MyKey.ToString(),s);
                foreach (var items in group.MyGroup)
                {
                    node.Nodes.Add(items.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var q = from n in nums
                    group n by MyKey(n) into g
                    select new
                    {
                        MyKey = g.Key,
                        MyMax = g.Max(),
                        MyMin = g.Min(),
                        MyCount = g.Count(),
                        MyAvg = g.Average(),
                        MySum = g.Sum(),
                        MyGroup = g
                    };

            //DataGridView
            dataGridView1.DataSource = q.ToList();

            //TreeView
            foreach (var group in q)
            {
                string s = $"{group.MyKey}({group.MyCount})";
                TreeNode node = treeView1.Nodes.Add(group.MyKey.ToString(), s);
                foreach (var items in group.MyGroup)
                {
                    node.Nodes.Add(items.ToString());
                }
            }
            
            //Chart
            chart1.DataSource = q.ToList();

            chart1.Series[0].Name = "MyCount";
            chart1.Series[0].XValueMember = "MyKey";
            chart1.Series[0].YValueMembers = "MyCount";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            chart1.Series[1].Name = "MyAvg";
            chart1.Series[1].XValueMember = "MyKey";
            chart1.Series[1].YValueMembers = "MyAvg";
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


        }

        private string MyKey(int n)
        {
            if (n < 5) return "Small";
            else if (n < 10) return "Medium";
            else return "Large";
        }

        private void button38_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new DirectoryInfo(@"C:\windows");
            FileInfo[] files = dir.GetFiles();
            dataGridView1.DataSource = files;
            var q = from f in files
                    group f by f.Extension into g
                    orderby g.Count() descending
                    select new
                    {
                        MyKey = g.Key,
                        MyCount = g.Count(),
                        MyGroup = g
                    };
            //DataGridView
            dataGridView2.DataSource = q.ToList();
            //TreeView
            foreach (var group in q)
            {
                string s = $"{group.MyKey}({group.MyCount})";
                TreeNode node = treeView1.Nodes.Add(group.MyKey.ToString(), s);
                foreach (var items in group.MyGroup)
                {
                    node.Nodes.Add(items.ToString());
                }
            }
            //ListView
            foreach (var group in q)
            {
                ListViewGroup lvg = listView1.Groups.Add(group.MyKey.ToString(), group.MyKey.ToString());
                foreach (var items in group.MyGroup)
                {
                    listView1.Items.Add(items.ToString()).Group = lvg;
                }
            }
            //Chart
            chart1.DataSource = q.ToList();
            chart1.Series[0].Name = "MyCount";
            chart1.Series[0].XValueMember = "MyKey";
            chart1.Series[0].YValueMembers = "MyCount";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ordersTableAdapter1.Fill(nwDataSet1.Orders);
            var q = from o in nwDataSet1.Orders
                    group o by o.OrderDate.Year into g
                    orderby g.Key descending
                    select new
                    {
                        Year = g.Key,
                        Count = g.Count(),
                    };
            //DataGridView
            dataGridView1.DataSource = q.ToList();

            //1997共有幾筆
            int count = (from o in nwDataSet1.Orders
                         where o.OrderDate.Year == 1997
                         select o).Count();
            MessageBox.Show($"1997年的訂單一共有{count}筆");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new DirectoryInfo(@"C:\windows");
            FileInfo[] files = dir.GetFiles();
            int count = (from f in files
                         let s = f.Extension
                         where s == ".exe"
                         select f).Count();
            MessageBox.Show($".exe檔共有{count}個");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = "This is a pen. This is a book.   This is an apple.";
            char[] chars = { ' ', ',', '.' };
            string[] words = s.Split(chars,StringSplitOptions.RemoveEmptyEntries);
            var q = from w in words
                    group w by w.ToLower() into g
                    select new
                    {
                        g.Key,
                        count = g.Count()
                    };
            dataGridView1.DataSource = q.ToList();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int[] nums1 = { 1, 2, 3, 5, 11, 2 };
            int[] nums2 = { 1, 3, 66, 77, 111 };
            IEnumerable<int> q;

            q = nums1.Intersect(nums2);
            string nums = "int[] nums1 = { 1, 2, 3, 5, 11, 2 };\nint[] nums2 = { 1, 3, 66, 77, 111 };\n\n";
            string s = "";
            foreach (var i in q)
            {
                s += i + ", ";
            }
            MessageBox.Show(nums + "Intersect " + s);

            q = nums1.Distinct();
            s = "";
            foreach (var i in q)
            {
                s += i + ", ";
            }
            MessageBox.Show(nums + "Distinct " + s);

            q = nums1.Union(nums2);
            s = "";
            foreach (var i in q)
            {
                s += i + ", ";
            }
            MessageBox.Show(nums + "Union " + s);

            bool result;
            result = nums1.Any(n => n > 3);
            MessageBox.Show(nums + "Any(>3) is " + result);
            result = nums1.All(n => n > 1);
            MessageBox.Show(nums + "All(>1) is " + result);
            result = nums1.Contains(2);
            MessageBox.Show(nums + "Contains(2) is " + result);

            int n1;
            n1 = nums1.First();
            MessageBox.Show(nums + "First is " + n1);
            n1 = nums1.Last();
            MessageBox.Show(nums + "Last is " + n1);
            //n1 = nums1.ElementAt(13);
            n1 = nums1.ElementAtOrDefault(13);
            MessageBox.Show(nums + "ElementAtOrDefault(13) " + n1);

            //產生作業
            var q1 = Enumerable.Range(1, 1000).Select(n => new { n });
            dataGridView1.DataSource = q1.ToList();
            var q2 = Enumerable.Repeat(60, 1000).Select(n => new { n });
            dataGridView2.DataSource = q2.ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            productsTableAdapter1.Fill(nwDataSet1.Products);

            var q = from p in nwDataSet1.Products
                    group p by p.CategoryID into g
                    select new
                    {
                        CategoryID= g.Key,
                        Avg = $"{g.Average(p=>p.UnitPrice):C2}"
                    };

            dataGridView1.DataSource = q.ToList();

            //JOIN (太T-SQL)
            categoriesTableAdapter1.Fill(nwDataSet1.Categories);

            var q2= from p in nwDataSet1.Products join c in nwDataSet1.Categories
                    on p.CategoryID equals c.CategoryID
                    group p by c.CategoryName into g
                    select new
                    {
                        CategoryName = g.Key,
                        Avg = $"{g.Average(p => p.UnitPrice):C2}"
                    };

            dataGridView2.DataSource = q2.ToList();

        }

    }
}
