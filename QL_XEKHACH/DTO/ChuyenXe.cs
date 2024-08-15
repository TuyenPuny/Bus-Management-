using System;
using System.Data;


namespace QL_XEKHACH.DTO
{
     class ChuyenXe
    {
        private int maChuyen;
        private int maTuyen;
        private int maXe;
        private DateTime start;
        public string StartString { get; set; }
        private DateTime end;
        private int gheTrong;
        private int maTaiXe;

        public int MaTuyen { get => maTuyen; set => maTuyen = value; }
        public int MaXe { get => maXe; set => maXe = value; }
        public DateTime Start { get => start; set => start = value; }
        public DateTime End { get => end; set => end = value; }
        public int GheTrong { get => gheTrong; set => gheTrong = value; }
        public int MaTaiXe { get => maTaiXe; set => maTaiXe = value; }
        public int MaChuyen { get => maChuyen; set => maChuyen = value; }

        public ChuyenXe(DataRow row)
        {
            this.MaChuyen = (int)row["MACHUYENXE"];
            this.maTuyen = (int)row["maTuyen"];
            this.MaXe = (int)row["MaXe"];
            this.Start = DateTime.Parse(row["GIOXUATPHAT"].ToString());
            this.End = DateTime.Parse(row["GIODEN"].ToString());
            this.GheTrong = (int)row["GheTrong"];
            this.MaTaiXe = (int)row["MaTaiXe"];
        }
        public ChuyenXe(int machuyen,int matuyen, int maXe,DateTime start , DateTime end,int ghetrong,int mataixe) {
            this.MaChuyen= machuyen;
            this.MaTuyen= matuyen; 
            this.MaXe = maXe;
            this.Start = start;
            this.End = end;
            this.GheTrong = ghetrong;
            this.MaTaiXe = mataixe;
        }
    }
}
