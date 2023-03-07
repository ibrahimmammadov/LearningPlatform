// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using LpIdentityServer.Extensions;
using System;
using System.Collections.Generic;

namespace LpIdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission" } },
            new ApiResource("resource_photo_stock"){Scopes={ "photo_stock_fullpermission" } },
            new ApiResource("resource_basket"){Scopes={ "basket_fullpermission" } },
            new ApiResource("resource_discount"){Scopes={ "discount_fullpermission" } },
            new ApiResource("resource_order"){Scopes={ "order_fullpermission" } },
            new ApiResource("resource_payment"){Scopes={ "payment_fullpermission" } },
            new ApiResource("resource_gateway"){Scopes={ "gateway_fullpermission" } },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource(){Name="roles",DisplayName="Roles",Description="User Roles",UserClaims=new []{"role"}}
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
           new ApiScope[]
           {
               new ApiScope("catalog_fullpermission","FullAccess for Catalog API"),
               new ApiScope("photo_stock_fullpermission","FullAccess for Photo Stock API"),
               new ApiScope("basket_fullpermission","FullAccess for Basket API"),
               new ApiScope("discount_fullpermission","FullAccess for Discount API"),
               new ApiScope("order_fullpermission","FullAccess for Order API"),
               new ApiScope("payment_fullpermission","FullAccess for Payment API"),
               new ApiScope("gateway_fullpermission","FullAccess for Gateway"),
               new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
           };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "WebMvcClient",
                    ClientName = ".Net 6 Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("baku".Sha256()) },

                    AllowedScopes = { "catalog_fullpermission", "photo_stock_fullpermission", "gateway_fullpermission", IdentityServerConstants.LocalApi.ScopeName }
                },

                  new Client
                {
                    ClientId = "WebMvcClientForUser",
                    ClientName = ".Net 6 Credentials Client",
                    AllowOfflineAccess = true,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("baku".Sha256()) },

                    AllowedScopes = {"basket_fullpermission","order_fullpermission","gateway_fullpermission",IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.LocalApi.ScopeName,
                      IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile,
                      IdentityServerConstants.StandardScopes.OfflineAccess,"roles"},
                    AccessTokenLifetime = 1*60*60,
                    RefreshTokenExpiration =TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime =(int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse
                },

                     new Client
                {
                    ClientName = "Token Exchange Client",
                    ClientId = "TokenExchangeClient",

                    AllowedGrantTypes = GrantTypesExtension.TokenExchangeClientCredentials,
                    ClientSecrets = { new Secret("baku".Sha256()) },

                    AllowedScopes = { "discount_fullpermission", "payment_fullpermission", IdentityServerConstants.StandardScopes.OpenId }
                }

                // interactive client using code flow + pkce
                //new Client
                //{
                //    ClientId = "interactive",
                //    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                //    AllowedGrantTypes = GrantTypes.Code,

                //    RedirectUris = { "https://localhost:44300/signin-oidc" },
                //    FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                //    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                //    AllowOfflineAccess = true,
                //    AllowedScopes = { "openid", "profile", "scope2" }
                //},
            };
    }
}