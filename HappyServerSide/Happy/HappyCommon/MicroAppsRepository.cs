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
    public class MicroAppsRepository : IRepository
    {
        readonly string _happyStorageConnectionString;
        readonly CloudStorageAccount _happyStorageAccount;
        CloudTableClient _tableClient;
        CloudTable _table;
        TableOperation _insertOperation;
        TableOperation _deleteOperation;
        TableOperation _replaceOperation;

        public const string DEFAULT_TABLE_NAME = "microapps";
        public const string DEFAULT_PART_KEY = "HAPPY";
        public const string DEFAULT_STR_VALUE = "EMPTY";

        private readonly IConfigManager _configManager;
  
        bool _disposed = false;
        string _partKey = string.Empty;

        public MicroAppsRepository(string tableName, string partKey)
            : this(new HappyConfigManager(), tableName, partKey)
        {
        }


        public MicroAppsRepository(IConfigManager configMgr,
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

        public void InsertMicroAppDetails(MicroApp device)
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
        public List<MicroApp> GetAll(string rowKey)
        {
            List<MicroApp> dEntList = new List<MicroApp>();
            MicroApp dEnt = null;

            TableQuery<MicroApp> rangeQuery = new TableQuery<MicroApp>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partKey),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, rowKey)
                 )
                );

            foreach (MicroApp e in _table.ExecuteQuery(rangeQuery))
            {
                char[] delim = { ',' };

                dEnt = new MicroApp();
                dEnt.RowKey = (e.RowKey == null) ? DEFAULT_STR_VALUE : e.RowKey;
                //dEnt.Id = (e.RowKey == null) ? "EMPTY" : e.RowKey;
                dEnt.Name = (e.Name == null) ? DEFAULT_STR_VALUE : e.Name;
                dEnt.Url = (e.Url == null) ? DEFAULT_STR_VALUE : e.Url;
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

            TableQuery<MicroApp> rangeQuery = new TableQuery<MicroApp>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partKey),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, rowKey)
                 )
                );

            foreach (MicroApp e in _table.ExecuteQuery(rangeQuery))
            {
                retVal = true;
                break;
            }

            return retVal;
        }


        public List<MicroApp> GetAll()
        {
            List<MicroApp> dEntList = new List<MicroApp>();
            MicroApp dEnt;

            TableQuery<MicroApp> rangeQuery = new TableQuery<MicroApp>().Where(                
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partKey)
                );

            foreach (MicroApp e in _table.ExecuteQuery(rangeQuery))
            {
                char[] delim = { ',' };

                dEnt = new MicroApp();
                dEnt.RowKey = (e.RowKey == null) ? DEFAULT_STR_VALUE : e.RowKey;
                //dEnt.Id = (e.RowKey == null) ? "EMPTY" : e.RowKey;
                dEnt.Name = (e.Name == null) ? DEFAULT_STR_VALUE : e.Name;
                dEnt.Url = (e.Url == null) ? DEFAULT_STR_VALUE : e.Url;
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


        public List<MicroApp> GetAllForRole(string role)
        {
            List<MicroApp> dEntList = new List<MicroApp>();
            MicroApp dEnt;

            TableQuery<MicroApp> rangeQuery = new TableQuery<MicroApp>().Where(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partKey)
                );

            foreach (MicroApp e in _table.ExecuteQuery(rangeQuery))
            {
                char[] delim = { ',' };

                dEnt = new MicroApp();
                dEnt.RowKey = (e.RowKey == null) ? DEFAULT_STR_VALUE : e.RowKey;
                //dEnt.Id = (e.RowKey == null) ? "EMPTY" : e.RowKey;
                dEnt.Name = (e.Name == null) ? DEFAULT_STR_VALUE : e.Name;
                dEnt.Url = (e.Url == null) ? DEFAULT_STR_VALUE : e.Url;
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

        public List<MicroApp> GetAllPending()
        {
            List<MicroApp> dEntList = new List<MicroApp>();
            MicroApp dEnt;

            TableQuery<MicroApp> rangeQuery = new TableQuery<MicroApp>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partKey),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("IsApprovedToUse", QueryComparisons.Equal, "Pending")
                 )
                );

            foreach (MicroApp e in _table.ExecuteQuery(rangeQuery))
            {
                dEnt = new MicroApp();
                dEnt.RowKey = (e.RowKey == null) ? "EMPTY" : e.RowKey;
                //dEnt.Id = (e.RowKey == null) ? "EMPTY" : e.RowKey;
                dEnt.Name = (e.Name == null) ? "EMPTY" : e.Name;
             


                dEntList.Add(dEnt);
            }

            return dEntList;
        }


        public MicroApp UpdateEntity(string rowKey, MicroApp device, string approvalStatus)
        {

            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<MicroApp>(_partKey, rowKey);

            //  Execute the operation.
            TableResult retrievedResult = _table.Execute(retrieveOperation);

            //Assign the result to a object.
            MicroApp updateEntity = (MicroApp)retrievedResult.Result;

            if (updateEntity != null && device != null)
            {
                //updateEntity.Id = device.Id;
                // updateEntity.ETag = device.ETag;
            }

            if (updateEntity != null && approvalStatus != null)
            {
                //updateEntity.IsApprovedToUse = approvalStatus;
            }

            if (updateEntity != null)
            {
                //Create the Replace TableOperation.
                TableOperation updateOperation = TableOperation.Replace(updateEntity);

                //  Execute the operation.
                _table.Execute(updateOperation);
            }

            return updateEntity;
        }

        public MicroApp UpdateApprovalStatus(string rowKey, string approvalStatus)
        {

            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<MicroApp>(_partKey, rowKey);

            //  Execute the operation.
            TableResult retrievedResult = _table.Execute(retrieveOperation);

            //Assign the result to a object.
            MicroApp updateEntity = (MicroApp)retrievedResult.Result;

            if (updateEntity != null)
            {
               // updateEntity.IsApprovedToUse = approvalStatus;

                //Create the Replace TableOperation.
                TableOperation updateOperation = TableOperation.Replace(updateEntity);

                //  Execute the operation.
                _table.Execute(updateOperation);
            }
            return updateEntity;
        }

    }

    public class MicroApp : TableEntity
    {
        public string DEFAULT_PARTKEY = "HAPPY";

        public MicroApp()
        {

        }
        public MicroApp(string name, string url, string desc)
        {
            
            Name = name;
            Url = url;
            Description = desc;
            PartitionKey = DEFAULT_PARTKEY;
            RowKey = name;
            
        }
        public string Name
        {
            get; set;
        }
        public string Url
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
        public string [] AllowedRoles
        {
            get;set;
        }

        public string htmlButton
        {
            get;set;
        }

    }
}
