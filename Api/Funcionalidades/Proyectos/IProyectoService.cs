namespace Api.Funcionalidades.Proyectos;

public interface IProyectoService
{
    IEnumerable<ProyectoQueryDto> GetProyectos();
    void CreateProyecto(ProyectoCommandDto proyectoDto);
    void UpdateProyecto(Guid idProyecto, ProyectoCommandDto proyectoDto);
    void DeleteProyecto(Guid idProyecto);
}
