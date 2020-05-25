﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    interface ITestManager
    {

        IEnumerable<TestNum> GetAllNums();
        TestNum GetNumById(int id);
        void AddNum(TestNum testNum);
        void EditNum(TestNum testNum);
        void DeleteNum(int id);
    }
}