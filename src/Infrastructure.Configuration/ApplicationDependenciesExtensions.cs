namespace PetProjects.MicroTransactionsApi.Infrastructure.Configuration
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using PetProjects.Framework.Cqrs.DependencyResolver;
    using PetProjects.Framework.Cqrs.Extensions.AspNetCore;
    using PetProjects.Framework.Cqrs.Mediator;
    using PetProjects.MicroTransactionsApi.Application.CommandHandlers.Transactions;
    using PetProjects.MicroTransactionsApi.Application.Dto;
    using PetProjects.MicroTransactionsApi.Application.Dto.Transactions;
    using PetProjects.MicroTransactionsApi.Application.Queries.Transactions;
    using PetProjects.MicroTransactionsApi.Application.QueryHandlers.Transactions;

    using CreateTransactionCommand = PetProjects.MicroTransactionsApi.Application.Commands.Transactions.CreateTransactionCommand;

    internal static class ApplicationDependenciesExtensions
    {
        internal static IServiceCollection AddMediator(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddCqrsDependencyResolver(builder => builder
                    .RegisterQueryHandlers()
                    .RegisterCommandHandlers())
                .AddScoped<ISimpleMediator, SimpleMediator>();
        }

        internal static IServiceCollection ConfigureApplicationServices(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddMediator();
        }

        private static IDependencyResolverBuilder RegisterQueryHandlers(this IDependencyResolverBuilder builder)
        {
            return builder
                .RegisterQueryHandlerAsync<GetTransactionByIdQueryHandler, GetTransactionByIdQuery, TransactionByIdDto>()
                .RegisterQueryHandlerAsync<GetTransactionsQueryHandler, GetTransactionsQuery, PagedResultDto<TransactionByIdDto>>();
        }

        private static IDependencyResolverBuilder RegisterCommandHandlers(this IDependencyResolverBuilder builder)
        {
            return builder
                .RegisterCommandHandlerWithResponseAsync<CreateTransactionCommandHandler, CreateTransactionCommand, Guid>();
        }
    }
}