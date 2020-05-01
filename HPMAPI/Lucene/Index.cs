using HPMAPI.Entities;
using HPMAPI.Repositories;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Store.Azure;
using Lucene.Net.Util;
using Microsoft.Azure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPMAPI.Interfaces
{
    public class Index //: IIndex
    {
        public IndexWriter Writer { get; private set; }
        public IndexReader Reader { get; private set; }
        public Index()
        {
/*            var connectionString = "UseDevelopmentStorage=true";

            var cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            //var indexLocation = @"C:\Users\dmegl_000\Documents\GitHub\HPMAPI\HPMAPI\Lucene\Index";
            AzureDirectory dir = new AzureDirectory(cloudStorageAccount, "packageCatalog");
            
            //var dir = FSDirectory.Open(indexLocation);

            //create an analyzer to process the text
            var analyzer = new StandardAnalyzer(LuceneVersion.LUCENE_48);

            //create an index writer
            var indexConfig = new IndexWriterConfig(LuceneVersion.LUCENE_48, analyzer);
            indexConfig.OpenMode = OpenMode.CREATE_OR_APPEND;
            Writer = new IndexWriter(dir, indexConfig);
            Writer.Flush(triggerMerge: false, applyAllDeletes: false);*/
        }

        public void AddPackage(Entities.Package pkg)
        {
 /*           Document doc = new Document
            {
                // StringField indexes but doesn't tokenize
                 new StringField("location",
                    pkg.location,
                    Field.Store.YES),
                new TextField("name",
                    pkg.name,
                    Field.Store.YES),
                new TextField("description",
                    pkg.description,
                    Field.Store.YES),
                new TextField("category", pkg.category, Field.Store.YES)
            };
            
            Writer.AddDocument(doc);*/
        }

        public void DeleteAll()
        {
  //          Writer.DeleteAll();
        }

        public void Save()
        {
 //           Writer.Commit();
 //           Writer.Flush(triggerMerge: false, applyAllDeletes: false);
        }

        public List<string> Search(string searchString)
        {
            List<string> packages = new List<string>();
         /*   QueryParser parser = new QueryParser(LuceneVersion.LUCENE_48, "description", new SimpleAnalyzer(LuceneVersion.LUCENE_48));
            var query = parser.Parse(searchString);


 

            var searcher = new IndexSearcher(Writer.GetReader(applyAllDeletes: true));
            var hits = searcher.Search(query, 20 ).ScoreDocs;
            foreach (var hit in hits)
            {
                var foundDoc = searcher.Doc(hit.Doc);
                packages.Add(foundDoc.Get("location"));
            }*/
            return packages;
        }
    }
}
