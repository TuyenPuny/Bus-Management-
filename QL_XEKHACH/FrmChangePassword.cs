using QL_XEKHACH.DAO;
using QL_XEKHACH.DTO;
using QL_XEKHACH.Utilities;
using System;
using System.Windows.Forms;

namespace QL_XEKHACH
{
    public partial class FrmChangePassword : Form
    {
        int manv = 0;
        string pass = "";
        string username = string.Empty;
        public FrmChangePassword(NhanVien nv)
        {
            InitializeComponent();
            manv = nv.MaNV;
            pass = nv.PassWord;
            username = nv.UserName;
        }
        private void FrmChangePassword_Load(object sender, EventArgs e)
        {

        }

        private void btnCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSwitchRegister_Click(object sender, EventArgs e)
        {
            string OldPass = (txtOldPassword.Text);
            string NewPass = (txtNewPassword.Text);
            string ReNewPass = (txtReNewPassword.Text).ToLower();
            string hasPassNew = Password.Create_MD5(NewPass).ToLower();
            string hasPassOld = Password.Create_MD5(OldPass).ToLower();
            if (pass != hasPassOld)
            {
                MessageBox.Show("Mật khẩu không chính xác!");
                txtOldPassword.Focus();
                return;
            }    
            if (OldPass == NewPass)
            {
                MessageBox.Show("Mật khẩu mới trùng với mật khẩu cũ!");
                txtReNewPassword.Focus();
                return;

            }
            else
            {
                if (ReNewPass != NewPass)
                {
                    MessageBox.Show("Nhập lại mật khẩu không trùng khớp!");
                    txtReNewPassword.Focus();
                    return;

                }
                else
                {

                    DialogResult result = MessageBox.Show("Xác nhận đổi mật khẩu mới ?","Xác nhận", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        
                        bool kq = NhanVienDAO.Instance.ChangePassword(username, hasPassOld, hasPassNew);
                        if (kq == true)
                        {
                            MessageBox.Show("Đổi mật khẩu thành công ");
                            pass = hasPassNew;
                        }
                        else
                        {
                            MessageBox.Show("Đổi mật khẩu thất bại ");
                        }
                    }
                    else
                    {
                        return;
                    }

                }    
            }   
            


        }
        private void CbxShowPassWord_CheckedChanged(object sender, EventArgs e)
        {

            if (CbxShowPassWord.Checked)
            {
                txtOldPassword.PasswordChar = (char)0;
                txtNewPassword.PasswordChar = (char)0;

            }
            else
            {
                txtOldPassword.PasswordChar = '*';
                txtNewPassword.PasswordChar = '*';
            }
        }
    }
}
