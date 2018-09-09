namespace Deal.UI.Models.Common
{
    public class Errors
    {
        public int Code { get; set; }

        public int? SourceCode { get; set; }

        public int? Degree { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Message { get; set; }
    }
}