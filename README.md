
There are three different issues I have found related to loading and referencing DLLs in .Net 8 when compared to .Net Framework 4.8.

# Issue1 - Project reference using Private = False

Go into the Issue1 folder and open the solution.  There you will find four projects.

1. **WpfApp** - This is a .Net 4.8 WPF application.  I customized the CSPROJ to mimic close to what the product I am using does.  The main thing is that it outputs to a common folder.
2. **DependencyLib** - This is a .Net 4.8 class library. The CSPROJ was also customized to output to the same common bin folder.
3. **CoreWpfApp** - This is a .Net 8 WPF application.  I customized the CSPROJ to mimic close to what the product I am using does.  The main thing is that it outputs to a common folder.
4. **CoreDependencyLib** - This is a .Net 8 class library. The CSPROJ was also customized to output to the same common bin folder.

If you look in the CSPROJ file of WpfApp, you will see a project reference to DependencyLib, where it has the Private attribute set to False.
If you look in the CSPROJ file of CoreWpfApp, you will see a project reference to CoreDependencyLib, where it has the Private attribute set to False.

Next, in both applications in order to see the issue, I have converted the Application classes to compile as a Page so I could specify my own startup object.  If you look in Program.cs, you will see that Main does two things.
- It attempts to access a type from DepenendencyLib/CoreDependencyLib which will cause the runtime to load the DLL.
- It then launches the application.

All of this is wrapped in a try-catch that shows the exception text in a message box.

If you run the .Net 4.8 app, it runs fine and should show a window with a blue background.
If you run the .Net 8 app, it throws an exception and shows the exception text in a popup.  This is due to using Private=false.

As a workaround for the .Net 8 WPF app, if you change the project reference to use `CopyLocal` instead of `Private`, then rebuild and run it works and launches a window with an orange background.

We have a lot of C# / WPF code that has been developed since 2008 and forward using Private=False, as this was the recommended setting to to prevent copying the dependency library to the output folder.  This doesn't work properly in .Net 8.

As to why we have everything copy to a common output folder, we have over 500 projects in our solution.  When you use Private=true, it copies the dependencies eating up gigabytes extra of disk space and increasing our build times due to all of the extra file I/O.  Building to a shared output folder was the right solution to optimize build times and disk requirements.
