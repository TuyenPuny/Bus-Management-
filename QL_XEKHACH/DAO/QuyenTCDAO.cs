using QL_XEKHACH.DTO;
using System.Data;


namespace QL_XEKHACH.DAO
{
    public class QuyenTCDAO
    {
        private static QuyenTCDAO instance;

        public static QuyenTCDAO Instance
        {
            get { if (instance == null) instance = new QuyenTCDAO(); return instance; }
            private set { instance = value; }
        }

        private QuyenTCDAO() { }
        public QuyenTC GetQuyenTCByUserName(string userName)
        {
            string query = "SELECT * FROM dbo.GetQuyen( @userName )";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { userName });

            foreach (DataRow item in data.Rows)
            {
                return new QuyenTC(item);
            }
            return null;
        }
    }
}   
