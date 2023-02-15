using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host.Executors;
using Microsoft.Extensions.Options;

namespace Tests1.ServiceHost.QueueClient
{
    public interface IJobHostFactory
    {
        IJobHost Get();
    }

    public class JobHostFactory : IJobHostFactory
    {
        private readonly IOptions<JobHostOptions> options;
        private readonly IJobHostContextFactory contextFactory;

        public JobHostFactory(IOptions<JobHostOptions> options, IJobHostContextFactory contextFactory)
        {
            this.options = options;
            this.contextFactory = contextFactory;
        }

        public IJobHost Get()
        {
            return new JobHost(options, contextFactory);
        }
    }
}