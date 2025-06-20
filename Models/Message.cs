using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public int Roomid { get; set; }
        public int Senderid { get; set; }
        public string Content { get; set; }
    }
}
