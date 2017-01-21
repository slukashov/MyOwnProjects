using System;

namespace BlogSpider.Job.States
{
    public class CrawlJob : IEquatable<CrawlJob>
    {
        public Uri Root { get; }
        public string Domain => Root.AbsoluteUri;
        public bool FetchImages { get; }

        public CrawlJob(Uri root, bool fetchImages)
        {
            FetchImages = fetchImages;
            Root = root;
        }
        public override string ToString()
        {
            return Root.ToString();
        }

        #region Equality

        public bool Equals(CrawlJob other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || Equals(Root, other.Root);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((CrawlJob)obj);
        }

        public override int GetHashCode()
        {
            return Root?.GetHashCode() ?? 0;
        }

        #endregion
    }
}
