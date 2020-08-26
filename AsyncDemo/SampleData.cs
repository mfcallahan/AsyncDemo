namespace AsyncDemo
{
    public class SampleData
    {
        public string A { get;}
        public string B { get; }
        public int C { get; }

        public SampleData(string dataA, string dataB, int dataC)
        {
            A = dataA;
            B = dataB;
            C = dataC;
        }
    }
}
