namespace CSharpSnippets.EFCore.EFInterceptors;
internal class Data : IRemovable
{
  public int ID { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime WillRemoveAt { get; set; }
}