OpenGL-Room
===========

C# room example

.. image:: https://i.imgur.com/JloRtUD.png

**OpenGL**

OpenGL is a graphics library that can be used to display geometrical models on the screen with help of the GPU. With it you can create 3D objects, determine perspectives or define and display different effects. 
OpenGL sends primitive commands directly to the graphics card. Here the OpenGL version 2.1 is used, because in this version approx. 200 functions are defined, with which one can work well. Version 3.X or version 4.X contain too many or too few commands. The wrapper (SharpGL) here also uses version 2.1 by default.

The machine code of OpenGL in Windows can be found in the file opengl32.dll. Additional functions for 3D calculation are defined in the file glu32.dll. These mentioned libraries are used in SharpGL.

OpenGL 2.1 Reference Page: https://www.khronos.org/registry/OpenGL-Refpages/gl2.1/


**SharpGL**

SharpGL wraps all modern OpenGL features, provides helpful wrappers for advanced objects like Vertex Buffer Arrays and shaders, as well as offering a powerful Scene Graph and utility library to help you build your projects.

GitHup repository: https://github.com/dwmkerr/sharpgl


**OpenGL-Room**

Represents a room with walls, a window, a door and an electrical outlet. The repository can be downloaded and easily be compiled with Visual Studio 2017 and is
a fast way for the entry into OpenGL.

Current features:

-	You can click on the 2D surface and it will be shown where you clicked on the 3D model

-	The window and door shimmers through the wall

-	You can rotate the model comfortably with the mouse

-	If you look from the outside on a wall, it becomes transparent

**Purpose of the program**

In a larger CAD System, we want to switch to OpenGL. It is a relatively small-scale project. The annual turnover is under 1 million euros. Customers are craftsman or real estate companies.
After much research activities, we decided to slowly switch to OpenGL and also use the wrapper SharpGL to accelerate the performance of our product. This GitHub project is for the initial design of the features.

Followings are required:

-	Representation of approx. 50 objects

-	Change the camera perspective in different ways

-	Catch points, line segments, spatial edges and other points on geometric objects when you move the mouse over them

-	Working on layers

-	Display of round objects like columns or round walls

-	Mark one or more objects from different perspectives

-	Display text on objects with perspective to the camera or along the x-axis

-	...
The performance must also be optimized. This is currently not enough to smoothly display several rooms with countless objects. The performance is even under the presentation of objects with the help of just the CPU.

In order to work on these features, it is therefore not enough to only implement and use the SharpGL library as dll. This must therefore be made available here as a changeable and expandable project within the Visual Studio Workspace. How it was done here. This is the only way to refine algorithms, add complex objects or to add functions to the library.


The priority of the project is uncertain  :rage1:
