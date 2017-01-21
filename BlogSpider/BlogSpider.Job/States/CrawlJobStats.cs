namespace BlogSpider.Job.States
{
    public class CrawlJobStats
    {
        public CrawlJobStats(CrawlJob key)
        {
            Key = key;
        }

        public CrawlJob Key { get; }

        public int TotalDocumentsDiscovered => HtmlDocumentsDiscovered + ImagesDiscovered;

        public int HtmlDocumentsDiscovered { get; private set; }

        public int ImagesDiscovered { get; private set; }

        public int TotalDocumentsDownloaded => HtmlDocumentsDownloaded + ImagesDownloaded;

        public int HtmlDocumentsDownloaded { get; private set; }

        public int ImagesDownloaded { get; private set; }

        public int TotalBytesDownloaded => HtmlBytesDownloaded + ImageBytesDownloaded;

        public int HtmlBytesDownloaded { get; private set; }

        public int ImageBytesDownloaded { get; private set; }

        public bool IsEmpty => TotalDocumentsDiscovered == 0;

        public CrawlJobStats Copy(int? htmlDiscovered = null, int? imgDiscovered = null, int? htmlDownloaded = null,
            int? imgDownloaded = null, int? htmlBytesDownloaded = null, int? imgBytesDownloaded = null)
        {
            return new CrawlJobStats(Key)
            {
                HtmlDocumentsDiscovered = htmlDiscovered ?? HtmlDocumentsDiscovered,
                HtmlDocumentsDownloaded = htmlDownloaded ?? HtmlDocumentsDownloaded,
                ImagesDiscovered = imgDiscovered ?? ImagesDiscovered,
                ImagesDownloaded = imgDownloaded ?? ImagesDownloaded,
                HtmlBytesDownloaded = htmlBytesDownloaded ?? TotalBytesDownloaded,
                ImageBytesDownloaded = imgBytesDownloaded ?? ImageBytesDownloaded
            };
        }

        public CrawlJobStats WithCompleted(CompletedDocument doc)
        {
            if (doc.Document.IsImage)
            {
                return Copy(imgDownloaded: ImagesDownloaded + 1, imgBytesDownloaded: ImageBytesDownloaded + doc.NumBytes);
            }
            return Copy(htmlDownloaded: HtmlDocumentsDownloaded + 1,
                htmlBytesDownloaded: HtmlBytesDownloaded + doc.NumBytes);
        }

        public CrawlJobStats WithDiscovered(DiscoveredDocuments doc)
        {
            return Copy(HtmlDocumentsDiscovered + doc.HtmlDocs, ImagesDiscovered + doc.Images);
        }

        public bool CanMerge(CrawlJobStats other)
        {
            return Key.Equals(other.Key);
        }

        public CrawlJobStats Merge(CrawlJobStats other)
        {
            if (CanMerge(other))
            {
                return Copy(HtmlDocumentsDiscovered + other.HtmlDocumentsDiscovered,
                    ImagesDiscovered + other.ImagesDiscovered,
                    HtmlDocumentsDownloaded + other.HtmlDocumentsDownloaded,
                    ImagesDownloaded + other.ImagesDownloaded,
                    HtmlBytesDownloaded + other.HtmlBytesDownloaded,
                    ImageBytesDownloaded + other.ImageBytesDownloaded);
            }

            return this;
        }

        public CrawlJobStats Reset()
        {
            return new CrawlJobStats(Key);
        }

        public override string ToString()
        {
            return $"Discovered: {TotalDocumentsDiscovered} (HTML: {HtmlDocumentsDiscovered}, IMG: {ImagesDiscovered}) -- " +
                   $"Downloaded {TotalDocumentsDownloaded} (HTML: {HtmlDocumentsDownloaded}, IMG: {ImagesDownloaded}) -- " +
                   $"Bytes {TotalBytesDownloaded} (HTML: {HtmlBytesDownloaded}, IMG: {ImageBytesDownloaded})";
        }
    }
}
