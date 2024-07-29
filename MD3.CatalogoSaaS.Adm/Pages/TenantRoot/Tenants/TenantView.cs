namespace MD3.CatalogoSaaS.Adm.Pages.TenantRoot.Tenants
{
    public class TenantView
    {
        public int Id { get; set; }
        public string? IdGeral { get; set; }
        public string? Nome { get; set; }
        public int? Pai_Id { get; set; }
        public IEnumerable<(string, string)>? Contas { get; set; }
    }
}
