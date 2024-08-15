using System;
using System.Data;


namespace QL_XEKHACH.DTO
{
    internal class TaiXe
    {
        private int maTaiXe;
        private string tenTaiXe;
        private DateTime ngaySinh;
        private string gioiTinh;
        private string diaChi;
        private string cMND;
        private string sDT;
        private string email;
        public TaiXe(DataRow row)
        {

            MaTaiXe = (int)row["MATAIXE"];
            TenTaiXe = row["TENTAIXE"].ToString();
            NgaySinh = (DateTime)row["NGAYSINH"];
            GioiTinh = row["GIOITINH"].ToString();
            DiaChi = row["DIACHI"].ToString();
            CMND = row["CMND"].ToString();
            SDT = row["SDT"].ToString();
            Email = row["EMAIL"].ToString();
        }
        

        public TaiXe(int maTaiXe,string tenTaiXe, DateTime ngaySinh, string gioiTinh, string diaChi, string cMND, string sDT, string email)
        {
            MaTaiXe = maTaiXe;
            TenTaiXe = tenTaiXe;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            DiaChi = diaChi;
            CMND = cMND;
            SDT = sDT;
            Email = email;
        }
        public int MaTaiXe { get => maTaiXe; set => maTaiXe = value; }
        public string TenTaiXe { get => tenTaiXe; set => tenTaiXe = value; }
        public DateTime NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string CMND { get => cMND; set => cMND = value; }
        public string SDT { get => sDT; set => sDT = value; }
        public string Email { get => email; set => email = value; }
    }
}
