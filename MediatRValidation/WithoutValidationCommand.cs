using MediatR;

namespace CSharpSnippets.CQRS.MediatRValidation;
internal record WithoutValidationCommand(Guid Id, string StrField, int IntField) : IRequest;