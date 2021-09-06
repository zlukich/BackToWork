using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackToWork.Interfaces
{
    public interface IOrdererModel
    {
        string login_orderer { get; set; }
        string mobile_orderer { get; set; }
        string company_description { get; set; }
        string name_orderer { get; set; }
        bool activated { get; set; }
        string email_orderer { get; set; }
    }
}
