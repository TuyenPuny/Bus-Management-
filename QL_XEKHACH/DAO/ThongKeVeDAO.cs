using QL_XEKHACH.DTO;
using System;
using System.Collections.Generic;
using System.Data;


namespace QL_XEKHACH.DAO
{
    internal class ThongKeVeDAO
    {
        private static ThongKeVeDAO instance;

        public static ThongKeVeDAO Instance
        {
            get { if (instance == null) instance = new ThongKeVeDAO(); return ThongKeVeDAO.instance; }
            private set { ThongKeVeDAO.instance = value; }
        }

        private ThongKeVeDAO() { }
        public List<ThongKeVe> GetThongKeVeTheoThang(int nam)
            {
                List<ThongKeVe> thongKeData = new List<ThongKeVe>();

                string query = "Exec USP_GetSLVeTheoThang @nam";

                DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { nam });

                foreach (DataRow row in data.Rows)
                {
                    int year = Convert.ToInt32(row["Nam"]);
                    int month = Convert.ToInt32(row["Thang"]);
                    int soLuongVe = Convert.ToInt32(row["SoVeDaBan"]);
                    ThongKeVe thongKeVe = new ThongKeVe(year, month, soLuongVe);
                    thongKeData.Add(thongKeVe);
                }
                return thongKeData;
        }
        public List<ThongKeVe> GetThongKeDoanhTHuTheoThang(int nam)
        {
            List<ThongKeVe> thongKeData = new List<ThongKeVe>();

            string query = "EXEC USP_GetDoanhThuTheoThang @nam";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { nam });

            foreach (DataRow row in data.Rows)
            {
                int year = Convert.ToInt32(row["Nam"]);
                int month = Convert.ToInt32(row["Thang"]);
                int doanhthu = Convert.ToInt32(row["DoanhThu"]);
                ThongKeVe thongKeVe = new ThongKeVe(year, month, doanhthu);
                thongKeData.Add(thongKeVe);
            }
            return thongKeData;
        }
    }

}
