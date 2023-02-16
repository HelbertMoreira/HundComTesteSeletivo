namespace HundCom_Postagem.Services
{
    public class TopicoServices
    {
        private readonly AppDbContext _context;

        public TopicoServices(AppDbContext context)
        {
            _context = context;
        }
    }
}
