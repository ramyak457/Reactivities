using System;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Command;

public class EditActivity
{
    public class Command : IRequest
    {
        public required Activity activity;
    }
    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await context.Activities
            .FindAsync([request.activity.Id],cancellationToken)
            ?? throw new Exception("activity not found");
            
            mapper.Map(request.activity,activity);

            await context.SaveChangesAsync(cancellationToken);

        }
    }
}
