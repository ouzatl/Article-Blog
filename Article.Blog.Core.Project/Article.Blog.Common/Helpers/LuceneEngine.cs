using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Article.Blog.Common.Helpers
{
    public class LuceneEngine
    {
        private Analyzer _Analyzer;
        private Directory _Directory;
        private IndexWriter _IndexWriter;
        private IndexSearcher _IndexSearcher;
        private Document _Document;
        private QueryParser _QueryParser;
        private Query _Query;
        private string _IndexPath = @"C:\Users\Oğuz\Desktop\Article-Blog\LuceneIndex";

        public LuceneEngine()
        {
            _Analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            _Directory = FSDirectory.Open(_IndexPath);
        }

        public void AddToIndex(IEnumerable values)
        {
            using (_IndexWriter = new IndexWriter(_Directory, _Analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (var loopEntity in values)
                {
                    _Document = new Document();

                    foreach (var loopProperty in loopEntity.GetType().GetProperties())
                    {
                        _Document.Add(new Field(loopProperty.Name, loopProperty.GetValue(loopEntity).ToString(), Field.Store.YES, Field.Index.ANALYZED));

                        _IndexWriter.AddDocument(_Document);
                        _IndexWriter.Optimize();
                        _IndexWriter.Commit();
                    }
                }
            }
        }

        public List<Article.Blog.Data.Models.Article> Search(string field, string keyword)
        {
            _QueryParser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, field, _Analyzer);
            _Query = _QueryParser.Parse(keyword);

            using (_IndexSearcher = new IndexSearcher(_Directory, true))
            {
                var article = new List<Article.Blog.Data.Models.Article>();
                var result = _IndexSearcher.Search(_Query, 10);

                foreach (var loopDoc in result.ScoreDocs.OrderBy(s => s.Score))
                {
                    _Document = _IndexSearcher.Doc(loopDoc.Doc);

                    article.Add(new Article.Blog.Data.Models.Article() { Id = System.Convert.ToInt32(_Document.Get("Id")), Description = _Document.Get("Description") });
                }

                return article;
            }
        }
    }
}