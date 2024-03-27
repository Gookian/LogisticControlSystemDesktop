namespace LogisticControlSystemDesktop.Models
{
    public class Parametr
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public string PropertyName { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
