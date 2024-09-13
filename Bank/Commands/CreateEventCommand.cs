using Bank.Models;
using MediatR;

namespace Bank.Commands;

public record CreateEventCommand(MovementType Type, int Amount) : IRequest;