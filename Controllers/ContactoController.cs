using Microsoft.AspNetCore.Mvc;
using TechForge.Modelo.Entidades;
using TechForge.Modelo.Entidades.Generales;
using TechForge.Negocio;

namespace TechForgeApi.Controllers
{
    /// <summary>
    /// Controlador para la manipulacion de notificaciones atraves de Twilio
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Contacto")]
    public class ContactoController
    {
        private ContactoBusiness _bss { get; set; } = new();

        /// <summary>
        /// Obtiene todos los contactos
        /// </summary>
        /// <returns> </returns>
        [HttpGet("ObtenerContactos")]
        [ProducesResponseType(typeof(Respuesta), 200)]
        [ProducesResponseType(typeof(Respuesta), 400)]
        [ProducesResponseType(typeof(Respuesta), 500)]
        [Produces("application/json")]
        [FormatFilter]
        public List<Contacto> ObtenerContactos()
        {
            return  _bss.ObtenerContactos();
        }


        /// <summary>
        /// Guarda un contacto
        /// </summary>
        /// <param name="contacto"></param>
        /// <returns></returns>
        [HttpPost("Guardar")]
        [ProducesResponseType(typeof(Respuesta), 200)]
        [ProducesResponseType(typeof(Respuesta), 400)]
        [ProducesResponseType(typeof(Respuesta), 500)]
        [Produces("application/json")]
        [FormatFilter]
        public Respuesta Guardar(Contacto contacto)
        {
            return _bss.Guardar(contacto);
        }
    }
}
    

