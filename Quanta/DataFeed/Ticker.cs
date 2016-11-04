namespace Quanta.DataFeed
{
    public class Ticker
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string TypeName { get; set; }
        public string Isin { get; set; }
        public int ListLevel { get; set; }
        public string Code { get; set; }

        public override string ToString()
        {
            return "Code: " + Code + ", Name: " + Name + ", ShortName: " + ShortName + ", TypeName: " + TypeName + ", ListLevel:" + ListLevel + ", ISIN:" + Isin;
        }
    }
}
