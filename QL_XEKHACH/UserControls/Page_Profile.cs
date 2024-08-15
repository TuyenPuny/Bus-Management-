using QL_XEKHACH.DAO;
using QL_XEKHACH.DTO;
using System;
using System.Drawing;

using System.Windows.Forms;

namespace QL_XEKHACH.UserControls
{
    public partial class Page_Profile : UserControl
    {
        public NhanVien nhanvien;
        public Page_Profile(string userName)
        {
            InitializeComponent();

            load_profile(userName);
        }
        void load_profile(string userName)
        {
            NhanVien Nhanvien = NhanVienDAO.Instance.GetAccountByUserName(userName);
            nhanvien = Nhanvien;
            dtpNgaySinh.BackColor = Color.FromArgb(20, 20, 50); ;
            dtpNgaySinh.ForeColor = Color.CornflowerBlue;
            dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpNgaySinh.CustomFormat = "dd-MM-yyyy";
            txtDisplayName.Text = nhanvien.TenNV.ToString();
            txtUserName.Text = nhanvien.UserName.ToString();
            txtCCCD.Text = nhanvien.CMND.ToString();
            txtDiaChi.Text = nhanvien.DiaChi.ToString();
            txtEmail.Text = nhanvien.Email.ToString();
            if (nhanvien.GioiTinh.ToString() == "Nam")
            {
                radioBoy.Checked = true;
            }
            else radioGirl.Checked = true;
            txtSDT.Text = nhanvien.SDT.ToString();
            dtpNgaySinh.Value = nhanvien.NgaySinh;
        }
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
           
            FrmChangePassword changePassword = new FrmChangePassword(nhanvien);
            changePassword.ShowDialog();

        }

        private void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            

            DialogResult result = MessageBox.Show("Xác nhập cập nhật thông tin tài khoản mới ", "Xác nhận", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                string TenNV = txtDisplayName.Text; 
                string UserName = txtUserName.Text;
                string Email = txtEmail.Text;
                string CMND = txtCCCD.Text;
                string SĐT = txtSDT.Text;
                string DiaChi = txtDiaChi.Text;
                string GioiTinh;
                int maquyen = nhanvien.MaQuyen;
                string trangthai = nhanvien.TrangThai.ToString();
                if (radioBoy.Checked)
                {
                    GioiTinh = "Nam";
                }
                else
                {
                    GioiTinh = "Nữ";
                }
                DateTime selectedDate = dtpNgaySinh.Value;
                string NgaySinh = selectedDate.ToString("yyyy-MM-dd");
                bool kq = NhanVienDAO.Instance.UpdateAccount(UserName, TenNV, NgaySinh, GioiTinh, DiaChi, CMND, SĐT, Email, trangthai, maquyen);
                if (kq == true)
                {
                    MessageBox.Show("Update profile thành công ");
                    load_profile(UserName);
                }
                else
                {
                    MessageBox.Show("Update profile thất bại ");
                }
            }
            else
            {
                return;
            }


        }
    }
}
