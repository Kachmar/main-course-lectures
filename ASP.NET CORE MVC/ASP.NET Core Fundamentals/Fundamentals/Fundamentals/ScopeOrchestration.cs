using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentals
{
    public class ScopeOrchestration
    {
        private readonly IScopedService scopedService;

        private readonly ITransientService transientService;

        private readonly ISingletonService singletonService;

        public ScopeOrchestration(
            IScopedService scopedService,
            ITransientService transientService, ISingletonService singletonService)
        {
            this.scopedService = scopedService;
            this.transientService = transientService;
            this.singletonService = singletonService;
        }

        public string GetScopesStringTask()
        {
            return $"Scoped: {this.scopedService.Id} , Singleton: {this.singletonService.Id}, Transient: {this.transientService.Id}";
        }
    }
}
