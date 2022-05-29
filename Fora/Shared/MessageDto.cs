using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fora.Shared
{
    public class MessageDto
    {
        public int MessageId { get; set; }
        public int ThreadId { get; set; }
        public string Message { get; set; } = String.Empty;
    }
}
