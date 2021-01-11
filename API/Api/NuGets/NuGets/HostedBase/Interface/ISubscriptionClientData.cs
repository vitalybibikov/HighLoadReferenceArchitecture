namespace NuGets.NuGets.HostedBase.Interface
{
    public interface ISubscriptionClientData
    {
        string ConnectionString { get;  }

        string StorageName { get;  }

        string SubscriptionName { get; set; }
    }
}
