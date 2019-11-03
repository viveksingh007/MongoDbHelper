using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MongoDBHelper.Test
{
    [TestClass]
    public class DBHelperUnitTests
    {
        [TestMethod]
        public void IsApplictionInstalled()
        {
            var dbHelper = new DBHelper();
            var result = dbHelper.IsApplictionInstalled("MongoDB 4.2.1 2008R2Plus SSL (64 bit)");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsServerRunning()
        {
            var dbHelper = new DBHelper();
            var result = dbHelper.IsServerRunning("mongodb://localhost:27017");
            Assert.IsTrue(result);
        }
    }
}
