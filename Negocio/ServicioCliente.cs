using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionesBD;
using Dominio;

namespace Negocio
{
    public class ServicioCliente
    {
        private readonly ClienteRepository _repo;
        public ServicioCliente(ClienteRepository repo) => _repo = repo;

        public Cliente BuscarPorDocumento(string doc) => _repo.GetByDocumento(doc);
        public int RegistrarNuevo(Cliente c) => _repo.Insert(c);
    }
}