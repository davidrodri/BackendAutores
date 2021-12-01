namespace WebApiAutores.Entidades
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titutlo { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
    }
}
