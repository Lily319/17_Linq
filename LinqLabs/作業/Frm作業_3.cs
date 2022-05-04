using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_3 : Form
    {
        public Frm作業_3()
        {
            InitializeComponent();
        }

        private void AllClean()
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            treeView1.Nodes.Clear();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            AllClean();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 15, 14, 13, 12, 11, 10 };
            TreeNode node = null;
            int count = 0;
            foreach(int n in nums)
            {
                if (treeView1.Nodes[MyNumsKey(n)] == null)
                {
                    count = 0;
                    node = treeView1.Nodes.Add(MyNumsKey(n), MyNumsKey(n));
                    node.Nodes.Add(n.ToString());
                    count++;
                    node.Text = $"{MyNumsKey(n)}({count})";
                }
                else
                {
                    node.Nodes.Add(n.ToString());
                    count++;
                    node.Text = $"{MyNumsKey(n)}({count})";
                }
            }
            //TreeNode node = treeView1.Nodes.Add("Small");
            //foreach (int n in nums)
            //{
            //    if (n < 5)
            //    {
            //        node.Nodes.Add(n.ToString());
            //    }
            //}
            //node = treeView1.Nodes.Add("Medium");
            //foreach (int n in nums)
            //{
            //    if (n >= 5 && n < 10)
            //    {
            //        node.Nodes.Add(n.ToString());
            //    }
            //}
            //node = treeView1.Nodes.Add("Large");
            //foreach (int n in nums)
            //{
            //    if (n >=10)
            //    {
            //        node.Nodes.Add(n.ToString());
            //    }
            //}
        }
        private string MyNumsKey(int n)
        {
            if (n < 5) return "Small";
            else if (n >= 5 && n < 10) return "Medium";
            else return "Large";
        }

        IEnumerable<FileInfo> q1;
        int flag;
        private void button38_Click(object sender, EventArgs e)
        {
            AllClean();
            flag = 1;
            DirectoryInfo dir = new DirectoryInfo(@"C:\windows");
            FileInfo[] files = dir.GetFiles();
            var q = files.OrderByDescending(f => f.Length).GroupBy(f => MyLengthKey(f)).Select(g => new
            {
                Length = g.Key,
                Count = g.Count(),
                Group = g
            });
            dataGridView1.DataSource = q.ToList();

            q1 = files.Where(f => MyLengthKey(f) == dataGridView1.CurrentRow.Cells[0].Value).OrderByDescending(f=>f.Length);
            dataGridView2.DataSource = q1.ToList();
            foreach (var group in q)
            {
                string s = $"{group.Length}({group.Count})";
                TreeNode node = treeView1.Nodes.Add(group.Length.ToString(), s);
                foreach (var items in group.Group)
                {
                    node.Nodes.Add(items.ToString());
                }
            }
        }
        private object MyLengthKey(FileInfo f)
        {
            if (f.Length < 1000) return "Small";
            else if (f.Length < 50000) return "Medium";
            else return "Large";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AllClean();
            flag = 1;
            DirectoryInfo dir = new DirectoryInfo(@"C:\windows");
            FileInfo[] files = dir.GetFiles();
            var q = from f in files
                    orderby f.CreationTime.Year descending
                    group f by f.CreationTime.Year into g
                    select new
                    {
                        Year = g.Key,
                        Count = g.Count(),
                        Group = g
                    };
            dataGridView1.DataSource = q.ToList();
            q1 = files.Where(f => f.CreationTime.Year == (int)(dataGridView1.CurrentRow.Cells[0].Value)).OrderByDescending(f=>f.CreationTime.Year);
            dataGridView2.DataSource = q1.ToList();
            foreach (var group in q)
            {
                string s = $"{group.Year}({group.Count})";
                TreeNode node = treeView1.Nodes.Add(group.Year.ToString(), s);
                foreach (var items in group.Group)
                {
                    node.Nodes.Add(items.ToString());
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (flag == 1)
            {
                dataGridView2.DataSource = q1.ToList();
            }
            if (flag == 2)
            {
                dataGridView2.DataSource = q2.ToList();
            }
            if (flag == 3)
            {
                dataGridView2.DataSource = q3.ToList();
            }
        }

        NorthwindEntities dbContext = new NorthwindEntities();
        IEnumerable<Product> q2;
        private void button8_Click(object sender, EventArgs e)
        {
            AllClean();
            flag = 2;
            var q = dbContext.Products.AsEnumerable().GroupBy(p => MyUnitPriceKey(p)).Select(g => new
            {
                Key= g.Key,
                Count= g.Count(),
                Group=g
            });
            dataGridView1.DataSource = q.ToList();
            q2 = dbContext.Products.AsEnumerable().Where(p => MyUnitPriceKey(p) == dataGridView1.CurrentRow.Cells[0].Value);
            dataGridView2.DataSource = q2.ToList();

            foreach (var group in q)
            {
                string s = $"{group.Key}({group.Count})";
                TreeNode node = treeView1.Nodes.Add(group.Key.ToString(), s);
                foreach (var items in group.Group)
                {
                    node.Nodes.Add(items.ProductName);
                }
            }
        }
        private object MyUnitPriceKey(Product p)
        {
            if (p.UnitPrice < 20) return "Low";
            else if (p.UnitPrice > 50) return "High";
            else return "Medium";
        }

        IEnumerable<Order> q3;
        private void button15_Click(object sender, EventArgs e)
        {
            AllClean();
            flag = 3;
            var q = dbContext.Orders.AsEnumerable().GroupBy(o =>o.OrderDate.Value.Year).Select(g => new
            {
                Year = g.Key,
                Count = g.Count(),
                Group = g
            });
            dataGridView1.DataSource = q.ToList();
            q3 = dbContext.Orders.AsEnumerable().Where(o => o.OrderDate.Value.Year == (int)(dataGridView1.CurrentRow.Cells[0].Value));
            dataGridView2.DataSource = q3.ToList();

            foreach (var group in q)
            {
                string s = $"{group.Year}({group.Count})";
                TreeNode node = treeView1.Nodes.Add(group.Year.ToString(), s);
                foreach (var items in group.Group)
                {
                    node.Nodes.Add(items.OrderID.ToString());
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            AllClean();
            flag = 3;
            var q = dbContext.Orders.AsEnumerable().GroupBy(o => o.OrderDate.Value.ToString("yyyy年MM月")).Select(g => new
            {
                Month = g.Key,
                Count = g.Count(),
                Group = g
            });
            dataGridView1.DataSource = q.ToList();
            q3 = dbContext.Orders.AsEnumerable().Where(o => o.OrderDate.Value.ToString("yyyy年MM月") == dataGridView1.CurrentRow.Cells[0].Value.ToString());
            dataGridView2.DataSource = q3.ToList();

            foreach (var group in q)
            {
                string s = $"{group.Month}({group.Count})";
                TreeNode node = treeView1.Nodes.Add(group.Month.ToString(), s);
                foreach (var items in group.Group)
                {
                    node.Nodes.Add(items.OrderID.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AllClean();
            float SumTotalPrice = dbContext.Order_Details.AsEnumerable().Sum(od => (float)od.UnitPrice * od.Quantity * (1 - od.Discount));
            MessageBox.Show($"總銷售金額{SumTotalPrice}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AllClean();
            var q = (dbContext.Order_Details.AsEnumerable().GroupBy(od => $"{od.Order.Employee.FirstName} {od.Order.Employee.LastName}")
                .Select(g => new
                {
                    employee = g.Key,
                    performance = $"{g.Sum(od => (float)od.UnitPrice * od.Quantity * (1 - od.Discount)):C2}"
                }).OrderByDescending(g => decimal.Parse(g.performance,System.Globalization.NumberStyles.Currency))).Take(5);
            dataGridView1.DataSource = q.ToList();
            dataGridView1.AutoResizeColumns();
        }


        private void button9_Click(object sender, EventArgs e)
        {
            AllClean();
            var q = dbContext.Products.OrderByDescending(p => p.UnitPrice).Select(p=>new 
            {
                p.ProductName,
                p.Category.CategoryName,
                p.UnitPrice
            }).Take(5);
            dataGridView1.DataSource = q.ToList();
            dataGridView1.AutoResizeColumns();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AllClean();
            bool result = dbContext.Products.Any(p => p.UnitPrice > 300);
            MessageBox.Show(result ? "有" : "沒有");
        }
    }
}
