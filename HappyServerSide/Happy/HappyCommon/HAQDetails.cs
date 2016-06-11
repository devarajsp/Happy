using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Microsoft.WindowsAzure.Storage.Table.Queryable;
using Microsoft.WindowsAzure.Storage.Analytics;
using Microsoft.WindowsAzure.Storage.Table.Protocol;
using System.Reflection;

using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using Microsoft.WindowsAzure;
using System.Data.Services.Client;
using System.Reflection;

namespace HappyCommon
{
    public class HAQDetailsRepository : IRepository
    {
        readonly string _happyStorageConnectionString;
        readonly CloudStorageAccount _happyStorageAccount;
        CloudTableClient _tableClient;
        CloudTable _table;
        TableOperation _insertOperation;
        TableOperation _deleteOperation;
        TableOperation _replaceOperation;

        public const string DEFAULT_TABLE_NAME = "HAQDetails";
        public const string DEFAULT_PART_KEY = "HAPPY";
        public const string DEFAULT_STR_VALUE = "EMPTY";

        private readonly IConfigManager _configManager;

        bool _disposed = false;
        string _partKey = string.Empty;

        public HAQDetailsRepository(string tableName, string partKey)
            : this(new HappyConfigManager(), tableName, partKey)
        {
        }


        public HAQDetailsRepository(IConfigManager configMgr,
                                     string tableName, string partKey)
        {
            _configManager = configMgr;

            _happyStorageConnectionString = _configManager.GetConfigurationSetting("AzureStorage.ConnectionString");
            _happyStorageAccount = CloudStorageAccount.Parse(_happyStorageConnectionString);
            _tableClient = _happyStorageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            _table = _tableClient.GetTableReference(tableName);
            _partKey = partKey;
        }

        public void InsertHAQDetails(HAQDetails device)
        {
            TableResult result;
            // Create the table if it doesn't exist.
            _table.CreateIfNotExists();
            try
            {
                // Create the TableOperation object that inserts the customer entity.
                _insertOperation = TableOperation.Insert(device);

                // Execute the insert operation.
                result = _table.Execute(_insertOperation);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<HAQDetails> GetAll(string rowKey)
        {
            List<HAQDetails> dEntList = new List<HAQDetails>();
            HAQDetails dEnt = null;

            TableQuery<HAQDetails> rangeQuery = new TableQuery<HAQDetails>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partKey),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, rowKey)
                 )
                );

            foreach (HAQDetails e in _table.ExecuteQuery(rangeQuery))
            {
                char[] delim = { ',' };

                dEnt = new HAQDetails();
                dEnt.RowKey = (e.RowKey == null) ? DEFAULT_STR_VALUE : e.RowKey;
                //dEnt.Id = (e.RowKey == null) ? "EMPTY" : e.RowKey;
                dEnt.Name = (e.Name == null) ? DEFAULT_STR_VALUE : e.Name;
                dEnt.HATopic = (e.HATopic == null) ? DEFAULT_STR_VALUE : e.HATopic;
                if (dEnt.roles != null)
                {
                    dEnt.AllowedRoles = dEnt.roles.Split(delim);
                }
                dEnt.roles = (e.roles == null) ? DEFAULT_STR_VALUE : e.roles;
                dEnt.Result = (e.Result == null) ? DEFAULT_STR_VALUE : e.Result;


                dEntList.Add(dEnt);
            }

            return dEntList;
        }


        public bool IsExist(string rowKey)
        {
            bool retVal = false;

            TableQuery<HAQDetails> rangeQuery = new TableQuery<HAQDetails>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partKey),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, rowKey)
                 )
                );

            foreach (HAQDetails e in _table.ExecuteQuery(rangeQuery))
            {
                retVal = true;
                break;
            }

            return retVal;
        }


        public List<HAQDetails> GetAll()
        {
            List<HAQDetails> dEntList = new List<HAQDetails>();
            HAQDetails dEnt;

            TableQuery<HAQDetails> rangeQuery = new TableQuery<HAQDetails>().Where(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partKey)
                );

            foreach (HAQDetails e in _table.ExecuteQuery(rangeQuery))
            {
                char[] delim = { ',' };

                dEnt = new HAQDetails();
                dEnt.RowKey = (e.RowKey == null) ? DEFAULT_STR_VALUE : e.RowKey;
                //dEnt.Id = (e.RowKey == null) ? "EMPTY" : e.RowKey;
                dEnt.Name = (e.Name == null) ? DEFAULT_STR_VALUE : e.Name;
                dEnt.HATopic = (e.HATopic == null) ? DEFAULT_STR_VALUE : e.HATopic;
                if (e.roles != null)
                {
                    e.roles = e.roles.Replace(" ", "");
                    dEnt.AllowedRoles = e.roles.Split(delim);

                }
                dEnt.roles = (e.roles == null) ? DEFAULT_STR_VALUE : e.roles;
                dEnt.Result = (e.Result == null) ? DEFAULT_STR_VALUE : e.Result;


                dEntList.Add(dEnt);

            }

            return dEntList;
        }


        public List<HAQDetails> GetAllForRole(string role)
        {
            List<HAQDetails> dEntList = new List<HAQDetails>();
            HAQDetails dEnt;

            TableQuery<HAQDetails> rangeQuery = new TableQuery<HAQDetails>().Where(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partKey)
                );

            foreach (HAQDetails e in _table.ExecuteQuery(rangeQuery))
            {
                char[] delim = { ',' };

                dEnt = new HAQDetails();
                dEnt.RowKey = (e.RowKey == null) ? DEFAULT_STR_VALUE : e.RowKey;
                //dEnt.Id = (e.RowKey == null) ? "EMPTY" : e.RowKey;
                dEnt.Name = (e.Name == null) ? DEFAULT_STR_VALUE : e.Name;
                dEnt.HATopic = (e.HATopic == null) ? DEFAULT_STR_VALUE : e.HATopic;
                if (e.roles != null)
                {

                    e.roles = e.roles.Replace(" ", "");
                    dEnt.AllowedRoles = e.roles.Split(delim);

                }
                dEnt.roles = (e.roles == null) ? DEFAULT_STR_VALUE : e.roles;
                dEnt.Result = (e.Result == null) ? DEFAULT_STR_VALUE : e.Result;

                if (dEnt.roles.ToLower().IndexOf(role.ToLower()) >= 0)
                {
                    dEntList.Add(dEnt);
                }

            }

            return dEntList;
        }


    }

    public class HAQDetails : TableEntity
    {
        public string DEFAULT_PARTKEY = "HAPPY";

        public HAQDetails()
        {

        }
        public HAQDetails(string name, string hatopic, string res)
        {

            Name = name;
            HATopic = hatopic;
            Result = res;
            PartitionKey = DEFAULT_PARTKEY;
            RowKey = name+DateTime.Now.Ticks.ToString();

        }
        public string Name
        {
            get; set;
        }
        public string HATopic
        {
            get; set;
        }

        public string Result
        {
            get; set;
        }

        public string roles
        {
            get; set;
        }
        public string[] AllowedRoles
        {
            get; set;
        }

        public string htmlString
        {
            get; set;
        }

    }
}
