using QL_XEKHACH.DAO;
using QL_XEKHACH.DTO;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace QL_XEKHACH
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            kiemtraKetNoi();
            InitializeComponent();
        }
        string connectionSTR = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public SqlConnection connect;
        public void kiemtraKetNoi()
        {
            try
            {
                connect = new SqlConnection(connectionSTR);
                connect.Open();
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kết nối đến cơ sở dữ liệu không thành công...");
                Application.Exit();
            }
        }
        WaitFormDAO waitForm = new WaitFormDAO();
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            
            txtUserName.Text = Properties.Settings.Default.UserName;
            txtPassWord.Text = Properties.Settings.Default.PassWord;
            Properties.Settings.Default.Save();
            if (Properties.Settings.Default.Remember == true)
            {
                CbxRemenberMe.Checked = true;

            }   
            else CbxRemenberMe.Checked &= false;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string passWord = txtPassWord.Text;
            if (CbxRemenberMe.Checked)
            {
                Properties.Settings.Default.UserName = txtUserName.Text;
                Properties.Settings.Default.PassWord = txtPassWord.Text;
                Properties.Settings.Default.Remember = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.UserName = "";
                Properties.Settings.Default.PassWord = "";
                
                Properties.Settings.Default.Save();
            }
            if (Login(userName, passWord))
            {
                waitForm.Show(this);
                Thread.Sleep(5000);
                      
                NhanVien loginAccount = NhanVienDAO.Instance.GetAccountByUserName(userName);
                if(loginAccount == null )
                {
                    MessageBox.Show("đăng nhập thất bại! ");
                    waitForm.Close();
                }    
                else
                {
                    QuyenTC qtc = QuyenTCDAO.Instance.GetQuyenTCByUserName(userName);
                    if(qtc.TenQuyen == "admin")
                    {
                        FrmAdmin f = new FrmAdmin(loginAccount);
                        this.Hide();
                        f.Show();
                        waitForm.Close();
                    }
                    if(qtc.TenQuyen == "user")
                    {
                        FrmUser f = new FrmUser(loginAccount);
                        this.Hide();
                        f.Show();
                        waitForm.Close();
                    }    
                    
                }    
               
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!");
                Properties.Settings.Default.UserName = "";
                Properties.Settings.Default.PassWord = "";
                Properties.Settings.Default.Save();
                txtUserName.Focus();
                CbxRemenberMe.Checked = false;
            }

            
        }
        bool Login(string userName, string passWord)
        {
            return NhanVienDAO.Instance.Login(userName, passWord);
        }
        private void CbxShowPassWord_CheckedChanged(object sender, EventArgs e)
        {
            if (CbxShowPassWord.Checked)
            {
                txtPassWord.PasswordChar = (char)0;
            }
            else
            {
                txtPassWord.PasswordChar = '*';
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        
        private void label13_Click(object sender, EventArgs e)
        {
            FrmForgotpassword frmRegister = new FrmForgotpassword();
            this.Hide();
            frmRegister.Show();
        }

        

    }
}
