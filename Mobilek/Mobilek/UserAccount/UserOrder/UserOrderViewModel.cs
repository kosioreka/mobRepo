using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilek
{
    class UserOrderViewModel : ObservableObject, IPageViewModel
    {

        public string Name
        {
            get { return "Your Order"; }
        }
    }
}
