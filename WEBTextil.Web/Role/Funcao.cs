using WEBTextil.Data.Repositorios;
using WEBTextil.Dominio.Entidades;
using WEBTextil.Dominio.Interfaces.Respositorios;
using System;
using System.Collections.Generic;
using System.Web.Security;
using Unity;

namespace WEBTextil.Web.Role
{
    public class Funcao : RoleProvider
    {
        public int UsuarioId { get; set; }
        public string[] funcoes { get; set; }

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var container = new UnityContainer();
            container.RegisterType<IUsuarioRepositorio, UsuarioRepositorio>();
            IUsuarioRepositorio UsuarioRepositorio = container.Resolve<IUsuarioRepositorio>();

            Usuario usuario = UsuarioRepositorio.BuscaUsuarioPorNome(username);
            UsuarioId = usuario.UsuarioId;

            List<string> permissoes = new List<string>();

            if (usuario.Admin)
            {
                permissoes.Add("Atendimento");
                permissoes.Add("Cidade");
                permissoes.Add("Empresa");
                permissoes.Add("Endereço");
                permissoes.Add("Estado");
                permissoes.Add("Filial");
                permissoes.Add("Frequencia");
                permissoes.Add("Parceiro");
                permissoes.Add("Rota");
                permissoes.Add("RotaFilial");
                permissoes.Add("Admin");
            }
            else
            {
                foreach (var permissao in usuario.Permissao.Split(','))
                {
                    permissoes.Add(permissao);
                }
            }

            return permissoes.ToArray();

        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}