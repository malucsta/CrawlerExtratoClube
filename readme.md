# Crawler

O projeto tem a principal funcionalidade pautada em buscar os números nos benefícios correpondentes a um determinado CPF no portal do ExtratoClube. 

### Fluxo
Ao enviar a requisição para consulta dos cpfs através de um post no endpoint `api/enrollments`, a camada de aplicação é responsável por tratar essa demanda. 
Para que isso seja feito da forma mais otimizada, primeiro é consultado o cache, implementado com o redis. Caso seja encontrado um registro de cache cuja chave seja o cpf a ser consultado, é devolvida a resposta de forma imediata. 
Caso não haja registros no cache, buscaremos no ElasticSearch. Caso haja o registro, ele estará indexado e também será retornado. Tudo de forma rápida e fluida. 
No caso de não estar em nenhum dos dois anteriores, isso significa que: ou é a primeira busca do registro ou a informação que está salva no elastic é antiga. Sendo assim, a busca será feita através do Crawler. 
	-> A configuração desse período de expiração é feito através de variáveis de ambiente. Assim, não é necessário um novo deploy da aplicação caso seja alterado, dando flexibilidade e segurança nas operações. 

### Crawler
Uma vez que a busca alcançou a camada do Crawler, é feita uma nova busca no cache. Isso acontece por algumas razões:
1. O desafio diz explicitamente que buscas para o mesmo CPF devem poder ser repetidas na fila
2. Por ser escalável e possivelmente replicado, duas instâncias do Crawler podem processar duas requisições idênticas para o mesmo CPF e, por conseguinte, gastar recursos computacionais a toa. A consulta no cache otimiza parte dessas concorrências.
Caso mais ninguém tenha processado anteriormente aqueles dados, a busca é finalmente feita no site e devolvida através de uma outra fila. 

### Considerações 
- No projeto há também uma implementação de RateLimit aproveitando o uso do Redis. Ainda que um gateway fosse a solução mais adequada, para evitar a sobrecarga de requisições, é configurado um número máximo de requisições por determinado período em segundos.

### Instruções
Como o projeto está dockerizado, para subir as dependências basta rodar: 
``` docker compose up ```

### Pendências
1. Adicionar elasticsearch
2. Ajustar selenium -> não descobri ainda como rodar ele em um ambiente containerizado
 (Apenas por estas razões o projeto não encontra-se completo)

