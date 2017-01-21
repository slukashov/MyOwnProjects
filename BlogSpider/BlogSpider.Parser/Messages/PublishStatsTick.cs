namespace BlogSpider.Parser.Messages
{
    public class PublishStatsTick
    {
        private PublishStatsTick() { }

        public static PublishStatsTick Instance { get; } = new PublishStatsTick();
    }
}
