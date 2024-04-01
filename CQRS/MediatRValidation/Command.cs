using MediatR;

namespace CSharpSnippets.CQRS.MediatRValidation;

public record Command(Guid Id, string StrField, int IntField) : IRequest<int>;