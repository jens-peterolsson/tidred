namespace Tidred.Repo
{
  public partial class Project
  {
    public bool IsFixedPrice { get { return this.ProjectFixedPrice != null; } }
  }
}
