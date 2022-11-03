using System.Globalization;

namespace CarAcademyProject.Middleware
{
    public class ApplicationExeption : Exception
    {
        public ApplicationExeption() : base() { }

        public ApplicationExeption(string message) : base(message) { }

        public ApplicationExeption(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
