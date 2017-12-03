namespace PetProjects.MicroTransactionsApi.Infrastructure.Configuration
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using PetProjects.Framework.Cqrs.DependencyResolver;
    using PetProjects.Framework.Cqrs.Extensions.AspNetCore;
    using PetProjects.Framework.Cqrs.Mediator;
    using PetProjects.MicroTransactionsApi.Application.CommandHandlers.Transactions;
    using PetProjects.MicroTransactionsApi.Application.Commands.Transactions;
    using PetProjects.MicroTransactionsApi.Application.Dto.Transactions;
    using PetProjects.MicroTransactionsApi.Application.Queries.Transactions;
    using PetProjects.MicroTransactionsApi.Application.QueryHandlers.Transactions;

    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddCqrsDependencyResolver(builder => builder
                .RegisterQueryHandlers()
                .RegisterCommandHandlers());

            serviceCollection.AddScoped<ISimpleMediator, SimpleMediator>();

            return serviceCollection;
        }

        private static IDependencyResolverBuilder RegisterQueryHandlers(this IDependencyResolverBuilder builder)
        {
            builder
                .RegisterQueryHandlerAsync<GetTransactionByIdQueryHandler, GetTransactionByIdQuery, TransactionByIdDto>()
                .RegisterQueryHandlerAsync<GetTransactionsQueryHandler, GetTransactionsQuery, TransactionsPageDto>();

            return builder;
        }

        private static IDependencyResolverBuilder RegisterCommandHandlers(this IDependencyResolverBuilder builder)
        {
            builder
                .RegisterCommandHandlerWithResponseAsync<CreateTransactionCommandHandler, CreateTransactionCommand, Guid>();

            return builder;
        }
    }
}
