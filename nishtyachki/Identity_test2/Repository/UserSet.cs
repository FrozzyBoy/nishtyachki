using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminApp.Repository
{
    public class UserSet
    {
        private AdminAppService.IAdminAppService _service;

        public UserSet(AdminAppService.IAdminAppService _service)
        {
            // TODO: Complete member initialization
            this._service = _service;

        }
    }
}
