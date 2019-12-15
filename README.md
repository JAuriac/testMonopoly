The goal of this project was to create a Monopoly-like game using one or severals Design Pattern. Monopoly is a strategy game in which to win the players have to eliminate each other.
A player is eliminated when he can't pay anymore a player or the bank. In order to simplify the project, severals rules has been modificated or deleted.

We have used 2 different Design Patterns.

The first one is the Singleton Design Pattern. The Singleton Design Pattern is one of the best tool to create an object. This model involves a single class with the 
task to take care of the object as well as making sure only one instance of the item is created. The aforesaid class makes possible the acess to the object without necesseraly having
it instancier, which can be very useful.

The second Design Pattern we chose to use is the State Design Pattern. He authorises the change of behaviour of one class depending on its state. We took advantage of it:
at the beginning of the player's turn (while(!isOver)), the program checks if the player is in jail or not.
If he's imprisoned, the classe's behaviour will be different (game round "jail") than if the player isn't (game round "classic"). The different behaviours are handled by different items
(which represent the different states of the player) and a context object of which the behaviour changes according to its state object changes.

Rules's modifications peculiar to our game:
-the goal is to be the first player to reach 100 000 (the player can't be eliminated),
-each passing by square one (square index 0) gives 10 000 to the player,
-the houses and hostels increase this income.