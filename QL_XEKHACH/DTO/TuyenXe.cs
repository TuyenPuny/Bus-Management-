
using System.Data;


namespace QL_XEKHACH.DTO    
{
     class TuyenXe
    {

        private int maTuyen;
        private string tenTuyen;
        private string diemXuatPhat;
        private string diemDen;
        private int gia;
        private int quangDuong;

        public string TenTuyen { get => tenTuyen; set => tenTuyen = value; }
        public string DiemXuatPhat { get => diemXuatPhat; set => diemXuatPhat = value; }
        public string DiemDen { get => diemDen; set => diemDen = value; }
        public int Gia { get => gia; set => gia = value; }
        public int QuangDuong { get => quangDuong; set => quangDuong = value;    }
        public int MaTuyen { get => maTuyen; set => maTuyen = value; }

        public TuyenXe(DataRow row)
        {
            this.MaTuyen = (int)row["MaTuyen"];
            this.TenTuyen = row["TenTuyen"].ToString();
            this.DiemXuatPhat = row["DiemXuatPhat"].ToString();
            this.DiemDen = row["DiemDen"].ToString();
            this.Gia = int.Parse(row["BANGGIA"].ToString());
            this.QuangDuong = (int)row["QuangDuong"];
        }
        public TuyenXe( string tenTuyen, string diemXuatPhat, string diemDen, int gia, int quangDuong)
        {   this.MaTuyen = maTuyen;
            this.TenTuyen = tenTuyen;
            this.DiemXuatPhat= diemXuatPhat;
            this.DiemDen = diemDen;
            this.Gia  = gia;
            this.QuangDuong = quangDuong;
        }
    }
}
