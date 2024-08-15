using System.Data;


namespace QL_XEKHACH.DTO
{
    class VeXe
    {
        private int maVe;
        private int maNhanVien;
        private int maChuyenXe;
        private string tenKH;
        private string sDT;
        private string viTri;
        private string tinhTrang;
        private string ghiChu;
        public int MaVe { get => maVe; set => maVe = value; }
        public int MaNhanVien { get => maNhanVien; set => maNhanVien = value; }
        public int MaChuyenXe { get => maChuyenXe; set => maChuyenXe = value; }
        public string TenKH { get => tenKH; set => tenKH = value; }
        public string SDT { get => sDT; set => sDT = value; }
        public string ViTri { get => viTri; set => viTri = value; }
        public string TinhTrang { get => tinhTrang; set => tinhTrang = value; }
        public string GhiChu { get => ghiChu; set => ghiChu = value; }

        public VeXe(DataRow row)
        {
            this.MaVe = (int)row["MaVe"];
            this.MaNhanVien = (int)row["MANV"];
            this.MaChuyenXe = (int)row["MACHUYENXE"];
            this.TenKH = row["TENKH"].ToString();
            this.SDT = row["SDT"].ToString();
            this.ViTri = row["Vitri"].ToString();
            this.TinhTrang = row["Tinhtrang"].ToString();
            this.GhiChu = row["GhiChu"].ToString();
        }
        public VeXe(int mave, int manv,int macx,string tenkh, string sdt, string vitri, string tinhtrang,string ghichu)
        {
            this.MaVe = mave;
            this.MaNhanVien = manv;
            this.MaChuyenXe= macx;
            this.TenKH = tenkh;
            this.SDT = sdt;
            this.ViTri = vitri;
            this.TinhTrang = tinhtrang;
            this.GhiChu = ghichu;
        }

        
    }
}
