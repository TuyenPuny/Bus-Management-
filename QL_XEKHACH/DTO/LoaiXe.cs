
using System.Data;


namespace QL_XEKHACH.DTO
{
     class LoaiXe
    {
        private int maLoaixe;
        private string tenLoaiXe;
        private int soGhe;
        

        public int MaLoaixe { get => maLoaixe; set => maLoaixe = value; }
        public string TenLoaiXe { get => tenLoaiXe; set => tenLoaiXe = value; }
        public int SoGhe { get => soGhe; set => soGhe = value; }
        public LoaiXe(DataRow row)
        {
            this.MaLoaixe = (int)row["MALOAIXE"];
            this.TenLoaiXe = row["TENLOAIXE"].ToString();
            this.SoGhe = (int)row["SOGHE"];

        }
       
        public LoaiXe(int maloaixe,string tenLoaiXe,int soghe) {
            this.MaLoaixe= maloaixe;
            this.TenLoaiXe= tenLoaiXe;
            this.SoGhe= soghe;
        }
    }
}
