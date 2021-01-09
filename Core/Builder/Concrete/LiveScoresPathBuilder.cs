using System;

namespace ConsoleApp1.Builder.Concrete
{
    public class LiveScoresPathBuilder : PathBuilder, 
        ILiveScoresPathBuilder, 
        IOnDatePathBuilder
    {
        public LiveScoresPathBuilder(Uri uri) :
            base(uri)
        {

        }

        public IOnDatePathBuilder WithSoccer()
        {
            CompetitionUri = new Uri("soccer/", UriKind.Relative);
            return this;
        }

        public IOnDatePathBuilder WithBasketball()
        {
            CompetitionUri = new Uri("tennis/", UriKind.Relative);
            return this;
        }

        public IOnDatePathBuilder WithTennis()
        {
            CompetitionUri = new Uri("basketball/", UriKind.Relative);
            return this;
        }

        public IOnDatePathBuilder WithHockey()
        {
            CompetitionUri = new Uri("hockey/", UriKind.Relative);
            return this;
        }

        public IPathBuilder OnDate(DateTime time)
        {
            OnDateUri = new Uri($"{time:yyyy-MM-dd/}", UriKind.Relative);
            return this;
        }

        public IPathBuilder Live()
        {
            OnDateUri = new Uri($"live/", UriKind.Relative);
            return this;
        }
    }
}
