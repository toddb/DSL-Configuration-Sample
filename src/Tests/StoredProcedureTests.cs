using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Data.Common;

namespace visFleetConnector.Tests
{
    /// <summary>
    /// Summary description for StoredProcedureTests
    /// </summary>
    [TestClass]
    public class StoredProcedureTests
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        private static SqlConnection connection { get; set; }
        
        [ClassInitialize()]
        public static void InitialiseDatabase(TestContext testContext)
        {
            connection = new SqlConnection("");
            connection.Open();
        }

        [ClassCleanup()]
        public static void TeardownDatabase()
        {
            connection.Close();
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        //
        // Use ClassCleanup to run code after all tests in a class have run
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Test_IFP_Vehicle_Read()
        {
            SqlCommand command = new SqlCommand
                                     {
                                         CommandText = "IFP_Vehicle_Read_FleetNo",
                                         Connection = connection,
                                         CommandType=CommandType.StoredProcedure
                                     };
            command.Parameters.AddWithValue("@vehicle", 1);
            
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);

            Assert.AreEqual(1, dataset.Tables.Count);
            Assert.AreEqual(1, dataset.Tables[0].Rows.Count);

        }

        
    }
}
