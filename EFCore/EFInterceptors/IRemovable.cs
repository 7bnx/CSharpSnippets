namespace CSharpSnippets.EFCore.EFInterceptors;
internal interface IRemovable
{
  DateTime CreatedAt { get; set; }
  DateTime WillRemoveAt { get; set; }
}