using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class TestManager : ITestManager
    {
        private static List<TestNum> nums = new List<TestNum>()
        {
            new TestNum{Id=1,Value=100},
            new TestNum{Id=2,Value=50000},
            new TestNum{Id=3,Value=-3}
        };
        /*new TestNum(1,100),
            new TestNum(2,50000),
            new TestNum(3,-3)*/

        public void AddNum(TestNum testNum)
        {
            nums.Add(testNum);
        }

        public void DeleteNum(int id)
        {
            TestNum num = nums.Where(x => x.Id == id).FirstOrDefault();
            if (num == null)
            {
                throw new Exception("cant delete something that does not exist");
            }
            nums.Remove(num);
        }

        public void EditNum(TestNum testNum)
        {
            TestNum num = nums.Where(x => x.Id == testNum.Id).FirstOrDefault();
            num.Value = testNum.Value;

        }

        public IEnumerable<TestNum> GetAllNums()
        {
            return nums;
        }

        public TestNum GetNumById(int id)
        {
            return nums.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
