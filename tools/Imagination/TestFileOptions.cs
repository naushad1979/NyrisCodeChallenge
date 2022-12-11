using System.IO;

namespace Imagination
{
    internal sealed class TestFileOptions
    {
        //public string BaseDirectory { get; set; } = Directory.GetCurrentDirectory();
        public string BaseDirectory 
        {
            get
            {
                return Path.Combine(Directory.GetParent("..").Parent.FullName,"resources");
            }
        } 
        
    }
}
