using LiveCharts.Wpf;
using LiveCharts;
using QL_XEKHACH.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QL_XEKHACH.DAO;
using Newtonsoft.Json.Linq;

namespace QL_XEKHACH.UserControls
{
    public partial class Page_ThongKeVe : UserControl
    {
        public Page_ThongKeVe()
        {
            InitializeComponent();
        }
        
        private void Page_ThongKeVe_Load(object sender, EventArgs e)
        {
            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Tháng",
                Labels = new[] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" }

            });
            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Vé bán ra",
                LabelFormatter = value => value.ToString()
            });
            cartesianChart2.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Tháng",
                Labels = new[] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" }

            });
            cartesianChart2.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Doanh thu (VNĐ)",
                LabelFormatter = value => value.ToString()
            });
            
            cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;
            cartesianChart2.LegendLocation = LiveCharts.LegendLocation.Right;
        }
        public void LoadDataToChart1(int nam)
        {
            List<ThongKeVe> thongkeve = ThongKeVeDAO.Instance.GetThongKeVeTheoThang(nam);
            cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();
            List<long> values = new List<long>();
            for (int month = 1; month <= 12; month++)
            {
                long value = 0;
                foreach (var thongKeVe in thongkeve)
                {
                    if (thongKeVe.Year == nam && thongKeVe.Month == month)
                    {
                        value = thongKeVe.Value;
                    }

                }
                values.Add(value);

            }
           
            var lineSeries = new LineSeries
            {
                Title = nam.ToString(),
                Values = new ChartValues<long>(values)
            };
            series.Add(lineSeries);
            cartesianChart1.Series = series;
        }
        public void LoadDataToChart2(int nam)
        {
            List<ThongKeVe> thongkedoanhthu = ThongKeVeDAO.Instance.GetThongKeDoanhTHuTheoThang(nam);
            cartesianChart2.Series.Clear();
            SeriesCollection series = new SeriesCollection();
            List<long> values = new List<long>();
            for (int month = 1; month <= 12; month++)
            {
                long value = 0;
                foreach (var thongKeVe in thongkedoanhthu)
                {

                    if (thongKeVe.Year == nam && thongKeVe.Month == month)
                    {
                        value = thongKeVe.Value;
                    }
                }
                values.Add(value);
            }
            
            var lineSeries = new LineSeries
            {
                Title = nam.ToString(),
                Values = new ChartValues<long>(values)
            };

            series.Add(lineSeries);
            cartesianChart2.Series = series;
        }
        void load_doanhthu_Ve(int nam)
        {
            List<ThongKeVe> thongkeve = ThongKeVeDAO.Instance.GetThongKeVeTheoThang(nam);
            List<ThongKeVe> thongkedoanhthu = ThongKeVeDAO.Instance.GetThongKeDoanhTHuTheoThang(nam);
            long values = 0;
            foreach (var thongKeVes in thongkeve)
            {
                for (int month = 1; month <= 12; month++)
                {
                    if (thongKeVes.Year == nam && thongKeVes.Month == month)
                    {
                        values+= thongKeVes.Value;
                    }
                   
                }

            }
            long values1 = 0;
            foreach (var thongKedoanhthu in thongkedoanhthu)
            {
                
                for (int month = 1; month <= 12; month++)
                {
                    if (thongKedoanhthu.Year == nam && thongKedoanhthu.Month == month)
                    {
                        values1 += thongKedoanhthu.Value;
                    }
                }

            }
            textBox1.Text = values.ToString();
            textBox2.Text = values1.ToString();

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nam = int.Parse(comboBox1.Text);
            LoadDataToChart1(nam);
            LoadDataToChart2(nam);
            load_doanhthu_Ve( nam);
        }
    }
}

