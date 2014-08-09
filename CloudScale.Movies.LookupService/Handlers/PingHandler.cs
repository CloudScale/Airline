﻿using CloudScale.Movies.Messages;
using Nimbus;
using Nimbus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScale.Movies.LookupService.Handlers
{
    public class PingHandler : IHandleRequest<PingRequest, PingResponse>
    {
        public Task<PingResponse> Handle(PingRequest request)
        {
            return Task.Run(() => { return new PingResponse() { Details = this.GetType().FullName }; });
        }
    }
}
