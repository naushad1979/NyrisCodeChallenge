namespace Imagination.Services
{
    public class CoversionResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public byte[] TargatedStream { get; set; }
    }
}
