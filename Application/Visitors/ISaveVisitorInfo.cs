﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Visitors
{
    public interface ISaveVisitorInfo
    {
        void Execute(RequestSaveVisitorInfoDto request);
    }
}
