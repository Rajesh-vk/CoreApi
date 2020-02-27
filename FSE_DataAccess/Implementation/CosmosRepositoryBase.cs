using FSE_DataAccess.Interfaces;
using System;
using Microsoft.Azure.Documents.Client;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using System.Collections.Generic;
using System.Linq;

namespace FSE_DataAccess.Implementation
{
    public abstract class CosmosRepositoryBase<TEntity> : ICosmosRepositoryBase<TEntity> where TEntity : class
    {
        private string EndpointUrl = "https://localhost:8081"; 

        private string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

        private DocumentClient cosmosClient;

        private Microsoft.Azure.Documents.Database database;

        private Microsoft.Azure.Documents.DocumentCollection docCollection;

        private string databaseId = "MyFirstCosmosDB";

        private string docCollectionId = "MyFirstCosmosDocCollection";

        public CosmosRepositoryBase(string collectionId)
        {
            this.docCollectionId = collectionId;
            this.cosmosClient = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);
            this.database = new Database() { Id = databaseId };
            this.database = this.cosmosClient.CreateDatabaseIfNotExistsAsync(database).Result;
            this.docCollection = new DocumentCollection() { Id = docCollectionId };
            this.docCollection = this.cosmosClient.CreateDocumentCollectionIfNotExistsAsync("dbs/" + databaseId, docCollection).Result;
        }

        void ICosmosRepositoryBase<TEntity>.Delete(string id)
        {
            var response = this.cosmosClient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(databaseId, docCollectionId, id)).Result;
        }

        IEnumerable<TEntity> ICosmosRepositoryBase<TEntity>.GetAll()
        {
            return this.cosmosClient.CreateDocumentQuery<TEntity>(UriFactory.CreateDocumentCollectionUri(databaseId, docCollectionId), "select * from c")
                                    .ToList();
        }

        TEntity ICosmosRepositoryBase<TEntity>.GetById(string id)
        {
            var output = this.cosmosClient.CreateDocumentQuery<TEntity>(UriFactory.CreateDocumentCollectionUri(databaseId, docCollectionId), "select * from c where c.id = '" + id + "'");
            var response = output.ToList();
            if (response != null && response.Count() > 0)
                return response.FirstOrDefault();
            else return null;
        }

        void ICosmosRepositoryBase<TEntity>.Insert(TEntity obj)
        {
            var document1 = this.cosmosClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseId, docCollectionId), obj).Result;
        }

        void ICosmosRepositoryBase<TEntity>.Save()
        {
            throw new NotImplementedException();
        }

        void ICosmosRepositoryBase<TEntity>.Update(string id,TEntity obj)
        {
            var document1 = this.cosmosClient.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, docCollectionId, id), obj).Result;
        }
    }
}
