using System;

namespace ConsoleApp1.Builder.Concrete
{
    public abstract class PathBuilder : IPathBuilder
    {
        private readonly Uri uri;
        private protected Uri CompetitionUri { get; set; }

        private protected Uri OnDateUri { get; set; }

        protected PathBuilder(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            this.uri = uri;
        }

        public virtual Uri Build()
        {
            Uri result = uri;

            if (CompetitionUri != null)
            {
                result = new Uri(uri, CompetitionUri);
            }

            if (OnDateUri != null)
            {
                result = new Uri(result, OnDateUri);
            }

            return result;
        }
    }
}
