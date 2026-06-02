namespace Notificador.Core.DomainMapper
{
    public class DomainMappingProfile : AutoMapper.Profile
    {
        public DomainMappingProfile()
        {
            CreateMap<Notificador.Contracts.Models.MensajeDto, Notificador.Core.Models.Mensaje>();
        }
    }
}
