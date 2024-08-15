using QL_XEKHACH.DTO;
using System.Collections.Generic;
using System.Data;


namespace QL_XEKHACH.DAO
{
    internal class LoaiXeDAO
    {
        private static LoaiXeDAO instance;

        public static LoaiXeDAO Instance
        {
            get { if (instance == null) instance = new LoaiXeDAO(); return instance; }
            private set { instance = value; }
        }
        public static int SeatWidth = 60;
        public static int SeatHeight = 40;
        public List<LoaiXe> GetLoaiXeFormChuyenXe(int maTuyen, string gioxuatphat)
        {
            List<LoaiXe> LoaiXeList = new List<LoaiXe>();
            string a = gioxuatphat;
            string query = string.Format("EXEC USP_LoaiXeFromGioXuatPhat {0}, '{1}'", maTuyen, gioxuatphat) ;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                LoaiXe loaixe = new LoaiXe(item);
                LoaiXeList.Add(loaixe);
            }
            return LoaiXeList;
        }
        public List<LoaiXe> GetALLLoaiXe()
        {
            List<LoaiXe> listXe = new List<LoaiXe>();
            string query = "SELECT * FROM LOAIXE";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                LoaiXe loaixe = new LoaiXe(item);
                listXe.Add(loaixe);
            }
            return listXe;
        }
    }

    
}
    