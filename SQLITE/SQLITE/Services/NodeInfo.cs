namespace SQLITE.Services
{
    public class NodeInfo
    {
        public NodeType Type { get; set; }

        public int Id { get; set; }

        public virtual object ObtenerObjeto() { return null; }
    }
}
