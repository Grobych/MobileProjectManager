using System;
using System.Collections.Generic;
using System.Text;

namespace MobileProjectManager.Models
{
    class User
    {
        public bool IsOnline { get; set; }

        public int ID { get; set; }

        public string Name { get; set; }

        public byte[] Img { get; set; }
    }
}
