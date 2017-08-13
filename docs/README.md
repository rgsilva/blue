<p style="text-align: center;">
  <img align="center" src="https://github.com/rgsilva/blue/raw/master/docs/logo.small.png" />
</p>

O Blue é um protótipo de projeto para um sensor de consumo de água. O conceito do projeto foi criado pela [Juliana Scarpin](github.com/scarpinjuliana) e um protótipo de hardware e software foi implementado pelo [Ricardo Gomes](github.com/rgsilva).

# Disclaimer

A parte de hardware e software projeto é para fins de protótipo e não devem ser utilizado como base para um produto final. Boa parte do código e decisões aqui foram realizadas levando em conta deadlines, e soluções mais apropriadas para os problemas encontrados são discutidas mais adiante nesta documentação.

# Organização

O kit do sensor consiste de dois tipos de módulos diferentes: um sensor e um hub de comunicações. Note que o termo "sensor" aqui se refere ao módulo completo (incluindo transmissor), e não apenas ao sensor de fluxo.

A comunicação entre os módulos ocorre via radio-frequência a 433 MHz. Isto foi implementado com o uso de transmissores e receptores simples (MX-RM-5V e FS1000A). Apesar da baixa potência dos dois, com o uso de uma antena é possível realizar uma comunicação simples. A transmissão final dos dados para um servidor é realizada via Wifi e requisições HTTP.

A idéia de utilizar RF ao invés de WiFi diretamente no módulo sensor é de permitir com que o sensor fique o tempo todo desconectado da rede, e evitar o grande consumo de energia que é criado pelo uso de uma rede WiFi. Assim, o sensor poderia se utilizar de diversas fontes de energia, desde baterias e supercapacitores, até mesmo painéis solares e dínamos. O hub, entretanto, foi planejado para ficar constantemente ligado na tomada.

## Módulo sensor

O módulo sensor consiste de um microcontrolador (Arduino Uno - ATmega328P), um sensor de fluxo (YF-S201) e um transmissor de rádio (FS1000A). O microcontrolador monitora pulsos vindo do sensor de fluxo, faz a conversão matemática dos valores e envia um burst de dados na frequência 433 MHz. O pacote de transmissão consiste de 3 campos:

* Número de sequência (para detectar repetições e perdas)
* Valor da medição

O motivo do envio via burst é para garantir que a informação irá chegar ao destino. Apesar de não ser a solução ideal, para fins de prototipagem ela se mostrou eficiente.

Além de enviar as informações, o módulo sensor também monitora o tempo de fluxo de água. Isto significa que, após um tempo pré-determinado de água correndo, ele irá emitir beeps. O conceito por trás desta feature é emitir um alerta para o usuário de que muita água está sendo utilizada, como no caso de um banho muito demorado, ou até mesmo uma torneira com defeito.

Novamente, para fins de prototipagem, este valor é uma constante interna do módulo.

([código fonte](https://github.com/rgsilva/blue/tree/master/Prototype/sender))

## Módulo hub

O hub consiste de um microcontrolador (ESP8266 ESP-12) e um recepetor de rádio (MX-RM-5V). O microcontrolador escuta por transmissões de sensores na frequência 433 MHz e, ao receber, registra a informação em memória. Também é verificado o número de sequência para evitar processar pacotes já recebidos.

A cada pacote recebido, uma requisição HTTP para um servidor pré-definido será realizada, e então, no servidor, esta informação será logada. Para isto, o microcontrolador está constantemente conectado na rede WiFi. A configuração da rede e outras funções de debug estão disponíveis por uma interface serial (disponível via USB na ESP-12).

([código fonte](https://github.com/rgsilva/blue/tree/master/Prototype/hub))

## Servidor

O servidor consiste unicamente de scripts que irão registrar o consumo, fazendo um log com timestamp e valores recebidos pelo hub. Com isto, é possível calcular o consumo em um determinado intervalo, tal como dia, semana, mês ou ano.

(o código fonte para a parte do servidor será publicado no futuro)

## Programa de configuração

Para facilitar a configuração da rede WiFi do módulo hub, e permitir testar e realizar debug das transmissões, um pequeno programa de configuração foi criado em C#. O código deste projeto é unicamente para testes e não deve ser considerado como critério de programação ideal ou correta.

([código fonte](https://github.com/rgsilva/blue/tree/master/WaterMeter%20Config%20Tool))

# Melhorias futuras

Por se tratar apenas de um protótipo, há uma quantidade considerável de coisas que podem ser melhoradas.

## Consumo de energia

Os módulos criados papra fins de _proof-of-concept_ dependem de energia constante e não utilizam nenhum tipo de modo de baixo consumo de energia. Isto não é de grande complexidade de implementação, e é essencial para o módulo sensor. Já o hub não iria desfrutar disto, já que seu consumo, mesmo ligado constantemente, é bastante baixo.

## Alimentação

Há diversas alternativas para alimentação do módulo sensor. As mais interessantes são as seguintes:

* Wireless energy harvesting juntamente com o uso de supercapacitores para armazenar a energia obtida. Como a quantidade de energia que pode ser gerada por esta técnica é bastante baixa, aqui um baixo consumo se torna crítico, além do uso de supercapacitores, já que baterias são geralmente mais complexas de carregar.

* Dínamos também poderiam ser utilizados no próprio módulo para obter energia enquanto o fluxo é monitorado. A energia teria de ser armazenada em algum meio (bateria ou supercapacitor), e deverá ser filtrada por um circuito antes, já que dínamos costumam gerar ruído.

* Painéis solares são uma opção bastante viável para monitoramento em locais abertos, tais como irrigações de plantações. Esta opção, entretanto, não se torna muito viável para uso doméstico, já que o módulo muitas vezes pode ser instalado em locais sem luminosidade.

* Baterias também são uma opção viável na falta de alternativas melhores. Seria necessário, entretanto, achar uma forma de monitorá-las e indicar ao usuário que elas devem ser trocadas.

## Comunicação

A comunicação atualmente é feita via 433 MHz e de uma maneira não muito eficiente. Uma alternativa seria trocar para BLE (Bluetooth Low Energy), o que permite inclusive a comunicação de outros dispositivos (eg. celular) diretamente com o sensor para configuração. Além disto, atualmente a comunicação não é bidirecional, o que geraria um problema para o usuário final se ele necessitasse trocar, por exemplo, o tempo máximo de fluxo constante de água (para alertas de banhos longos).

No caso de permanência com 433 MHz, seria necessária também a identificação do sensor, como com um número de série. Além disso, o hub deveria ter uma lista de sensores cadastrados, e esse processo de pareamento deveria ser feito de alguma forma automática. Entretanto, isto não seria um problema tão complexo com o uso de BLE, já que temos a identificação do dispositivo (MAC) e pareamento.

## Sensor de fluxo

O sensor de fluxo utilizado para o protótipo é bastante simples e induz ao erro caso a câmara interna dele não esteja 100% cheia. Há formas de evitar este tipo de problema, como colocar antes do registro final (eg. antes da torneira de uma pia), mas isto não necessariamente garante a solução completa do problema. Sensores mais apropriados, ou soluções mais eficientes, precisariam ser implementadas neste caso.

Além disso, o sensor utilizado não possui uma grande precisão, e portanto precisaria ser trocado.

# Contato

* [Juliana Scarpin](https://www.linkedin.com/in/julianascarpin/)
* [Ricardo Gomes da Silva](https://www.linkedin.com/in/ricardo-gomes-da-silva-43879032/)
