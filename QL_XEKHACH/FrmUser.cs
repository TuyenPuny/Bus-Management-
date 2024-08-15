using QL_XEKHACH.DTO;
using QL_XEKHACH.UserControls;
using System;

using System.Drawing;

using System.Windows.Forms;

namespace QL_XEKHACH
{
    public partial class FrmUser : Form
    {
        private NhanVien loginAccount;

        internal NhanVien LoginAccount
        {
            get => loginAccount;
            set { loginAccount = value; ChangeAccount(loginAccount); }
        }

        
        public FrmUser(NhanVien nv)
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
            btnVeXe.BackColor = Color.FromArgb(29, 29, 66);
            btnDoiVe.BackColor = Color.FromArgb(29, 29, 66);
            btnHuyVe.BackColor = Color.FromArgb(29, 29, 66);
            Page_Profile profile = new Page_Profile(loginAccount.UserName);
            addUserControl(profile);
        }
        
        private void btnBanVe_Click(object sender, EventArgs e)
        {
            btnVeXe.BackColor = Color.FromArgb(20, 20, 50);
            btnTaiKhoan.BackColor = Color.FromArgb(29, 29, 66);
            btnDoiVe.BackColor = Color.FromArgb(29, 29, 66);
            btnHuyVe.BackColor = Color.FromArgb(29, 29, 66);
            Page_VeXe vexe = new Page_VeXe(loginAccount);
            addUserControl(vexe);
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

        private void btnHuyVe_Click(object sender, EventArgs e)
        {
            btnHuyVe.BackColor = Color.FromArgb(20, 20, 50);
            btnTaiKhoan.BackColor = Color.FromArgb(29, 29, 66);
            btnVeXe.BackColor = Color.FromArgb(29, 29, 66);
            btnDoiVe.BackColor = Color.FromArgb(29, 29, 66);
            Page_HuyVe huyve = new Page_HuyVe();
            addUserControl(huyve);
        }

        private void btnDoiVe_Click(object sender, EventArgs e)
        {
            btnDoiVe.BackColor = Color.FromArgb(20, 20, 50);
            btnHuyVe.BackColor = Color.FromArgb(29, 29, 66);
            btnTaiKhoan.BackColor = Color.FromArgb(29, 29, 66);
            btnVeXe.BackColor = Color.FromArgb(29, 29, 66);
            Page_DoiVe huyve = new Page_DoiVe();
            addUserControl(huyve);
        }
    }
}
