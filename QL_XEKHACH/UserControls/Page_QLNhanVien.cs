using QL_XEKHACH.DAO;
using QL_XEKHACH.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QL_XEKHACH.UserControls
{
    public partial class Page_QLNhanVien : UserControl
    {
        public Page_QLNhanVien()
        {
            InitializeComponent();
        }

        private void Page_QLNhanVien_Load(object sender, EventArgs e)
        {
            lsvNV.Columns.Add("Mã nhân viên");
            lsvNV.Columns.Add("Tên nhân viên");
            lsvNV.Columns.Add("Tên đăng nhập");
            lsvNV.Columns.Add("Ngày sinh");
            lsvNV.Columns.Add("Giới tính");
            lsvNV.Columns.Add("Địa chỉ");
            lsvNV.Columns.Add("CMND");
            lsvNV.Columns.Add("Số điện thoại");
            lsvNV.Columns.Add("Email");
            lsvNV.Columns.Add("Trạng thái");
            lsvNV.Columns.Add("Mã Quyền");
            lsvNV.GridLines = true;
            lsvNV.FullRowSelect = true;
            loadDSNhanVien();
        }

        void loadDSNhanVien()
        {
            lsvNV.Items.Clear();
            List<NhanVien> nhanVienList = NhanVienDAO.Instance.GetAllNhanVien();
            foreach (NhanVien nhanVien in nhanVienList)
            {
                ListViewItem item = new ListViewItem(nhanVien.MaNV.ToString());
                item.SubItems.Add(nhanVien.TenNV.ToString());
                item.SubItems.Add(nhanVien.UserName.ToString());
                item.SubItems.Add(nhanVien.NgaySinh.ToString());
                item.SubItems.Add(nhanVien.GioiTinh.ToString());
                item.SubItems.Add(nhanVien.DiaChi.ToString());
                item.SubItems.Add(nhanVien.CMND.ToString());
                item.SubItems.Add(nhanVien.SDT.ToString());
                item.SubItems.Add(nhanVien.Email.ToString());
                item.SubItems.Add(nhanVien.TrangThai.ToString());
                item.SubItems.Add(nhanVien.MaQuyen.ToString());
                lsvNV.Items.Add(item);
            }
            foreach (ColumnHeader column in lsvNV.Columns)
            {
                column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                int headerWidth = TextRenderer.MeasureText(column.Text, lsvNV.Font).Width + 10;
                column.Width = Math.Max(column.Width, headerWidth);
            }
        }



        private void btnEdit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xác nhập cập nhật thông tin nhân viên ", "Xác nhận", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                string TenNV = txtTenNV.Text;
                string UserName = txtUserName.Text;
                DateTime selectedDate = dtpNgaySinh.Value;
                string NgaySinh = selectedDate.ToString("yyyy-MM-dd");
                string DiaChi = txtDiaChi.Text;
                string CMND = txtCMND.Text;
                string SĐT = txtSĐT.Text;
                string Email = txtEmail.Text;
                string TrangThai = cbTrangThai.Text;
                string GioiTinh;
                if (radioBoy.Checked)
                {
                    GioiTinh = "Nam";
                }
                else
                {
                    GioiTinh = "Nữ";
                }
                int MaQuyen;
                if (radioAmin.Checked)
                {
                    MaQuyen = 2;
                }
                else
                {
                    MaQuyen = 1;
                }

                bool kq = NhanVienDAO.Instance.UpdateAccount(UserName, TenNV, NgaySinh, GioiTinh, DiaChi, CMND, SĐT, Email, TrangThai, MaQuyen);
                if (kq == true)
                {
                    MessageBox.Show("Sửa thành công ");
                    loadDSNhanVien();
                }
                else
                {
                    MessageBox.Show("Sửa thất bại ");
                }
            }
            else
                return;
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xác nhập cập nhật thông tin nhân viên ", "Xác nhận", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                string UserName = txtUserName.Text;
                bool kq = NhanVienDAO.Instance.DeleteAccount(UserName);
                if (kq == true)
                {
                    MessageBox.Show("Xóa thành công ");
                    loadDSNhanVien();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại ");
                }
            }
            else
                return;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string TenNV = txtTenNV.Text;
            string UserName = txtUserName.Text;
            string NgaySinh;
            if (checkBoxNgaySinh.Checked)
            {
                DateTime selectedDate = dtpNgaySinh.Value;
                NgaySinh = selectedDate.ToString("yyyy-MM-dd");
            }
            else
            NgaySinh = string.Empty;
            string DiaChi = txtDiaChi.Text;
            string CMND = txtCMND.Text;
            string SĐT = txtSĐT.Text;
            string Email = txtEmail.Text;
            string TrangThai;
            if (checkBoxTrangThai.Checked)
            {
                TrangThai = cbTrangThai.Text;
            }
            else
                TrangThai = string.Empty;
            string GioiTinh;
            if (checkBoxGioiTinh.Checked)
            {
                if (radioBoy.Checked)
                {
                    GioiTinh = "Nam";
                }
                else
                {
                    GioiTinh = "Nữ";
                }
            }
            else
                GioiTinh = string.Empty;
            int MaQuyen;
            if(checkBoxTenQuyen.Checked)
            {
                if (radioAmin.Checked)
                {
                    MaQuyen = 2;
                }
                else
                {
                    MaQuyen = 1;
                }
            }
            else
                MaQuyen = 0;


            lsvNV.Items.Clear();
            List<NhanVien> nhanVienList = NhanVienDAO.Instance.GetALLListTIMKIEM(UserName, TenNV, NgaySinh, GioiTinh, DiaChi, CMND, SĐT, Email, TrangThai, MaQuyen);
            foreach (NhanVien nhanVien in nhanVienList)
            {
                ListViewItem item = new ListViewItem(nhanVien.MaNV.ToString());
                item.SubItems.Add(nhanVien.TenNV.ToString());
                item.SubItems.Add(nhanVien.UserName.ToString());
                item.SubItems.Add(nhanVien.NgaySinh.ToString());
                item.SubItems.Add(nhanVien.GioiTinh.ToString());
                item.SubItems.Add(nhanVien.DiaChi.ToString());
                item.SubItems.Add(nhanVien.CMND.ToString());
                item.SubItems.Add(nhanVien.SDT.ToString());
                item.SubItems.Add(nhanVien.Email.ToString());
                item.SubItems.Add(nhanVien.TrangThai.ToString());
                item.SubItems.Add(nhanVien.MaQuyen.ToString());
                lsvNV.Items.Add(item);
            }
            foreach (ColumnHeader column in lsvNV.Columns)
            {
                column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                int headerWidth = TextRenderer.MeasureText(column.Text, lsvNV.Font).Width + 10;
                column.Width = Math.Max(column.Width, headerWidth);
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            loadDSNhanVien();
        }

        private void lsvNV_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (lsvNV.SelectedItems.Count > 0)
            {

                string gioiTinh = lsvNV.SelectedItems[0].SubItems[4].Text;
                if (gioiTinh == "Nam")
                {
                    radioBoy.Checked = true;
                }
                else if (gioiTinh == "Nữ")
                {
                    radioGirl.Checked = true;
                }

                string Maquyen = lsvNV.SelectedItems[0].SubItems[10].Text;
                if (Maquyen == "2")
                {
                    radioAmin.Checked = true;
                }
                else if (Maquyen == "1")
                {
                    radioUser.Checked = true;
                }
                txtTenNV.Text = lsvNV.SelectedItems[0].SubItems[1].Text;
                txtUserName.Text = lsvNV.SelectedItems[0].SubItems[2].Text;
                dtpNgaySinh.Text = lsvNV.SelectedItems[0].SubItems[3].Text;
                txtDiaChi.Text = lsvNV.SelectedItems[0].SubItems[5].Text;
                txtCMND.Text = lsvNV.SelectedItems[0].SubItems[6].Text;
                txtSĐT.Text = lsvNV.SelectedItems[0].SubItems[7].Text;
                txtEmail.Text = lsvNV.SelectedItems[0].SubItems[8].Text;
                cbTrangThai.Text = lsvNV.SelectedItems[0].SubItems[9].Text;

            }
        }

        private void btnAddNhanVien_Click(object sender, EventArgs e)
        {
            string TenNV = txtTenNV.Text;
            string UserName = txtUserName.Text;
            DateTime selectedDate = dtpNgaySinh.Value;
            string NgaySinh = selectedDate.ToString("yyyy-MM-dd");
            string DiaChi = txtDiaChi.Text;
            string CMND = txtCMND.Text;
            string SĐT = txtSĐT.Text;
            string Email = txtEmail.Text;
            string TrangThai = cbTrangThai.Text;
            string GioiTinh;
            if (radioBoy.Checked)
            {
                GioiTinh = "Nam";
            }
            else
            {
                GioiTinh = "Nữ";
            }
            int MaQuyen;
            if (radioAmin.Checked)
            {
                MaQuyen = 2;
            }
            else
            {
                MaQuyen = 1;
            }

            if (string.IsNullOrEmpty(TenNV) || string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(NgaySinh)
                || string.IsNullOrEmpty(DiaChi) || string.IsNullOrEmpty(CMND) || string.IsNullOrEmpty(SĐT)
                || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(TrangThai) || string.IsNullOrEmpty(GioiTinh) || MaQuyen == 0)
                    
            {
                MessageBox.Show("Vui lòng điền đầy đủ các thông tin cần thiết!");
                return;
            }

            DialogResult result = MessageBox.Show("Xác nhập thêm nhân viên mới ", "Xác nhận", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
               
                
                bool kq = NhanVienDAO.Instance.InsertAccount(UserName, TenNV, NgaySinh, GioiTinh, DiaChi, CMND, SĐT, Email, TrangThai, MaQuyen);
                if (kq == true)
                {
                    MessageBox.Show("Thêm thành công ");
                    loadDSNhanVien();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại ");
                }

            }
            else
                return;
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            string UserName = txtUserName.Text;
            bool kq = NhanVienDAO.Instance.ResetPassword(UserName);
            if (kq == true)
            {
                MessageBox.Show("Reset mật khẩu thành công ");
                loadDSNhanVien();
            }
            else
            {
                MessageBox.Show("Reset mật khẩu thất bại ");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTenNV.Text = "";
            txtUserName.Text = "";
            dtpNgaySinh.Text = "";
            txtDiaChi.Text = "";
            txtCMND.Text = "";
            txtSĐT.Text = "";
            txtEmail.Text = "";
            cbTrangThai.Text = "";
            radioBoy.Checked = false;
            radioGirl.Checked = false;
            radioAmin.Checked = false;
            radioUser.Checked = false;
        }
    }
}
