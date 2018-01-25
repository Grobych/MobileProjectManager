using System;
using System.Collections.Generic;
using System.Text;

namespace STPMP.Entity
{
    class User
    {
        private string name;
        private int ID;
        private bool isOnline;

        public bool IsOnline { get => isOnline; set => isOnline = value; }
        public int GetID { get => ID; set => ID = value; }
        public string Name { get => name; set => name = value; }

    }
}
