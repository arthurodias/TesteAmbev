using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.EventHandlers
{
    public class SaleCreatedEventHandler : INotificationHandler<SaleCreatedEvent>
    {
        private readonly ILogger<SaleCreatedEventHandler> _logger;

        public SaleCreatedEventHandler(ILogger<SaleCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
        {
            var sale = notification.Sale;

            _logger.LogInformation(
                "Evento SaleCreated disparado para venda #{SaleNumber} com {ItemCount} itens. Cliente: {Customer}",
                sale.SaleNumber,
                sale.Items.Count,
                sale.Customer
            );

            return Task.CompletedTask;
        }
    }
}