### Considerações:
- No futuro: vale a pena implementar uma política de retry no caso da mensagem não conseguir ser enviada no momento da requisição -> resiliência
- No elastic é salva também a data de criação do registro. Assim, saberemos se ele foi salvo há muito tempo e precisa ser consultado novamente ou não
- Idealmente, para comunicação com o front, usaria o SignalR. Para fins de simplificação, usarei uma fila mesmo do RabbitMq
