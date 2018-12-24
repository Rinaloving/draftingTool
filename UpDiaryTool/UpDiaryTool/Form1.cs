using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpDiaryTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SFTPHelper sftp = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            sftp = new SFTPHelper("192.168.7.23", "22", "Admin", "123");
            showfile(sftp);

        }
        /// <summary>
        /// 拖动到控件区域
        /// </summary>
        private void show_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }
        /// <summary>
        /// 拖动到控件区域并松开鼠标
        /// </summary>
        private void up_DragEnter(object sender, DragEventArgs e)
        {

            string [] localpathlist = (e.Data.GetData(DataFormats.FileDrop) as string []);
            try
            {
                localpathlist.ToList().ForEach(n=> {
                    sftp.Put(n, "/biz/371400/BizMsg/" + Path.GetFileName(n));
                    showarea.Text = null;
                    showfile(sftp);
                });
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
            
        }

        public  void showfile(SFTPHelper sftp)
        {
            
            ArrayList list = sftp.GetFileList("/biz/371400/BizMsg/", "");
            //List<string> ls = new List<string>();
            //foreach (var item in list)
            //{
            //    ls.Add(item.ToString());
            //}
            //指定数据源
           // DataTable dt = ListToDatatableHelper.ToDataTable(ls);
           // this.dataGridView1.DataSource = dt;
            foreach (var item in list)
            {
                this.showarea.Text += item + "\r\n";
                
            }
            //设置文字默认是不被选中状态
            this.showarea.SelectionStart = 0;
            this.showarea.SelectionLength = 0;
        }
    }
}
