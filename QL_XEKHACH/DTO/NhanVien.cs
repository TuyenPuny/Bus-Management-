using System;
using System.Data;


namespace QL_XEKHACH.DTO
{
    public class NhanVien
    {
		private int maNV;
		private string userName;
		private string passWord;
		private string tenNV;
		private DateTime ngaySinh;
		private string gioiTinh;
		private string diaChi;
		private string cMND;
		private string sDT;
		private string email;
        private string trangThai;
		private int maQuyen;
        public NhanVien(DataRow row)
        {
            
            MaNV = (int)row["MANV"];
            UserName =  row["USERNAME"].ToString();
            PassWord =  row["PASSWORD"].ToString();
            TenNV = row["TENNV"].ToString();
            NgaySinh =  (DateTime)row["NGAYSINH"];
            GioiTinh =  row["GIOITINH"].ToString();
            DiaChi =  row["DIACHI"].ToString();
            CMND =row["CMND"].ToString();
            SDT =  row["SDT"].ToString();
            Email = row["EMAIL"].ToString();
            TrangThai = row["TRANGTHAI"].ToString();
            MaQuyen = (int)row["MAQUYEN"];
        }
        public int MaNV { get => maNV; set => maNV = value; }
        public string UserName { get => userName; set => userName = value; }
        public string PassWord { get => passWord; set => passWord = value; }
        public string TenNV { get => tenNV; set => tenNV = value; }
        public DateTime NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string CMND { get => cMND; set => cMND = value; }
        public string SDT { get => sDT; set => sDT = value; }
        public string Email { get => email; set => email = value; }
        public int MaQuyen { get => maQuyen; set => maQuyen = value; }
        public string TrangThai { get => trangThai; set => trangThai = value; }

        public NhanVien(int maNV, string userName, string passWord, string tenNV, DateTime ngaySinh, string gioiTinh, string diaChi, string cMND, string sDT, string email,string trangThai, int maQuyen)
        {
            MaNV = maNV;
            UserName = userName;
            PassWord = passWord;
            TenNV = tenNV;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            DiaChi = diaChi;
            CMND = cMND;
            SDT = sDT;
            Email = email;
            TrangThai = trangThai;
            MaQuyen = maQuyen;
        }
    }
}
