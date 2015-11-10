using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Store;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Lucene.Net.Analysis.Standard;
using System.IO;
using Version = Lucene.Net.Util.Version;

namespace TestFactory.Business.Components.Lucene
{
    public class BaseSearcher
    {
        protected const int HITS_LIMIT = 1000;

        protected string path;

        protected FSDirectory _dir;

        public BaseSearcher( )
            : this(Path.Combine("./", "lucene_index"))
        {
        }

        public BaseSearcher(string directoryPath)
        {
            path = directoryPath;
            directory = FSDirectory.Open(new DirectoryInfo(path));
        }

        protected FSDirectory directory
        {
            get
            {
                if (IndexWriter.IsLocked(_dir))
                {
                    IndexWriter.Unlock(_dir);
                }
                var lockFilePath = Path.Combine(path, "write.lock");
                if (File.Exists(lockFilePath))
                {
                    File.Delete(lockFilePath);
                }
                return _dir;
            }
            set
            {
                _dir = value;
            }
        }

        protected Query parseQuery(string searchQuery, QueryParser parser)
        {
            Query query;
            try
            {
                query = parser.Parse(searchQuery.Trim());
            }
            catch (ParseException)
            {
                query = parser.Parse(QueryParser.Escape(searchQuery.Trim()));
            }
            return query;
        }

        public bool ClearIndex()
        {
            try
            {
                var analyzer = new StandardAnalyzer(Version.LUCENE_30);
                using (var writer = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    writer.DeleteAll();
                    analyzer.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
