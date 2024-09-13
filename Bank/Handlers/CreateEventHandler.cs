using Bank.Commands;
using MediatR;

namespace Bank.Handlers;

public class CreateEventHandler : IRequestHandler<CreateEventCommand>
{

    public Task<Unit> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {


        return Task.FromResult(Unit.Value);
    }
}