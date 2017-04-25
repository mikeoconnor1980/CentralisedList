using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using ODD.Objects;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ODD.Data
{
    public interface IObligationData
    {
        Task<Obligation> Save(Obligation obligation);

        Task<Obligation> LoadById(string obligationId);

        Task<Obligation> ReplaceById(string obligationId, Obligation obligation);

        Task<Obligation> UpsertById(string obligationId, Obligation obligation);

        void DeleteById(string obligationId);
    }

    public class ObligationData : IObligationData
    {
        #region Private Fields

        DocumentClient _documentclient;
        string _databaseId;
        string _collectionId;

        #endregion

        #region Public Constructors

        public ObligationData(DataCommand dc, string collectionId)
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

        public async Task<Obligation> LoadById(string obligationId)
        {
            try
            {
                var response = await _documentclient.ReadDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, obligationId));
                return JsonConvert.DeserializeObject<Obligation>(response.Resource.ToString());
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

        public async Task<Obligation> Save(Obligation obligation)
        {
            try
            {
                var response = await _documentclient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId), obligation);
                return JsonConvert.DeserializeObject<Obligation>(response.Resource.ToString());
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

        public async Task<Obligation> ReplaceById(string obligationId, Obligation obligation)
        {
            var response = await _documentclient.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, obligationId), obligation);
            return JsonConvert.DeserializeObject<Obligation>(response.Resource.ToString());
        }

        public async Task<Obligation> UpsertById(string obligationId, Obligation obligation)
        {
            var response = await _documentclient.UpsertDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, obligationId), obligation);
            return JsonConvert.DeserializeObject<Obligation>(response.Resource.ToString());
        }

        public async void DeleteById(string obligationId)
        {
            try
            {
                var response = await _documentclient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, obligationId));
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

        #endregion
    }
}
