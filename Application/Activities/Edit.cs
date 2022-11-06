using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity {get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            public readonly DataContext _context;
            private readonly IMapper _mapepr;

            public Handler(DataContext context, IMapper mapepr)
            {
                _mapepr = mapepr;
                _context = context;
                
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Activity.Id);

                _mapepr.Map(request.Activity, activity);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}