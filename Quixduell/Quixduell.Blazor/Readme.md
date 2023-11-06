# Deploy Azure SQL Database with EF Core 
1. Run: Terraform Script for open Firewall: `terraform apply -var='sqlfirewallrule=Public'`
2. Get Connection String from Terraform:  `terraform output AzureSQLConnectionString`
3. Open Package Manager Console 
4. Run Update/ Migrate Script: `Update-Database -Project Quixduell.Blazor -Connection [SQL Connection String]`
5. Run: Terraform Script for close Firewall: `terraform apply`


# Deploy local Docker SQL Server
1. Run Docker-Compose Project 
2. Open Package Manager Console
3. Run Update/Migrate Script: `Update-Database -Connection "Server=localhost,4500;Database=QuixDB;Persist Security Info=False;User ID=sa;Password=bZYu04XMuyMqXWAcq9;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;" -StartupProject Quixduell.Blazor -Context ApplicationDbContext`