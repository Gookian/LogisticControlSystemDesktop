using static System.Net.Mime.MediaTypeNames;

namespace LogisticControlSystemDesktop.Models
{
    public class IdTargetValueItem
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
