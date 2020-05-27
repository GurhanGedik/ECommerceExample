using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Web.Lib
{
    public class WorkContext
    {

        public virtual Customer GetAuthenticatedCustomer()
        {
            GurhanDbEntities db = new GurhanDbEntities();
            var cookieEmail = HttpContext.Current.User.Identity.Name;

            return db.Customers.FirstOrDefault(x => x.Email == cookieEmail);

        }
    }
}