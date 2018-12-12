namespace FastWell.UnitTestExamples
{
    public class Kindervriend
    {
        public string Name { get; }
        public string Woonplaats { get; }
        public int Leeftijd { get; }

        public Kindervriend(string name, string woonplaats, int leeftijd)
        {
            Name = name;
            Woonplaats = woonplaats;
            Leeftijd = leeftijd;
        }
    }
}