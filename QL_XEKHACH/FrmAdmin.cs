using QL_XEKHACH.DTO;
using QL_XEKHACH.UserControls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QL_XEKHACH
{
    public partial class FrmAdmin : Form
    {
        private NhanVien loginAccount;

        internal NhanVien LoginAccount
        {
            get => loginAccount;
            set { loginAccount = value; ChangeAccount(loginAccount); }
        }

        
        public FrmAdmin(NhanVien nv)
        {
            InitializeComponent();
            this.LoginAccount = nv;
        } 

       
        void ChangeAccount(NhanVien nv)
        {
            labelTenNV.Text = nv.TenNV.ToString();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelControl.Controls.Clear();
            panelControl.Controls.Add(userControl);
            userControl.BringToFront();
           
        }
        
        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            runbtnTaiKhoan_Click();
        }


        private void btnCLose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        protected void runbtnTaiKhoan_Click()
        {

            btnTaiKhoan.BackColor = Color.FromArgb(20, 20, 50);
            btnQLVe.BackColor = Color.FromArgb(29, 29, 66);
            btnQLXe.BackColor = Color.FromArgb(29, 29, 66);
            btnQLLoTrinh.BackColor = Color.FromArgb(29, 29, 66);
            btnQLNhanVien.BackColor = Color.FromArgb(29, 29, 66);
            Page_Profile profile = new Page_Profile(loginAccount.UserName); 
            addUserControl(profile);
        }

        private void btnQLLoTrinh_Click(object sender, EventArgs e)
        {
            
            btnTaiKhoan.BackColor = Color.FromArgb(29, 29, 66);
            btnQLVe.BackColor = Color.FromArgb(29, 29, 66);
            btnQLXe.BackColor = Color.FromArgb(29, 29, 66);
            btnQLLoTrinh.BackColor = Color.FromArgb(20, 20, 50);
            btnQLNhanVien.BackColor = Color.FromArgb(29, 29, 66);
            Page_LoTrinh lotring = new Page_LoTrinh();

            addUserControl(lotring);

        }

        private void btnQLVe_Click(object sender, EventArgs e)
        {
            
            btnTaiKhoan.BackColor = Color.FromArgb(29, 29, 66);
            btnQLVe.BackColor = Color.FromArgb(20, 20, 50);
            btnQLXe.BackColor = Color.FromArgb(29, 29, 66);
            btnQLLoTrinh.BackColor = Color.FromArgb(29, 29, 66);
            btnQLNhanVien.BackColor = Color.FromArgb(29, 29, 66);
            Page_ThongKeVe ve = new Page_ThongKeVe();
            addUserControl(ve);

        }

        private void btnQLXe_Click(object sender, EventArgs e)
        {
            btnTaiKhoan.BackColor = Color.FromArgb(29, 29, 66);
            btnQLVe.BackColor = Color.FromArgb(29, 29, 66);
            btnQLXe.BackColor = Color.FromArgb(20, 20, 50);
            btnQLLoTrinh.BackColor = Color.FromArgb(29, 29, 66);
            btnQLNhanVien.BackColor = Color.FromArgb(29, 29, 66);
            Page_QLXe xe = new Page_QLXe();
            addUserControl(xe);

        }

        private void btnQLNhanVien_Click(object sender, EventArgs e)
        {
            btnTaiKhoan.BackColor = Color.FromArgb(29, 29, 66);
            btnQLVe.BackColor = Color.FromArgb(29, 29, 66);
            btnQLXe.BackColor = Color.FromArgb(29, 29, 66);
            btnQLLoTrinh.BackColor = Color.FromArgb(29, 29, 66);
            btnQLNhanVien.BackColor = Color.FromArgb(20, 20, 50);
            Page_QLNhanVien nhanvien = new Page_QLNhanVien();
            addUserControl(nhanvien);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
            this.Close();
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            runbtnTaiKhoan_Click();
        }
    }
}
