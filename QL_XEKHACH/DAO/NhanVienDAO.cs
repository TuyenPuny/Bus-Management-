using System.Collections.Generic;
using System.Data;
using QL_XEKHACH.DTO;
using QL_XEKHACH.Utilities;
namespace QL_XEKHACH.DAO
{
    public class NhanVienDAO
    {
        private static NhanVienDAO instance;

        public static NhanVienDAO Instance
        {
            get { if (instance == null) instance = new NhanVienDAO(); return instance; }
            private set { instance = value; }
        }

        private NhanVienDAO() { }

        public List<NhanVien> GetAllNhanVien()
        {
            List<NhanVien> list = new List<NhanVien>();

            string query = "SELECT * FROM dbo.ActiveNVBANVEView";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                NhanVien nhanVien = new NhanVien(item);
                list.Add(nhanVien);
            }

            return list;
        }
        
        public bool Login(string userName, string passWord)
        {
            string hasPass = "";
            hasPass = Password.Create_MD5(passWord);
            string query = "SELECT dbo.f_dangnhap( @userName , @matkhau )";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { userName, hasPass });
            return result.Rows.Count > 0;
        }

        public bool DoiMatKhau(string userName, string pass, string newPass)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("exec USP_ChangePassword @userName , @password , @newPassword", new object[] { userName, pass, newPass });

            return result > 0;
        }

        public NhanVien GetAccountByUserName(string userName)
        {
                string query = "SELECT * FROM dbo.GetAccountByUserName( @userName )";
                DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { userName });

                foreach (DataRow item in data.Rows)
                {
                    return new NhanVien(item);
                }

                return null;
         }

        public bool InsertAccount(string name, string displayName, string ngaysinh, string gioitinh, string diachi, string cmnd, string sdt, string email, string trangthai, int type)
        {
            string query = string.Format("INSERT INTO NVBANVE ( USERNAME, PASSWORD,TENNV, NGAYSINH,GIOITINH,DIACHI,CMND,SDT,EMAIL,TRANGTHAI, MAQUYEN )VALUES  ( '{0}', '{1}', N'{2}', '{3}',N'{4}',N'{5}','{6}','{7}','{8}',N'{9}',{10})", name, "e10adc3949ba59abbe56e057f20f883e", displayName, ngaysinh, gioitinh, diachi, cmnd, sdt, email, trangthai, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateAccount(string name, string displayName, string ngaysinh, string gioitinh, string diachi, string cmnd, string sdt, string email, string trangthai, int type)
        {
           
            string query = string.Format("UPDATE NVBANVE SET TENNV = N'{1}', NGAYSINH = '{2}',GIOITINH = N'{3}',DIACHI = N'{4}',CMND = '{5}',SDT = '{6}',EMAIL = '{7}',TRANGTHAI =N'{8}',MAQUYEN = {9} WHERE UserName = '{0}'", name, displayName, ngaysinh, gioitinh, diachi, cmnd, sdt, email, trangthai, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool RequestPassword(string username, string CCCD, string SDT, string email)
        {
            string query = string.Format("SELECT * FROM NVBANVE WHERE USERNAME = '{0}',CMND = '{1}',SDT = '{2}',EMAIL = '{3}'", username, CCCD, SDT, email);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteAccount(string name)
        {
            string query = string.Format("UPDATE NVBANVE SET TRANGTHAI =N'đã nghỉ làm' where UserName = N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool ChangePassword(string manv, string oldpassword, string newpassword)
        {
            string query ="EXEC USP_UpdateAccount @userName , @password , @newPassword";
            int result = DataProvider.Instance.ExecuteNonQuery(query , new object[] { manv , oldpassword , newpassword });

            return result > 0;
        }
        public bool ResetPassword(string name)
        {
            string query = string.Format("update NVBANVE set password = N'e10adc3949ba59abbe56e057f20f883e' where UserName = '{0}'", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public List<NhanVien> GetALLListTIMKIEM(string name, string displayName, string ngaysinh, string gioitinh, string diachi, string cmnd, string sdt, string email, string trangthai, int type)
        {
            List<NhanVien> ListNv = new List<NhanVien>();
            string chuoi = "SELECT * FROM dbo.GetNhanVienBanVeFiltered(";
            if (!string.IsNullOrEmpty(name))
            {
                chuoi += "'" + name + "',";
            }
            else
            {
                chuoi += "null,";
            }
            if (!string.IsNullOrEmpty(displayName))
            {
                chuoi += "'" + displayName + "',";
            }
            else
            {
                chuoi += "null,";
            }
            if (!string.IsNullOrEmpty(ngaysinh))
            {
                chuoi += "'" + ngaysinh + "',";
            }
            else
            {
                chuoi += "null,";
            }
            if (!string.IsNullOrEmpty(gioitinh))
            {
                chuoi += "N'" + gioitinh + "',";
            }
            else
            {
                chuoi += "null,";
            }
            if (!string.IsNullOrEmpty(diachi))
            {
                chuoi += "N'" + diachi + "',";
            }
            else
            {
                chuoi += "null,";
            }
            if (!string.IsNullOrEmpty(cmnd))
            {
                chuoi += "'" + cmnd + "',";
            }
            else
            {
                chuoi += "null,";
            }
            if (!string.IsNullOrEmpty(sdt))
            {
                chuoi += "'" + sdt + "',";
            }
            else
            {
                chuoi += "null,";
            }
            if (!string.IsNullOrEmpty(email))
            {
                chuoi += "'" + email + "',";
            }
            else
            {
                chuoi += "null,";
            }
            if (!string.IsNullOrEmpty(trangthai))
            {
                chuoi += "N'" + trangthai + "',";
            }
            else
            {
                chuoi += "null,";
            }
             chuoi += "" + type+");";
            DataTable data = DataProvider.Instance.ExecuteQuery(chuoi);

            foreach (DataRow item in data.Rows)
            {
                NhanVien nhanvien = new NhanVien(item);
                ListNv.Add(nhanvien);
            }
            return ListNv;
        }
    }
}
