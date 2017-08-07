# Blue Water Meter

This is a IoT water meter prototype developed together with Juliana Scarpin. This is not meant to be used in proeuction.

The concept behind it the following: each Sender (with a proper ID) will send its measurements data over a wireless link, preferably one that doesn't consume a lot of power (433 is better than wifi, for example). A well-located hub will receive this data and process it by uploading it to a proper server on a wireless network.


## Playground

A bunch of tests and playground code in general.

## Prototype

This folder contains the actual prototype code.

## WaterMeter Config Tool

This folder contains the code for the prototype programming tool. It's also a (crappy/buggy) debugging tool when serial isn't available. Don't expect great code quality here, it's mostly hacks to create a better configuration tool.
