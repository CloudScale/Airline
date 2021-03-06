﻿using System.Threading.Tasks;
using CloudScale.Movies.Messages;
using Nimbus.Handlers;

namespace CloudScale.Api.Handlers
{
	public class PingHandler : IHandleMulticastRequest<PingRequest, PingResponse>
	{
		public Task<PingResponse> Handle(PingRequest request)
		{
			return Task.Run(() => new PingResponse
			{
				Details = GetType() + " : " + GetType().Assembly.GetName().Version,
				HealthCheck = Metrics.HealthChecks.GetStatus()
			});
		}
	}
}