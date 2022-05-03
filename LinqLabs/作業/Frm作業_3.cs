using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_3 : Form
    {
        public Frm作業_3()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            treeView1.Nodes.Clear();
            TreeNode node = null;
            foreach(int n in nums)
            {
                if (treeView1.Nodes[MyNumsKey(n)] == null)
                {
                    node = treeView1.Nodes.Add(MyNumsKey(n), MyNumsKey(n));
                    node.Nodes.Add(n.ToString());
                }
                else
                {
                    node.Nodes.Add(n.ToString());
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

        private void button38_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            treeView1.Nodes.Clear();
            DirectoryInfo dir = new DirectoryInfo(@"C:\windows");
            FileInfo[] files = dir.GetFiles();
            dataGridView1.DataSource = files.OrderByDescending(f => f.Length).ToList();
            var q = from f in files
                    orderby f.Length descending
                    group f by MyLengthKey(f) into g
                    select new
                    {
                        Length= g.Key,
                        Count= g.Count(),
                        Group=g
                    };
            dataGridView2.DataSource = q.ToList();

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
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            treeView1.Nodes.Clear();
            DirectoryInfo dir = new DirectoryInfo(@"C:\windows");
            FileInfo[] files = dir.GetFiles();
            dataGridView1.DataSource = files.OrderByDescending(f => f.CreationTime.Year).ToList();
            var q = from f in files
                    orderby f.CreationTime.Year descending
                    group f by f.CreationTime.Year into g
                    select new
                    {
                        Year = g.Key,
                        Count = g.Count(),
                        Group = g
                    };
            dataGridView2.DataSource = q.ToList();

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
    }
}
