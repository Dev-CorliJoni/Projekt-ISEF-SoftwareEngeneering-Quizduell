# Configure the Azure provider
terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "3.70.0"
    }
  }

  required_version = ">= 1.1.0"
}

provider "azurerm" {
  features {}
}

terraform {
  cloud {
    organization = "ISEF01"
    workspaces {
      name = "CLONE-Projekt-ISEF-SoftwareEngeneering-Quizduell"
    }
  }
}


#needed to create Budget Alert
data "azurerm_subscription" "current" {}

#Change to set Database Open 
variable "sqlfirewallrule" {
  type        = string
  description = "(Public) for DB Update/Backup or (Azure) for productive use"
  default     = "Azure"
}

#Required for Continous Deployment via Github
variable "github_auth_token" {
  type        = string
  description = "Github Auth Token from Github > Developer Settings > Personal Access Tokens > Tokens Classic (needs to have repo permission)"
}

#Required for Continous Deployment via Github
variable "SendGrid_Auth_Token" {
  type        = string
  description = "API Token for Send Grid"
}

variable "SendGrid_DefaultSender" {
  type = string
  description = "Default E-Mail for sending with SendGrid"
  default = "quixduell@linder-warmbach.de"
}


variable "AppName" {
  type        = string
  description = "The Name of the App"
  default     = "quixduell"
}


resource "random_password" "password" {
  length           = 16
  special          = true
  override_special = "!#$%&*()-+[]{}<>:?"
}

#Resource Group, Container for all Azure Resources
resource "azurerm_resource_group" "ResourceGroup" {
  name     = "${var.AppName}resourcegroup"
  location = "East Us"
  tags = {
    environment = "dev"
  }
}

#Alert if App cost some money 
resource "azurerm_consumption_budget_subscription" "SubscriptionBudget" {

  subscription_id = data.azurerm_subscription.current.id
  name            = "${var.AppName}resourcegroupbudget"

  amount     = 10
  time_grain = "Quarterly"

  notification {
    enabled        = true
    threshold      = 20.0
    operator       = "EqualTo"
    threshold_type = "Forecasted"

    contact_emails = [
      "mark.linder@linder-warmbach.de",
      "hansefredchen@gmail.com",
    ]
  }

  notification {
    enabled        = true
    threshold      = 30.0
    operator       = "EqualTo"
    threshold_type = "Forecasted"

    contact_emails = [
      "mark.linder@linder-warmbach.de",
      "hansefredchen@gmail.com",
    ]
  }

  notification {
    enabled        = true
    threshold      = 40.0
    operator       = "EqualTo"
    threshold_type = "Forecasted"

    contact_emails = [
      "mark.linder@linder-warmbach.de",
      "hansefredchen@gmail.com",
    ]
  }

  notification {
    enabled        = true
    threshold      = 50.0
    operator       = "EqualTo"
    threshold_type = "Forecasted"

    contact_emails = [
      "mark.linder@linder-warmbach.de",
      "hansefredchen@gmail.com",
    ]
  }

  notification {
    enabled        = true
    threshold      = 60.0
    operator       = "EqualTo"
    threshold_type = "Forecasted"

    contact_emails = [
      "mark.linder@linder-warmbach.de",
      "hansefredchen@gmail.com",
    ]
  }
  time_period {
    start_date = "2023-10-01T00:00:00Z"
    end_date   = "2024-10-01T00:00:00Z"
  }

}

#Use Free Tier for Web App, and Windows OS
resource "azurerm_service_plan" "AppServiceplan" {
  name                = "${var.AppName}serviceplan"
  resource_group_name = azurerm_resource_group.ResourceGroup.name
  location            = azurerm_resource_group.ResourceGroup.location
  os_type             = "Windows"
  sku_name            = "F1"
}

#Create Web App for Frontend, use dotnet 6, set Connection String for Azure DB
resource "azurerm_windows_web_app" "FrontWebapp" {
  name                = "${var.AppName}webapp"
  resource_group_name = azurerm_resource_group.ResourceGroup.name
  location            = azurerm_resource_group.ResourceGroup.location
  service_plan_id     = azurerm_service_plan.AppServiceplan.id
  https_only          = true

  site_config {
    always_on = false
    http2_enabled = true

    application_stack {
      current_stack  = "dotnet"
      dotnet_version = "v6.0"
    }
  }
  logs {
    application_logs {
      file_system_level = "Error"
    }
  }
  connection_string {
    name  = "SQL"
    type  = "SQLAzure"
    value = "Server=tcp:${azurerm_mssql_server.SqlServer.name}.database.windows.net,1433;Database=${azurerm_mssql_database.SqlServerDB.name};Persist Security Info=False;User ID=${azurerm_mssql_server.SqlServer.administrator_login};Password=${azurerm_mssql_server.SqlServer.administrator_login_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=180;"
  }

   app_settings = {
    "EmailConfiguration" = "SendGrid"
     "SendGridEmailConfiguration__ApiKey" = var.SendGrid_Auth_Token
     "SendGridEmailConfiguration__DefaultSender" = var.SendGrid_DefaultSender
   }

}

#Create Azure SQL with random SQL PW 
resource "azurerm_mssql_server" "SqlServer" {
  name                         = "${var.AppName}sqlserver"
  resource_group_name          = azurerm_resource_group.ResourceGroup.name
  location                     = azurerm_resource_group.ResourceGroup.location
  version                      = "12.0"
  administrator_login          = "${var.AppName}DBAdmin"
  administrator_login_password = random_password.password.result
}

resource "azurerm_mssql_firewall_rule" "FirewallRule" {
  name             = var.sqlfirewallrule == "Public" ? "Allow All" : "Allow Azure Services"
  server_id        = azurerm_mssql_server.SqlServer.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = var.sqlfirewallrule == "Public" ? "255.255.255.255" : "0.0.0.0"
}

resource "azurerm_mssql_database" "SqlServerDB" {
  name      = "${var.AppName}sqlserverdb"
  server_id = azurerm_mssql_server.SqlServer.id
  collation = "SQL_Latin1_General_CP1_CI_AS"

  auto_pause_delay_in_minutes = 60
  max_size_gb                 = 32
  min_capacity                = 0.5
  read_replica_count          = 0
  read_scale                  = false
  geo_backup_enabled          = true
  sku_name                    = "GP_S_Gen5_2"
  zone_redundant              = false
  storage_account_type        = "Local"


}

resource "azurerm_app_service_source_control" "source_control" {
  app_id   = azurerm_windows_web_app.FrontWebapp.id
  repo_url = "https://github.com/Dev-CorliJoni/Projekt-ISEF-SoftwareEngeneering-Quizduell"
  branch   = "main"

  github_action_configuration {
    code_configuration {
      runtime_stack   = "dotnetcore"
      runtime_version = "v6.0"
    }
    generate_workflow_file = false
  }

}

resource "azurerm_source_control_token" "source_control_token" {
  type         = "GitHub"
  token        = var.github_auth_token
  token_secret = var.github_auth_token
}



output "AzureSQLConnectionString" {
  value     = azurerm_windows_web_app.FrontWebapp.connection_string
  sensitive = true
}


