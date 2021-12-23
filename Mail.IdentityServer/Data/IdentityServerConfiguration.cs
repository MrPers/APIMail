using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Mail.IdentityServer
{
    public static class IdentityServerConfiguration
    {
        public static IEnumerable<Client> GetClients() =>
        new List<Client>
        {
            new Client
            {
                ClientId = "client_angular_id", //Идентификатор клиента, инициировавшего запрос.
                RequireClientSecret = false, //Указывает, нужен ли этому клиенту секрет для запроса токенов из конечной точки токена
                RequireConsent = false, //Указывает, требуется ли экран согласия
                RequirePkce = true, //Указывает, нужен ли этому клиенту секрет для запроса токенов из конечной точки токена
                AllowOfflineAccess = true,//Определяет, может ли этот клиент запрашивать токены обновления
                //IdentityTokenLifetime = 10, //через сколько секунд токен обновлен(по умолчанию 300 секунд / 5 минут)
                //AllowAccessTokensViaBrowser = true, //Указывает, разрешено ли этому клиенту получать токены доступа через браузер
                //AccessTokenType = AccessTokenType.Reference, //Указывает, является ли токен доступа ссылочным токеном или автономным токеном JWT
                AccessTokenLifetime = 300, //Время жизни токена доступа в секундах(по умолчанию 3600 секунд / 1 час)
                //AuthorizationCodeLifetime = 5, //Время жизни кода авторизации в секундах (по умолчанию 300 секунд / 5 минут)
                AllowedGrantTypes =  GrantTypes.Code, //Задает типы грантов, которые разрешено использовать клиенту
                AllowedCorsOrigins = { "http://localhost:4200" },
                RedirectUris = { "http://localhost:4200/auth-callback", "http://localhost:4200/refresh" },
                PostLogoutRedirectUris = { "http://localhost:4200/" },
                AllowedScopes =
                {
                    "Order",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            },
            new Client
            {
                ClientId = "client_id_js",
                RequireClientSecret = false,
                RequireConsent = false,
                RequirePkce = true,
                AllowOfflineAccess = true,  //Определяет, может ли этот клиент запрашивать токены обновления
                AccessTokenLifetime = 5, //Время жизни токена доступа в секундах
                AllowedGrantTypes =  GrantTypes.Code,
                AllowedCorsOrigins = { "https://localhost:9001" },
                RedirectUris = { "https://localhost:9001/callback.html", "https://localhost:9001/refresh.html" },
                PostLogoutRedirectUris = { "https://localhost:9001/index.html" },
                AllowedScopes =
                {
                    "Order",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            },
            new Client
            {
                ClientId = "client_id_mvc",
                ClientSecrets = { new Secret("client_secret_mvc".ToSha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                AllowedScopes =
                {
                    "Order",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },

                RedirectUris = {"https://localhost:2001/signin-oidc"},
                PostLogoutRedirectUris = {"https://localhost:2001/signout-callback-oidc"},

                RequireConsent = false,

                AccessTokenLifetime = 5,

                AllowOfflineAccess = true

                // AlwaysIncludeUserClaimsInIdToken = true
            }
        };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource> {
                new ApiResource("Order")
            };

        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        //public static IEnumerable<ApiScope> GetApiScopes() =>
        //    new List<ApiScope> {
        //        new ApiScope("Order")
        //    };
    }
}
