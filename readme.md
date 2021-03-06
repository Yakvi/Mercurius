# Mercurius - mobile currency converter
## Introduction
Mercurius is a simple currency converter app for iOS and Android.

## Features
### Core
User may select the amount to convert:
* Source currency
* End currency
* Conversion date (today by default)

Conversion happens automatically based on the amount inserted in either field.

### Additional features
* Local data caching (with option to clear it)
* Usage of native mobile date picking
* Preservation of preferred currencies between sessions

## Build info
### Unity Version
2019.3 (release)

### External packages
* [Easy Mobile Pro](https://assetstore.unity.com/packages/tools/integration/easy-mobile-pro-75476): Environment setup
* [Mobile Dialog Unity](https://unitylist.com/p/dp9/Mobile-Dialog-Unity): Native date picker
* Text Mesh Pro

### API used
* [Exchangeratesapi.io](https://exchangeratesapi.io/): Daily exchange rates based on data by the [European Central Bank](https://www.ecb.europa.eu/stats/policy_and_exchange_rates/euro_reference_exchange_rates/html/index.en.html).