# Blue Water Meter

O Blue Water Meter é um protótipo de projeto para um sensor de consumo de água. O conceito do projeto foi criado pela [Juliana Scarpin](github.com/scarpinjuliana) e um protótipo de hardware e software foi implementado pelo [Ricardo Gomes](github.com/rgsilva).

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

## Módulo hub

O hub consiste de um microcontrolador (ESP8266 ESP-12) e um recepetor de rádio (MX-RM-5V). O microcontrolador escuta por transmissões de sensores na frequência 433 MHz e, ao receber, registra a informação em memória. Também é verificado o número de sequência para evitar processar pacotes já recebidos.

A cada pacote recebido, uma requisição HTTP para um servidor pré-definido será realizada, e então, no servidor, esta informação será logada. Para isto, o microcontrolador está constantemente conectado na rede WiFi. A configuração da rede e outras funções de debug estão disponíveis por uma interface serial (disponível via USB na ESP-12).

