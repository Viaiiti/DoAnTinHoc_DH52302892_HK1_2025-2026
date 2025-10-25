using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace QuanLyKhoLinhKien
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Cấu hình DataGridView
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowHeadersVisible = false;
        }

        private void btnDocFile_Click(object sender, EventArgs e)
        {
            // Mở hộp thoại chọn file CSV
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            openFile.Title = "Chọn file CSV chứa dữ liệu Card đồ họa";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFile.FileName;
                DataTable data = DocCSV(filePath);
                dataGridView1.DataSource = data;
            }
        }

        // Hàm đọc CSV và trả về DataTable
        private DataTable DocCSV(string filePath)
        {
            DataTable dt = new DataTable();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                if (lines.Length > 0)
                {
                    // Tách tiêu đề (header)
                    string[] header = lines[0].Split(',');
                    foreach (string column in header)
                    {
                        dt.Columns.Add(column.Trim());
                    }

                    // Thêm dữ liệu từng dòng
                    for (int i = 1; i < lines.Length; i++)
                    {
                        string[] data = lines[i].Split(',');
                        if (data.Length == dt.Columns.Count)
                            dt.Rows.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đọc file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }
    }
}
