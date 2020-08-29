using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.DTOs;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.Domain.Interfaces.Services;
using Devboost.DroneDelivery.Domain.Params;
using Devboost.DroneDelivery.Domain.VOs;
using Microsoft.IdentityModel.Tokens;

namespace Devboost.DroneDelivery.DomainService
{
    public class AuthService: IAuthService
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly AppSettingsVO _appSettings;

        public AuthService(IUsuariosRepository usuariosRepository, AppSettingsVO appSettings)
        {
            _usuariosRepository = usuariosRepository;
            _appSettings = appSettings;
        }

        public async Task<TokenDTO> GetToken(AuthParam login)
        {
            var user = new UsuarioEntity
            {
                Login = login.Login,
                Senha = login.Senha
            };
            var retorno = new TokenDTO
            {
                Authenticated = false,
                CreationDate = null,
                ExpirationDate = null,
                Message = "Authentication Failure."
            };
            var retornoRepo = await _usuariosRepository.GetUsuarioByLoginSenha(user);
            if (retornoRepo != null)
            {
                retorno.Authenticated = true;
                retorno.AccessToken = GerarJwt(retornoRepo);
                retorno.CreationDate = DateTime.Now;
                retorno.ExpirationDate = DateTime.Now.AddHours(Convert.ToInt32(_appSettings.ExpirationHours));
                retorno.Message = "OK";
            }

            return retorno;

        }
        
        private string GerarJwt(UsuarioEntity ssoUser)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = _appSettings.Emitter,
                    Audience = _appSettings.ValidOn,

                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, ssoUser.Login),
                        new Claim(ClaimTypes.Role, ssoUser.Role.ToString())
                    }),

                    Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                });

                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}