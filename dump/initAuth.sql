--
-- PostgreSQL database dump
--

-- Dumped from database version 16.1
-- Dumped by pg_dump version 16.1

-- Started on 2024-03-10 14:42:40

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 5 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: -
--

--
-- TOC entry 4911 (class 0 OID 0)
-- Dependencies: 5
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: -
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 220 (class 1259 OID 17815)
-- Name: AspNetRoleClaims; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."AspNetRoleClaims" (
    "Id" integer NOT NULL,
    "RoleId" text NOT NULL,
    "ClaimType" text,
    "ClaimValue" text
);


--
-- TOC entry 219 (class 1259 OID 17814)
-- Name: AspNetRoleClaims_Id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public."AspNetRoleClaims" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."AspNetRoleClaims_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 216 (class 1259 OID 17793)
-- Name: AspNetRoles; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."AspNetRoles" (
    "Id" text NOT NULL,
    "Name" character varying(256),
    "NormalizedName" character varying(256),
    "ConcurrencyStamp" text
);


--
-- TOC entry 222 (class 1259 OID 17828)
-- Name: AspNetUserClaims; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."AspNetUserClaims" (
    "Id" integer NOT NULL,
    "UserId" text NOT NULL,
    "ClaimType" text,
    "ClaimValue" text
);


--
-- TOC entry 221 (class 1259 OID 17827)
-- Name: AspNetUserClaims_Id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public."AspNetUserClaims" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."AspNetUserClaims_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 223 (class 1259 OID 17840)
-- Name: AspNetUserLogins; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."AspNetUserLogins" (
    "LoginProvider" text NOT NULL,
    "ProviderKey" text NOT NULL,
    "ProviderDisplayName" text,
    "UserId" text NOT NULL
);


--
-- TOC entry 224 (class 1259 OID 17852)
-- Name: AspNetUserRoles; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."AspNetUserRoles" (
    "UserId" text NOT NULL,
    "RoleId" text NOT NULL
);


--
-- TOC entry 225 (class 1259 OID 17869)
-- Name: AspNetUserTokens; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."AspNetUserTokens" (
    "UserId" text NOT NULL,
    "LoginProvider" text NOT NULL,
    "Name" text NOT NULL,
    "Value" text
);


--
-- TOC entry 217 (class 1259 OID 17800)
-- Name: AspNetUsers; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."AspNetUsers" (
    "Id" text NOT NULL,
    "Name" text NOT NULL,
    "Surname" text NOT NULL,
    "Patronymic" text,
    "Birthday" date,
    "UserName" character varying(256),
    "NormalizedUserName" character varying(256),
    "Email" character varying(256),
    "NormalizedEmail" character varying(256),
    "EmailConfirmed" boolean NOT NULL,
    "PasswordHash" text,
    "SecurityStamp" text,
    "ConcurrencyStamp" text,
    "PhoneNumber" text,
    "PhoneNumberConfirmed" boolean NOT NULL,
    "TwoFactorEnabled" boolean NOT NULL,
    "LockoutEnd" timestamp with time zone,
    "LockoutEnabled" boolean NOT NULL,
    "AccessFailedCount" integer NOT NULL
);


--
-- TOC entry 218 (class 1259 OID 17807)
-- Name: Clients; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."Clients" (
    "ClientId" text NOT NULL,
    "ClientData" text NOT NULL
);


--
-- TOC entry 215 (class 1259 OID 17788)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


--
-- TOC entry 4900 (class 0 OID 17815)
-- Dependencies: 220
-- Data for Name: AspNetRoleClaims; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 4896 (class 0 OID 17793)
-- Dependencies: 216
-- Data for Name: AspNetRoles; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."AspNetRoles" VALUES ('6020327f-455e-4c78-a5cc-41a7fbc88ca3', 'Passenger', 'PASSENGER', NULL);
INSERT INTO public."AspNetRoles" VALUES ('3034c873-58d3-4980-bf2e-d4a99bfbbfaa', 'Admin', 'ADMIN', NULL);
INSERT INTO public."AspNetRoles" VALUES ('27a23bbd-9665-4641-b650-7ca03d0da9a3', 'Airline', 'AIRLINE', NULL);


--
-- TOC entry 4902 (class 0 OID 17828)
-- Dependencies: 222
-- Data for Name: AspNetUserClaims; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."AspNetUserClaims" VALUES (1, '57f17373-15ca-467d-9a85-e4b9b1f13595', 'Carrier', 'SU');


--
-- TOC entry 4903 (class 0 OID 17840)
-- Dependencies: 223
-- Data for Name: AspNetUserLogins; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."AspNetUserLogins" VALUES ('Vkontakte', '492392504', 'Vkontakte', '43fc6578-677a-45bd-a5cc-0899b38b35b3');
INSERT INTO public."AspNetUserLogins" VALUES ('Yandex', '877147702', 'Yandex', 'f8349b6c-2053-4978-bc24-7e88904c0a89');


--
-- TOC entry 4904 (class 0 OID 17852)
-- Dependencies: 224
-- Data for Name: AspNetUserRoles; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."AspNetUserRoles" VALUES ('f8349b6c-2053-4978-bc24-7e88904c0a89', '3034c873-58d3-4980-bf2e-d4a99bfbbfaa');
INSERT INTO public."AspNetUserRoles" VALUES ('a6e1fce5-bd3d-4f7e-a7a8-07ebca65a3a8', '6020327f-455e-4c78-a5cc-41a7fbc88ca3');
INSERT INTO public."AspNetUserRoles" VALUES ('1c963f97-128b-4625-a8f6-ffbd737ccd52', '3034c873-58d3-4980-bf2e-d4a99bfbbfaa');
INSERT INTO public."AspNetUserRoles" VALUES ('57f17373-15ca-467d-9a85-e4b9b1f13595', '27a23bbd-9665-4641-b650-7ca03d0da9a3');
INSERT INTO public."AspNetUserRoles" VALUES ('b28b4949-c0d1-44fc-ace4-2ded5a8d82c8', '6020327f-455e-4c78-a5cc-41a7fbc88ca3');
INSERT INTO public."AspNetUserRoles" VALUES ('a0cb5d8c-6e3a-474d-aaed-eebf42a27f6a', '3034c873-58d3-4980-bf2e-d4a99bfbbfaa');
INSERT INTO public."AspNetUserRoles" VALUES ('0230f933-7b15-46ce-b900-f88b42729b9a', '3034c873-58d3-4980-bf2e-d4a99bfbbfaa');
INSERT INTO public."AspNetUserRoles" VALUES ('43fc6578-677a-45bd-a5cc-0899b38b35b3', '3034c873-58d3-4980-bf2e-d4a99bfbbfaa');
INSERT INTO public."AspNetUserRoles" VALUES ('66f7a2cf-fb52-4e4d-b9c8-c2266320791d', '6020327f-455e-4c78-a5cc-41a7fbc88ca3');
INSERT INTO public."AspNetUserRoles" VALUES ('2f27fcb1-6bfc-4ed0-85aa-7b3266f82510', '27a23bbd-9665-4641-b650-7ca03d0da9a3');
INSERT INTO public."AspNetUserRoles" VALUES ('0ce046ec-52d2-4303-9c50-9b87592c5a3a', '27a23bbd-9665-4641-b650-7ca03d0da9a3');
INSERT INTO public."AspNetUserRoles" VALUES ('1b3af2f9-79d5-4053-9217-e79f4b8b25d3', '27a23bbd-9665-4641-b650-7ca03d0da9a3');


--
-- TOC entry 4905 (class 0 OID 17869)
-- Dependencies: 225
-- Data for Name: AspNetUserTokens; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 4897 (class 0 OID 17800)
-- Dependencies: 217
-- Data for Name: AspNetUsers; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."AspNetUsers" VALUES ('57f17373-15ca-467d-9a85-e4b9b1f13595', 'Дима', 'Грек', 'Иванович', '2024-02-01', 'SU-Dim', 'SU-DIM', 'dimas1703@gmail.com', 'DIMAS1703@GMAIL.COM', true, 'AQAAAAIAAYagAAAAEAgitpKcMuqVRYDdVp7BhEqfMBZd7VFMae128MXHdTzcY9QO5mcDchLBTZlAlIcjNw==', 'GHK5GHFVWXCBTNA647AV22B47FOQXIOQ', 'a830c53a-2f34-494d-8b9c-e6dc7cdac37d', NULL, false, false, NULL, true, 0);
INSERT INTO public."AspNetUsers" VALUES ('1c963f97-128b-4625-a8f6-ffbd737ccd52', 'Admin', 'Super', 'User', '2000-01-01', 'SuperAdmin', 'SUPERADMIN', 'SuperAdmin@gmail.com', 'SUPERADMIN@GMAIL.COM', true, 'AQAAAAIAAYagAAAAEM2xmR8AplyqbTkkyuS1pUSq6/VHHPKClKpKMkU8f71BaRXvexuhltAwRAEK/2xapw==', 'LLIGEU5IA6IF3NXSPK7MBXWTBU72OEJZ', '97390f91-5143-4928-b8a7-1a0afc3529f7', NULL, false, false, NULL, false, 0);
INSERT INTO public."AspNetUsers" VALUES ('a0cb5d8c-6e3a-474d-aaed-eebf42a27f6a', 'Геннадий', 'Константинов', 'Иванович', '2024-02-02', 'StarDed', 'STARDED', '0.ebobla.0@gmail.com', '0.EBOBLA.0@GMAIL.COM', true, 'AQAAAAIAAYagAAAAEDguFMEhgDkQjEFxi/gdig7HymwYGGsGI9WWgRsH3XQqMONmOe9yJiaS0285+gIyGA==', 'JTGI375OZVSASUG55SFXVPSDA5XRT6JY', 'ee8df31c-3ed0-4254-896b-00afd397d280', NULL, false, false, NULL, true, 0);
INSERT INTO public."AspNetUsers" VALUES ('66f7a2cf-fb52-4e4d-b9c8-c2266320791d', 'Геннадий', 'Александр', 'Иванович', '2024-03-20', 'Test1', 'TEST1', 'test1@gmail.com', 'TEST1@GMAIL.COM', false, 'AQAAAAIAAYagAAAAEPI7rzexEG58Qq6iA68OcntobT1LCNm1+oMP5E+tmVW0/F7xAo02dIw2+k+vpOvp1w==', 'CONQY2JM4CVCT5IUNMUH7PMBRAU34REM', 'aeae073d-022a-4f53-8937-9994662b21b3', NULL, false, false, NULL, true, 0);
INSERT INTO public."AspNetUsers" VALUES ('b28b4949-c0d1-44fc-ace4-2ded5a8d82c8', 'Геннадий', 'Константинов', 'vitalik', '2023-11-06', 'vitalik', 'VITALIK', 'alexsandrkons73@gmail.com', 'ALEXSANDRKONS73@GMAIL.COM', true, 'AQAAAAIAAYagAAAAEA6LOLcBmDoIrKOn7D6tWihppQTgRpfFVQoYjYEnqUkhH8ZSy9H/W4V97QI+szQRjw==', '7VT4SQBVDTUP5GK7DEWAOGEKZFISVPEA', '206267eb-b422-46e9-b3a4-2d277aecee74', NULL, false, false, NULL, true, 0);
INSERT INTO public."AspNetUsers" VALUES ('43fc6578-677a-45bd-a5cc-0899b38b35b3', 'Александр', 'Константинов', NULL, '2000-01-01', '492392504', '492392504', 'genkonst5@gmail.com', 'GENKONST5@GMAIL.COM', true, NULL, 'PIP6XC2DAZCIWA7FWVRK46X7DTNL5RGD', 'efa1989e-d627-4fe0-aa58-a061dcdf690c', NULL, false, false, NULL, true, 0);
INSERT INTO public."AspNetUsers" VALUES ('2f27fcb1-6bfc-4ed0-85aa-7b3266f82510', 'Геннадий', 'Александр', 'vitalik', '2024-03-07', 'SuperAdmin3', 'SUPERADMIN3', 'genqwenst5@gmail.com', 'GENQWENST5@GMAIL.COM', false, 'AQAAAAIAAYagAAAAEIRazT0t80zNm1iod6gN/Auz3xS0J0+bUJ1DpLxox70Rv5WziCrHMCd/d2UgaiXEyg==', 'D2OZKD45EGWAA6IA3QIP5SLW7JDRGYUA', '5af71c3c-081c-43a5-8eb1-953c99d06e8b', NULL, false, false, NULL, true, 0);
INSERT INTO public."AspNetUsers" VALUES ('0ce046ec-52d2-4303-9c50-9b87592c5a3a', 'Геннадий', 'Александр', 'admin2033', '2024-03-18', 'SuperAdmin342', 'SUPERADMIN342', 'gefewewst5@gmail.com', 'GEFEWEWST5@GMAIL.COM', false, 'AQAAAAIAAYagAAAAELV7R09KoaFcdUNKTE4tR1CQXnDYrDqN4gdbq/VAZqT7dRgp/X6rQGcjtzh2+2vFNg==', 'RSFV2NTQJP55VZMANPZOWRJQYNDP5KKM', 'e1afec20-a04b-410b-8584-2d971e0084e2', NULL, false, false, NULL, true, 0);
INSERT INTO public."AspNetUsers" VALUES ('1b3af2f9-79d5-4053-9217-e79f4b8b25d3', 'Геннадий', 'Александр', 'Иванович', '2024-03-06', 'HellDivers223', 'HELLDIVERS223', 'genasdaa@gmail.com', 'GENASDAA@GMAIL.COM', false, 'AQAAAAIAAYagAAAAECCJqGnvcYod8BatZNVOyYFGmyH4wyzNpirTPZ48PtUhPfRHNOjohZa/6ueyUYuQGQ==', '62IM6RJYDEVU3ZUACCF5CNCK6HQOZSKH', '87aa4ab2-58ad-4c0c-8662-85e245176171', NULL, false, false, NULL, true, 0);
INSERT INTO public."AspNetUsers" VALUES ('f8349b6c-2053-4978-bc24-7e88904c0a89', 'Геннадий', 'Константинов', 'Геннадьевич', '2002-07-18', 'Yandex-877147702', 'YANDEX-877147702', 'lIdiotl@yandex.ru', 'LIDIOTL@YANDEX.RU', true, NULL, 'LGZRX6L5TBBDCRLD7ZOR3HG63LDZOY7E', '66f2ad58-b004-4a9b-9245-def854c67ff1', NULL, false, false, NULL, true, 0);
INSERT INTO public."AspNetUsers" VALUES ('0230f933-7b15-46ce-b900-f88b42729b9a', 'Геннадий', 'Константинов', 'Иванович', '2024-02-01', 'HellDivers2', 'HELLDIVERS2', 'aleksandrkonstantinov870@gmail.com', 'ALEKSANDRKONSTANTINOV870@GMAIL.COM', true, 'AQAAAAIAAYagAAAAELOPs6kMJzoHpgpaeX3UUW37nu775/Gt4OsfbxyuZnBtoeNEVGrv4vAbyIAlURSQDQ==', 'EWBGXUGWN5PBUR3TJQWKETFDZYJZR7LH', 'c1d28ff8-0ffd-4104-9bf3-96b7ec895c93', NULL, false, false, NULL, true, 0);
INSERT INTO public."AspNetUsers" VALUES ('a6e1fce5-bd3d-4f7e-a7a8-07ebca65a3a8', 'Геннадий', 'Александр', 'Дмитриевич', '2024-03-07', 'Test56', 'TEST56', 'ge2nst5test@gmail.com', 'GE2NST5TEST@GMAIL.COM', false, 'AQAAAAIAAYagAAAAECxW28PU5Sg6zLSz7MsyRY0/vPF1k6/fQxn0RlI7ERN4LXHMQFJr6yNx/m5geyBiRA==', 'NZWSFFGKSA4XVDYLPFMKOBBJEC3TYS2K', '1e3e2567-ee28-453d-bb1c-c411ef309faf', NULL, false, false, NULL, true, 0);


--
-- TOC entry 4898 (class 0 OID 17807)
-- Dependencies: 218
-- Data for Name: Clients; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."Clients" VALUES ('angular', '{"Enabled":true,"ClientId":"angular","ProtocolType":"oidc","ClientSecrets":[],"RequireClientSecret":false,"ClientName":null,"Description":null,"ClientUri":null,"LogoUri":null,"RequireConsent":false,"AllowRememberConsent":true,"AllowedGrantTypes":["authorization_code"],"RequirePkce":true,"AllowPlainTextPkce":false,"RequireRequestObject":false,"AllowAccessTokensViaBrowser":true,"RedirectUris":["http://localhost:4200"],"PostLogoutRedirectUris":["http://localhost:4200"],"FrontChannelLogoutUri":null,"FrontChannelLogoutSessionRequired":true,"BackChannelLogoutUri":null,"BackChannelLogoutSessionRequired":true,"AllowOfflineAccess":false,"AllowedScopes":["openid","api"],"AlwaysIncludeUserClaimsInIdToken":false,"IdentityTokenLifetime":300,"AllowedIdentityTokenSigningAlgorithms":[],"AccessTokenLifetime":3600,"AuthorizationCodeLifetime":300,"AbsoluteRefreshTokenLifetime":2592000,"SlidingRefreshTokenLifetime":1296000,"ConsentLifetime":null,"RefreshTokenUsage":1,"UpdateAccessTokenClaimsOnRefresh":false,"RefreshTokenExpiration":1,"AccessTokenType":0,"EnableLocalLogin":true,"IdentityProviderRestrictions":[],"IncludeJwtId":true,"Claims":[],"AlwaysSendClientClaims":false,"ClientClaimsPrefix":"client_","PairWiseSubjectSalt":null,"UserSsoLifetime":null,"UserCodeType":null,"DeviceCodeLifetime":300,"AllowedCorsOrigins":["http://localhost:4200"],"Properties":{}}');
INSERT INTO public."Clients" VALUES ('passengerClient', '{"Enabled":true,"ClientId":"passengerClient","ProtocolType":"oidc","ClientSecrets":[{"Description":null,"Value":"17SeKCgRJ7jZ/thl8RY5pLd+GGMPKwfmSgwJ72GXTeY=","Expiration":null,"Type":"SharedSecret"}],"RequireClientSecret":true,"ClientName":null,"Description":null,"ClientUri":null,"LogoUri":null,"RequireConsent":false,"AllowRememberConsent":true,"AllowedGrantTypes":["client_credentials"],"RequirePkce":true,"AllowPlainTextPkce":false,"RequireRequestObject":false,"AllowAccessTokensViaBrowser":false,"RedirectUris":[],"PostLogoutRedirectUris":[],"FrontChannelLogoutUri":null,"FrontChannelLogoutSessionRequired":true,"BackChannelLogoutUri":null,"BackChannelLogoutSessionRequired":true,"AllowOfflineAccess":false,"AllowedScopes":["api"],"AlwaysIncludeUserClaimsInIdToken":false,"IdentityTokenLifetime":300,"AllowedIdentityTokenSigningAlgorithms":[],"AccessTokenLifetime":3600,"AuthorizationCodeLifetime":300,"AbsoluteRefreshTokenLifetime":2592000,"SlidingRefreshTokenLifetime":1296000,"ConsentLifetime":null,"RefreshTokenUsage":1,"UpdateAccessTokenClaimsOnRefresh":false,"RefreshTokenExpiration":1,"AccessTokenType":0,"EnableLocalLogin":true,"IdentityProviderRestrictions":[],"IncludeJwtId":true,"Claims":[],"AlwaysSendClientClaims":false,"ClientClaimsPrefix":"client_","PairWiseSubjectSalt":null,"UserSsoLifetime":null,"UserCodeType":null,"DeviceCodeLifetime":300,"AllowedCorsOrigins":[],"Properties":{}}');
INSERT INTO public."Clients" VALUES ('postman', '{"Enabled":true,"ClientId":"postman","ProtocolType":"oidc","ClientSecrets":[],"RequireClientSecret":false,"ClientName":null,"Description":null,"ClientUri":null,"LogoUri":null,"RequireConsent":false,"AllowRememberConsent":true,"AllowedGrantTypes":["authorization_code"],"RequirePkce":true,"AllowPlainTextPkce":false,"RequireRequestObject":false,"AllowAccessTokensViaBrowser":true,"RedirectUris":["https://oauth.pstmn.io/v1/callback"],"PostLogoutRedirectUris":[],"FrontChannelLogoutUri":null,"FrontChannelLogoutSessionRequired":true,"BackChannelLogoutUri":null,"BackChannelLogoutSessionRequired":true,"AllowOfflineAccess":true,"AllowedScopes":["api","openid","profile"],"AlwaysIncludeUserClaimsInIdToken":false,"IdentityTokenLifetime":300,"AllowedIdentityTokenSigningAlgorithms":[],"AccessTokenLifetime":3600,"AuthorizationCodeLifetime":300,"AbsoluteRefreshTokenLifetime":2592000,"SlidingRefreshTokenLifetime":1296000,"ConsentLifetime":null,"RefreshTokenUsage":1,"UpdateAccessTokenClaimsOnRefresh":false,"RefreshTokenExpiration":1,"AccessTokenType":0,"EnableLocalLogin":true,"IdentityProviderRestrictions":[],"IncludeJwtId":true,"Claims":[],"AlwaysSendClientClaims":false,"ClientClaimsPrefix":"client_","PairWiseSubjectSalt":null,"UserSsoLifetime":null,"UserCodeType":null,"DeviceCodeLifetime":300,"AllowedCorsOrigins":[],"Properties":{}}');


--
-- TOC entry 4895 (class 0 OID 17788)
-- Dependencies: 215
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."__EFMigrationsHistory" VALUES ('20240211114649_InitialCreate', '8.0.1');


--
-- TOC entry 4912 (class 0 OID 0)
-- Dependencies: 219
-- Name: AspNetRoleClaims_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."AspNetRoleClaims_Id_seq"', 1, false);


--
-- TOC entry 4913 (class 0 OID 0)
-- Dependencies: 221
-- Name: AspNetUserClaims_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."AspNetUserClaims_Id_seq"', 23, true);


--
-- TOC entry 4734 (class 2606 OID 17821)
-- Name: AspNetRoleClaims PK_AspNetRoleClaims; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."AspNetRoleClaims"
    ADD CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY ("Id");


--
-- TOC entry 4724 (class 2606 OID 17799)
-- Name: AspNetRoles PK_AspNetRoles; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."AspNetRoles"
    ADD CONSTRAINT "PK_AspNetRoles" PRIMARY KEY ("Id");


--
-- TOC entry 4737 (class 2606 OID 17834)
-- Name: AspNetUserClaims PK_AspNetUserClaims; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."AspNetUserClaims"
    ADD CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY ("Id");


--
-- TOC entry 4740 (class 2606 OID 17846)
-- Name: AspNetUserLogins PK_AspNetUserLogins; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."AspNetUserLogins"
    ADD CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey");


--
-- TOC entry 4743 (class 2606 OID 17858)
-- Name: AspNetUserRoles PK_AspNetUserRoles; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."AspNetUserRoles"
    ADD CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId");


--
-- TOC entry 4745 (class 2606 OID 17875)
-- Name: AspNetUserTokens PK_AspNetUserTokens; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."AspNetUserTokens"
    ADD CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name");


--
-- TOC entry 4728 (class 2606 OID 17806)
-- Name: AspNetUsers PK_AspNetUsers; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."AspNetUsers"
    ADD CONSTRAINT "PK_AspNetUsers" PRIMARY KEY ("Id");


--
-- TOC entry 4731 (class 2606 OID 17813)
-- Name: Clients PK_Clients; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Clients"
    ADD CONSTRAINT "PK_Clients" PRIMARY KEY ("ClientId");


--
-- TOC entry 4722 (class 2606 OID 17792)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 4726 (class 1259 OID 17886)
-- Name: EmailIndex; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "EmailIndex" ON public."AspNetUsers" USING btree ("NormalizedEmail");


--
-- TOC entry 4732 (class 1259 OID 17881)
-- Name: IX_AspNetRoleClaims_RoleId; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON public."AspNetRoleClaims" USING btree ("RoleId");


--
-- TOC entry 4735 (class 1259 OID 17883)
-- Name: IX_AspNetUserClaims_UserId; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_AspNetUserClaims_UserId" ON public."AspNetUserClaims" USING btree ("UserId");


--
-- TOC entry 4738 (class 1259 OID 17884)
-- Name: IX_AspNetUserLogins_UserId; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_AspNetUserLogins_UserId" ON public."AspNetUserLogins" USING btree ("UserId");


--
-- TOC entry 4741 (class 1259 OID 17885)
-- Name: IX_AspNetUserRoles_RoleId; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_AspNetUserRoles_RoleId" ON public."AspNetUserRoles" USING btree ("RoleId");


--
-- TOC entry 4725 (class 1259 OID 17882)
-- Name: RoleNameIndex; Type: INDEX; Schema: public; Owner: -
--

CREATE UNIQUE INDEX "RoleNameIndex" ON public."AspNetRoles" USING btree ("NormalizedName");


--
-- TOC entry 4729 (class 1259 OID 17887)
-- Name: UserNameIndex; Type: INDEX; Schema: public; Owner: -
--

CREATE UNIQUE INDEX "UserNameIndex" ON public."AspNetUsers" USING btree ("NormalizedUserName");


--
-- TOC entry 4746 (class 2606 OID 17822)
-- Name: AspNetRoleClaims FK_AspNetRoleClaims_AspNetRoles_RoleId; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."AspNetRoleClaims"
    ADD CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES public."AspNetRoles"("Id") ON DELETE CASCADE;


--
-- TOC entry 4747 (class 2606 OID 17835)
-- Name: AspNetUserClaims FK_AspNetUserClaims_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."AspNetUserClaims"
    ADD CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AspNetUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 4748 (class 2606 OID 17847)
-- Name: AspNetUserLogins FK_AspNetUserLogins_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."AspNetUserLogins"
    ADD CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AspNetUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 4749 (class 2606 OID 17859)
-- Name: AspNetUserRoles FK_AspNetUserRoles_AspNetRoles_RoleId; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."AspNetUserRoles"
    ADD CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES public."AspNetRoles"("Id") ON DELETE CASCADE;


--
-- TOC entry 4750 (class 2606 OID 17864)
-- Name: AspNetUserRoles FK_AspNetUserRoles_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."AspNetUserRoles"
    ADD CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AspNetUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 4751 (class 2606 OID 17876)
-- Name: AspNetUserTokens FK_AspNetUserTokens_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."AspNetUserTokens"
    ADD CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AspNetUsers"("Id") ON DELETE CASCADE;


-- Completed on 2024-03-10 14:42:40

--
-- PostgreSQL database dump complete
--

