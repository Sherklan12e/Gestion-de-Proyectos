namespace biblioteca.Validacion;

public class Guard
{
    public static void ValidarNull(object valor, string nombreCampo)
    {
        if (valor == null)
            throw new ArgumentNullException(nombreCampo, $"El campo {nombreCampo} no puede ser nulo");
    }

    public static void ValidarStringVacio(string valor, string nombreCampo)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException($"El campo {nombreCampo} no puede estar vacío");
    }

    public static void ValidarEmail(string email)
    {
        if (!email.Contains("@") || !email.Contains("."))
            throw new ArgumentException("El formato del email no es válido");
    }

    public static void ValidarLongitudMinima(string valor, int longitud, string nombreCampo)
    {
        if (valor.Length < longitud)
            throw new ArgumentException($"El campo {nombreCampo} debe tener al menos {longitud} caracteres");
    }

    public static void ValidarFecha(DateTime fecha, string nombreCampo)
    {
        if (fecha == default)
            throw new ArgumentException($"La {nombreCampo} no es válida");
    }

    public static void ValidarGuid(Guid id, string nombreCampo)
    {
        if (id == Guid.Empty)
            throw new ArgumentException($"El {nombreCampo} no es válido esta vacio el guid");
    }
}
