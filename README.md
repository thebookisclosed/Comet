# Managed Disk Cleanup (Comet)
Comet is a managed version of Disk Cleanup developed in C#.
Its purpose is to help out those who would like to automate specific cleanup related tasks.
It also serves as a peek into the world of backwards compatibility, given the fact that the Disk Cleanup API has barely changed since its introduction in 1997.

Changes made to the Data Driven Cleaner object's functionality introduced in Windows 8 appear to be essential in order for the object to be activatable through a C# program. As a result, users running Windows 7 or older will be met with a message informing them of limited functionality at startup.

# Working with COM
This project is among the first few where I've worked with COM, thus some implementation may not be exactly ideal. Feedback is greatly appreciated.
