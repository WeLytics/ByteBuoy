using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByteBuoy.Domain.Entities.Config;

namespace ByteBuoy.Agent.JobExecution
{
	internal class JobExecutionContext(AgentConfig agentConfig)
	{
		public AgentConfig GetAgentConfig() => agentConfig;

		public string[] GetGlobalgnoredFiles() => !string.IsNullOrEmpty(agentConfig.IgnoreFiles) ?  agentConfig.IgnoreFiles.Split(";", StringSplitOptions.RemoveEmptyEntries) : [];
	}
}
