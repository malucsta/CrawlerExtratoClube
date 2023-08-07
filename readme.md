# Crawler

O projeto tem a principal funcionalidade pautada em buscar os n�meros nos benef�cios correpondentes a um determinado CPF no portal do ExtratoClube. 

### Fluxo
Ao enviar a requisi��o para consulta dos cpfs atrav�s de um post no endpoint `api/enrollments`, a camada de aplica��o � respons�vel por tratar essa demanda. 
Para que isso seja feito da forma mais otimizada, primeiro � consultado o cache, implementado com o redis. Caso seja encontrado um registro de cache cuja chave seja o cpf a ser consultado, � devolvida a resposta de forma imediata. 
Caso n�o haja registros no cache, buscaremos no ElasticSearch. Caso haja o registro, ele estar� indexado e tamb�m ser� retornado. Tudo de forma r�pida e fluida. 
No caso de n�o estar em nenhum dos dois anteriores, isso significa que: ou � a primeira busca do registro ou a informa��o que est� salva no elastic � antiga. Sendo assim, a busca ser� feita atrav�s do Crawler. 
	-> A configura��o desse per�odo de expira��o � feito atrav�s de vari�veis de ambiente. Assim, n�o � necess�rio um novo deploy da aplica��o caso seja alterado, dando flexibilidade e seguran�a nas opera��es. 

### Crawler
Uma vez que a busca alcan�ou a camada do Crawler, � feita uma nova busca no cache. Isso acontece por algumas raz�es:
1. O desafio diz explicitamente que buscas para o mesmo CPF devem poder ser repetidas na fila
2. Por ser escal�vel e possivelmente replicado, duas inst�ncias do Crawler podem processar duas requisi��es id�nticas para o mesmo CPF e, por conseguinte, gastar recursos computacionais a toa. A consulta no cache otimiza parte dessas concorr�ncias.
Caso mais ningu�m tenha processado anteriormente aqueles dados, a busca � finalmente feita no site e devolvida atrav�s de uma outra fila. 

### Considera��es 
- No projeto h� tamb�m uma implementa��o de RateLimit aproveitando o uso do Redis. Ainda que um gateway fosse a solu��o mais adequada, para evitar a sobrecarga de requisi��es, � configurado um n�mero m�ximo de requisi��es por determinado per�odo em segundos.

### Instru��es
Como o projeto est� dockerizado, para subir as depend�ncias basta rodar: 
``` docker compose up ```

### Pend�ncias
1. Adicionar elasticsearch
2. Ajustar selenium -> n�o descobri ainda como rodar ele em um ambiente containerizado
 (Apenas por estas raz�es o projeto n�o encontra-se completo)

