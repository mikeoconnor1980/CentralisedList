using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using ODD.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ODD.Data
{
    public interface IClientData
    {
        Task<Client> Save(Client client);

        Task<Client> LoadById(string id);

        Task<Client> ReplaceById(string id, Client client, bool insert);

        Task<Client> UpsertById(string id, Client client);

        void DeleteById(string id);
    }


    public class ClientData : IClientData
    {
        #region Private Fields

        DocumentClient _documentclient;
        string _databaseId;
        string _collectionId;

        #endregion

        #region Public Constructors

        public ClientData(DataCommand dc, string collectionId)
        {
            _documentclient = dc.docclient;
            _databaseId = dc.databaseId;
            _collectionId = collectionId;
            CreateCollectionIfNotExists();

        }

        #endregion

        #region Private Methods
        private void CreateCollectionIfNotExists()
        {
            try
            {
                _documentclient.ReadDocumentCollectionAsync(
                    UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _documentclient.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(_databaseId),
                        new DocumentCollection { Id = _collectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }
        #endregion

        #region Public Methods

        public async Task<List<Client>> Load()
        {
            try
            {
                string collectionLink = UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId).ToString();
                //var response = await _documentclient.ReadDocumentCollectionAsync(());
                return _documentclient.CreateDocumentQuery<Client>(collectionLink).ToList();
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Document not found");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Client> LoadById(string id)
        {
            try
            {
                var response = await _documentclient.ReadDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, id));
                return JsonConvert.DeserializeObject<Client>(response.Resource.ToString());
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Document not found");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Client> Save(Client client)
        {
            try
            {
                var response = await _documentclient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId), client);
                return JsonConvert.DeserializeObject<Client>(response.Resource.ToString());
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new Exception("Document already exists");
                }
                else
                {
                    throw;
                }
            }

        }

        public async Task<Client> ReplaceById(string id, Client client, bool insert)
        {
            try
            {
                string documentLink = UriFactory.CreateDocumentUri(_databaseId, _collectionId, id).ToString();
                var response = await _documentclient.ReplaceDocumentAsync(documentLink, client);
                return JsonConvert.DeserializeObject<Client>(response.Resource.ToString());
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    return await this.Save(client);
                }
                else
                { throw; }
            }
        }

        public async Task<Client> UpsertById(string id, Client client)
        {
            string documentLink = UriFactory.CreateDocumentUri(_databaseId, _collectionId, id).ToString();
            var response = await _documentclient.UpsertDocumentAsync(documentLink, client);
            return JsonConvert.DeserializeObject<Client>(response.Resource.ToString());
        }

        public void UpsertById2(string id, Client client)
        {
            try
            {
                _documentclient.UpsertDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, id), client);
            }
            catch (AggregateException ex)
            {
                throw ex;
            }
            catch (DocumentClientException ex)
            {
                throw ex;

            }
        }

        public void DeleteById(string id)
        {
            throw new NotImplementedException();
        }



        #endregion
    }
}
