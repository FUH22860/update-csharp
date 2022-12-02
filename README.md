# update-c#

A rewrite of the legendary update script in C#
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
## Roadmap

- Figure out how to do options/arguments
- Backup important files
- Figure out how to make a single executable
## Contributing

Contributions are always welcome!
