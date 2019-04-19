# Managed Disk Cleanup
![Logo](https://i.imgur.com/RFB7RjN.png)

The goal of this project is to create an experience similar to the inbox Disk Cleanup program that ships with Windows. While regular users may simply opt to use this project as a free and open-source alternative, developers can make use of it to learn about the inner workings of the EmptyVolumeCache API that has been almost unchanged since its inception in 1997.

One of the incentives for this project's development is the opened possibility for creating scripts or programs that clean up precisely what you want, instead of having to resort to using Disk Cleanup with command line arguments.

Some of the goals for future releases:

  - Introduce Restore Point cleanup (Disk Cleanup offers this in its second tab in Administrator mode)
  - Command line-only mode -- intended for better automation, with granular progress reporting
  - An improved system for remembering which categories you've selected (currently mimics Disk Cleanup)
  - Create a repository with a ready-made example for utilizing the API outside of a GUI

## Incompatibilities
Windows 8 introduced changes to the "Data Driven Cleaner" object that are essential for it to work under C#. This is a generic cleanup handler utilized by a handful of the offered cleanup categories. As a result, users using Windows 7 or an earlier OS will be met with a message informing them of limited functionality at startup.

## Reporting errors or bugs
If you happen to run into any of these, filing an issue here will be the most helpful. Besides describing the problem itself, please also include:
 - Your system version (ideally the build number from winver)
 - Your current system locale
 - Whether you were running the program as Administrator

## Suggesting features / providing feedback
In case you'd like to share a suggestion, reaching out to me on [Twitter](https://twitter.com/thebookisclosed) would be the best.