using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BlogSpider.Job.States
{
    public class CrawlDocument : IEquatable<CrawlDocument>
    {
        public Uri DocumentUri { get; }
        public bool IsImage { get; }
        public static IEqualityComparer<CrawlDocument> DocumentUriComparer { get; } = new DocumentUriEqualityComparer();

        public CrawlDocument(Uri documentUri, bool isImage = false)
        {
            IsImage = isImage;
            Debug.Assert(documentUri.IsAbsoluteUri, "documentUri must be absolute");
            DocumentUri = documentUri;
        }

        public bool Equals(CrawlDocument otherDocument)
        {
            if (ReferenceEquals(null, otherDocument)) return false;
            return ReferenceEquals(this, otherDocument) || Equals(DocumentUri, otherDocument.DocumentUri);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((CrawlDocument)obj);
        }

        public override int GetHashCode()
        {
            return DocumentUri?.GetHashCode() ?? 0;
        }

        private sealed class DocumentUriEqualityComparer : IEqualityComparer<CrawlDocument>
        {
            public bool Equals(CrawlDocument x, CrawlDocument y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return Equals(x.DocumentUri, y.DocumentUri);
            }

            public int GetHashCode(CrawlDocument obj)
            {
                return obj.DocumentUri?.GetHashCode() ?? 0;
            }
        }
    }
}