using QL_XEKHACH.DAO;
using QL_XEKHACH.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QL_XEKHACH.UserControls
{
    public partial class Page_QLXe : UserControl
    {
        public Page_QLXe()
        {
            InitializeComponent();
        }

        private void Page_QLXe_Load(object sender, EventArgs e)
        {
            lsvXe.Columns.Add("Mã Xe");
            lsvXe.Columns.Add("Biển số xe");
            lsvXe.Columns.Add("Trạng thái");
            lsvXe.Columns.Add("Loại xe");
            lsvXe.GridLines = true;
            lsvXe.FullRowSelect = true;
            loadDSXe();
            listLoaiXe.Columns.Add("Mã loại xe");
            listLoaiXe.Columns.Add("tên xe");
            listLoaiXe.Columns.Add("số ghế");
            listLoaiXe.GridLines = true;
            listLoaiXe.FullRowSelect = true;
            loadDSLoaiXe();
            load_LoaixeToComboBox();
        }
        void loadDSXe()
        {
            lsvXe.Items.Clear();
            List<Xe> XeList = XeDAO.Instance.GetALLXe();
            foreach (Xe xe in XeList)
            {
                ListViewItem item = new ListViewItem(xe.MaXe.ToString());
                item.SubItems.Add(xe.BienSoXe.ToString());
                item.SubItems.Add(xe.TrangThai.ToString());
                item.SubItems.Add(xe.MaLoaiXe.ToString());
                lsvXe.Items.Add(item);
            }
            foreach (ColumnHeader column in lsvXe.Columns)
            {
                column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                int headerWidth = TextRenderer.MeasureText(column.Text, lsvXe.Font).Width + 10;
                column.Width = Math.Max(column.Width, headerWidth);
            }
        }
        void loadDSLoaiXe()
        {
            listLoaiXe.Items.Clear();
            List<LoaiXe> XeList = LoaiXeDAO.Instance.GetALLLoaiXe();
            foreach (LoaiXe Loaixe in XeList)
            {
                ListViewItem item = new ListViewItem(Loaixe.MaLoaixe.ToString());
                item.SubItems.Add(Loaixe.TenLoaiXe.ToString());
                item.SubItems.Add(Loaixe.SoGhe.ToString());

                listLoaiXe.Items.Add(item);
            }
            foreach (ColumnHeader column in listLoaiXe.Columns)
            {
                column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                int headerWidth = TextRenderer.MeasureText(column.Text, listLoaiXe.Font).Width + 10;
                column.Width = Math.Max(column.Width, headerWidth);
            }
        }
        void load_LoaixeToComboBox()
        {
            List<LoaiXe> listLoaiXe = LoaiXeDAO.Instance.GetALLLoaiXe();
            cbxLoaixe.DataSource = listLoaiXe;
            cbxLoaixe.DisplayMember = "tenLoaiXe";
            cbxLoaixe.ValueMember = "maLoaixe";
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string biensoxe = txtBienSoXe.Text;
            string trangthai = cbxTrangThai.Text;
            LoaiXe selectedLoaixe = (LoaiXe)cbxLoaixe.SelectedItem;
            int maloaixe = selectedLoaixe.MaLoaixe;
            string tenloaixe = selectedLoaixe.TenLoaiXe;
            if (string.IsNullOrEmpty(trangthai) || string.IsNullOrEmpty(biensoxe) || maloaixe==0)
            {
                MessageBox.Show("Vui lòng điền các thông tin cần thiết để thêm xe!");
                return;
            }    
            DialogResult result = MessageBox.Show("Xác nhận thêm xe có biển số là: " + biensoxe + " loại:" + tenloaixe + " không ? ", "Xác nhận", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                bool kq = XeDAO.Instance.InsertXe(biensoxe, trangthai, maloaixe);
                if (kq == true)
                {
                    MessageBox.Show("Thêm xe thành công ");
                    loadDSXe();
                }
                else
                {
                    MessageBox.Show("Thêm xe thất bại ");
                }
            }
            else
            {
                return;
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            string MAXE = txtMaXe.Text;
            string BIENSOXE = txtBienSoXe.Text;
            string TRANGTHAI = cbxTrangThai.Text;
            LoaiXe selectedLoaixe = (LoaiXe)cbxLoaixe.SelectedItem;
            int maloaixe = selectedLoaixe.MaLoaixe;
            if (string.IsNullOrEmpty(MAXE))
            {
                MessageBox.Show("Vui lòng cung cấp mã xe để cập nhật thông tin xe!");
                return;
            }
            DialogResult result = MessageBox.Show("Xác nhận cập nhật thông tin xe? ", "Xác nhận", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                bool kq = XeDAO.Instance.UpdateXe(MAXE, BIENSOXE, TRANGTHAI, maloaixe);
                if (kq == true)
                {
                    MessageBox.Show("Cập nhật thành công ");
                    loadDSXe();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại ");
                }
            }
            else return;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string MAXE = txtMaXe.Text;
            if (string.IsNullOrEmpty(MAXE))
            {
                MessageBox.Show("Vui lòng cung cấp mã xe để cập nhật thông tin xe!");
                return;
            }
            DialogResult result = MessageBox.Show("Xác nhận xoá xe? ", "Xác nhận", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                bool kq = XeDAO.Instance.DeleteXe(MAXE);
                if (kq == true)
                {
                    MessageBox.Show("Xóa thành công ");
                    loadDSXe();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại ");
                }
            }
            else { return; }
        }
        private void SelectComboBoxValue<T>(ComboBox comboBox, Func<T, string> valueSelector, string valueToSelect)
        {
            foreach (var item in comboBox.Items)
            {
                if (item is T yourObject && valueSelector(yourObject) == valueToSelect)
                {
                    comboBox.SelectedItem = item;
                    break;
                }
            }
        }
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            loadDSXe();
        }
        private void lsvXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvXe.SelectedItems.Count > 0)
            {
                txtMaXe.Text = lsvXe.SelectedItems[0].Text;
                txtBienSoXe.Text = lsvXe.SelectedItems[0].SubItems[1].Text;
                cbxTrangThai.Text = lsvXe.SelectedItems[0].SubItems[2].Text;
                string maloaixe = lsvXe.SelectedItems[0].SubItems[3].Text;
                SelectComboBoxValue(cbxLoaixe, (LoaiXe loaixe) => loaixe.MaLoaixe.ToString(), maloaixe);

            }
        }
        private void listLoaiXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listLoaiXe.SelectedItems.Count > 0)
            {
                string tenloaixe = listLoaiXe.SelectedItems[0].SubItems[1].Text;
                SelectComboBoxValue(cbxLoaixe, (LoaiXe loaixe) => loaixe.TenLoaiXe, tenloaixe);
            }    
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            int MAXE = 0;
            if (!string.IsNullOrEmpty(txtMaXe.Text))
            {
                MAXE = int.Parse(txtMaXe.Text);
            }    
            string BIENSOXE = txtBienSoXe.Text;
            string TRANGTHAI = cbxTrangThai.Text;
            LoaiXe selectedLoaixe = (LoaiXe)cbxLoaixe.SelectedItem;
            int maloaixe = selectedLoaixe.MaLoaixe;
            lsvXe.Items.Clear();
            List<Xe> XeList = XeDAO.Instance.GetALLListXeTimKiem(MAXE, BIENSOXE, TRANGTHAI, maloaixe);
            foreach (Xe xe in XeList)
            {
                ListViewItem item = new ListViewItem(xe.MaXe.ToString());
                item.SubItems.Add(xe.BienSoXe.ToString());
                item.SubItems.Add(xe.TrangThai.ToString());
                item.SubItems.Add(xe.MaLoaiXe.ToString());
                lsvXe.Items.Add(item);
            }
            foreach (ColumnHeader column in lsvXe.Columns)
            {
                column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                int headerWidth = TextRenderer.MeasureText(column.Text, lsvXe.Font).Width + 10;
                column.Width = Math.Max(column.Width, headerWidth);
            }
        }
    }   
}
