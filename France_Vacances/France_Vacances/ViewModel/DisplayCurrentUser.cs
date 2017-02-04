using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using France_Vacances.Model;

namespace France_Vacances.ViewModel
{
    public class DisplayCurrentUser
    {
        private UserSingelton _currentUserSingelton;

        public DisplayCurrentUser()
        {
            _currentUserSingelton = UserSingelton.GetInstance();
        }
    }
}
