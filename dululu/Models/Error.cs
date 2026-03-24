
namespace dululu.Models
{
    public class Error
    {
        public string Property {  get; set; }
        public string Value { get; set; }

        internal static void Add(Error error)
        {
            throw new NotImplementedException();
        }

        internal static bool Any()
        {
            throw new NotImplementedException();
        }
    }
}
