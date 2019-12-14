L'objectif de ce projet était de créer un jeu type Monopoly en utilisant un ou plusieurs
Design Pattern. Pour rappelle, le Monopoly est un jeu de stratégie où, pour gagner, les 
joueurs doivent éliminer les autres. Un joueur est éliminer quand il ne peut plus payer
un joueur ou la banque. Afin de simplifier le projet, un certains nombre de règles ont été
modifiées voire supprimées.

Dans notre cas, nous avons utilisé deux Design Pattern différents.

Premièrement, le Design Pattern Singleton. Le Design Pattern Singleton offre l'un des meilleurs
moyens de créer un objet. Ce modèle implique une seule classe, qui s'occupera de gérer l'objet,
tout en s'assurant qu'une seule unité de cet objet soit créé. Ladite classe permet d'accéder
à l'objet créé sans avoir besoin de l'instancier, ce qui peut se révéler très utile.

Deuxièmement, nous avons choisi d'utiliser le Design Pattern State. Il permet que le comportement
d'une classe change en fonction de son état. Typiquement, nous avons utilisé cela à notre avantage :
au début du tour d'un joueur, on vérifie s'il est emprisonné (dans le while(!isOver) il y a un 
if(lesJoueurs[whichTurn].getState() == deb si le joueur n'est pas en prison, else si il y est).
Si tel est le cas, la classe aura un
comportement différent (tour de jeu "prison") de s'il ne l'est pas (tour de jeu "classique"). Les
différents comportements sont gérés par différents objets (qui représentent les différents états)
et un context object dont le comportement varie en fonction des changements d'états de son objet.

Modifications des règles propres à notre jeu :
-le but est d'être le premier joueur à atteindre les 100 000,
-chaque passage par la case départ (case index 0) rapporte 10 000,
-les maisons et les hotêls permettent d'augmenter ce revenu.
