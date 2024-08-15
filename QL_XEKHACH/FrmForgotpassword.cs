using QL_XEKHACH.DAO;
using System;
using System.Windows.Forms;

namespace QL_XEKHACH
{
    public partial class FrmForgotpassword : Form
    {
        public FrmForgotpassword()
        {
            InitializeComponent();
        }
        private void FrmRegister_Load(object sender, EventArgs e)
        {

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
            FrmLogin frmLogin = new FrmLogin();
            this.Close();
            frmLogin.Show();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string email = txtEmail.Text;
            bool isEmail = DataProvider.Instance.IsEmail(email);
            if(!isEmail)
            {
                MessageBox.Show("Email sai định dạng!");
                txtEmail.Focus();
                return;
            }    
            string CCCD = txtCCCD.Text;
            string SDT = txtSDT.Text;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) 
                || string.IsNullOrEmpty(CCCD) || string.IsNullOrEmpty(SDT) )
            {
                MessageBox.Show("Vui lòng điền đầy đủ hết các thông tin theo yêu cầu!");
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("Xác nhập gửi yêu cầu cấp mật khẩu?", "Xác nhận", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    bool kq = NhanVienDAO.Instance.RequestPassword(username , CCCD, SDT, email);
                    if (kq == true)
                    {
                        MessageBox.Show("Gửi yêu cầu thành công! Kiểm tra email : " + email+ " để nhận mật khẩu!");
                    }
                    else
                    {
                        MessageBox.Show("Gửi yêu cầu thất bại! Vui lòng kiểm tra lại các thông tin đã nhập");
                    }
                }    
            }

        }
    }
}
