# update-c#

A rewrite of the legendary update script in C#

This time with file handling entirely in C# while keeping all scripting parts in bash.
## Authors

- [@ProfessionalUwU](http://192.168.0.69:3000/ProfessionalUwU)
## Run Locally

Clone the project

```bash
  git clone http://192.168.0.69:3000/ProfessionalUwU/update-csharp.git
```

Go to the project directory

```bash
  cd update-csharp
```

Install dependencies

```bash
  pacman -S dotnet-runtime dotnet-sdk
```

Build project

```bash
  dotnet build update.csproj 
```

Publish project

```bash
  dotnet publish --configuration Release --arch x64 --use-current-runtime --self-contained 
```

Go to the publish folder
```bash
  cd update-csharp/bin/Release/net7.0/linux-x64/publish
```

Run executable

```bash
  ./update
```
## Roadmap/ToDo

- Figure out how to do options/arguments
- Backup all necessary files
- Shrink size of the executable
- Potentially speed up file handling
- Color output according to state (success = green, failure = red, info = yellow)
- Backup pacman database
- Compress all files to single archive

## Sites I used to help make this project
- [dotnetperls](https://dotnetperls.com)
- [stackoverflow](https://stackoverflow.com/questions/tagged/c%23)
- [c-sharpcorner](https://www.c-sharpcorner.com)
## Contributing

Contributions are always welcome!
