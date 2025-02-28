#### Clone repo

git clone https://github.com/dennihous/EcommerceAPI.git
cd EcommerceAPI

#### Install the NuGet packages

dotnet restore

#### Set up the database

dotnet ef migrations add InitialCreate
dotnet ef database update