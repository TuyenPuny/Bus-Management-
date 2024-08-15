using QL_XEKHACH.DTO;
using System.Collections.Generic;
using System.Data;


namespace QL_XEKHACH.DAO
{
    internal class TaiXeDAO
    {
        private static TaiXeDAO instance;

        public static TaiXeDAO Instance
        {
            get { if (instance == null) instance = new TaiXeDAO(); return instance; }
            private set { instance = value; }
        }

        private TaiXeDAO() { }
        public List<TaiXe> GetALLTaixeFormTuyen(int matuyen, int maloaixe, string ngaychay)
        {
            List<TaiXe> listXe = new List<TaiXe>();

            string query = string.Format("USP_TaiXeFromTuyenXe {0},{1},'{2}'", matuyen, maloaixe, ngaychay);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                TaiXe Taixe = new TaiXe(item);
                listXe.Add(Taixe);
            }
            return listXe;
        }

        public List<TaiXe> GetAllTaixe()
        {
            List<TaiXe> listXe = new List<TaiXe>();

            string query = "SELECT * FROM TAIXE";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                TaiXe Taixe = new TaiXe(item);
                listXe.Add(Taixe);
            }
            return listXe;
        }
        
    }
}
