using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoRentalSystemY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRentalSystemY.Tests
      
    //for checking working of rental class 
{
    [TestClass()]
    public class Form1Tests
    {
        [TestMethod()]
        //this method is used to ytest tge insert  the code 
        public void Form1Test()
        {
            Assert.Fail();

            Rental obj = new Rental();
            obj.Ins("ok");
        }




    }
}