# VR Food Truck Game Prototype

## Game Description 
This is a VR game where you build sandwiches and deliver them to starving customers using a cannon on a food truck. 

[![Header Image](https://github.com/stevie57/VR-Cooking-Training-Prototype/blob/main/Assets/Textures/Sandwich%20image.png)](https://www.youtube.com/watch?v=idGoXzuUl7Y)

## Game Overview
This is a time based game so you are simply trying to get as high of a score that you can before time runs out. You gain points by sending a sandwich with the correct ingredients requested by the customer inside.

I like to think of this as a VR version of Overcooked which was a cooking game that I really liked.

## Loading Scenes Asynchronously and Object Pooling
One of my learning goals for this project was to better manage loading within the game. To create a more seamless experience for the player I have a screen fade that occurs during game transitions. The screen fades to black and then levels are loaded asynchronously. During the period where the screen is black I have all the needed object pools and other game objects loaded. The goal is to make it so that any screen tearing/skipping or loading goes unnoticed by the player as a result.

## Unity XR Toolkit and Action-based Input
This is my first time implementing Unity's XR Interaction Toolkit with the new action-based input system. I found that its pretty intuitive and not too difficult to use. I added additional functionality to the scripts to make this VR game work.

## Sandwich and Topping System
The Sandwich system relies on a gameobject with a sandwich handler and gameobject with a topping handler. The topping gameobjects are created in an object pool at the start of the scene and retrieved from the pool when the player goes to grab a topping. How the topping interacts with the sandwich is simply through checking for the sandwich handler when the topping collides with other objects. If the topping collides against a object with a sandwich handler, the topping is set to inactive and calls the add topping method in the sandwich and passes on a string for what topping needs to be added.

The sandwich keeps track of what toppings have been added to it in a list<string> . A customer can then check this list in the sandwich handler to see if the toppings it currently has matches the customers own personal list for sandwich toppings.

## Customer System
Customers are initialized upon loading the game level and sent in a Queue<customer> and set to inactive. Current customer is set to active and then they send their list of topping requests to the player topping request UI canvas. One unique challenge here was that I didn't want the customers to overlap with their spawn positions so when they are assigned a spawn location they check it against a list of used spawn locations. If the random spawn location they were assigned is used, the script generates a new spawn location number until it gets one that is unused.

## VR Canon
The VR canon is controlled with a simple lever that uses Unity's configurable joint and locks its position and rotation to mimic a lever. The cannon aim is adjusted based on how you move the lever and it is is calculated by transforming the current position of the lever against its maximum and minimum range.

## Check out my other VR Projects
* **VR Card Battle Game** : https://github.com/stevie57/VR-BattleGame
* **My VR Web Portfolio and Blog** : http://www.stevievu.com/p/about-me_31.html
