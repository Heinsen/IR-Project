﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheApplication.Controller
{
    public interface IQueryParser
    {
        string InformationNeedParser(string userQuery);
    }
}
