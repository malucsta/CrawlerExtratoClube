### Considera��es:
- No futuro: vale a pena implementar uma pol�tica de retry no caso da mensagem n�o conseguir ser enviada no momento da requisi��o -> resili�ncia
- No elastic � salva tamb�m a data de cria��o do registro. Assim, saberemos se ele foi salvo h� muito tempo e precisa ser consultado novamente ou n�o
- Idealmente, para comunica��o com o front, usaria o SignalR. Para fins de simplifica��o, usarei uma fila mesmo do RabbitMq
