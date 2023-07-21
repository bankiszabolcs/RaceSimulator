# Race simulation
3 types of vehicles take part in the competition: Car, Motorcycle, Truck. 10 of each type compete for 50 hours, the progress of each competitor is checked hourly. To make testing easier you can adjust how many sec does one hour equal.
A subtle change affects the behavior of the vehicles participating in the competition. There is a 30% chance of rain every hour. At the end of the competition, the result is announced. The result includes the name of the vehicle, the distance traveled and the type.

## Car functions:
- Normal speed: between 80-110 km/h (random), this data does not change during the race
- If there is a broken down truck on the track, the speed of the car is limited to 75 km/h in the given hour
- Randomly generated name from two lists (e.g. Blitz Viper)

## Motor functions:
- Normal speed: 100 km/h, this figure does not change during the race
- If it is raining, it moves 5-10 km/h (random) slower than its normal speed
- Name: Motor + number of iteration when the object was created, unique value (e.g. Motor 1, Motor 2)

## Truck features:
- Normal speed: 100km/h
- 5% chance to be down for 2 hours
- Name: Random number between 0-1000

***

# Verseny szimuláció
A cél, egy verseny szimuláció futtatására alkalmas kód megírása, OOP alapelvek betartásával. A versenyben 3 típusú jármű vesz részt: Autó, Motor, Kamion. Mindegyik típusból 10 db versenyez 50 órán keresztül, az egyes versenyzők előrehaladását óránként vizsgáljuk. A változékony időjárás befolyásolja a versenyben részt vevő járművek viselkedését. Minden órában 30% az esély arra, hogy esik az eső. A verseny végeztével az eredmény kiírása kerül. Az eredménynek tartalmaznia kell a járművek nevét, megtett útját és a típusát.
A random számításoknál beépített funkció használata ajánlott.

## Autó funkciók:
- Normál sebesség: 80-110 km/h (random) között, ez az adat a verseny során nem változik
- Ha van a pályán lerobbant kamion, akkor az autó sebessége 75 km/h –ra korlátozódik az adott órában
- Név: Kétlistából véletlenszerűen generált név (pl. Blitz Viper) 

## Motor funkciók:
- Normál sebesség: 100 km/h, ez az adat a verseny során nem változik
- Ha esik, akkor 5-10 km/h (random) lassabban halad a normál sebességénél
- Név: Motor + az iteráció száma, amikor létre lett hozva az objektum, egyedi érték (pl. Motor 1, Motor 2)

## Kamion funkciók:
- Normál sebesség: 100km/h
- 5% esély, hogy 2 órára lerobban
- Név: Random szám 0-1000 között
