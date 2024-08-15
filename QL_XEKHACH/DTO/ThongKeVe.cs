namespace QL_XEKHACH.DTO
{
     class ThongKeVe
    {
        private int year;
        private int month;
        private int value;

        public int Year { get => year; set => year = value; }
        public int Month { get => month; set => month = value; }
        public int Value { get => value; set => this.value = value; }
        public ThongKeVe(int year, int month, int value)
        {
            this.Year = year;
            this.Month = month;
            this.Value = value;
        }
    }
}
