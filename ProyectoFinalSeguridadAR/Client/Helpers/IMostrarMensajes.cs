using System.Threading.Tasks;

namespace ProyectoFinalSeguridadAR.Client.Helpers
{
    public interface IMostrarMensajes
    {
        Task MostrarMensajeError(string mensaje);

        Task MostrarMensajeExitoso(string mensaje);
    }
}
