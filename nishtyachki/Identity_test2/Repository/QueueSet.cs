using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminApp.Repository
{
    public class QueueSet
    {
        private AdminAppService.IAdminAppService _service;

        public QueueSet(AdminAppService.IAdminAppService _service)
        {
            // TODO: Complete member initialization
            this._service = _service;
        }
    }
}
