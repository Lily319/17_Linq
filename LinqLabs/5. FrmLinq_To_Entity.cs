using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLinq_To_Entity : Form
    {
        public FrmLinq_To_Entity()
        {
            InitializeComponent();

            Console.Write("xxx...open()...select*from...close()...");
            dbContext.Database.Log = Console.Write;
        }

        NorthwindEntities dbContext = new NorthwindEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            //加入新項目>>
            //(修改)MODEL按右鍵>>從資料庫更新模型
            NorthwindEntities dbContext = new NorthwindEntities();
            var q = from p in dbContext.Products
                    where p.UnitPrice > 30
                    select p;
            dataGridView1.DataSource = q.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dbContext.Categories.First().Products.ToList();
            MessageBox.Show(dbContext.Products.First().Category.CategoryName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            //MODEL按右鍵>>從資料庫更新模型>>加入預存程序>>會變成方法
            dataGridView1.DataSource = dbContext.Sales_by_Year(new DateTime(1997, 1, 1), DateTime.Now).ToList();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            var q = from p in dbContext.Products.AsEnumerable()
                    orderby p.UnitsInStock descending, p.ProductID
                    select new
                    {
                        p.ProductName,
                        p.UnitPrice,
                        p.UnitsInStock,
                        TotalPrice=$"{p.UnitPrice*p.UnitsInStock:c2}" //未加.AsEnumerable()時 無法格式化
                    };
            dataGridView1.DataSource = q.ToList();

            //等於以下

            var q1 = dbContext.Products.OrderByDescending(p => p.UnitsInStock).ThenBy(p => p.ProductID);
            dataGridView2.DataSource = q1.ToList();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            dataGridView3.DataSource = null;
            var q = dbContext.Products.Select(p => new
            {
                p.CategoryID,
                p.Category.CategoryName,
                p.ProductName,
                p.UnitPrice
            });
            dataGridView3.DataSource = q.ToList();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            //inner join
            var q = from c in dbContext.Categories
                    from p in c.Products
                    select new
                    {
                        c.CategoryID,
                        c.CategoryName,
                        p.ProductName,
                        p.UnitPrice
                    };
            dataGridView1.DataSource = q.ToList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            var q = from p in dbContext.Products.AsEnumerable()
                    group p by p.Category.CategoryName into g
                    select new
                    {
                        CategoryName = g.Key,
                        Avg = $"{g.Average(p => p.UnitPrice):c2}"
                    };
            dataGridView1.DataSource = q.ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            var q = dbContext.Orders.GroupBy(o => o.OrderDate.Value.Year).Select(g => new
            {
                Year = g.Key,
                Count = g.Count()
            });
            dataGridView1.DataSource = q.ToList();
        }

        private void button55_Click(object sender, EventArgs e)
        {
            Product prod = new Product { ProductName = DateTime.Now.ToLongTimeString(), Discontinued = true };
            dbContext.Products.Add(prod);//在記憶體
            dbContext.SaveChanges();//儲存變更到實體資料庫
        }
    }
}
