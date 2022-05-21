global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;

global using System.Text;
global using System.Security.Claims;
global using System.IdentityModel.Tokens.Jwt;

global using CommonLibrary.Server;
global using CommonLibrary.AssemplyScanning;
global using CommonLibrary.ViewModels;

global using Account.Server.Entities;
global using Account.Server.Repositories;
global using Account.Server.Validations;
global using FluentValidation;
global using System.Security.Cryptography;
global using Account.Server.TokenServices;
