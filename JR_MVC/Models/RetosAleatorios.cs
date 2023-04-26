namespace JR_MVC.Models
{
    public class RetosAleatorios
    {
        static public string RetosListado(int i)
        {
            List<string> retos = new List<string>();
            retos.Add("Una novela gráfica o un manga");
            retos.Add("Un libro que gire en torno al amor");
            retos.Add("Una novela de literatura asiática");
            retos.Add("Una cuento o una coleccion de cuentos");
            retos.Add("Una libro de una saga que dejaste a medias");
            retos.Add("Una libro que te atrajo por su portada");
            retos.Add("Una libro de un género que no suelas leer");
            retos.Add("Una libro que te hayan recomendado");
            retos.Add("Una clásico de literatura");
            retos.Add("Una libro escrito por una mujer");
            retos.Add("Una obra de más de 400 páginas");
            retos.Add("Una historia ambientada en época navideña");
            retos.Add("Una libro de un autor autopublicado");
            retos.Add("Un libro de no ficción");
            retos.Add("Un libro publicado en 2022");
            retos.Add("Una novela basada en hechos reales");
            retos.Add("Una antalogía de relatos");
            retos.Add("Una novela de terror");
            retos.Add("Un libro del que se haya hecho pelicula");
            retos.Add("Un libro que transcurra durante el verano");
            retos.Add("Un libro de un autor del que nunca hayas leído nada antes");
            retos.Add("Un libro que tenga una sola palabra en el título");
            retos.Add("Un libro que tu escritor favorito haya recomendado");
            retos.Add("Un libro que elija alguien para ti(un familiar, un amigo, un compañero de trabajo");
            retos.Add("Que el protagonista del libro sea una reina o rey.");
            retos.Add("Libro con temática LGBTQ+");
            retos.Add("Leer sobre la vida de un escritor o personaje histórico, puede ser una biografía");
            retos.Add("Leer una historia de misterio o suspenso");
            retos.Add("Un libro escrito por un autor de tu país");
            retos.Add(" Lea un libro que se haya hecho popular en BookTok");

            return (retos[i]);
        }

        static public string TuReto()
        {
            
            int min = 0;
            int max = 29;

            Random rnd = new Random();
            int numReto = rnd.Next(min, max);
            string reto = RetosListado(numReto);

            return reto;
        }
    }
}
