using System;

namespace BlogSpider.Tracking.ToUri
{
    public static class ActorSafeNameFromUri
    {
        public static string ToActorName(this Uri uri)
        {
            return Uri.EscapeDataString(uri.ToString());
        }
    }
}
