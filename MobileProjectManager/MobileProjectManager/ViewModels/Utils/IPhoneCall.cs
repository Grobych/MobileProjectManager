using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileProjectManager.ViewModels.Utils
{
    public interface IPhoneCall
    {
        Task Call(string number);
    }
}
