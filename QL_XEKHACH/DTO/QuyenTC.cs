using System.Data;


namespace QL_XEKHACH.DTO
{
    public class QuyenTC
    {
        private int maQuyen;
        private string tenQuyen;
        public int MaQuyen { get => maQuyen; set => maQuyen = value; }
        public string TenQuyen { get => tenQuyen; set => tenQuyen = value; }

        public QuyenTC(DataRow row)
        {
            this.MaQuyen = (int)row["MAQUYEN"];
            this.TenQuyen = row["TENQUYEN"].ToString();
            
        }
        public QuyenTC(int maQuyen, string tenQuyen)
        {
            this.MaQuyen = maQuyen;
            this.TenQuyen = tenQuyen;
        }
    }
}
