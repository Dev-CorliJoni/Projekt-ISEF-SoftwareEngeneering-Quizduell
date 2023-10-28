# Deploy Azure SQL Database with EF Core 
1. Run: Terraform Script for open Firewall: `terraform apply -var='sqlfirewallrule=Public'`
2. Get Connection String from Terraform:  `terraform output AzureSQLConnectionString`
3. Open Package Manager Console 
4. Run Update/ Migrate Script: `Update-Database -Project Quixduell.Blazor -Connection [SQL Connection String]`
5. Run: Terraform Script for close Firewall: `terraform apply`