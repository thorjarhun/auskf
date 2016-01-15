namespace AUSKF
{
    using System;
    using Castle.Core;
    using Domain.Data;
    using Domain.Entities;
    using Domain.Entities.Identity;
    using Domain.Interfaces;
    using Domain.Providers;
    using Domain.Providers.Identity;
    using Domain.Providers.Interfaces;
    using Domain.Repositories;
    using Domain.Repositories.Interfaces;
    using Domain.Services;
    using Domain.Services.Interfaces;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;

    public class ContainerConfig
    {
        public static void RegisterComponents()
        {
            // this will register all of our controllers
            var assemblyDiscoveryService = Ioc.Instance.Resolve<IAssemblyDiscoveryService>();
            assemblyDiscoveryService.GenerateDependencyList();
            var controllerRegistrationService = Ioc.Instance.Resolve<IControllerRegistrationService>();
            controllerRegistrationService.RegisterControllers();

            // Data
            Ioc.Instance.AddComponentWithLifestyle("IDataContext", typeof(IDataContext), typeof(DataContext), LifestyleType.PerWebRequest);
            // Repositories
            RegisterRepositories();

            Ioc.Instance.AddComponentWithLifestyle("ApplicationSignInManager", typeof(IApplicationSignInManager), typeof(ApplicationSignInManager), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("ApplicationUserManager", typeof(IApplicationUserManager), typeof(ApplicationUserManager), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("AuthenticationManager", typeof(IAuthenticationManager), typeof(ApplicationAuthenticationManager), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("IUserStore", typeof(IUserStore<User, int>), typeof(UserStoreProvider<User, int>), LifestyleType.Transient);
            Ioc.Instance.AddComponentWithLifestyle("ClaimsIdentityFactory", typeof(IClaimsIdentityFactory<User, int>), typeof(ApplicationClaimsIdentityFactory), LifestyleType.PerWebRequest);


        }


        // TODO auto repository registration
        private static void RegisterRepositories()
        {
            Ioc.Instance.AddComponentWithLifestyle("IUserRepository", typeof(IEntityRepository<User, int>), typeof(EntityRepository<User, int>), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("IAddressRepository", typeof(IEntityRepository<Address, int>), typeof(EntityRepository<Address, int>), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("IPromotionRepository", typeof(IEntityRepository<Promotion, Guid>), typeof(EntityRepository<Promotion, Guid>), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("IUserClaimRepository", typeof(IEntityRepository<UserClaim, Guid>), typeof(EntityRepository<UserClaim, Guid>), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("IUserLoginRepository", typeof(IEntityRepository<UserLogin, Guid>), typeof(EntityRepository<UserLogin, Guid>), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("IUserRoleRepository", typeof(IEntityRepository<UserRole, Guid>), typeof(EntityRepository<UserRole, Guid>), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("ILogRepository", typeof(IEntityRepository<Log, Guid>), typeof(EntityRepository<Log, Guid>), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("IKendoRankRepository", typeof(IEntityRepository<Rank, int>), typeof(EntityRepository<Rank, int>), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("IDojoRepository", typeof(IEntityRepository<Dojo, int>), typeof(EntityRepository<Dojo, int>), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("IFederationRepository", typeof(IEntityRepository<Federation, int>), typeof(EntityRepository<Federation, int>), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("IEventsRepository", typeof(IEntityRepository<Event, Guid>), typeof(EntityRepository<Event, Guid>), LifestyleType.PerWebRequest);
        }
    }
}