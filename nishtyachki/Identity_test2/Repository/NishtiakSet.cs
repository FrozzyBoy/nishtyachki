using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminApp.Repository
{
    public class NishtiakSet
    {
        private AdminAppService.IAdminAppService _service;

        public NishtiakSet(AdminAppService.IAdminAppService _service)
        {
            // TODO: Complete member initialization
            this._service = _service;
        }
    }
}
