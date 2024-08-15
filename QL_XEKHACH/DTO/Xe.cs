using System.Data;
namespace QL_XEKHACH.DTO
{
     class Xe
     {
        private int maXe;
        private string bienSoXe;
        private int maLoaiXe;
        private string trangThai;
        public int MaXe { get => maXe; set => maXe = value; }
        public string BienSoXe { get => bienSoXe; set => bienSoXe = value; }
        public int MaLoaiXe { get => maLoaiXe; set => maLoaiXe = value; }
        public string TrangThai { get => trangThai; set => trangThai = value; }
        public Xe(DataRow row)
        {
            this.MaXe = (int)row["MAXE"];
            this.BienSoXe = row["BienSoXe"].ToString();
            this.TrangThai = row["TrangThai"].ToString();
            this.MaLoaiXe = (int)row["MaLoaiXe"];
        }
        public  Xe( int maxe, string biensoxe , string trangThai, int maLoaiXe)
        {
            this.MaXe = maxe;
            this.BienSoXe = biensoxe;
            this.MaLoaiXe = maLoaiXe;
            this.TrangThai = trangThai;
        }
    }
}
