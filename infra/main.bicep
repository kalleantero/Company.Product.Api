targetScope = 'subscription'

param environment string
param location string
param tags object
param productName string

var resourceGroupName = 'rg-${productName}-${environment}'

module resourceGroupModule 'br/repo:bicep/modules/resourcegroup/resourcegroup.bicep:v0.9' = {
  name: resourceGroupName
  scope: subscription()
  params: {
    resourceGroupName: resourceGroupName
    location: location
    tags: tags
  }
}

module appservicePlanModule 'br/repo:bicep/modules/appservice/appserviceplan.bicep:v0.9' = {
  name: 'appservicePlanModule'
  scope: resourceGroup(resourceGroupName)
  params: {
    location: location
    tags: tags
    environment: environment
    productName: productName
    planName: 'B1'
    planTier: 'Basic'
    capacity: 1
  }
  dependsOn: [ resourceGroupModule ]
}

module logAnalyticsWorkspaceModule 'br/repo:bicep/modules/loganalytics/loganalytics.bicep:v0.9' = {
  name: 'logAnalyticsWorkspaceModule'
  scope: resourceGroup(resourceGroupName)
  params: {
    location: location
    tags: tags
    environment: environment
    productName: productName
  }
  dependsOn: [ resourceGroupModule ]
}

module applicationInsightsModule 'br/repo:bicep/modules/appinsights/appinsights.bicep:v0.11' = {
  name: 'applicationInsightsModule'
  scope: resourceGroup(resourceGroupName)
  params: {
    location: location
    logAnalyticsWorkspaceId: logAnalyticsWorkspaceModule.outputs.logAnalyticsWorkspaceId
    tags: tags
    environment: environment
    productName: productName
  }
  dependsOn: [ resourceGroupModule, logAnalyticsWorkspaceModule ]
}

module dashboardModule 'br/repo:bicep/modules/appinsights/dashboard.bicep:v0.9' = {
  name: 'dashboardModule'
  scope: resourceGroup(resourceGroupName)
  params: {
    location: location
    tags: tags
    applicationInsightsName: applicationInsightsModule.outputs.appInsightsName
  }
  dependsOn: [ resourceGroupModule, applicationInsightsModule, logAnalyticsWorkspaceModule ]
}

module apiAppServiceModule 'br/repo:bicep/modules/appservice/appservice.bicep:v0.9' = {
  name: 'apiAppServiceModule'
  scope: resourceGroup(resourceGroupName)
  dependsOn: [
    resourceGroupModule, applicationInsightsModule, logAnalyticsWorkspaceModule
  ]
  params: {
    appInsightsInstrumentationKey: applicationInsightsModule.outputs.appInsightsInstrumentationKey
    environment: environment
    location: location
    serverFarmId: appservicePlanModule.outputs.serverFarmId
    tags: tags
    productName: productName
    startUpCommand: 'dotnet Company.Product.Api.dll'
  }
}
