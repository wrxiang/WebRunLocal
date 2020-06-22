using AutoUpdateService.Services;
using Fleck;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace AutoUpdateService
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AutoSizeColumn(dataGridView);

            string lisenerPort = ConfigurationManager.AppSettings["ListenerPort"];
            WsocketService.StartWebSocketService(lisenerPort);


            int index = this.dataGridView.Rows.Add();
            this.dataGridView.Rows[index].Cells[0].Value = false;
            this.dataGridView.Rows[index].Cells[1].Value = "2";
            this.dataGridView.Rows[index].Cells[2].Value = "监听";
            index = this.dataGridView.Rows.Add();
            this.dataGridView.Rows[index].Cells[0].Value = false;
            this.dataGridView.Rows[index].Cells[1].Value = "2";
            this.dataGridView.Rows[index].Cells[2].Value = "监听";
            index = this.dataGridView.Rows.Add();
            this.dataGridView.Rows[index].Cells[0].Value = false;
            this.dataGridView.Rows[index].Cells[1].Value = "2";
            this.dataGridView.Rows[index].Cells[2].Value = "监听";
            index = this.dataGridView.Rows.Add();
            this.dataGridView.Rows[index].Cells[0].Value = false;
            this.dataGridView.Rows[index].Cells[1].Value = "2";
            this.dataGridView.Rows[index].Cells[2].Value = "监听";
            index = this.dataGridView.Rows.Add();
            this.dataGridView.Rows[index].Cells[0].Value = false;
            this.dataGridView.Rows[index].Cells[1].Value = "2";
            this.dataGridView.Rows[index].Cells[2].Value = "监听";
            index = this.dataGridView.Rows.Add();
            this.dataGridView.Rows[index].Cells[0].Value = false;
            this.dataGridView.Rows[index].Cells[1].Value = "2";
            index = this.dataGridView.Rows.Add();
            this.dataGridView.Rows[index].Cells[0].Value = false;
            this.dataGridView.Rows[index].Cells[1].Value = "2";
            this.dataGridView.Rows[index].Cells[2].Value = "监听";
            index = this.dataGridView.Rows.Add();
            this.dataGridView.Rows[index].Cells[0].Value = false;
            this.dataGridView.Rows[index].Cells[1].Value = "2";
            this.dataGridView.Rows[index].Cells[2].Value = "监听";
            index = this.dataGridView.Rows.Add();
            this.dataGridView.Rows[index].Cells[0].Value = false;
            this.dataGridView.Rows[index].Cells[1].Value = "2";
            index = this.dataGridView.Rows.Add();
            this.dataGridView.Rows[index].Cells[0].Value = false;
            this.dataGridView.Rows[index].Cells[1].Value = "2";
            this.dataGridView.Rows[index].Cells[2].Value = "监听";
            index = this.dataGridView.Rows.Add();
            this.dataGridView.Rows[index].Cells[0].Value = false;
            this.dataGridView.Rows[index].Cells[1].Value = "2";
            this.dataGridView.Rows[index].Cells[2].Value = "监听";
            this.dataGridView.Rows[index].Cells[2].Value = "监听";
            this.dataGridView.Rows[index].Cells[2].Value = "监听";

        }

        /// <summary>
        /// 使DataGridView的列自适应宽度
        /// </summary>
        /// <param name="dgViewFiles"></param>
        private void AutoSizeColumn(DataGridView dgViewFiles)
        {
            int width = 0;
            //使列自使用宽度
            //对于DataGridView的每一个列都调整
            for (int i = 0; i < dgViewFiles.Columns.Count; i++)
            {
                //将每一列都调整为自动适应模式
                dgViewFiles.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                //记录整个DataGridView的宽度
                width += dgViewFiles.Columns[i].Width;
            }
            //判断调整后的宽度与原来设定的宽度的关系，如果是调整后的宽度大于原来设定的宽度，
            //则将DataGridView的列自动调整模式设置为显示的列即可，
            //如果是小于原来设定的宽度，将模式改为填充。
            if (width > dgViewFiles.Size.Width)
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            //冻结某列 从左开始 0，1，2
            dgViewFiles.Columns[1].Frozen = true;
        }




    }
}
