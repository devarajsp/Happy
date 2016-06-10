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
    public class HappyAlertsRepository : IRepository
    {
        readonly string _happyStorageConnectionString;
        readonly CloudStorageAccount _happyStorageAccount;
        CloudTableClient _tableClient;
        CloudTable _table;
        TableOperation _insertOperation;
        TableOperation _deleteOperation;
        TableOperation _replaceOperation;

        public const string DEFAULT_TABLE_NAME = "HappyAlerts";
        public const string DEFAULT_PART_KEY = "HAPPY";
        public const string DEFAULT_STR_VALUE = "EMPTY";

        private readonly IConfigManager _configManager;

        bool _disposed = false;
        string _partKey = string.Empty;

        public HappyAlertsRepository(string tableName, string partKey)
            : this(new HappyConfigManager(), tableName, partKey)
        {
        }


        public HappyAlertsRepository(IConfigManager configMgr,
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

        public void InsertHappyAlertDetails(HappyAlert device)
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
        public List<HappyAlert> GetAll(string rowKey)
        {
            List<HappyAlert> dEntList = new List<HappyAlert>();
            HappyAlert dEnt = null;

            TableQuery<HappyAlert> rangeQuery = new TableQuery<HappyAlert>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partKey),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, rowKey)
                 )
                );

            foreach (HappyAlert e in _table.ExecuteQuery(rangeQuery))
            {
                char[] delim = { ',' };

                dEnt = new HappyAlert();
                dEnt.RowKey = (e.RowKey == null) ? DEFAULT_STR_VALUE : e.RowKey;
                //dEnt.Id = (e.RowKey == null) ? "EMPTY" : e.RowKey;
                dEnt.Name = (e.Name == null) ? DEFAULT_STR_VALUE : e.Name;
                dEnt.AlertMessage = (e.AlertMessage == null) ? DEFAULT_STR_VALUE : e.AlertMessage;
                if (dEnt.roles != null)
                {
                    dEnt.AllowedRoles = dEnt.roles.Split(delim);
                }
                dEnt.roles = (e.roles == null) ? DEFAULT_STR_VALUE : e.roles;
                dEnt.Description = (e.Description == null) ? DEFAULT_STR_VALUE : e.Description;


                dEntList.Add(dEnt);
            }

            return dEntList;
        }


        public bool IsExist(string rowKey)
        {
            bool retVal = false;

            TableQuery<HappyAlert> rangeQuery = new TableQuery<HappyAlert>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partKey),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, rowKey)
                 )
                );

            foreach (HappyAlert e in _table.ExecuteQuery(rangeQuery))
            {
                retVal = true;
                break;
            }

            return retVal;
        }


        public List<HappyAlert> GetAll()
        {
            List<HappyAlert> dEntList = new List<HappyAlert>();
            HappyAlert dEnt;

            TableQuery<HappyAlert> rangeQuery = new TableQuery<HappyAlert>().Where(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partKey)
                );

            foreach (HappyAlert e in _table.ExecuteQuery(rangeQuery))
            {
                char[] delim = { ',' };

                dEnt = new HappyAlert();
                dEnt.RowKey = (e.RowKey == null) ? DEFAULT_STR_VALUE : e.RowKey;
                //dEnt.Id = (e.RowKey == null) ? "EMPTY" : e.RowKey;
                dEnt.Name = (e.Name == null) ? DEFAULT_STR_VALUE : e.Name;
                dEnt.AlertMessage = (e.AlertMessage == null) ? DEFAULT_STR_VALUE : e.AlertMessage;
                if (e.roles != null)
                {
                    e.roles = e.roles.Replace(" ", "");
                    dEnt.AllowedRoles = e.roles.Split(delim);

                }
                dEnt.roles = (e.roles == null) ? DEFAULT_STR_VALUE : e.roles;
                dEnt.Description = (e.Description == null) ? DEFAULT_STR_VALUE : e.Description;


                dEntList.Add(dEnt);

            }

            return dEntList;
        }


        public List<HappyAlert> GetLatest()
        {
            List<HappyAlert> dEntList = new List<HappyAlert>();
            HappyAlert dEnt;

            TableQuery<HappyAlert> rangeQuery = new TableQuery<HappyAlert>().Where(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partKey)
                );

            foreach (HappyAlert e in _table.ExecuteQuery(rangeQuery))
            {
                char[] delim = { ',' };

                dEnt = new HappyAlert();
                dEnt.RowKey = (e.RowKey == null) ? DEFAULT_STR_VALUE : e.RowKey;
                //dEnt.Id = (e.RowKey == null) ? "EMPTY" : e.RowKey;
                dEnt.Name = (e.Name == null) ? DEFAULT_STR_VALUE : e.Name;
                dEnt.AlertMessage = (e.AlertMessage == null) ? DEFAULT_STR_VALUE : e.AlertMessage;
                if (e.roles != null)
                {
                    e.roles = e.roles.Replace(" ", "");
                    dEnt.AllowedRoles = e.roles.Split(delim);

                }
                dEnt.roles = (e.roles == null) ? DEFAULT_STR_VALUE : e.roles;
                dEnt.Description = (e.Description == null) ? DEFAULT_STR_VALUE : e.Description;


                dEntList.Add(dEnt);

            }

            return dEntList;
        }

        public List<HappyAlert> GetAllForRole(string role)
        {
            List<HappyAlert> dEntList = new List<HappyAlert>();
            HappyAlert dEnt;

            TableQuery<HappyAlert> rangeQuery = new TableQuery<HappyAlert>().Where(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partKey)
                );

            foreach (HappyAlert e in _table.ExecuteQuery(rangeQuery))
            {
                char[] delim = { ',' };

                dEnt = new HappyAlert();
                dEnt.RowKey = (e.RowKey == null) ? DEFAULT_STR_VALUE : e.RowKey;
                //dEnt.Id = (e.RowKey == null) ? "EMPTY" : e.RowKey;
                dEnt.Name = (e.Name == null) ? DEFAULT_STR_VALUE : e.Name;
                dEnt.AlertMessage = (e.AlertMessage == null) ? DEFAULT_STR_VALUE : e.AlertMessage;
                if (e.roles != null)
                {

                    e.roles = e.roles.Replace(" ", "");
                    dEnt.AllowedRoles = e.roles.Split(delim);

                }
                dEnt.roles = (e.roles == null) ? DEFAULT_STR_VALUE : e.roles;
                dEnt.Description = (e.Description == null) ? DEFAULT_STR_VALUE : e.Description;

                if (dEnt.roles.ToLower().IndexOf(role.ToLower()) >= 0)
                {
                    dEntList.Add(dEnt);
                }

            }

            return dEntList;
        }


    }

    public class HappyAlert : TableEntity
    {
        public string DEFAULT_PARTKEY = "HAPPY";

        public static long runningNumber = 0;
        public HappyAlert()
        {

        }
        public HappyAlert(string name, string amsg, string desc)
        {

            Name = name;
            AlertMessage = amsg;
            Description = desc;
            PartitionKey = DEFAULT_PARTKEY;
            Sno = runningNumber++;
            RowKey = name + runningNumber.ToString();
        }

        public long Sno
        {
            get;set;
        }


        public string Name
        {
            get; set;
        }
        public string AlertMessage
        {
            get; set;
        }

        public string Description
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
