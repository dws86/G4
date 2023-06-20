# RELATÓRIO TÉCNICO
#
## Desenvolvimento de Sistema para Redes de Computadores
### Grupo: Daniel Walter Dos Santos

## INTRODUÇÃO

Com o advento do trabalho remoto cresce o número de empresas interessadas em buscar soluções seguras que permitam à seus funcionários o acesso a recursos corporativos fora do ambiente empresarial. A empresa com a qual estamos desenvolvendo este projeto não é diferente e adota uma solução popular baseada em VPN, que fornece uma conexão segura e criptografada entre o dispositivo do usuário e a rede da empresa.

A nossa empresa parceira utiliza o OpenVPN, uma solução popular, de fácil configuração e suporte a uma ampla variedade de sistemas operacionais. Para facilitar a criação de usuários de VPN, muitas empresas optam por usar shell scripts em Ubuntu, ClearOS, CentOS entre outros, de modo a automatizar o processo de criação de usuários.

O presente relatório busca detalhar a implementação de uma solução baseada em Linux que permita a criação e o gerenciamento de usuários no OpenVPN de forma intuitiva e rápida, com uma interface desenhada e destinada a todos os níveis de usuários em tecnologia.

## OBJETIVOS

Contextualizar a solução de problemas ao criar usuários de VPN pelo OpenVPN, utilizando-se de um shell script executado em Ubuntu que visa acessar arquivos em um servidor com Windows Server de forma intuitiva e dinâmica, tornando o procedimento mais simples e eficaz.

# METODOLOGIA

#### SOBRE A EMPRESA

A instituição parceira atua no ramo da construção civil, sendo uma empresa de médio porte que possui seus setores bem definidos, não havendo, porém, uma equipe interna de TI. O suporte em tecnologia se dá através de uma equipe terceirizada. Um dos recursos tecnológicos mais utilizados é justamente o acesso aos arquivos confidenciais da empresa através do VPN, tais arquivos localizados em um servidor local da empresa. Cada gestor em seu setor é responsável por solicitar à terceirizada (TI) a criação de usuários VPN conforme a demanda exige, seja por trabalho remoto, viagens, doenças, reuniões online, entre outros.
 
Os setores que mais demandam esse serviço são o setor de marketing, gestão de projetos, RH e principalmente os setores externos como o de engenharia e almoxarifado.
Abaixo podemos exemplos representados de forma geral através dessas personas os problemas e características de uma parcela dos funcionários da empresa e isso nos auxiliou a entender um pouco como eles utilizam esse recurso no dia a dia:

![Persona](prints/Persona.png "Persona")

![Depoimento](prints/Depoimento.png "Depoimento")

Foi ressaltado o atraso na disponibilização dos acessos para funcionários em home office, atrapalhando algumas vezes o andamento das reuniões online e a disponibilização de arquivos importantes. De outra perspectiva, a constante necessidade de solicitar novas credenciais a cada vez que um novo funcionário entrar na equipe acaba retardando a integração do mesmo nos projetos que já estão em andamento, visto que a disponibilização dos arquivos só ocorre quando o acesso VPN estiver normalizado. Para ele, a possibilidade dele mesmo criar os “acessos ao VPN” conforme necessidade, agilizaria todo o processo e tornaria a integração de novos funcionários mais rápida, já que, sendo ele o gestor de um setor que demanda uma versatilidade quanto a localização de seu trabalho, todos os funcionários precisam do recurso, sem exceção.

#### PROCESSO DE UTILIZAÇÃO DO VPN

A utilização do VPN é um recurso bem mais exigido nos setores externos da empresa, visto que além da maior rotatividade de funcionários TODOS eles utilizam o recurso para acessos aos servidores na matriz. A criação dos usuários no VPN é um processo um pouco demorado, exige a abertura de uma solicitação para a terceirizada que responde de acordo com seu próprio gerenciamento interno. O gestor ao receber as credenciais de acesso repassa ao membro solicitante da equipe e este enfim poderá utilizá-lo. De acordo com alguns gestores, entre solicitação e o recebimento do certificado de acesso levam em média de 1 a 2 dias.

Em uma rápida busca na internet encontramos alguns softwares que poderiam solucionar certas questões que encontramos na empresa. A tabela a seguir mostra um pouco dos pontos fortes e fracos das soluções encontradas:

| Produto | Descrição | Pontos Fortes | Pontos Fracos |
| ------ | ------ | ------ | ------ |
| Okta | Okta é uma plataforma de gerenciamento de identidade e acesso em nuvem que pode ser usada para automatizar a criação de usuários para VPNs. | A possibilidade de manter a senha de aplicativos legados salvos. A autenticação de dois fatores utilizando o push no celular (Android). Centralizar os apps e suas credenciais de acesso no Okta | Okta pode ser caro para pequenas empresas ou organizações com orçamentos limitados.2.Suporte: Embora a equipe de suporte da Okta seja geralmente responsiva, alguns usuários relataram longos tempos de espera para tíquetes de suporte.
| Cisco Jabber | Compartilhamento da área de trabalho,	mensagens instantâneas e conferências para empresas, além da capacidade de colaborar em qualquer lugar a partir do seu telefone. | um aplicativo tudo em um, ele te dá a possibilidade de ter seu ramal no seu computador, celular ou tablet, podendo enviar mensagens para seus colegas, compartilhar documentos, telas, acessar correio de voz ou até mesmo	salas	de videoconferência. | Oferece uma série de funcionalidades que muitas das vezes não são úteis e o seu custo permanece mesmo sem o uso das mesmas. Tornando sua implementação mais complexa.
| PureVPN | A PureVPN é uma empresa do setor de segurança cibernética com uma constelação autogerida de mais de 300 mil IPs, mais de 2.000 servidores em mais de 180 locais e uma garantia de devolução do dinheiro em 31 dias. O PureVPN oferece acessibilidade ao conteúdo, garantindo anonimato total aos usuários online. | O PureVPN é fácil de instalar e manter em computadores e dispositivos móveis, com aplicativos disponíveis para vários dispositivos. Fornecendo a possibilidade também de escolher um local ou aceitar a melhor conexão. Isso significa que há facilidade de uso juntamente com uma escolha de configurações mais complexas, como um IP dedicado. | Ocorrem diversos problemas ao tentar realizar implementações e configurações mais complexas. Em especial na sua funcionalidade de escolher a melhor conexão, deixando a desejar em momentos inoportunos.
| VyprVPN | Ferramenta de VPN móvel e no local que ajuda os usuários a navegar em rede privada e impedir que o provedor de internet supervisione	as comunicações. | Oferece muitos locais para acessar, o que o torna uma VPN muito confiável para usar. Os preços são razoavelmente precisos e oferecem muitos recursos e facilidade de implementação. | Falta de um recurso de encaminhamento de porta pode fazer com que alguns usuários escolham outras alternativas. Presença de anúncios, marketing e mensagens direcionados ao usuário e instabilidade na conexão ao utilizar.

Como podemos ver, algumas soluções apresentam certos atrasos nas criações dos usuários e na rede em si, outras são pagas ou apresentam anúncios em sua utilização e outras possuem um nível de complexidade que exigiria conhecimentos específicos dos usuários. Frente a essas questões, a opção de criar uma solução interna que ofereça aos gestores alguns dos pontos positivos que encontramos em outros produtos, ao mesmo tempo que disponibilize apenas recursos essenciais se tornou bem mais viável. Essa dinâmica facilitaria o andamento de vários processos que dependem do acesso aos servidores por parte da equipe, diminuindo o tempo de espera pelo recurso.

## APRESENTANDO UMA SOLUÇÃO

Após algumas reuniões com os membros da equipe e levando em consideração a visão de Raquel e Daniel que compartilharam suas experiências conosco, idealizamos a criação de um shell script que seja capaz de criar
 
credenciais de acesso ao VPN e que faça o download dos arquivos necessários no computador a ser utilizado. O utilizador poderá criar seu usuário em uma interface WEB de forma simples e rápida, já disponibilizando de imediato o acesso aos usuários. Além disso, ofereceria em seus recursos estatísticas de usuário, alertas, análises, histórico de conexões, entre outros. Tais dados seriam úteis para também para a equipe terceirizada de TI em forma de estatísticas de uso.
Abaixo podemos ver exemplo de uma possível interface a ser adotada:

![VPN](https://github.com/dws86/G4/prints/VPN.png "VPN")

## PRÓXIMAS ETAPAS

Para as próximas etapas daremos início a construção de alguns parâmetros do script e alguns testes, a seguir alguns tópicos que abordaremos:

- Definir as funcionalidades do script: o primeiro passo é definir quais funcionalidades o shell script deve ter, como criação de usuários, alteração de senhas, exclusão de usuários, entre outras. Essa definição deve ser baseada nas necessidades da empresa com a qual estamos desenvolvendo o projeto, utilizando de personagens e histórias para essa definição.

- Definir a interface web: a interface web deve ser intuitiva e fácil de usar. O usuário deverá realizar as operações necessárias de forma rápida e eficiente. É importante considerar as melhores práticas de design de interface, como usabilidade e acessibilidade.

- Escrever o código do shell script: com as funcionalidades e interface definidas, é hora de escrever o código do shell script. Seguindo boas práticas de programação, como modularização do código e documentação adequada.

- Testar o script: após a escrita do código, é importante testar todas as funcionalidades para garantir que o script esteja funcionando corretamente. Criando cenários de teste para verificar se todas as funcionalidades estão funcionando corretamente.

- Implementar a interface web: após o teste do script, é hora de implementar a interface web. É importante que a interface seja integrada ao script de forma transparente e intuitiva para o usuário.

- Testar a interface web: após a implementação da interface web, é importante testar todas as funcionalidades para garantir que a interface esteja funcionando corretamente e que o usuário possa realizar as operações necessárias de forma rápida e eficiente.

- Documentação: por fim, é importante documentar todas as etapas do processo de criação do shell script, incluindo as funcionalidades, o código, a interface e os testes realizados. A documentação deve ser clara e concisa, para que outros usuários possam entender e utilizar o shell script adequadamente.

- Em resumo, a metodologia no processo de criação de um shell script para criação de usuários por meio de um web Linux com uma interface intuitiva envolve a definição das funcionalidades e interface, a escrita do código, o teste do script, a implementação da interface web, o teste da interface web e a documentação adequada.

# CONCLUSÃO

Em resumo, o uso de um shell script para criar usuários no Windows Server é uma solução eficiente e escalável para gerenciar a criação de contas de usuário para usuários do OpenVPN. O script pode ser facilmente personalizado para
 
atender às necessidades específicas da organização, como a adição de permissões de usuário e grupos.

Além disso, a automação de tarefas repetitivas, como a criação de contas de usuário, pode ajudar a minimizar erros humanos e reduzir o tempo gasto na administração do servidor. Com o shell script, o suporte técnico pode se concentrar em outras tarefas importantes enquanto o script executa a criação de usuários de forma rápida e eficiente.

Em suma, a criação de um shell script para a criação de usuários no OpenVPN e no Windows Server é uma maneira inteligente e eficaz de gerenciar a rede e fornecer acesso seguro aos recursos de rede para os usuários.

Levando em consideração a necessidade da empresa em ter diversos usuários acessando um servidor de arquivos de forma externa, a criação de usuários ser intuitiva permite que cada setor possa gerenciar seus usuários fazendo com que tenhamos um serviço prático e eficiente que economiza tempo e passa a depender menos do suporte técnico local.
