using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ODD.Data
{
    public class DataCommand
    {
        #region Public Fields

        public DocumentClient docclient { get; set; }
        public string databaseId { get; set; }
        public string collectionId { get; set; }

        #endregion

        public DataCommand(string EndpointUri, string PrimaryKey, string DatabaseId, string CollectionId)
        {
            docclient = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
            docclient.OpenAsync();

            databaseId = DatabaseId;
            collectionId = CollectionId;
        }
    }
}
