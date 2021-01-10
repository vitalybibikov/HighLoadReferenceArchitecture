namespace Core.NuGets.HostedBase.Interface
{
    public interface IQueueClientData
    {
        string ConnectionString { get;  }

        string StorageName { get;  }
    }
}
